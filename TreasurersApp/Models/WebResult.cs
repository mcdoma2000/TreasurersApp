using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasurersApp.Models
{
    public class WebResult<T>
    {
        public bool Success { get; set; }
        public List<string> StatusMessages { get; set; }
        public T Data { get; set; }

        public WebResult(bool success, List<string> statusMessages, T data)
        {
            this.Success = success;
            this.StatusMessages = new List<string>(statusMessages);
            this.Data = data;
        }
    }
}
