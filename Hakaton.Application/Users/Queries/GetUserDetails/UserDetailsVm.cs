using AutoMapper;
using Hakaton.Application.Common.Mappings;
using Hakaton.Domain;

namespace Hakaton.Application.Users.Queries.GetUserDetails
{
    public class UserDetailsVm: IMapwith<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>().ReverseMap();
        }
    }
}
