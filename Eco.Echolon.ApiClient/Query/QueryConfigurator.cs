using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Echolon.ApiClient.Model.DomainTypes;

namespace Eco.Echolon.ApiClient.Query
{
    public class QueryConfigurator
    {
        public uint MaxRecursiveDepth { get; set; } = 2;
        private HashSet<Type> SingleValueTypes { get; }
        private List<Func<Type, bool>> Funcs { get; }

        public QueryConfigurator()
        {
            SingleValueTypes = new HashSet<Type>()
            {
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(Guid),
                typeof(decimal)
            };
            Funcs = new List<Func<Type, bool>>(new Func<Type, bool>[]
            {
                (t) => t.IsSubclassOf(typeof(DomainType))
            });
        }

        public void AddSingleValueType<T>()
        {
            AddSingleValueType(typeof(T));
        }
        
        public void AddSingleValueType(Type type)
        {
            SingleValueTypes.Add(type);
        }

        public bool IsSingleValueType<T>()
        {
            return IsSingleValueType(typeof(T));
        }
        
        public bool IsSingleValueType(Type type)
        {
            if (type.IsArray)
                type = type.GetElementType() ?? type;
            return SingleValueTypes.Contains(type) ||
                   Funcs.Any(f => f(type));
        }
        
        public void RemoveSingleValueType<T>()
        {
            RemoveSingleValueType(typeof(T));
        }

        public void RemoveSingleValueType(Type type)
        {
            SingleValueTypes.Remove(type);
        }
    }
}