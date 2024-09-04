using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.BaseResponse
{
    [Serializable]
    public class ResponseResult<T> where T : class
    {
        [JsonProperty("StatusCode")]
        public string? StatusCode { get; set; }

        [JsonProperty("Message")]
        public string? Message { get; set; }
        
        [JsonProperty("Data")]
        public T? Data { get; set; }
    }
}
