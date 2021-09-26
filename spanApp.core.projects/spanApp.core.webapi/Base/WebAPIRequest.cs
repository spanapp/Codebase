using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace spanApp.core.webapi.Base
{
    [XmlRoot]
    [DataContract]
    [Serializable]
    public class WebAPIRequest 
    {
        [DataMember]
        public string JSONData { get; set; }
        private JObject _data = new JObject();

        [IgnoreDataMember]
        public JObject Data
        {
            get
            {              
                    if (JSONData!= string.Empty)
                    {
                        _data = JObject.Parse(JSONData);
                    }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public WebAPIRequest()
        {
            Data = new JObject();

        }

        public T ValueOf<T>(string key)
        {
            Type type = typeof(T);

            if (Data[key] != null)
            {
                return new JArray(Data[key])[0].ToObject<T>();
            }
            else
            {
                return default(T);
            }
        }
    }
}