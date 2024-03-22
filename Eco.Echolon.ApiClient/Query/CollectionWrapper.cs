namespace Eco.Echolon.ApiClient.Query
{
    public class CollectionWrapper<T>
    {
        public decimal Count { get; }
        public decimal? Skip { get; }
        public decimal? First { get; }
        public ItemWrapper<T>[] Data { get; }

        public CollectionWrapper(decimal count, decimal? skip, decimal? first, ItemWrapper<T>[] data)
        {
            Count = count;
            Skip = skip;
            First = first;
            Data = data;
        }
    }
}

public class ItemWrapper<T>
{
    public T Item { get; }

    public ItemWrapper(T item)
    {
        Item = item;
    }
}