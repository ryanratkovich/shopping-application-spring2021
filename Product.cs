using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Product
    {
        public virtual double Price { get; set; }  //Virtual property to be overriden in subclasses
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public Product(double price, string name, string des, int id)   //Constructor
        {
            Price = price;
            Name = name;
            Description = des;
            Id = id;
        }

        public override string ToString() 
        {
            return $"Price: ${Price} - Product Name: {Name} - Description: {Description} - ID: {Id}";
        }
    }
}
