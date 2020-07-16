using HotelReservations.Core.Entity;
using HotelReservations.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations
{
    public class Program
    {
        static void Main(string[] args)
        {

        }
        public static bool CreateReservation(int startDate, int endDate)
        {

            if (ValidateDateFormat(startDate, endDate) == false) {
                return false;
            }

            return MakeBooking(startDate, endDate);

        }

        private static bool ValidateDateFormat(int startDate, int endDate)
        {
            if ((startDate < 0 || startDate > 365) || (endDate < 0 || endDate > 365))
            {
                return false;
            }
            else {
                return true;
            }
        }

        private static bool MakeBooking(int startDate, int endDate)
        {
            var isSuccessfulReservation = false;
            List<int> daysForReservation = CreateReservationDateRange(startDate, endDate);

            var reservedRoomNumbers = new List<Guid>();

            using (var context = new ApplicationContext())
            {
                var allRooms = context.Rooms;
                GetNumberOfReservedRooms(daysForReservation, reservedRoomNumbers, context);
                CreateReservation(startDate, endDate, ref isSuccessfulReservation, reservedRoomNumbers, context, allRooms);
                context.SaveChanges();
            }
            return isSuccessfulReservation;
        }

        private static void CreateReservation(int startDate, int endDate, ref bool isSuccessfulReservation, List<Guid> reservedRoomNumbers,  ApplicationContext context, Microsoft.EntityFrameworkCore.DbSet<Room> allRooms, Reservation newReservation = null)
        {
            foreach (var room in allRooms)
            {
                if (!reservedRoomNumbers.Contains(room.Id))
                {
                    newReservation = new Reservation()
                    {
                        Id = new Guid(),
                        EndDate = endDate,
                        StartDate = startDate,
                        RoomId = room.Id
                    };
                    isSuccessfulReservation = true;
                    break;
                }
            }
            if(newReservation != null)
            context.Reservations.Add(newReservation);
        }

        private static void GetNumberOfReservedRooms(List<int> daysForReservation, List<Guid> reservedRoomNumbers, ApplicationContext context)
        {
            var allReservations = context.Reservations;
            foreach (var reservation in allReservations)
            {
                var reservedDays = CreateReservationDateRange(reservation.StartDate, reservation.EndDate);
                if (daysForReservation.Any(x => reservedDays.Any(y => y == x)))
                {
                    reservedRoomNumbers.Add(reservation.RoomId);
                }
            }
        }

        private static List<int> CreateReservationDateRange(int startDate, int endDate)
        {
            var daysForReservation = new List<int>();
            for (int i = startDate; i <= endDate; i++)
            {
                daysForReservation.Add(i);
            }

            return daysForReservation;
        }

        public static void DeleteReservationsAndRooms() {
            using (var context = new ApplicationContext())
            {
                var rooms = context.Rooms;
                foreach (var room in rooms)
                {
                    context.Remove(room);
                }

                var reservations = context.Reservations;
                foreach (var reservation in reservations)
                {
                    context.Remove(reservation);
                }
                context.SaveChanges();
            }
        }

        public static void AddingRooms(int numberOfRooms)
        {
            using (var context = new ApplicationContext())
            {
                for (int i = 0; i < numberOfRooms; i++)
                {
                context.Rooms.Add(new Room());
                }
                context.SaveChanges();
            }
        }
    }
}

