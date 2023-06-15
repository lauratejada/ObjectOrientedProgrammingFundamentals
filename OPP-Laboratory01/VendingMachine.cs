using OPP_Laboratory01;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OPP_Laboratory01
{
    class VendingMachine
    {
        private int _serialNumber;// a unique ID for the machine itself
        private Dictionary<int, int> _moneyFloat;//a dictionary which tracks the
                                                 //quantities of various money pieces that the machine possesses.
        private Dictionary<Product, int> _inventory;//a dictionary which tracks how many
                                                    //of each product the machine possesses.
        public int SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (value > 0)
                {
                    _serialNumber = value;
                }
                else
                {
                    throw new Exception("The serial number should be a valid number (greater than zero)");
                }
            }

        }

        public Dictionary<int, int> MoneyFloat
        {
            get { return _moneyFloat; }
            set
            {
                if (value != null)
                {
                    _moneyFloat = value;
                }
                else
                {
                    throw new Exception("This List should doesn't have values");
                }
            }
        }

        public Dictionary<Product, int> Inventory
        {
            get { return _inventory; }
            set
            {
                if (value != null)
                {
                    _inventory = value;
                }
                else
                {
                    throw new Exception("This List should doesn't have values");
                }
            }
        }

        public int GetQuantityOfACoinInMachine(int coin)
        {
            bool isValidCoin = _moneyFloat.TryGetValue(coin, out int quantity);
            if (!isValidCoin)
            {
                throw new Exception("This coin is not valid or not exist");
            }
            else
            {
                return quantity;
            }
        }

        //The constructor should initialize the dictionary properties, and provide it a serial number.
        public VendingMachine(int idNumber, Dictionary<int, int> money, Dictionary<Product, int> inventory)
        {
            if (idNumber > 0 && money.Count > 0 && inventory.Count > 0)
            {
                this._serialNumber = idNumber;
                this._moneyFloat = money;
                this._inventory = inventory;
            }
        }

        public VendingMachine(int idNumber)
        {   // validating and initialazing serial number
            if (idNumber > 0)
            {
                _serialNumber = idNumber;
            }
            // initializing the quantity of coins to zero for if type of coin
            _moneyFloat = new Dictionary<int, int>();
            /*{
                { 20, 0 },
                { 10, 0 },
                { 5, 0 },
                { 2, 0 },
                { 1, 0 }
            };*/

            _inventory = new Dictionary<Product, int>();
        }

        //adds a product to the vending machine’s product inventory, including new items or
        //items already in inventory.It should return a string confirming the product name,
        //code, price, and new quantity.
        public string StockItem(Product product, int quantity)
        {
            //inventory
            if (quantity > 0 && product != null)
            {
                // check if product exists to add new quantity
                if (_inventory.ContainsKey(product))
                {
                    //update quantity of product by adding to the existent ones
                    _inventory[product] = _inventory[product] + quantity;
                }
                else
                {
                    // if product not exist add as new in inventory
                    _inventory.Add(product, quantity);
                }
            }
            else
            {
                throw new Exception("This product does not exist or the quantity is not valid");
            }
            return $"Succesfully added : {product.Name}, {product.Code}, {product.Price}, {_inventory[product]}";
        }

        // adds money pieces of the given denomination to the machine’s change float.
        // It should return a string confirming the entire stock of the float.
        public string StockFloat(int moneyDenomination, int quantity)
        {
            if (quantity > 0)
            {
                switch (moneyDenomination)
                {
                    case 1: { _moneyFloat.Add(1, quantity); break; }
                    case 2: { _moneyFloat.Add(2, quantity); break; }
                    case 5: { _moneyFloat.Add(5, quantity); break; }
                    case 10: { _moneyFloat.Add(10, quantity); break; }
                    case 20: { _moneyFloat.Add(20, quantity); break; }
                    default: { throw new Exception("This coin is not valid"); break; }
                }
            }
            else
            {
                throw new Exception("The quantity of coins is not valid");
            }

            //Console.WriteLine("money inventory");
            string stringOfStockOfFloat = "";
            foreach (KeyValuePair<int, int> money in _moneyFloat)
            {
                stringOfStockOfFloat += $" ${money.Key}: {money.Value}, ";
            }
            return $"Succesfully added: \n {stringOfStockOfFloat}";
        }

        //provides a product code to the machine and a list of money pieces provided. 
        //    - The method returns a string appropriate to whatever has occurred.
        //    (ie. “Error, no item with code “E17”, “Error: Item is out of stock”, “Error: insufficient money provided”,
        //    “Please enjoy your ‘Jelly Beans’ and take your change of $0.60”).
        // this is the method for vending a product
        public string VendItem(string code, List<int> money)
        {
            // - It finds that product with the code given in the machine’s product inventory,
            // ensures that it is in stock, and then ensures that the money provided is sufficient to pay for the product.
            int productPrice = 0;
            int changeRequiredAmount = 0;
            bool productIsInStock;
            string returnMessage = "";
            if (!String.IsNullOrEmpty(code))
            {
                bool productCodeExists = false;
                foreach (KeyValuePair<Product, int> item in _inventory)
                {
                    productIsInStock = false;
                    if (item.Key.Code.ToString() == code)
                    {
                        if (item.Value > 0)
                        {
                            productIsInStock = true;
                            productPrice = item.Key.Price;
                        }
                        else
                        {
                            returnMessage = $"Error: Item is out of stock";
                        }
                        productCodeExists = true;
                        break;
                    }
                }

                if (!productCodeExists)
                {
                    returnMessage = $"Error, no item with code {code}";
                }
            }
            else
            {
                returnMessage = "Error: code is not valid";
            }

            if (money != null && money.Count > 0)
            {
                int totalAmountEntered = 0;
                foreach (int coin in money)
                {
                    totalAmountEntered += coin;
                }

                if (totalAmountEntered > productPrice)
                {
                    // calculate the change
                    changeRequiredAmount = totalAmountEntered - productPrice;
                }
                else
                {
                    returnMessage = "Error: insufficient money provided";
                }

            }


            // - It then vends the required change, if any, and reduces the quantity of that item in inventory and the returned change.
           // Dictionary<int,int> changeCounter = new Dictionary<int,int>();


            if (changeRequiredAmount > 0)
            {  
                List<int> denominations = MoneyFloat.Keys.OrderByDescending(x => x).ToList();
                Dictionary<int, int> changeReturned
                     = new Dictionary<int, int>();

                foreach (int coin in denominations)
                {
                    if (changeRequiredAmount >= coin && MoneyFloat.GetValueOrDefault(coin) > 0)
                    {
                        int numberToDispense = changeRequiredAmount / coin;
                        if (MoneyFloat.GetValueOrDefault(coin) < numberToDispense)
                        { 
                            numberToDispense = MoneyFloat.GetValueOrDefault(coin);
                        }

                        // substract from the current amount
                        changeRequiredAmount -= coin * numberToDispense;

                        changeReturned.Add(coin, numberToDispense);
                    }
                }

                if (changeRequiredAmount > 0)
                {
                    returnMessage = "Unable to dispense change. Returning coins inserted...";
                }
                else
                {
                    returnMessage = "Returning your change: ";
                    foreach (KeyValuePair<int, int> pair in changeReturned)
                    {
                        returnMessage += $"{pair.Key} x {pair.Value}, ";  
                    }
                    
                }
            }

            if(changeRequiredAmount <= 0) // && productIsInStock)
            {
                foreach (KeyValuePair<Product, int> item in _inventory)
                {
                    int val = 0;
                    // productIsInStock = false;
                    if (item.Key.Code.ToString() == code)
                    {
                        val = item.Value;
                        val = val - 1;
                       // item[KeyValuePair] = val;
                    }
                }
            }

            return returnMessage;
        }
    }
}
