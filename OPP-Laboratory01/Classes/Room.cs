using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Lab04.Classes
{
    /*
     * -   `class Room` represents a room. Every room has a `string Number`,  `int Capacity`, `boolean Occupied`, and `List<Reservation> Reservations`.
     */
    public class Room
    {
        private string _number;
        private int _capacity;
        private bool _isOccupied;
        private List<Reservation> _reservations = new List<Reservation>();

        public string Number { get { return _number; } }
        public int Capacity { get { return _capacity; } }

        public bool IsOccupied { get { return _isOccupied; } }

        public void SetNumber(string number) 
        {
            if (String.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number), "Room number can not be empty or null");
            }

            _number = number;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Room Capacity should be a positive value");
            }

            _capacity = capacity; 
        } 

        public void SetIsOccupied(bool isOccupied) 
        {
            if (isOccupied.GetType() != typeof(bool))
            {
                throw new InvalidOperationException("Invalid value for IsOccupied" + nameof(isOccupied));
            }

            _isOccupied = isOccupied;
        }

        public void AddAReservationForRecords(Reservation reservation) 
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }
            _reservations.Add(reservation);
        }

        /*
         *   -  The list keeps track of ALL reservations for history, so the Occupied property determines if the room has a current occupant.
         */
        public Room(string number, int capacity)
        {
            try 
            { 
                SetNumber(number);
                SetCapacity(capacity);
                SetIsOccupied(false);
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error creating a room {ex.Message}");
            }
        }
    }
}
