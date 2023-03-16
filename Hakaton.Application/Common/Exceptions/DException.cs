using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Common.Exceptions
{
    public class DException : Exception
    {
        public DException() : base() { }
        public DException(string msg) : base(msg) { }

        public override string StackTrace => string.Empty;
    }
}
