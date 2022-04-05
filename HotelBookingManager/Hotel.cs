using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace HotelBookingManager
{
    public sealed class Hotel : IBookingManager
    {
        // Singleton pattern
        private static volatile Hotel _instance;
        private static object safe = new Object();

        // Not specified to implement a thread-safe Room class (say), so I used .NET
        // concurrency-safe collections for simplicity.
        // In a multi-threaded/multi-process environment we could implement explicit locking
        // (as in the singleton getInstance method below) or a mutex for a multi-process environment.
        // Dictionary so we can have non-contiguous room numbers as per the spec.
        private static volatile ConcurrentDictionary<int, ConcurrentDictionary<DateTime, string>> _rooms;

        private Hotel()
        {
            _rooms = new ConcurrentDictionary<int, ConcurrentDictionary<DateTime, string>>();
            _rooms[101] = new ConcurrentDictionary<DateTime, string>();
            _rooms[102] = new ConcurrentDictionary<DateTime, string>();
            _rooms[203] = new ConcurrentDictionary<DateTime, string>();
            _rooms[204] = new ConcurrentDictionary<DateTime, string>();
        }

        public static Hotel getInstance()
        {

            if (_instance == null)
            {
                lock (safe)
                {
                    if (_instance == null)
                    {
                        _instance = new Hotel();
                    }
                }
            }
            return _instance;
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            if(!_rooms.Keys.Contains(room))
            {
                throw new Exception("Room " + room.ToString() + " does not exist");
            }
            return !_rooms[room].Keys.Contains(date);
        }

        /**
         * Add a booking for the given guest in the given room on the given 
         * date. If the room is not available, throw a suitable Exception. 
         */
        public void AddBooking(string guest, int room, DateTime date)
        {
            if (!_rooms.Keys.Contains(room))
            {
                throw new Exception("Room " + room.ToString() + " does not exist");
            }
            if (!IsRoomAvailable(room, date))
            {
                throw new Exception("Room " + room.ToString() + " is not available on " + date.ToShortDateString());
            }
            _rooms[room][date] = guest;
        }

        /**
        * Return a list of all the available room numbers for the given date 
        */
        public IEnumerable<int> getAvailableRooms(DateTime date)
        {
            return _rooms.Where(a => !a.Value.Keys.Contains(date)).Select(a => a.Key);
        }

    }
}
