Comando para instalar o helm:

```bash
helm upgrade --install api-identity -f api-common.yaml -f ./api-identity/values.yaml ./api-template -n devstore
helm upgrade --install api-catalog -f api-common.yaml -f ./api-catalog/values.yaml ./api-template -n devstore
helm upgrade --install api-cart -f api-common.yaml -f ./api-cart/values.yaml ./api-template -n devstore
helm upgrade --install api-customer -f api-common.yaml -f ./api-customer/values.yaml ./api-template -n devstore
helm upgrade --install api-billing -f api-common.yaml -f ./api-billing/values.yaml ./api-template -n devstore
helm upgrade --install api-orders -f api-common.yaml -f ./api-orders/values.yaml ./api-template -n devstore
helm upgrade --install api-bff-checkout -f api-common.yaml -f ./api-bff-checkout/values.yaml ./api-template -n devstore
```