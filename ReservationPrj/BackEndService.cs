using Reservations.Model.Data;
using Reservations.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Library
{
    public class BackEndService : IBackEndService
    {
        public Reservation CreateReservation(List<OrderLine> order)
        {
            //Un produit peut être servi qu'une fois au sein d’une même réservation.
            var duplicateProduct = order.GroupBy(x => x.ProductId).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            if (duplicateProduct.Any())
            {
                throw new ArgumentException("Un produit peut être servi qu'une fois au sein d’une même réservation -- ID : " + String.Join(", ", duplicateProduct.ToArray()));
            }

            //On ne peut pas commander un produit qui n’existe pas dans l’inventaire.
            var existingProductInInventory = InventoryData.InventoryList.Where(x => order.Select(y=>y.ProductId).ToList().Contains( x.ProductId)).ToList();
            if(existingProductInInventory.Count != order.Count)
            {
                throw new ArgumentException("On ne peut pas commander un produit qui n’existe pas dans l’inventaire.");
            }


            var reservation = new Reservation(order);
            foreach(var singleOrder in order)
            {
                var selectedinventory = InventoryData.InventoryList.Where(x => x.ProductId == singleOrder.ProductId).FirstOrDefault();
                selectedinventory.Quantity = selectedinventory.Quantity - singleOrder.Quantity;
            }
            ReservationData.SetReservationData(reservation);
            return reservation;
        }

        public List<Reservation> GetReservations(int cursor, int limit)
        {
            //Get 'limit' reservation à partir de la 'cursor'ieme reservation
            //FIFO : OrderBy, déjà la ReservationId est auto-increment 
            return ReservationData.ReservationList.Skip(cursor).Take(limit).OrderBy(x=>x.ReservationId).ToList();
        }

        public List<Inventory> GetInventory(int cursor, int limit)
        {
            //Get 'limit' Inventory à partir de la 'cursor'ieme reservation
            return InventoryData.InventoryList.Skip(cursor).Take(limit).ToList();
        }

        

        public void SetInventory(string productId, int quantity)
        {
            var inventory = new Inventory(productId,quantity);
            InventoryData.SetInventoryData(inventory);
        }
    }
}
