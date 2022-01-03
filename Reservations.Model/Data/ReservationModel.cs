using Reservations.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Model.Data
{
    public static class ReservationData
    {
        public static void SetReservationData(Reservation reservation)
        {
            if(ReservationList == null)
            {
                ReservationList = new List<Reservation>();
            }
            ReservationList.Add(reservation);
        }
        public static List<Reservation> ReservationList { get; private set; }
    }
}
