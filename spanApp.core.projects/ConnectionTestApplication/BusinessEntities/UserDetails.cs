using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionTestApplication.BusinessEntities
{
    
    public class UserDetails
    {
        
        public ObjectId id { get; set; }
        public String ID { get; set; }
        public string UserName  { get; set; }
        public string Password { get; set; }
    }
}
