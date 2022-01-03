using Reservations.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Model.Data
{
    public static class InventoryData
    {
        public static void SetInventoryData(Inventory inventory)
        {
            if (InventoryList == null)
            {
                InventoryList = new List<Inventory>();
            }
            var inv = InventoryList.Where(x => x.ProductId == inventory.ProductId).FirstOrDefault();

            // Si l'ID existe alors on fait la MAJ de qte
            if (inv!=null)
            {
                inv.Quantity = inv.Quantity + inventory.Quantity;
            }
            // Si non on ajoute le produit
            else
            {
                InventoryList.Add(inventory);
            }
        }
        public static List<Inventory> InventoryList { get; private set; }
    }
}
