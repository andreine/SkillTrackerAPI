using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserSessionQuestion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IsCorrect { get; set; }
        public Question Question { get; set; }
    }
}
