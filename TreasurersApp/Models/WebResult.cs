using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Models
{
    public class WebResult<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("statusMessages")]
        public List<string> StatusMessages { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public WebResult(bool success, List<string> statusMessages, T data)
        {
            this.Success = success;
            this.StatusMessages = new List<string>(statusMessages);
            this.Data = data;
        }
    }
}
