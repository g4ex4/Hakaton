using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain
{
    public class User : IdentityUser<Guid>
    {
        public List<Note>? Notes { get; set; }
    }
}
