using KaiheilaBot;
using System;

namespace KaiheilaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotWrapper wrapper = new BotWrapper();
            wrapper.Start().Wait();
        }
    }
}
