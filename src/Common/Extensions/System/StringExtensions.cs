namespace Quaestor.Common.Extensions.System
{
    public static class StringExtensions
    {
        public static string WithUppercaseFirstCharacter(this string text)
        {
            return text[0].ToString().ToUpper() + text.Substring(1);
        }
    }
}
