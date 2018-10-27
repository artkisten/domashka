using System;
using System.Collections.Generic;
using System.Linq;

namespace linq
{
    public class Something
    {
        public string name { get; set; }
        public int age { get; set; }
        public int balance { get; set; }
        public string sex { get; set; }
    }
    class Program
    {

        static void Main(string[] args)
        {
            List<Something> array = new List<Something>
            {
                new Something(){name = "Mark", age = 30, balance = 3200, sex = "m" },
                new Something() { name = "Mason", age = 40, balance = 32200, sex = "m" },
                new Something(){name = "Den", age = 19, balance = 1000, sex = "m" },
                new Something(){name = "Emma", age = 25, balance = 16000, sex = "f" },
                new Something(){name = "Amelia", age = 18, balance = 8000, sex = "f" },
                new Something(){name = "Denis", age = 39, balance = 36000, sex = "m" },
                new Something(){name = "Alexander", age = 26, balance = 6000, sex = "m" },
                new Something(){name = "Lily", age = 28, balance = 17000, sex = "f" },
                new Something(){name = "Sofia", age = 18, balance = 12000, sex = "f" }
            };



            Console.WriteLine("имя человека с наибольшим возрастом");
            Console.WriteLine(array.OrderByDescending(x => x.age).FirstOrDefault().name);
            Console.WriteLine("имя человека с наибольшим балансом :D");
            Console.WriteLine(array.OrderByDescending(x => x.balance).FirstOrDefault().name);
            Console.WriteLine("имя человека с наибольшим возрастом и балансом");
            Console.WriteLine(array.OrderByDescending(x => x.age).OrderByDescending(x => x.balance).FirstOrDefault().name);

            var querys = array.OrderByDescending(x => x.sex);
            var queryb = array.OrderByDescending(x => x.balance);
            var querya = array.OrderByDescending(x => x.age);
            var query=array.Where(x => x.balance > 4000).Select(x => x.name);

            Console.WriteLine("имена тех у кого баланс больше 4000");
            foreach (var a in query)
            {
                Console.WriteLine($"*{a}*");
            }

            Console.WriteLine("сортировка по балансу");
            foreach (var a in queryb)
            {
                Console.WriteLine($"*{a.name} - {a.sex} - {a.age} - {a.balance}*");
            }

            Console.WriteLine("сортировка по полу");
            foreach (var a in querys)
            {
                Console.WriteLine($"*{a.name} - {a.sex} - {a.age} - {a.balance}*");
            }

            Console.WriteLine("сортировка по возрасту");

            foreach (var a in querya)
            {
                Console.WriteLine($"*{a.name} - {a.sex} - {a.age} - {a.balance}*");
            }

            Console.ReadLine();
        }
    }
}
