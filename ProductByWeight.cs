using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class ProductByWeight : Product
    {
        public double PricePerOunce { get; set; }
        public double Ounces { get; set; }
        
        public override double Price    //Overrides Price Method of Product class
        {
            get
            {
                return PricePerOunce * Ounces;
            }
        }
        
        public ProductByWeight(double price, string name, string des, int id, double price_per_ounce, double ounces) : base(price, name, des, id)
        {
            PricePerOunce = price_per_ounce;
            Ounces = ounces;
        }

        public override string ToString()
        {
            return $"Price: ${Price} - Name: {Name} - Description: {Description} - ID: {Id} - Price Per Ounce: ${PricePerOunce} - Ounces: {Ounces}";
        }
    }
}
