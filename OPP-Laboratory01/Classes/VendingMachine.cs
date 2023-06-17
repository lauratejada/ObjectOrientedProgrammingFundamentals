using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab02.Classes
{
    class VendingMachine
    {
        private int _serialNumber;// a unique ID for the machine itself
        private const int _initialSerialNumber = 0; //100 initial value of serialnumber previous to add
        private static int _addToNextNumber = 1; //value will increase after create an instance
        private readonly string _barCode;
        private Dictionary<int, int> _moneyFloat;//a dictionary which tracks the
                                                 //quantities of various money pieces that the machine possesses.
        private Dictionary<Product, int> _inventory;//a dictionary which tracks how many
                                                    //of each product the machine possesses.
        public int SerialNumber
        {
            get { return _serialNumber; }
        }

        public string BarCode
        {
            get { return _barCode; }
        }

        public Dictionary<int, int> MoneyFloat
        {
            get { return _moneyFloat; }
            protected set
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
            protected set
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
        public VendingMachine(string barCode)
        {
            if (!String.IsNullOrEmpty(barCode))
            {
                this._serialNumber = _initialSerialNumber + _addToNextNumber++;
                this._barCode = barCode;
                // initializing the quantity of coins to zero for if type of coin, and definignthe type of coins denomination  accepted
                _moneyFloat = new Dictionary<int, int>
                {
                    { 20, 0 },
                    { 10, 0 },
                    { 5, 0 },
                    { 2, 0 },
                    { 1, 0 }
                };

                _inventory = new Dictionary<Product, int>();
            }
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
                return $"Succesfully added : {product.Name}, {product.Code}, {product.Price}, {_inventory[product]}";
            }
            else
            {
                return "This product does not exist or the quantity is not valid";
            }
        }

        // adds money pieces of the given denomination to the machine’s change float.
        // It should return a string confirming the entire stock of the float.
        public string StockFloat(int moneyDenomination, int quantity)
        {
            if (quantity > 0 && moneyDenomination > 0)
            {
                if (_moneyFloat.ContainsKey(moneyDenomination))
                {
                    _moneyFloat[moneyDenomination] += quantity;
                    return $"Succesfully added: ${moneyDenomination}: {quantity}";
                }
                else
                {   //_moneyFloat[moneyDenomination] = quantity;
                    return "This denomination of coin is not accepted in this machine";
                }
            }
            else
            {
                return "The quantity of coins is not valid";
            }
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
            if (!String.IsNullOrEmpty(code))
            {
                bool productCodeExists = false;
                foreach (KeyValuePair<Product, int> item in _inventory)
                {
                    productIsInStock = false;
                    if (item.Key.Code.ToString() == code)
                    {
                        productCodeExists = true;
                        if (item.Value > 0)
                        {
                            productIsInStock = true;
                            productPrice = item.Key.Price;
                        }
                        else
                        {
                            return $"Error: Item is out of stock";
                        }
                        break;
                    }
                }

                if (!productCodeExists)
                {
                    return $"Error, no item with code {code} available";
                }
            }
            else
            {
                return "Error: code is not valid";
            }

            if (money.Count > 0)
            {
                //Console.WriteLine(money.Count);
                int totalAmountEntered = 0;

                foreach (int coin in money)
                {
                    totalAmountEntered += coin;
                }

                if (totalAmountEntered >= productPrice)
                {
                    // calculate the change
                    changeRequiredAmount = totalAmountEntered - productPrice;
                }
                else
                {
                    return "Error: insufficient money provided";
                }
            }

            // - It then vends the required change, if any, and reduces the quantity of that item in inventory and the returned change.
            if (changeRequiredAmount > 0)
            {
                List<int> denominations = MoneyFloat.Keys.OrderByDescending(x => x).ToList();
                Dictionary<int, int> changeReturned = new Dictionary<int, int>();

                foreach (int coin in denominations)
                {
                    if (changeRequiredAmount >= coin &&
                        MoneyFloat.GetValueOrDefault(coin) > 0)
                    {
                        int numberToDispense = changeRequiredAmount / coin;
                        if (MoneyFloat.GetValueOrDefault(coin) < numberToDispense)
                        {
                            numberToDispense = MoneyFloat.GetValueOrDefault(coin);
                        }
                        // substract from the current amount
                        changeRequiredAmount = changeRequiredAmount - (coin * numberToDispense);
                    }
                }

                if (changeRequiredAmount > 0)
                {
                    return "Unable to dispense change. Returning coins inserted...";
                }
                else
                {
                    return "Returning your change:  ";
                }
            }
            else
            {
                return $"Please enjoy your product and no change is required.";
            }
        }
    }
}
