using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tangy_Models
{
    public class SuccessModelDTO
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("successMessage")]
        public string SuccessMessage { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}
