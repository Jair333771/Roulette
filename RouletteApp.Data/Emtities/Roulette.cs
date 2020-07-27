using System.ComponentModel.DataAnnotations;

namespace RouletteApp.Data.Emtities
{
    public class Roulette
    {
        [Key]
        public int Id { get; set; }
        public int MaxAmount { get; set; }
        public bool State { get; set; }
    }
}