using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject32
{
    class Debt
    {
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
                    entranceNumber = int.Parse(attrClient[0]),
                    number = int.Parse(attrClient[1]),
                    surname = attrClient[2],
                    debt = double.Parse(attrClient[3])
                });
            }
            for (var tu = 1; tu < 10; tu++)
            {
                double min = data.Where(e => e.entranceNumber == tu)
                .Where(e => e.debt != 0)
                    .Min(e => e.debt);
                var query = data.Where(x => x.debt == min)
                    .GroupBy(e => $"{e.entranceNumber} {e.debt}", (k, v) => new
                    {
                        minDebt = k
                    });


                foreach (var item in query)
                {
                    Console.WriteLine("{0}", item.minDebt);
                }
            }
            file.Close();

            Console.ReadKey();

        }
    }
}
