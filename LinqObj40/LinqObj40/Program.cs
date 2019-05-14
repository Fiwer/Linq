using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject40
{
    class Petrol
    {
        public string company { get; set; }
        public string street { get; set; }
        public int mark { get; set; }
        public double money { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<Petrol>();

            FileStream file = new FileStream(@"..\..\petrol.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                data.Add(new Petrol
                {
                    company = attrClient[0],
                    street = attrClient[1],
                    mark = int.Parse(attrClient[2]),
                    money = double.Parse(attrClient[3])
                });
            }

            var query = data.OrderBy(x => x.street)
                .GroupBy(x => x.street, (k, v) => new
                {
                    street = k,
                    b92 = v.Where(y => y.mark == 92)
                    .Select(y => y.mark).Count(),
                    b95 = v.Where(y => y.mark == 95)
                    .Select(y => y.mark).Count(),
                    b98 = v.Where(y => y.mark == 98)
                    .Select(y => y.mark).Count()
                });
                
            
                foreach (var item in query)
                {
                    Console.WriteLine("Улица:{0} 92:{1} 95:{2} 98:{3}", item.street, item.b92, item.b95, item.b98);
                }
            file.Close();

            Console.ReadKey();

        }
    }
}

