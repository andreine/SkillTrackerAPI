using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question
    {
/*        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public string FourthAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public Session Session { get; set; }

    }
}
