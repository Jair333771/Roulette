using System;
using System.ComponentModel.DataAnnotations;

namespace RouletteApp.Data.Emtities
{
    public class Bet
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El valor de la apuesta es requerido")]
        [Range(100, 10000, ErrorMessage = "La apuesta debe estar entre 100 y 10000 USD")]
        public decimal Amount { get; set; }

        public string Color { get; set; }

        public int Number { get; set; }

        public string MyBet { get; set; }

        [Required(ErrorMessage = "El  id de ruleta es requerido")]
        public int IdRoulette { get; set; }
    }
}