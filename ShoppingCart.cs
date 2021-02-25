using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class ShoppingCart
    {
        public List<Product> Cart;  //Public list of Product objects to represent Shopping Cart storage

        public ShoppingCart()
        {  
            Cart = new List<Product>(); //Instantiate List upon object creation
        }

        public void Add(Product p)  //Add product to Cart
        {
            Cart.Add(p);
        }

        public void Remove(Product p)   //Remove product from Cart
        {
            Cart.Remove(p);
        }

        public void CheckOut(List<Product> Inventory)  //Prints receipt with subtotal and total
        {
            Console.WriteLine("\n\nReceipt:\n");
            Console.WriteLine("--------------------");
            for (int i = 0; i < Cart.Count; i++)
            {
                if (Cart[i] is ProductByQuantity)
                {
                    ProductByQuantity prod = (ProductByQuantity)Cart[i];
                    Console.WriteLine($"Product: {prod.Name} - Total Price: ${prod.Price} - Unit Price: ${prod.UnitPrice} - Total Units: {prod.Units}");
                }
                else if ((Cart[i] is ProductByWeight))
                {
                    ProductByWeight prod = (ProductByWeight)Cart[i];
                    Console.WriteLine($"Product: {prod.Name} - Total Price: ${prod.Price} - Price Per Ounce: ${prod.PricePerOunce} - Total Ounces: {prod.Ounces}");
                }
            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"Subtotal: ${Helper.GetSubTotal(Cart)}");
            Console.WriteLine("Sales Tax: 7%");
            Console.WriteLine($"Grand Total: ${Helper.GetTotal(Cart)}");
            Console.WriteLine("Thank you and have a nice day!");
            string input = Console.ReadLine();

            //Store contents of receipt into a string
            string receipt_contents = String.Empty;

            receipt_contents += "Receipt:\n";
            receipt_contents += "--------------------\n";
            for (int i = 0; i < Cart.Count; i++)
            {
                if (Cart[i] is ProductByQuantity)
                {
                    ProductByQuantity prod = (ProductByQuantity)Cart[i];
                    receipt_contents += $"Product: {prod.Name} - Total Price: ${prod.Price} - Unit Price: ${prod.UnitPrice} - Total Units: {prod.Units}\n";
                }
                else if ((Cart[i] is ProductByWeight))
                {
                    ProductByWeight prod = (ProductByWeight)Cart[i];
                    receipt_contents += $"Product: {prod.Name} - Total Price: ${prod.Price} - Price Per Ounce: ${prod.PricePerOunce} - Total Ounces: {prod.Ounces}\n";
                }
            }
            receipt_contents += "--------------------\n";
            receipt_contents += $"Subtotal: ${Helper.GetSubTotal(Cart)}\n";
            receipt_contents += "Sales Tax: 7%\n";
            receipt_contents += $"Grand Total: ${Helper.GetTotal(Cart)}\n";
            receipt_contents += "Thank you and have a nice day!";

            //Save contents of receipt to disk
            File.WriteAllText(@"C:\Users\Ryan Ratkovich\source\repos\Assignment2\Assignment2\bin\receipt.txt", receipt_contents);

            //Check for file containing Inventory in \bin
            if (!File.Exists(@"C:\Users\Ryan Ratkovich\source\repos\Assignment2\Assignment2\bin\Inventory.json"))   //If not found, serialize Inventory to JSON and save to disk
            {
                try
                {
                    File.WriteAllText(@"C:\Users\Ryan Ratkovich\source\repos\Assignment2\Assignment2\bin\Inventory.json", JsonConvert.SerializeObject(Inventory));
                } catch (DirectoryNotFoundException)
                {
                    //Fail to write to disk
                } catch (FileNotFoundException)
                {
                    //No file on disk
                }
            }

            Environment.Exit(-1);   //Exit Program
        }
    }
}
