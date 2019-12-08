using System.ComponentModel.DataAnnotations;
using static PonyWebChallenge.Extensions.EnumAttributeValidationExtension;
using static PonyWebChallenge.Extensions.PonyAttributeValidationExtension;
using static PonyWebChallenge.Models.DifficultiesEnum;

namespace PonyWebChallenge.Models
{
    public class MazeInitModel
    {
        [Required(ErrorMessage = "Maze dimensions should be between 15 and 25")]
        [Range(15, 25, ErrorMessage = "Maze dimensions should be between 15 and 25")]
        public int MazeWidth { get; set; }

        [Required(ErrorMessage = "Maze dimensions should be between 15 and 25")]
        [Range(15, 25, ErrorMessage = "Maze dimensions should be between 15 and 25")]
        public int MazeHeight { get; set; }
        [ValidatePony]
        public string PlayerName { get; set; }
        [ValidateDifficulty]
        public Difficulty Difficulty { get; set; }
    }
}