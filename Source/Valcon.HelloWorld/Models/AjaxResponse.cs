using System.Collections.Generic;
using Valcon.HelloWorld.Configuration;

namespace Valcon.HelloWorld.Models
{
    [PartialModel]
    public class AjaxResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}