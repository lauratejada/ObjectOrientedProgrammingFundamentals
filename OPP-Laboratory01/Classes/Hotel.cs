using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Lab04.Classes
{
    public class Hotel
    {
        private string _name;
        private string _address;
        private List<Room> _rooms = new List<Room>();
        private List<Client> _clients = new List<Client>();
        private List<Reservation> _reservations = new List<Reservation>();

        public string Name { get { return _name; } }

        public string Address { get { return _address; } }  

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Hotel name can not be empty or null");
            }
            string pattern = "^([0-9A-Za-z]+[/\\s/]?)+$";
            if (!Regex.IsMatch(name, pattern))
            {
                throw new ArgumentException("Hotel name can not have invalid characters", nameof(name));
            }

            _name = name;
        }

        public void SetAddress(string address)
        {
            if (String.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address), "Hotel address can not be empty or null");
            }
            /*
            string pattern = "^[0-9]+[/\\s/]*([a-zA-Z]+[/\\s/]*[a-zA-Z]+[/\\s/])*[0-9]*$";
            if (!Regex.IsMatch(address, pattern))
            {
                throw new ArgumentException("Hotel name can not have invalid characters", nameof(address));
            }
            */
            _address = address;
        }

        public void RegisterARoom(Room room) 
        {
            if (room == null)
            {
                throw new ArgumentNullException("Room should not be null to register", nameof(room));
            }

            _rooms.Add(room);
        }

        public void RegisterAClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("Client should not be null to register", nameof(client));
            }
            _clients.Add(client);
        }

        public void RegisterANewReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException("The reservation can not be registered with a null value.");
            }

            _reservations.Add(reservation);
            reservation.Client.AddAReservationForRecords(reservation);
            reservation.Room.AddAReservationForRecords(reservation);
        }

        
        public Hotel(string name, string address) 
        {
            try
            {
                SetName(name);
                SetAddress(address);
            }
            catch(Exception ex) 
            {
                throw new Exception($"Error creating hotel {ex.Message}");
            }
        }
    }
}