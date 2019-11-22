using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace dotnet_donet_shop
{
    class Inventory
    {
        public List<string> Toppings { get; set; }
        public List<string> Flavors { get; set; }

        public Inventory(List<string> toppings, List<string> flavors)
        {
            Toppings = toppings;
            Flavors = flavors;

        }


    }

    class Program
    {
        static Random random = new Random();

        static Inventory loadMenu()
        {
            string donutFiles = File.ReadAllText(@"./donuts.json");
            return JsonConvert.DeserializeObject<Inventory>(donutFiles);
        }

        static string getTopping(Inventory donutShop)
        {
            foreach (var topping in donutShop.Toppings)
            {
                Console.WriteLine(topping);
            }
            while (true)
            {
                Console.Write("Do you want to pick a topping [pick] or have a random topping [rand] : ");
                string donutChoice = Console.ReadLine();
                if (donutChoice == "rand")
                {
                    int index = random.Next(donutShop.Toppings.Count);
                    return donutShop.Toppings[index];

                }
                else if (donutChoice == "pick")
                {
                    Console.Write("What toppings would you like?: ");
                    string choice = Console.ReadLine();
                    foreach (var topping in donutShop.Toppings)
                    {
                        if (topping == choice)
                        {
                            return topping;
                        }
                    }
                }
            }
        }

        static string getFlavor(Inventory donutShop)
        {
            foreach (var flavor in donutShop.Flavors)
            {
                Console.WriteLine(flavor);
            }
            while (true)
            {
                Console.Write("Do you want to pick a flavor [pick] or have a random flavor [rand] : ");
                string donutChoice = Console.ReadLine();
                if (donutChoice == "rand")
                {
                    foreach (var flavor in donutShop.Flavors)
                    {
                        int index = random.Next(donutShop.Flavors.Count);
                        return donutShop.Flavors[index];
                    }
                }
                Console.Write(">>> ");
                string choice = Console.ReadLine();
                foreach (var flavor in donutShop.Flavors)
                {
                    if (flavor == choice)
                    {
                        return flavor;
                    }
                }
            }
        }

        static void WriteTransaction(string name, string toppings, string flavors, decimal money, decimal cashOut)
        {
            File.AppendAllText("./transactions.txt", $"\n{name}, {toppings}, {flavors}, {money}, {cashOut}");
        }

        static void Main(string[] args)
        {
            decimal donutPrice = 0.75M;
            Console.Write("What's your name: ");
            string name = Console.ReadLine();
            Inventory donutShop = loadMenu();
            string topping = getTopping(donutShop);
            Console.WriteLine($"Since you've picked a donut with {topping} as the topping what flavor would you like?");
            string flavor = getFlavor(donutShop);
            Console.Write("How much would you like to pay?: ");
            decimal money = Convert.ToDecimal(Console.ReadLine());
            decimal cashOut = donutPrice - money;
            Console.WriteLine($"{name} has ordered a {flavor} flavored donut topped with {topping} for ${money}");
            WriteTransaction(name, topping, flavor, money, cashOut);
        }
    }
}
