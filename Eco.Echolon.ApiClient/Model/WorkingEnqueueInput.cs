using System;

namespace Eco.Echolon.ApiClient.Model
{
    public class WorkingEnqueueInput<T>
    {
        public ItemLink[] Items { get; set; }
        public string Title { get; set; }
        public DateTimeOffset WorkedOn { get; set; }
        public T Data { get; set; }

        public WorkingEnqueueInput(ItemLink[] items, string title, DateTimeOffset workedOn, T data)
        {
            Items = items;
            Title = title;
            WorkedOn = workedOn;
            Data = data;
        }
    }
    
    public class ItemLink
    {
        public string Reference { get; set; }
        public Identity? Identity { get; set; }

        public ItemLink(string reference, Identity? identity = null)
        {
            Reference = reference;
            Identity = identity;
        }
    }
}