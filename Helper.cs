using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public static class Helper
    {
        public static void PrintMenu()   //Prints the menu
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. View Shopping Cart");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. Exit");
        }

        public static double GetSubTotal(List<Product> Products)    //Calculates subtotal
        {
            double subtotal = 0;
            foreach(var p in Products)
            {
                subtotal += p.Price;
            }
            return subtotal;
        }

        public static double GetTotal(List<Product> Products)   //Calculates total price
        {
            return GetSubTotal(Products) + (0.07 * GetSubTotal(Products));
        }
    }
}
