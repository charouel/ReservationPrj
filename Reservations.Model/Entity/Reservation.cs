using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Model.Entity
{
    public class Reservation
    {
        private static object sync = new object();
        private static int globalCount;

        public Reservation(List<OrderLine> orderLines)
        {
            // RG-1 :L'identifiant de la réservation est unique (integer auto-increment ou une string unique).
            lock (sync)
            {
                this.ReservationId = ++ globalCount;
            }
            OrdersLines = orderLines;
            CreatedAt = DateTime.Now;
            IsAvailable = true;
        }

        public int ReservationId { get; }
        public DateTime CreatedAt { get; }
        public List<OrderLine> OrdersLines { get; }
        public bool IsAvailable { get; }
               
    }
}
