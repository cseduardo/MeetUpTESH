using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpTESH.Models
{
    [JsonObject("result")]
    public class SpeechResult
    {
        public string Scenario { get; set; }
        public string Name { get; set; }
        public string Lexical { get; set; }
        public string Confidence { get; set; }
    }

    public class SpeechResults
    {
        public List<SpeechResult> results { get; set; }
    }
}
