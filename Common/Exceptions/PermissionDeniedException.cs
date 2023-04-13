using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    internal class PermissionDeniedException : Exception
    {
        public PermissionDeniedException() : base("You haven't permissions to do it")
        {
        }
    }
}
