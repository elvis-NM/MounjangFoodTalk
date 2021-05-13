using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.ViewModels
{
    public class EditQuestionViewModel
    {
        [Required]
        public int QuestionID { get; set; }

        [Required]
        public string QuestionName { get; set; }

        [Required]

        public DateTime QuestionDateAndTime { get; set; }

        [Required]
        public int CategoryID { get; set; }

    }
}
