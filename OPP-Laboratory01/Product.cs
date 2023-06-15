using OPP_Laboratory01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPP_Laboratory01
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
            if (!String.IsNullOrEmpty(name) && price > 0 && code > 0)
            {
                this._name = name;
                this._price = price;
                this._code = code;
            }
            else
            {
                throw new Exception("Please enter a valid value for the Name or Price or Product Code");
            }
        }
    }
}
