using AutoMapper;
using Hakaton.Application.Common.Mappings;
using Hakaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapwith<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<User,UserLookupDto>().ReverseMap();
        }
    }
}
