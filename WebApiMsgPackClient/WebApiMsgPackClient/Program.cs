using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMsgPackClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:14223/api/values/");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-msgpack"));
            var result = client.SendAsync(request).Result;
            //var serializer = MessagePackSerializer.Get<Car>();
            var serializer = MessagePackSerializer.Get<List<Car>>();
            //Car data = serializer.Unpack(result.Content.ReadAsStreamAsync().Result);
            List<Car> data = serializer.Unpack(result.Content.ReadAsStreamAsync().Result);

            int i = 1;
            //Console.WriteLine("Data {0} => ", i++);
            //Console.WriteLine("\tName: " + data.Name);
            //Console.WriteLine("\tID: " + data.ID);
            //Console.WriteLine("\tColor: " + data.Color);
            //Console.WriteLine("\tType1 =>");
            //Console.WriteLine("\t\tTypes: " + data.Type1.Types);
            //Console.WriteLine("\t\tBudget: " + data.Type1.Budget.ToString());
            //if (data.Type2 != null && data.Type2.Count > 0)
            //{
            //    Console.WriteLine("\tType2 =>");
            //    int j = 1;
            //    foreach (var b in data.Type2)
            //    {
            //        Console.WriteLine("\t\tType2 - {0}", j++);
            //        Console.WriteLine("\t\t\tTypes: " + b.Types);
            //        Console.WriteLine("\t\t\tBudget: " + b.Budget.ToString());
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("\tType2 => none");
            //}

            foreach (var a in data)
            {
                Console.WriteLine("Data {0} => ", i++);
                Console.WriteLine("\tName: " + a.Name);
                Console.WriteLine("\tID: " + a.ID);
                Console.WriteLine("\tColor: " + a.Color);
                Console.WriteLine("\tType1 =>");
                Console.WriteLine("\t\tTypes: " + a.Type1.Types);
                Console.WriteLine("\t\tBudget: " + a.Type1.Budget.ToString());
                if (a.Type2 != null && a.Type2.Count > 0)
                {
                    Console.WriteLine("\tType2 =>");
                    int j = 1;
                    foreach (var b in a.Type2)
                    {
                        Console.WriteLine("\t\tType2 - {0}", j++);
                        Console.WriteLine("\t\t\tTypes: " + b.Types);
                        Console.WriteLine("\t\t\tBudget: " + b.Budget.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\tType2 => none");
                }
            }

            Console.ReadKey();
        }
    }

    public class Car
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Color { get; set; }
        public CarType Type1 { get; set; }
        public List<CarType> Type2 { get; set; }
    }

    public class CarType
    {
        public string Types { get; set; }
        public int Budget { get; set; }
    }
}
