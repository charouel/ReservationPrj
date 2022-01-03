using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Model.Entity
{
    public class Inventory
    {
        public Inventory(string productId, int qte)
        {
            ProductId = productId;
            Quantity = qte;
        }

        public string ProductId { get; }
        public int Quantity { get; set; }
    }
}
