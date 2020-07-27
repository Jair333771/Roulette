using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RouletteApp.Core.Models
{
    public class BetResponseModel
    {
        [DataMember(EmitDefaultValue = false)]
        public List<string> Message { get; set; } = new List<string>();

        [DataMember(EmitDefaultValue = false)]
        public int? Number { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? IdRoulette { get; set; }

        public string Color { get; set; }
    }
}