using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LINQObject62
{
    class School
    {
        public int classroom { get; set; }
        public string name { get; set; }
        public int sum { get; set; }
        public int alg { get; set; }
        public int geo { get; set; }
        public int inf { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var schools = new List<School>();

            FileStream file = new FileStream(@"..\..\Lesson.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                schools.Add(new School
                {
                    classroom = int.Parse(attrClient[0]),
                    name = attrClient[1] + " " + attrClient[2],
                    alg = int.Parse(attrClient[3]),
                    geo = int.Parse(attrClient[4]),
                    inf = int.Parse(attrClient[5]),
                });
            }
 
            var nm = schools
                .GroupBy(x => x.name)
                .Select(i => new
                {
                    name = i
                })
                .OrderBy(e => e.name);

            var group = schools
                .Select(x => new { x.classroom, x.name, x.alg, x.geo, x.inf})
                .OrderByDescending(classroom => classroom.classroom);

            foreach (var item in group)
            {
                Console.WriteLine("Класс:{0},ФИО:{1}, Количество оценок: Алгебра:{2}, Геометрия:{3}, Информатика:{4} ", item.classroom, item.name, item.alg, item.geo, item.inf);
            }

            file.Close();

            Console.ReadKey();

        }
    }
}