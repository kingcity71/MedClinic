using System;
using System.Collections.Generic;
using System.Linq;
using MedClinic.Data;
namespace TestConsole
{
    class Test
    {
        public int Num { get; set; }
        public List<Test> Tests { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var date = DateTime.Today;

            var dayOfWeek = (int)new DateTime(date.Year, date.Month, 1).DayOfWeek;

            var difference = (dayOfWeek != 0 ?dayOfWeek: 7) - 1;
            
            var daysInMonth = Enumerable.Range(0, difference).Select(x=>0).ToList();
            daysInMonth.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).ToList());
            daysInMonth.AddRange(Enumerable.Range(daysInMonth.Last(), 
                                    daysInMonth.Count() <= 7 * 5 
                                            ? 7 * 5 - daysInMonth.Count() 
                                            : 7 * 6 - daysInMonth.Count()).Select(x=>0));

            var temp = daysInMonth.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / 7)
            .Select(x => x.Select(v => v.Value).ToArray())
            .ToArray();
        }

        static void Main71(string[] args)
        {
            var list = new[] { 1, 7, 4, 5 };

            var listOrdered = list.OrderBy(x=>x);
            
            var result = list.SequenceEqual(listOrdered);

        }

        static void Main34(string[] args)
        {
            var list = new[] { 1, 2, 3,4 };
            var list2 = new[] { 3,4,5,6 };
            var q1 = list.Except(list2).ToList();
            var q2 = list2.Except(list).ToList();
        }
        static void Main2(string[] args)
        {
            var test = new List<Test>()
            {
                new Test()
                {
                    Num = 1,
                    Tests = new List<Test>(){new Test(){ Num = 2 }, new Test() { Num = 3 }, new Test() { Num = 4 } }
                },
                new Test()
                {
                    Num = 5,
                    Tests = new List<Test>(){new Test(){ Num = 6 }, new Test() { Num = 7 }, new Test() { Num = 8 } }
                },
                new Test()
                {
                    Num = 9,
                    Tests = new List<Test>(){new Test(){ Num = 10 }, new Test() { Num = 11 }, new Test() { Num = 12 } }
                }
            };

            var testIds = test
                .SelectMany(x => x.Tests)
                .Select(x => x.Num).ToList();
        }
    }
}
