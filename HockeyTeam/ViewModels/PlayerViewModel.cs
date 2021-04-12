using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HockeyTeam.ViewModels
{
    public class PlayerViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The player name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a player name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a player name made up of letters and numbers only")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The player description cannot be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please enter a player description between 10 and 200 characters in length")]
        [RegularExpression(@"^[,;a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a player description made up of letters and numbers only")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "The score cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a score between 0.10 and 10000.00")]

        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "The score must be a number up to two decimal places")]
        public decimal Score { get; set; }
        [Display(Name = "Position")]
        public int PositionID { get; set; }
        public SelectList PositionList { get; set; }
        public List<SelectList> ImageLists { get; set; }
        public string[] PlayerImages { get; set; }
    }
}