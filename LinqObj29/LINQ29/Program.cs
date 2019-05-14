using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject29
{
    class Debt
    {
        public int entrance { get; set; }
        public int entranceNumber { get; set; }
        public int number { get; set; }
        public string surname { get; set; }
        public double debt { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<Debt>();

            FileStream file = new FileStream(@"..\..\debt.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                data.Add(new Debt
                {
                    entrance = int.Parse(attrClient[0]),
                    entranceNumber = int.Parse(attrClient[1]),
                    number = int.Parse(attrClient[2]),
                    surname = attrClient[3],
                    debt = double.Parse(attrClient[4])
                });
            }

            for (var i = 1; i < 5; i++)
            {
                double max = data.Where(x => x.entrance == i)
                    .Max(a => a.debt);
                var query = data.Where(x => x.debt == max)
                    .GroupBy(e => $"Этаж:{e.entranceNumber}, Номер квартиры:{e.number}, Фамилия:{e.surname}, Долг:{e.debt}", (k, v) => new
                    {
                        maxDebt = k
                    });

                foreach (var item in query)
                {
                    Console.WriteLine("{0}", item.maxDebt);

                }
            }
            file.Close();

            Console.ReadKey();

        }
    }
}
