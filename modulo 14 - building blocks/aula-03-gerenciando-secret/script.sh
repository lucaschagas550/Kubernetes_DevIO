#!/bin/sh
criar_secret() {
    echo "Criando secret"
    CERT_PASSWORD=$(head /dev/urandom | tr -dc 'A-Za-z0-9!@#$%^&*()_+' | head -c 32; echo '')
    CERT_B64_PASSWORD=$(echo -n "$CERT_PASSWORD" | base64)
    openssl genrsa -out devstore.rsa 2048
    openssl req -sha256 -new -key devstore.rsa -out devstore.csr -subj '/CN=localhost'
    openssl x509 -req -sha256 -days 365 -in devstore.csr -signkey devstore.rsa -out devstore.crt
    openssl pkcs12 -export -out devstore.academy-localhost.pfx -inkey devstore.rsa -in devstore.crt -password pass:$CERT_PASSWORD
    PFX_B64=$(cat devstore.academy-localhost.pfx | base64 | tr -d '\n')
    SECRET_PAYLOAD=$(printf '{"apiVersion":"v1","kind":"Secret","metadata":{"name":"devstore-ssl"},"type":"Opaque","data":{"devstore.academy-localhost.pfx":"%s","password":"%s"}}' $PFX_B64 $CERT_B64_PASSWORD)
    curl --cacert $CACERT -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" -X POST -d "$SECRET_PAYLOAD" $APISERVER/api/v1/namespaces/devstore/secrets
}

criado_ha_mais_de_um_ano(){
    CREATED=$(echo $SECRET | jq -r '.metadata.creationTimestamp')
    IS_OLDER=$(python -c "from datetime import datetime, timedelta, timezone; \
                            created = datetime.fromisoformat('${CREATED}'); \
                            year_ago = datetime.now(timezone.utc) - timedelta(days=365); \
                            print(created < year_ago)")
    if [ "$IS_OLDER" = "True" ]; then
        echo "true"
    else
        echo "false"
    fi
}

deletar_secret(){
    echo "Deletando secret"
    curl --cacert $CACERT -H "Authorization: Bearer $TOKEN" -X DELETE $APISERVER/api/v1/namespaces/devstore/secrets/devstore-ssl
}

APISERVER=https://kubernetes.default.svc
SERVICEACCOUNT=/var/run/secrets/kubernetes.io/serviceaccount 
TOKEN=$(cat ${SERVICEACCOUNT}/token) 
CACERT=${SERVICEACCOUNT}/ca.crt

SECRET=$(curl -s --cacert $CACERT -H "Authorization: Bearer $TOKEN" -X GET $APISERVER/api/v1/namespaces/devstore/secrets/devstore-ssl)
HTTP_STATUS=$(echo $SECRET | jq -r '.code // 200')

if [ "$HTTP_STATUS" = "404" ]; then
    echo "Secret nao foi encontrado"
    criar_secret
elif [ $(criado_ha_mais_de_um_ano) = "true" ]; then
    echo "Secret expirou"
    # deletar a secret
    criar_secret
else
    echo "Secret valido"
fi

