using System;

namespace adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            var adapter = new ClockToTimerAdapter(new Clock());
            person.SetTime(adapter, "13:65");
            Console.WriteLine(person.GetTime(adapter));
        }
    }
}
