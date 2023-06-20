using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab02.Classes
{
    class Product
    {
        //Members
        //fields
        private string _name;
        private int _price; 
        private int _code;

        //properties
        public string Name
        {
            get
            { return _name; }
        }
        public int Price
        {
            get { return _price; }
        }
        public int Code
        {
            get { return _code; }
        }

        public Product(string name, int price, int code)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                   // Console.Write(name);
                    throw new Exception("Please enter a valid value for the Product Name");
                }

                if (!(price > 0))
                {
                    //Console.Write(price);
                    throw new Exception("Please enter a valid value for the Product Price");
                }
                
                if (!(code > 0))
                {
                    //Console.Write(code);
                    throw new Exception("Please enter a valid value for the Product Code");
                }

                 _name = name;
                 _price = price;
                 _code = code;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
            }
        }
    } 
}
