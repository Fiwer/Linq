using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject51
{
    class EGE
    {
        public int mat { get; set; }
        public int rus { get; set; }
        public int inf { get; set; }
        public string surname { get; set; }
        public string initials { get; set; }
        public int school { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<EGE>();

            FileStream file = new FileStream(@"..\..\EGE.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                data.Add(new EGE
                {
                    mat = int.Parse(attrClient[0]),
                    rus = int.Parse(attrClient[1]),
                    inf = int.Parse(attrClient[2]),
                    surname = attrClient[3],
                    initials = attrClient[4],
                    school = int.Parse(attrClient[5]),
                });
            }

            var query = data.OrderBy(x => x.school)
                .GroupBy(x => x.school, (k, v) => new
                {
                    school = k,
                    inf = v.Select(y => y.inf).Max()
                });

            var a = 0;
            foreach (var item in query)
            {
                if (item.school != a)
                {
                    Console.WriteLine("Номер школы:{0}, Балл ЕГЭ по информатике:{1} ", item.school, item.inf);
                    a = item.school;
                }
            }
            file.Close();
            Console.ReadKey();

        }
    }
}
