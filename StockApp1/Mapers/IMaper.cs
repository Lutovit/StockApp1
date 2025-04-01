namespace StockApp1.Mapers
{
    public interface IMaper<T, V>
    {
        V Map(T t, string symbol);
    }
}
