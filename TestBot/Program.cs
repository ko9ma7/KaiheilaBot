using KaiheilaBot;
using System;

namespace TestBot
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
