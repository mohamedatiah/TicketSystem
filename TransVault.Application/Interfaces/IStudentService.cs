using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransVault.Domain.Common;

namespace TransVault.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetByIdAsync(int id);
        Task<StudentDto> AddAsync(StudentDto document);
        Task DeleteAsync(int id);
    }
}
