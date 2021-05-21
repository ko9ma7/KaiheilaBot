using System;
using System.Reflection;

namespace KaiheilaBot.Core.Common.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute
            => Attribute.GetCustomAttribute(memberInfo, typeof(T)) as T;
    }
}