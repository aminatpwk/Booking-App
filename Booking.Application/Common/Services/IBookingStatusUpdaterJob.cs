using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Services
{
    public interface IBookingStatusUpdaterJob
    {
        Task UpdateCompletedBookings();
    }
}
