using System.Security.Claims;

namespace FutureWorkshopTicketSystem.Application.Interfaces
{
    public interface IAuthService
    {
        List<Claim> generateClaim(string email, string password,string userId);
        string GeneratejwtSecurityToken(IEnumerable<Claim> claims);
    }
}
