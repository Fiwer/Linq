using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQObject7
{
    public class Client
    {
        public int numberMonth { get; set; }
        public int year { get; set; }
        public int Id { get; set; }
        public int durationTraining { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var clients = new List<Client>();

            FileStream file = new FileStream(@"..\..\Client.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] attrClient = line.Split(' ');
                clients.Add(new Client
                {
                    numberMonth = int.Parse(attrClient[0]),
                    year = int.Parse(attrClient[1]),
                    Id = int.Parse(attrClient[2]),
                    durationTraining = int.Parse(attrClient[3])
                });
            }

            if (!clients.Any())
            {
                Console.WriteLine("Нет данных");
                return;
            }
            var clientGroup = clients.GroupBy(client => client.year)
                                                    .Select(groupByYear => new
                                                    {
                                                        year = groupByYear.Key,
                                                        monthArray = groupByYear.GroupBy(client => client.numberMonth)
                                                                                .Select(groupByMonth => new
                                                                                {
                                                                                    year = groupByYear.Key,
                                                                                    month = groupByMonth.Key,
                                                                                    duration = groupByMonth.Count()
                                                                                })
                                                    })
                                                    .SelectMany(group => group.monthArray)
                                                    .OrderBy(client => client.month)
                                                    .OrderByDescending(client => client.year)
                                                    .OrderBy(client => client.duration);


            foreach (var client in clientGroup)
            {
                Console.WriteLine("Год: {1} месяц: {2} продолжительность: {0} ", client.duration, client.year, client.month);
            }

            file.Close();

            Console.ReadKey();
        }
    }
}
