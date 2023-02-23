using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class EmployeeSessionsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IsCompleted { get; set; }
        public DateTime ActivationDate { get; set; }
        public string SessionName { get; set; }
        public int SessionId { get; set; }
    }
}
