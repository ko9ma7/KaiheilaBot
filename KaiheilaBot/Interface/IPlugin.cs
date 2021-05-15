using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot.Interface
{
    public interface IPlugin<T>
    {
        public bool Handle(T eventArgs);
    }
}
