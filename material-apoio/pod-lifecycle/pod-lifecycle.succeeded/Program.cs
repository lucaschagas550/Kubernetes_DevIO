for (int i = 0; i < 30; i++)
{
    Console.WriteLine($"Segundos: {i+1}");
    Thread.Sleep(TimeSpan.FromSeconds(1));
}
