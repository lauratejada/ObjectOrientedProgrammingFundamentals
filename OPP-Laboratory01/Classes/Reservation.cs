using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab04.Classes
{
    /*
     * -   `class Reservation` represents the objects created each time a Client wishes to reserve a Room. It has a `Datetime Date`, `int Occupants`, `bool isCurrent`, `Client client`, and `Room room`.
     */
    public class Reservation
    {
        private DateTime _date;
        private int _occupants;
        private bool _isCurrent; //  -   `IsCurrent` tracks if this is the current reservation applying to a client and a room.
        private Client _client;
        private Room _room;

        public Client Client { get { return _client; } }

        public Room Room { get { return _room; } }

        public bool IsCurrent { get { return _isCurrent; } }

        public int Occupants { get { return _occupants; } }

        public DateTime Date { get { return _date; } }
             
        public void SetClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client can not be null");
            }
            _client = client; 
        }
        
        public void SetRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException("room can not be null");
            }
            _room = room; 
        }

        public void SetOccupants(int occupants)
        {
            if (occupants < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(occupants), "Numbers of occupants should be a positive value");
            }
            _occupants = occupants; 
        }

        public Reservation(Client client, Room room, int occupants) 
        {
            try
            {
                if (room.Capacity < occupants)
                {
                    throw new Exception("The number of occupants is higher than the capacity");
                }

                if (client == null || room == null)
                {
                    throw new InvalidOperationException("Client or room can not be null");
                }

                SetClient(client);
                SetRoom(room);
                SetOccupants(occupants);
                _date = DateTime.Now;
                _isCurrent = true;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error creating a Reservation: {ex.Message}");
            }
        } 
    }
}