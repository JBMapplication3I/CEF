#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Common
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            var encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
    }
}
