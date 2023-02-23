using Domain.Dtos;
using Domain.Entities;
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
        public async Task<EmployeeSessionsDto> AddEmployeeSession(AddEmployeeSessionDto addEmployeeSessionDto)
        {

            var session = _context.Sessions.FirstOrDefault(x => x.Id == addEmployeeSessionDto.SessionId);
            var newSession = new UserSession
            {
                UserId = addEmployeeSessionDto.UserId,
                IsCompleted = 0,
                Session = session,
                ActivationDate = DateTime.Now,
            };

            var createdSession = _context.UserSessions.Add(newSession);
            await _context.SaveChangesAsync();


            var returnDto = await _context.UserSessions
                .Where(x => x.UserId == addEmployeeSessionDto.UserId && x.Session.Id == addEmployeeSessionDto.SessionId)
                .Select(x => new EmployeeSessionsDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ActivationDate = x.ActivationDate,
                IsCompleted = x.IsCompleted,
            }).FirstOrDefaultAsync();


            return returnDto;

        }

        public async Task<IList<EmployeeSessionsDto>> GetEmployeeSessions(string userId)
        {
            var employeeSessions = await _context.UserSessions.Where(x => x.UserId == userId).Select(x => new EmployeeSessionsDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ActivationDate = x.ActivationDate,
                IsCompleted = x.IsCompleted,
                SessionName = x.Session.Name,
                SessionId = x.Session.Id

            }).ToListAsync();

            return employeeSessions;

        }
    }
}
