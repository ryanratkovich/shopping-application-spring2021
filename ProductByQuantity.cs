using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class ProductByQuantity : Product
    {
        public double UnitPrice { get; set; }
        public int Units { get; set; }
        public override double Price    //Overrides Price Method of Product class
        {
            get
            {
                return UnitPrice * Units;
            }
        }
        
        public ProductByQuantity(double price, string name, string des, int id, double unit_price, int units) : base(price, name, des, id)
        {
            UnitPrice = unit_price;
            Units = units;
        }

        public override string ToString()
        {
            return $"Price: ${Price} - Name: {Name} - Description: {Description} - ID: {Id} - Unit Price: ${UnitPrice} - Units: {Units}";
        }
    }
}
