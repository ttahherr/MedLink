using Microsoft.AspNetCore.Identity;

namespace MedLink.Infrastructure.Identity;
public class ApplicationUser: IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}