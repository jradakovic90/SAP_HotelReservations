using HotelReservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCase1()
        {
            Program.DeleteReservationsAndRooms();
            Program.AddingRooms(1);

            Assert.IsFalse(Program.CreateReservation(-4, 2));
            Assert.IsFalse(Program.CreateReservation(200, 400));
        }

        [TestMethod]
        public void TestCase2()
        {
            Program.DeleteReservationsAndRooms();
            Program.AddingRooms(3);
            Assert.IsTrue(Program.CreateReservation(0, 5));
            Assert.IsTrue(Program.CreateReservation(7, 13));
            Assert.IsTrue(Program.CreateReservation(3, 9));
            Assert.IsTrue(Program.CreateReservation(5, 7));
            Assert.IsTrue(Program.CreateReservation(6, 6));
            Assert.IsTrue(Program.CreateReservation(0, 4));
        }

        [TestMethod]
        public void TestCase3()
        {
            Program.DeleteReservationsAndRooms();
            Program.AddingRooms(3);
            Assert.IsTrue(Program.CreateReservation(1, 3));
            Assert.IsTrue(Program.CreateReservation(2, 5));
            Assert.IsTrue(Program.CreateReservation(1, 9));
            Assert.IsFalse(Program.CreateReservation(0, 15));
        }

        [TestMethod]
        public void TestCase4()
        {
            Program.DeleteReservationsAndRooms();
            Program.AddingRooms(3);
            Assert.IsTrue(Program.CreateReservation(1, 3));
            Assert.IsTrue(Program.CreateReservation(0, 15));
            Assert.IsTrue(Program.CreateReservation(1, 9));
            Assert.IsFalse(Program.CreateReservation(2, 5));
            Assert.IsTrue(Program.CreateReservation(4, 9));
        }

        [TestMethod]
        public void TestCase5()
        {
            //according to: "If a booking request arrives and we can accept it, we accept it directly.We do not wait for later requests
            //(e.g.to maximize the utilization of our rooms)" - we used greedy algorithm and by that logic 
            //requests 5 will be declined and 8 will be accepted
            Program.DeleteReservationsAndRooms();
            Program.AddingRooms(2);
            Assert.IsTrue(Program.CreateReservation(1, 3));
            Assert.IsTrue(Program.CreateReservation(0, 4));
            Assert.IsFalse(Program.CreateReservation(2, 3));
            Assert.IsTrue(Program.CreateReservation(5, 5));
            Assert.IsFalse(Program.CreateReservation(4, 10));
            Assert.IsTrue(Program.CreateReservation(10, 10));
            Assert.IsTrue(Program.CreateReservation(6, 7));
            Assert.IsTrue(Program.CreateReservation(8, 10));
            Assert.IsTrue(Program.CreateReservation(8, 9));
        }
    }
}
