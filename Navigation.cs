using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public static class Navigation
    {
        public static void InventoryPaging(List<Product> p) //Allows for navigation through products in Inventory
        {
            bool listing = true;
            int count = 5;
            Console.WriteLine("\n--------------------");
            Console.WriteLine("Inventory:");
            while (listing) //Begin Paging (5 Products at a time)
            {
                if (p.Count - count > 0)
                {
                    for (var i = count - 5; i < count; ++i)
                    {
                        Console.WriteLine(p[i]);
                    }
                }
                else
                {
                    for (var i = count - 5; i < p.Count; ++i)
                    {
                        Console.WriteLine(p[i]);
                    }
                }
                if (count > 5)
                    Console.WriteLine("p - Previous");  //Present user with options 
                if (count < p.Count)
                    Console.WriteLine("n - Next");
                Console.WriteLine("\nPress any other key to proceed to add a product");
                Console.WriteLine("--------------------");
                string list_input = Console.ReadLine();
                Console.WriteLine("--------------------");
                if (list_input == "n" && count < p.Count)
                {
                    count += 5;
                }
                else if (list_input == "p" && count > 5)
                {
                    count -= 5;
                }
                else
                {
                    break;
                }
            }   //Paging Finished
        }

        public static void ShoppingCartPaging(ShoppingCart c)   //Allows for navigation through products in ShoppingCart
        {
            bool listing = true;
            int count = 5;
            while (listing) //Begin paging
            {
                if (c.Cart.Count - count > 0)
                {
                    for (var i = count - 5; i < count; ++i)
                    {
                        Console.WriteLine(c.Cart[i]);
                    }
                }
                else
                {
                    for (var i = count - 5; i < c.Cart.Count; ++i)
                    {
                        Console.WriteLine(c.Cart[i]);
                    }
                }
                if (count > 5)
                    Console.WriteLine("p - Previous");
                if (count < c.Cart.Count)
                    Console.WriteLine("n - Next");
                Console.WriteLine("\nPress any other key to proceed to remove a product");
                Console.WriteLine("--------------------");
                string list_input = Console.ReadLine();
                Console.WriteLine("--------------------");
                if (list_input == "n" && count < c.Cart.Count)
                {
                    count += 5;
                }
                else if (list_input == "p" && count > 5)
                {
                    count -= 5;
                }
                else
                {
                    break;
                }
            } //Finished Paging
        }
    }
}
