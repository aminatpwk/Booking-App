using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Errors
{
    public enum ErrorType
    {
        Failure = 0,
        BadRequest,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Validation,
        Database, 
        ExternalService
    }
}
