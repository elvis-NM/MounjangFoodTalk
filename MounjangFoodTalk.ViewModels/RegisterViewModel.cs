using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string Name { get; set; }


        [Required]

        public string Mobile { get; set; }
    }
}