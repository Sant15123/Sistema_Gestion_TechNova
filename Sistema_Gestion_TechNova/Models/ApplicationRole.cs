using Microsoft.AspNetCore.Identity;

namespace Sistema_Gestion_TechNova.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
