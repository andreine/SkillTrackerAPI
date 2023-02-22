using Domain.Dtos;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeSessionsDto > AddEmployeeSession(AddEmployeeSessionDto addEmployeeSessionDto)
        {
            throw new NotImplementedException();

        }

        public async Task<IList<EmployeeSessionsDto>> GetEmployeeSessions(string userId)
        {
            var employeeSessions = await _context.UserSessions.Where(x => x.UserId == userId).Select(x => new EmployeeSessionsDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ActivationDate = x.ActivationDate,
                IsCompleted = x.IsCompleted,
            }).ToListAsync();

            return employeeSessions;

        }
    }
}
