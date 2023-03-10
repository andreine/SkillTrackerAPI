using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeSessionsDto>> GetEmployeeSessions(string UserId);
        Task<EmployeeSessionsDto> AddEmployeeSession(AddEmployeeSessionDto addEmployeeSessionDto);
        Task<EmployeeDetailsDto> GetEmployeeDetailsOnSessionId(int userSessionId);
        Task<EmployeeDetailsDto> GetUserProfile(string userId);
    }
}
