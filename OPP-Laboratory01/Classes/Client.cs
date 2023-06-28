using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Lab04.Classes
{
    public class Client
    {
        private string _name;
        private long _creditCard;
        List<Reservation> _reservations = new List<Reservation>();
        public string Name { get { return _name; } }
        public long CreditCard { get { return _creditCard; } }
            
        public void SetCreditCard(long creditCard) 
        {
            if (creditCard < 0)
            {
                throw new ArgumentOutOfRangeException("Credit card number should be a positive long number",nameof(creditCard));
            }

            if(creditCard.ToString().Length == 12)
            {
                throw new ArgumentOutOfRangeException("Credit card number length should be 12 digits", nameof(creditCard));
            }

            _creditCard = creditCard;
        } 

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Client name can not be empty or null");
            }

            string pattern = "^([0-9A-Za-z]+[/\\s/]?)+$";

            if (!Regex.IsMatch(name, pattern ))
            {
                throw new ArgumentException("Client name can not have invalid characters", nameof(name));
            }

            _name = name;
        }

        public void AddAReservationForRecords(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }
            _reservations.Add(reservation);
        }

        public void PayForAReservation()
        {
            Console.WriteLine("The client is paying for a reservation");
        }

        public Client(string name, long creditCard) 
        {
            try 
            { 
                SetName(name);
                //SetCreditCard(creditCard);
            } 
            catch(Exception ex) 
            {
                throw new Exception($"Error creating a client : {ex.Message}");
            }
        }
    }
}
