using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IsCompleted { get; set; }
        public DateTime ActivationDate { get; set; } = DateTime.Now;
        public Session Session { get; set; }
    }
}
