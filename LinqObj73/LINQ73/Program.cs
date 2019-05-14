using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject73
{
    class Shop
    {
        public int year { get; set; }
        public int codeP { get; set; }
        public string street { get; set; }
        public int codeS { get; set; }
        public string shop { get; set; }
        public int discount { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<Shop>();

            FileStream file = new FileStream(@"..\..\shop.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                data.Add(new Shop
                {
                    year = int.Parse(attrClient[0]),
                    codeP = int.Parse(attrClient[1]),
                    street = attrClient[2],
                    codeS = int.Parse(attrClient[3]),
                    shop = attrClient[4],
                    discount = int.Parse(attrClient[5])
                });
            }


            var query = data.Where(x => x.codeP == x.codeS)
                .GroupBy(e => $"{e.shop} {e.street}", (k, v) => new
                {
                    shop = k,
                    count = v.Count()
                })
                .OrderBy(x => x.shop);
                
                
                       
            foreach (var item in query)
            {
                    Console.WriteLine("{0} {1}", item.shop, item.count);

            }
            file.Close();

            Console.ReadKey();

        }
    }
}
