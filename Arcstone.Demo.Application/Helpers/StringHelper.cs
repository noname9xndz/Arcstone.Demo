using System;

namespace Arcstone.Demo.Application.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpaceCustom(this string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return true;
            return false;
        }
    }
}