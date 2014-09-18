using System;

namespace NinjackeR.Pompidou
{
    public static class BitExtensions
    {
        public static string ToString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}