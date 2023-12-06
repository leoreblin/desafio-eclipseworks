using DesafioEclipseworks.WebAPI.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioEclipseworks.WebAPI.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; } = default!;
        public UserRole Role { get; }

        public User(string name, UserRole role)
        {
            Name = name;
            Role = role;
        }
    }
}
