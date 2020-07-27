using System;
using System.ComponentModel.DataAnnotations;

namespace RouletteApp.Data.Emtities
{
    public class Consumer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0, 10000, ErrorMessage = "La apuesta máxima es de USD 10.000")]
        public int Bet { get; set; }
        public decimal Money { get; set; }
    }
}