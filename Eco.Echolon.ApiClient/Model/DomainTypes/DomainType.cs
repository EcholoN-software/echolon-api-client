namespace Eco.Echolon.ApiClient.Model.DomainTypes
{
    public abstract class DomainType
    {
    }

    public abstract class DomainType<T> : DomainType
    {
        public T Value { get; }

        protected DomainType(T val)
        {
            Value = val;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator T(DomainType<T> v)
        {
            return v.Value;
        }
    }
}