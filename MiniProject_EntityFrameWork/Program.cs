using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.ComponentModel.Design;

namespace MiniProject_EntityFrameWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintDataTime();
            Console.Write("\nEnter your FullName:");
            string fullname = Console.ReadLine();
        Menu:
            Console.Clear();
            PrintDataTime();
            Console.WriteLine("\n\t*Hi "+fullname+"*");
            Console.WriteLine("\n\tMenu:");
            Console.WriteLine("\t1.view Product List");
            Console.WriteLine("\t2.Search Product List");
            Console.WriteLine("\t3.Add Product List");
            Console.WriteLine("\t4.Delete Product List");
            Console.WriteLine("\t5.Edit Product List");
            Console.Write("\n\tPleace select item of menu:");
            int Answer = Convert.ToInt32(Console.ReadLine());
            switch (Answer)
            {
                case 1:
                    Console.Write("\n\tChoose your option to see the list.");
                    Console.Write("\n\tPlease wait...");
                    Thread.Sleep(5000);
                    PrintInfoProduct();
                    Console.Write("\n\nFor come back to menu enter your any key...");
                    Console.ReadKey();
                    goto Menu;

                case 2:

                    Console.Write("\n\tChoose your option to search the list.");
                    Console.Write("\n\tPlease wait...");
                    Thread.Sleep(5000);
                    using (NORTHWNDEntities nORTHWND = new NORTHWNDEntities())
                    {
                        Console.Clear();
                        PrintDataTime();
                        Console.Write("\n\tEnter the desired Product ID:");
                        int ProductNumber = Convert.ToInt32(Console.ReadLine());
                        //var Pro =  nORTHWND.Products.FirstOrDefault(t => t.CategoryID == cateNumber);
                        var Pro = nORTHWND.Products.Find(ProductNumber);
                        if (Pro != null)
                        {
                            Console.Clear();
                            PrintDataTime();
                            Console.WriteLine("\n\tPleace wait...");
                            Thread.Sleep(2000);
                            Console.Clear();
                            PrintDataTime();
                            Console.WriteLine("\n\tProduct information with Product ID" + ProductNumber + ":\n");
                            Console.WriteLine($"\tID:{Pro.ProductID}\tName:{Pro.ProductName}" +
                                              $"\tCategoriesID:{Pro.CategoryID}\tPrice:{Pro.UnitPrice}" +
                                              $"\tStatus:{Pro.Discontinued}\n\t--------------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            Console.Clear();
                            PrintDataTime();
                            Console.WriteLine("\n\tNot Find....");
                        }
                    }
                    Console.Write("\n\n\tFor come back to menu enter your any key...");
                    Console.ReadKey();
                    goto Menu;

                case 3:
                    Console.Write("\n\tChoose your option to add the list.");
                    Console.Write("\n\tPlease wait...");
                    Thread.Sleep(5000);
                    Console.Clear();
                    PrintDataTime();
                    using (NORTHWNDEntities entities = new NORTHWNDEntities())
                    {
                        Console.Write("\tEnter Product Name:");
                        string ProductName = Convert.ToString(Console.ReadLine());
                        Console.Write("\tEnter category ID:");
                        int CategoryID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\tEnter Product Price:");
                        int UnitPrice = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\tEnter Product Discontinued:");
                        bool Discontinued = Convert.ToBoolean(Console.ReadLine());

                        entities.Products.Add(new Products()
                        {
                            ProductName = ProductName,
                            CategoryID = CategoryID,
                            UnitPrice = UnitPrice,
                            Discontinued = Discontinued

                        });
                        entities.SaveChanges();
                        Console.Clear();
                        PrintDataTime();
                        Console.WriteLine("\n\tNew Product added succesfully\n\tPleace wait "+fullname+"....");
                        Thread.Sleep(4000);
                        goto Menu;
                    }

                case 4:

                    Console.Write("\n\tChoose your option to remove the list.");
                    Console.Write("\n\tPlease wait...");
                    Thread.Sleep(5000);
                    using (NORTHWNDEntities nORTHWNDEntities = new NORTHWNDEntities())
                    {
                        PrintInfoProduct();
                        Console.Write("\n\tSelect the product ID for delete:");
                        int answer1 = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        using (NORTHWNDEntities dataBase = new NORTHWNDEntities())
                        {
                            var InfoPro = dataBase.Products.Find(answer1);
                            if (InfoPro != null)
                            {
                                dataBase.Products.Remove(InfoPro);
                                Console.WriteLine($"\n\tThe product with ID {answer1} was seccesfully removed.");
                            }
                            else
                            {
                                Console.Clear();
                                PrintDataTime();
                                Console.WriteLine("\n\tNot Found...");
                            }
                            dataBase.SaveChanges();
                            Thread.Sleep(3000);
                            goto Menu;
                        }
                    }
                case 5:
                    Console.Write("\n\tChoose your option to edit the list.");
                    Console.Write("\n\tPlease wait...");
                    Thread.Sleep(5000);
                    Console.Clear();
                    PrintDataTime();
                    PrintInfoProduct();
                    Console.Write("\n\tSelect the product ID for edit:");
                    int answer = Convert.ToInt32(Console.ReadLine());
                    using (NORTHWNDEntities entities = new NORTHWNDEntities())
                    {
                        var proInfo = entities.Products.Find(answer);
                        
                        Console.Clear();
                        PrintDataTime();
                        Console.Write("\n\tInformation Product with ID:"+answer);
                        Console.WriteLine($"\n\tName:{proInfo.ProductName}\n\tCategoryID:{proInfo.CategoryID}\n\tPrice:{proInfo.UnitPrice}\n\tStatus:{proInfo.Discontinued}\n\t--------------------------------");

                        Console.Write("\tEnter your new name for product:");
                        string newName = Console.ReadLine();
                        proInfo.ProductName = newName;

                        Console.Write("\tEnter your new CategoryID for product:");
                        int NewID = Convert.ToInt32(Console.ReadLine());
                        proInfo.CategoryID = NewID;

                        Console.Write("\tEnter your new price for product:");
                        int NewPrice = Convert.ToInt32(Console.ReadLine());
                        proInfo.UnitPrice = NewPrice;

                        if (proInfo.Discontinued == true)
                            proInfo.Discontinued = false;
                        else
                            proInfo.Discontinued = true;

                        entities.SaveChanges();
                        Console.Clear();
                        PrintDataTime();
                        Console.Write("\n\tInformation new Product:");
                        Console.WriteLine($"\n\tName:{proInfo.ProductName}\n\tCategoryID:{proInfo.CategoryID}\n\tPrice:{proInfo.UnitPrice}\n\tStatus:{proInfo.Discontinued}\n\t--------------------------------");
                    }
                    goto Menu;
            }

            Console.ReadKey();
        }
        public static void PrintDataTime()
        {
            PersianCalendar pc = new PersianCalendar();

            Console.WriteLine
            ($"Date:{pc.GetYear(DateTime.Now)}/{pc.GetMonth(DateTime.Now)}/{pc.GetDayOfMonth(DateTime.Now)}" +
             $"\tHour:{pc.GetHour(DateTime.Now)}:{pc.GetMinute(DateTime.Now)}");
            Console.WriteLine("**************************");
        }

        public static void PrintInfoProduct()
        {
            Console.Clear();
            PrintDataTime();
            using (NORTHWNDEntities database = new NORTHWNDEntities())
            {
                List<Products> products = database.Products.ToList();
                foreach (Products productInfo in products)
                {
                    Console.WriteLine($"ID:{productInfo.ProductID}\tName:{productInfo.ProductName}\t" +
                                      $"CategoriesID:{productInfo.CategoryID}\tPrice:{productInfo.UnitPrice}" +
                                      $"\tStatus:{productInfo.Discontinued}\n--------------------------------------------------------------------------------------------");
                    
                }
            }
        }
    }
}
