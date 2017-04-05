using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiWithMsgPack.Controllers
{
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

    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            List<Car> a = new List<Car>
            {
                new Car
                {
                    Name = "Mercedes Benz C-300",
                    ID = 300,
                    Color = "Black Jet",
                    Type1 = new CarType
                    {
                        Budget = 56564564,
                        Types = "Ok"
                    }
                },
                new Car
                {
                    Name = "Mercedes Benz C-300",
                    ID = 300,
                    Color = "Black Jet",
                    Type1 = new CarType
                    {
                        Budget = 56564564,
                        Types = "Ok"
                    }
                },
                new Car
                {
                    Name = "Mercedes Benz C-300",
                    ID = 300,
                    Color = "Black Jet",
                    Type1 = new CarType
                    {
                        Budget = 56564564,
                        Types = "Ok"
                    }
                },
                new Car
                {
                    Name = "Mercedes Benz C-300",
                    ID = 300,
                    Color = "Black Jet",
                    Type1 = new CarType
                    {
                        Budget = 56564564,
                        Types = "Ok"
                    },
                    Type2 = new List<CarType>
                    {
                        new CarType
                        {
                            Types = "Asdsad",
                            Budget = 843584848
                        },
                        new CarType
                        {
                            Types = "auiasudiad",
                            Budget = 909090
                        }
                    }
                }
            };

            return Ok(a);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
