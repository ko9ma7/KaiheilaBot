using System;

namespace KaiheilaBot.Core.Attributes
{
    /// <summary>
    /// 用于 RequestRecord 设置参数名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterNameAttribute : Attribute
    {
        private readonly string _name;
        
        public ParameterNameAttribute(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}