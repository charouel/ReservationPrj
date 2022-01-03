using Reservations.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Library
{
    public interface IBackEndService
    {
        Reservation CreateReservation(List<OrderLine> order);
        List<Reservation> GetReservations(int cursor, int limit);
        void SetInventory(string productId, int quantity);
        List<Inventory> GetInventory(int cursor, int limit);
    }
}
