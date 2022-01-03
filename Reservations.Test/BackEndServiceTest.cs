using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reservations.Library;
using Reservations.Model.Entity;
using System;
using System.Collections.Generic;
using Reservations.Model.Data;

namespace Reservations.Test
{
    [TestClass]
    public class BackEndServiceTest
    {

        [TestMethod]
        public void TestSetInventorySameProduct()
        {
            IBackEndService bachEndService = new BackEndService();
            bachEndService.SetInventory("SDA1X", 30);
            bachEndService.SetInventory("SDA1X", 50);
            bachEndService.SetInventory("SDA1X", -10);
            Assert.AreEqual(InventoryData.InventoryList.FindAll(x=>x.ProductId== "SDA1X")[0].Quantity, 70);
        }

        /// <summary>
        /// On ne peut pas commander un produit qui n’existe pas dans l’inventaire.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "On ne peut pas commander un produit qui n’existe pas dans l’inventaire.")]
        public void TestCreateReservationNotExisteInInventory()
        {
            IBackEndService bachEndService = new BackEndService();
            bachEndService.SetInventory("SDA1", 30);

            var oredrList = new List<OrderLine>();
            oredrList.Add(new OrderLine("SDA1", 3));
            oredrList.Add(new OrderLine("SDA34XXXX", 1));

            bachEndService.CreateReservation(oredrList);
        }

        /// <summary>
        /// On ne peut pas commander un produit qui n’existe pas dans l’inventaire.
        /// </summary>
        [TestMethod]        
        public void TestCreateReservationIsExisteInInventory()
        {
            IBackEndService bachEndService = new BackEndService();
            bachEndService.SetInventory("SDA1", 30);
            bachEndService.SetInventory("SDA34", 300);


            var oredrList = new List<OrderLine>();
            oredrList.Add(new OrderLine("SDA1", 3));
            oredrList.Add(new OrderLine("SDA34", 1));

            var reservation = bachEndService.CreateReservation(oredrList);
            Assert.IsNotNull(reservation);
        }

        [TestMethod]
        public void TestCreateReservationData()
        {
            

            var oredrList = new List<OrderLine>();
            IBackEndService bachEndService = new BackEndService();
            //Set Inventaire 
            bachEndService.SetInventory("SDA1", 300);
            bachEndService.SetInventory("SDA34", 300);
            bachEndService.SetInventory("SDA31", 300);
            bachEndService.SetInventory("SMMD1", 300);
            bachEndService.SetInventory("SMMD134", 300);
            bachEndService.SetInventory("SMMD131", 300);

            //Passer commande 1
            oredrList.Add(new OrderLine("SDA1", 3));
            oredrList.Add(new OrderLine("SDA34", 1));
            oredrList.Add(new OrderLine("SDA31", 9));

            bachEndService.CreateReservation(oredrList);

            //Passer commande 2
            oredrList.Add(new OrderLine("SMMD1", 1));
            oredrList.Add(new OrderLine("SMMD134", 1));
            oredrList.Add(new OrderLine("SMMD131", 29));

            bachEndService.CreateReservation(oredrList);
            Assert.AreEqual(ReservationData.ReservationList.Count, 2);

        }

        [TestMethod]
        public void TestGetReservationDataWithCursorAndLimit()
        {
            var oredrList = new List<OrderLine>();
            IBackEndService bachEndService = new BackEndService();
            //Set Inventory
            bachEndService.SetInventory("SDA1", 300);
            bachEndService.SetInventory("SDA2", 300);

            oredrList.Add(new OrderLine("SDA1", 3));
            oredrList.Add(new OrderLine("SDA2", 3));

            //Ajout de 16 Reservation 
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            bachEndService.CreateReservation(oredrList);
            //Get 10 reservation à partir de la 3ieme reservation
            var reservations = bachEndService.GetReservations(3, 10);
            Assert.AreEqual(reservations.Count, 10);
        }

        /// <summary>
        /// Test : Un produit peut être servi qu'une fois au sein d’une même réservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Un produit peut être servi qu'une fois au sein d’une même réservation -- ID : SDA1")]
        public void TestSetReservationDataWithSameProductError()
        {
            var oredrList = new List<OrderLine>();
            IBackEndService bachEndService = new BackEndService();
            //Set inventory
            bachEndService.SetInventory("SDA1", 300);

            oredrList.Add(new OrderLine("SDA1", 3));
            oredrList.Add(new OrderLine("SDA1", 3));
            bachEndService.CreateReservation(oredrList);
        }


        /// <summary>
        /// Test : Le process complet Ajout inventaire, reserver , get inventaire => vérifier si le calcule est bon
        /// </summary>
        [TestMethod]
        public void TestAllFunctionToCalculateResultInventory()
        {
            var oredrList = new List<OrderLine>();
            IBackEndService bachEndService = new BackEndService();
            //Set inventory
            bachEndService.SetInventory("SDA1X2", 300);
            //reserver 3 produits
            oredrList.Add(new OrderLine("SDA1X2", 3));
            bachEndService.CreateReservation(oredrList);

            //Get inventory
            var lstInventaro = bachEndService.GetInventory(0, 1);

            //vérifier si l'inventaire fonctionne tres bien
            Assert.AreEqual(lstInventaro[0].Quantity, 297);
        }

    }
}
