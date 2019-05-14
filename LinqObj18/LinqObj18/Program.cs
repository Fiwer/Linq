using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQObject18
{
    class School
    {
        public int year { get; set; }
        public int nSchool { get; set; }
        public string lastName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var schools = new List<School>();

            FileStream file = new FileStream(@"..\..\school.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                schools.Add(new School
                {
                    year = int.Parse(attrClient[0]),
                    nSchool = int.Parse(attrClient[1]),
                    lastName = attrClient[2],
                });
            }


            var query = schools.GroupBy(school => school.year)
                                .Select(groupByYear => new
                                {
                                    year = groupByYear.Key,
                                    count = groupByYear.Count()
                                })
                                .OrderByDescending(count => count.count);

            var average = query.Average(enrollee => enrollee.count);

            foreach (var item in query)
            {
                if (item.count >= average)
                {
                    Console.WriteLine("{1} {0}", item.year, item.count);
                }

            }
            file.Close();

            Console.ReadKey();

        }
    }
}
