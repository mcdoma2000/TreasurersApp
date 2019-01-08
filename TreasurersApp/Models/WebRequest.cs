using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Models
{
    public class WebRequest<T>
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public WebRequest(string userName, T data)
        {
            this.UserName = userName;
            this.Data = data;
        }
    }
}
