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
            var serializer = MessagePackSerializer.Get<List<Car>>();
            List<Car> data = serializer.Unpack(result.Content.ReadAsStreamAsync().Result);

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
