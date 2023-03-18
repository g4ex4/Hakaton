using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser.Interfaces
{
    public interface IAirportCodeResolver
    {
        string GetAirportName(string iataCode);
    }
}
