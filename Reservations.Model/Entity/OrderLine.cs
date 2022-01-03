using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Model.Entity
{
    public class OrderLine
    {
        public OrderLine(string productId,int qte)
        {
            //Un produit peut être servi qu'une fois au sein d’une même réservation.            
            ProductId = productId;
            Quantity = qte;
        }
        public string ProductId { get; }
        public int Quantity { get; }
    }
}
