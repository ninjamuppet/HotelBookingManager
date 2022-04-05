using System;
using Xunit;
using HotelBookingManager;
using System.Collections.Generic;

namespace HotelBookingManager.Tests
{
    public class HotelTests
    {
        [Fact]
        public void Hotel_AddNewBooking()
        {
            IBookingManager ib = Hotel.getInstance();
            DateTime today = DateTime.Now;
            ib.AddBooking("Dave Smith", 101, today);
            bool is101Available = ib.IsRoomAvailable(101, today);
            bool is102Available = ib.IsRoomAvailable(102, today);

            Assert.False(is101Available);
            Assert.True(is102Available);
        }

        [Fact]
        public void Hotel_CheckAvailableRooms()
        {
            IBookingManager ib = Hotel.getInstance();
            DateTime today = DateTime.Now;
            List<int> res = (List<int>)ib.getAvailableRooms(today);

            Assert.True(res.Contains(102) && res.Contains(203) && res.Contains(204) && res.Contains(101));
            ib.AddBooking("Dave Smith", 102, today);
            res = (List<int>)ib.getAvailableRooms(today);
            Assert.True(!res.Contains(102) && res.Contains(203) && res.Contains(204) && res.Contains(101));

        }

    }
}
