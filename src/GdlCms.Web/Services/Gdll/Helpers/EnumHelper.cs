using System;

namespace GdlCms.Web.Services.Gdll.Helpers
{
    public static class EnumHelper
    {
        public static T ToEnum<T>(this int i) where T : struct, IConvertible
        {
            var @enum = (T)Enum.Parse(typeof(T), i.ToString());
            return @enum;
        }
        public static T ToEnumerable<T>(this string s) where T : struct, IConvertible
        {
            var @enum = (T)Enum.Parse(typeof(T), s);
            return @enum;
        }

    }
}