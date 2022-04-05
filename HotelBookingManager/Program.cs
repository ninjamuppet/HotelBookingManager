using System;

namespace HotelBookingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IBookingManager bm = Hotel.getInstance();// create your manager here; 
            var today = new DateTime(2012, 3, 28);
            Console.WriteLine("Is room 101 available?: " + bm.IsRoomAvailable(101, today)); // outputs true 
            Console.WriteLine("Add booking for Patel in room 101 today");
            bm.AddBooking("Patel", 101, today);
            Console.WriteLine("Is room 101 available now?: " + bm.IsRoomAvailable(101, today)); // outputs false
            Console.WriteLine("Available rooms today: " + string.Join(",", bm.getAvailableRooms(today)));
            Console.WriteLine("Try to book booked room...");
            bm.AddBooking("Li", 101, today); // throws an exception
        }
    }
}
