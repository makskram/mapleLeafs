using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HockeyTeam.Models
{
    public class Position
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Heya, did you forget about position?")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Position name must be somewhere between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a position name beginning with a capital letter and made up of letters and spaces only")]
        [Display(Name = "Position Name")]
        public string Name { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}