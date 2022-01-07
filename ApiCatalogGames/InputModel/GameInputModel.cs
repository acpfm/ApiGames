using System.ComponentModel.DataAnnotations;

namespace ApiCatalogGames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name of the game must contain between 3 and 100 character")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The Name of the producer game must contain between 3 and 100 character")]
        public string Producer { get; set; }
        [Required]
        [Range(1, 500, ErrorMessage = "The price game must have between 1 and 500 dollars")]
        public double Price { get; set; }
    }
}
