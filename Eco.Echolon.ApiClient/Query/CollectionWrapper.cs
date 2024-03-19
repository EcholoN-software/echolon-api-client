namespace Eco.Echolon.ApiClient.Query
{
    public class CollectionWrapper<T>
    {
        public int Count { get; }
        public int Skip { get; }
        public int First { get; }
        public ItemWrapper<T> Data { get; }

        public CollectionWrapper(int count, int skip, int first, ItemWrapper<T> data)
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