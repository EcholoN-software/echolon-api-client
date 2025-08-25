namespace Eco.Echolon.ApiClient.Query
{
    public static class StringExtensions
    {
        public static string Uncapitalize(this string str)
        {
            return string.Concat(str[0].ToString().ToLower(),
                str.Substring(1, str.Length - 1));
        }
    }
}