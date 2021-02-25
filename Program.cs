using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Program
    {
        private static List<Product> Inventory = new List<Product>();           //Contains all products
        private static ShoppingCart ShoppingCart = new ShoppingCart();          //Stores products that user is purchasing

        static void Main(string[] args)
        {
            //Check for file containing Inventory in /bin
            if(File.Exists(@"C:\Users\Ryan Ratkovich\source\repos\Assignment2\Assignment2\bin\Inventory.json")) //If found, deserialize JSON into Inventory variable
                Inventory = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"C:\Users\Ryan Ratkovich\source\repos\Assignment2\Assignment2\bin\Inventory.json"));
            else
            {
                //Add 10 unique ProductByQuantity objects to Inventory
                Inventory.Add(new ProductByQuantity(0.25, "a", "A", 0, 0.25, 1));
                Inventory.Add(new ProductByQuantity(0.50, "b", "B", 1, 0.5, 1));
                Inventory.Add(new ProductByQuantity(1.00, "c", "C", 2, 1, 1));
                Inventory.Add(new ProductByQuantity(1.50, "d", "D", 3, 1.5, 1));
                Inventory.Add(new ProductByQuantity(2.00, "e", "E", 4, 2, 1));
                Inventory.Add(new ProductByQuantity(2.50, "f", "F", 5, 2.5, 1));
                Inventory.Add(new ProductByQuantity(3.00, "g", "G", 6, 3, 1));
                Inventory.Add(new ProductByQuantity(3.50, "h", "H", 7, 3.5, 1));
                Inventory.Add(new ProductByQuantity(4.00, "i", "I", 8, 4, 1));
                Inventory.Add(new ProductByQuantity(4.50, "j", "J", 9, 4.5, 1));

                //Add 10 unique ProductByWeight objects to Inventory
                Inventory.Add(new ProductByWeight(5.00, "k", "K", 10, 5.00, 1));
                Inventory.Add(new ProductByWeight(5.50, "l", "L", 11, 5.50, 1));
                Inventory.Add(new ProductByWeight(6.00, "m", "M", 12, 6.00, 1));
                Inventory.Add(new ProductByWeight(6.50, "n", "N", 13, 6.50, 1));
                Inventory.Add(new ProductByWeight(7.00, "o", "O", 14, 7.00, 1));
                Inventory.Add(new ProductByWeight(7.50, "p", "P", 15, 7.50, 1));
                Inventory.Add(new ProductByWeight(8.00, "q", "Q", 16, 8.00, 1));
                Inventory.Add(new ProductByWeight(8.50, "r", "R", 17, 8.50, 1));
                Inventory.Add(new ProductByWeight(9.00, "s", "S", 18, 9.00, 1));
                Inventory.Add(new ProductByWeight(9.50, "t", "T", 19, 9.50, 1));
            }

            Console.WriteLine("Welcome to Assignment 2 by Ryan Ratkovich");
            Helper.PrintMenu(); //Display Menu
            string input = Console.ReadLine();  //Get user input
            while (input != "4")    //While user has not chosen to exit the program
            {
                switch (input)
                {
                    case "1":   //Print the Inventory
                        Navigation.InventoryPaging(Inventory);  // Begin Inventory Paging
                        Console.WriteLine("Select a product (by ID) to add to the Shopping Cart. Enter -1 if you do not want to add any products.");
                        input = Console.ReadLine(); //Get user input
                        int prodId = Convert.ToInt32(input);   //Convert to int
                        if (prodId == -1)
                        {
                            Helper.PrintMenu();
                            break;
                        }
                        if (prodId < 0 || prodId >= Inventory.Count)   //Bound check
                        {
                            Console.WriteLine("Invalid ID.");
                            Helper.PrintMenu();
                            break;
                        }
                        if (Inventory.FirstOrDefault(i => i.Id == prodId) is ProductByQuantity) //Using LINQ, check if the Product is a ProductByQuantity object by ID
                        {
                            if (ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId) != null)  //If the product is already in the users shopping cart, update it
                            {
                                Console.WriteLine("How many of this product would you like to purchase?");
                                input = Console.ReadLine();
                                int units = Convert.ToInt32(input);   //Convert to int
                                if (units <= 0)
                                {
                                    Console.WriteLine("Invalid number.");
                                    break;
                                }
                                for (int i = 0; i < ShoppingCart.Cart.Count; i++)
                                {
                                    if (ShoppingCart.Cart[i] is ProductByQuantity)
                                    {
                                        ProductByQuantity prod = (ProductByQuantity)ShoppingCart.Cart[i];   //Store Cart[i] into a ProductByQuantity object to get access to Units property
                                        if (prod.Id == prodId) //Find the product
                                        {
                                            prod.Units += units;   //Add specified amount of units of the product to the Shopping Cart
                                            Console.WriteLine($"Added {units} units of Product to Shopping Cart.");
                                            break;
                                        }
                                    }
                                }
                            }
                            //If the product is not yet in the users shopping cart, add it
                            else
                            {
                                Console.WriteLine("How many of this product would you like to purchase?");
                                input = Console.ReadLine();
                                int units = Convert.ToInt32(input);   //Convert to int
                                if (units <= 0)
                                {
                                    Console.WriteLine("Invalid number.");
                                    break;
                                }
                                var temp = (ProductByQuantity)Inventory.FirstOrDefault(i => i.Id == prodId);
                                ProductByQuantity prod = new ProductByQuantity(temp.Price, temp.Name, temp.Description, temp.Id, temp.UnitPrice, units);
                                ShoppingCart.Add(prod);    //Add the amount of the product to the Shopping Cart
                            }
                        }
                        else if (Inventory.FirstOrDefault(i => i.Id == prodId) is ProductByWeight)  //Check if the Product is a ProductByWeight object
                        {
                            if (ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId) != null)  //If the product is already in the users shopping cart, update it
                            {
                                Console.WriteLine("How many ounces would you like to purchase?");
                                input = Console.ReadLine();
                                double ounces = Convert.ToDouble(input);   //Convert to int
                                if (ounces <= 0)
                                {
                                    Console.WriteLine("Invalid number.");
                                    break;
                                }
                                for (int i = 0; i < ShoppingCart.Cart.Count; i++)
                                {
                                    if (ShoppingCart.Cart[i] is ProductByWeight)
                                    {
                                        ProductByWeight prod = (ProductByWeight)ShoppingCart.Cart[i];   //Store Cart[i] into a ProductByWeight object to get access to Ounces property
                                        if (prod.Id == prodId) //Find the product
                                        {
                                            prod.Ounces += ounces;   //Add specified amount of ounces of the product to the Shopping Cart
                                            Console.WriteLine($"Added {ounces} ounces of Product to Shopping Cart.");
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            //If the product is not yet in the users shopping cart, add it
                            {
                                Console.WriteLine("How many ounces would you like to purchase?");
                                input = Console.ReadLine();
                                double ounces = Convert.ToDouble(input);    //Convert to double
                                if (ounces <= 0)
                                {
                                    Console.WriteLine("Invalid number.");
                                    break;
                                }
                                var temp = (ProductByWeight)Inventory.FirstOrDefault(i => i.Id == prodId);
                                ProductByWeight prod = new ProductByWeight(temp.Price, temp.Name, temp.Description, temp.Id, temp.PricePerOunce, ounces);
                                ShoppingCart.Add(prod);    // Add the amount of the product to the Shopping Cart
                            }
                        }
                        Helper.PrintMenu();
                        break;
                    case "2":   //Print the contents of the Shopping Cart
                        if (ShoppingCart.Cart.Count == 0)   //If Shopping Cart is empty, print message.
                        {
                            Console.WriteLine("Shopping Cart is empty.");
                            Helper.PrintMenu();
                            break;
                        }
                        Console.WriteLine("\n--------------------");
                        Console.WriteLine("Shopping Cart:");
                        Navigation.ShoppingCartPaging(ShoppingCart);    //Begin Paging
                        Console.WriteLine("Select a product (by ID) to remove from the Shopping Cart. Enter -1 if you would not like to remove any products.");
                        input = Console.ReadLine();
                        prodId = Convert.ToInt32(input);    //Store the ID
                        if (prodId == -1)   //Go back to menu if user enters -1
                        {
                            Helper.PrintMenu();
                            break;
                        }
                        if (prodId < 0 || prodId >= Inventory.Count)   //Bound check
                        {
                            Console.WriteLine("Invalid ID.");
                            Helper.PrintMenu();
                            break;
                        }
                        if (ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId) is ProductByQuantity)     //If the product is a ProductByQuantity object
                        {
                            Console.WriteLine("How many of this product would you like to remove from the Shopping Cart?");
                            input = Console.ReadLine();
                            int units = Convert.ToInt32(input);   //Convert to int
                            var temp = (ProductByQuantity)ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId);
                            if (units >= temp.Units)    //If user attemps to remove all (or more) of the product
                            {
                                foreach (var p in ShoppingCart.Cart)
                                {
                                    if (p.Id == prodId) //Find the product
                                    {
                                        ShoppingCart.Remove(p);    //Remove the product from the Shopping Cart
                                        Console.WriteLine("Removed Product from Shopping Cart.");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ShoppingCart.Cart.Count; i++)
                                {
                                    if (ShoppingCart.Cart[i] is ProductByQuantity)
                                    {
                                        ProductByQuantity prod = (ProductByQuantity)ShoppingCart.Cart[i];
                                        if (prod.Id == prodId) //Find the product
                                        {
                                            prod.Units -= units;   //Remove specified amount of units of the product from the Shopping Cart
                                            Console.WriteLine($"Removed {units} units of Product from Shopping Cart.");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId) is ProductByWeight)     //If the product is a ProductByWeight object
                        {
                            Console.WriteLine("How many ounces would you like to remove from the Shopping Cart?");
                            input = Console.ReadLine();
                            double ounces = Convert.ToDouble(input);    //Convert to double
                            var temp = (ProductByWeight)ShoppingCart.Cart.FirstOrDefault(i => i.Id == prodId);
                            if (ounces >= temp.Ounces)    //If user attemps to remove all (or more) of the product
                            {
                                foreach (var p in ShoppingCart.Cart)
                                {
                                    if (p.Id == prodId) //Find the product
                                    {
                                        ShoppingCart.Remove(p);    //Remove the product from the Shopping Cart
                                        Console.WriteLine("Removed Product from Shopping Cart.");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for(int i = 0; i < ShoppingCart.Cart.Count; i++)
                                {
                                    if (ShoppingCart.Cart[i] is ProductByWeight)
                                    {
                                        ProductByWeight prod = (ProductByWeight)ShoppingCart.Cart[i];
                                        if (prod.Id == prodId) //Find the product
                                        {
                                            prod.Ounces -= ounces;   //Remove specified amount of ounces of the product from the Shopping Cart
                                            Console.WriteLine($"Removed {ounces} ounces of Product from Shopping Cart.");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        Helper.PrintMenu();
                        break;
                    case "3":   //Finish shopping and print the receipt
                        ShoppingCart.CheckOut(Inventory);
                        break;
                    case "4":   //Exit program
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Enter a number [1-4].\n"); //Print error if invalid input
                        Helper.PrintMenu();
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}