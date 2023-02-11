using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SessionSkillActivityQuestion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsCorrect { get; set; }
        public Question Question { get; set; }
        public SessionSkillActivity SessionSkillActivity { get; set; }
    }
}
