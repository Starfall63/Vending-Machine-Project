using System;
using System.Globalization;
//test from mac
namespace Vending_Machine_Project
{
    class Program
    {
        struct Vending //Creates the structure for the array
        {
            public int code;
            public string item;
            public double price;
            public int stock;
        }
        static void Main(string[] args)
        {
            Vending[] VendingMachine = new Vending[10];//initiates the array 
            Stock(ref VendingMachine);//Adds values into the vending machine array
            Welcome(VendingMachine);//Moves to welcome screen of the vending machine
        }

        static void Welcome(Vending[] VendingMachine)
        {
            Console.Clear();
            Console.WriteLine("Welcome");
            menu(VendingMachine);
            Console.WriteLine("What would you like to do?\n" +
                "1. Choose an item\n" +
                "2. Exit");//To access the admin panel you enter 99 but that is not shown to the regular user for obvious reasons
            int choice = -1;

            while (choice == -1)//Validation for when user chooses what they want to do
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please Enter a valid number.");
                    Welcome(VendingMachine);

                }
            }

            int choice1 = -1;
            switch (choice)
            {
                case 1:
                    ItemChoice(ref choice1, VendingMachine);//User chooses what item they would like to purchase
                    Purchase(choice1, VendingMachine);//User pays for their chosen item
                    break;
                case 2:
                    Console.WriteLine("GoodBye.");
                    Environment.Exit(0);//Ends the program 
                    break;
                case 99:
                    login(VendingMachine);//Goes to the login screen for admin panel
                    break;
                default:
                    Console.WriteLine("Please Enter a valid number.\nPress Any Key to Continue.");
                    Console.ReadKey();
                    Welcome(VendingMachine);
                    break;
            }
        }


        static void ItemChoice(ref int choice, Vending[] VendingMachine)
        {
            //User chooses what they would like to buy

            while ((choice == -1) || choice > 9)//Validation for the user inputs
            {
                Console.Write("Enter the code of the item that you would like: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 9)
                    {
                        Console.WriteLine("Please enter a valid code");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid code.");
                    choice = -1;
                }
            }

            //Checks to make sure that the item is in stock before moving on
            if (VendingMachine[choice].stock == 0)
            {
                Console.WriteLine("We are out of stock please select another item.");
                choice = -1;
                ItemChoice(ref choice, VendingMachine);
            }
        }



        static void Purchase(int choice, Vending[] VendingMachine)
        {
            //User is paying in coins for their chosen item
            Console.Clear();
            double moneyInserted = -1;
            double totalMoneyInserted = 0;
            double price = VendingMachine[choice].price;
            double moneyTP = price * 100;//User will enter coins in pence so converts £ to pence
            Console.WriteLine("You have chosen {0}.\n" +
                "The Price of this item is: {1}", VendingMachine[choice].item, price.ToString("C", CultureInfo.CurrentCulture));

            Console.WriteLine("\nInsert Coins to Pay.\n" +
                "We only accept 10p, 20p, 50p, £1 and £2 coins\n" +
                "To enter £1 or equivalent, enter in pence form.\n" +
                "If you do not have enough to pay for your item enter 0 to exit.");

            while (moneyTP > 0)
            {
                Console.Write("\nInsert Coin:");
                while (moneyInserted == -1)//Validation for user input
                {
                    try
                    {
                        moneyInserted = double.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a valid coin.");
                        moneyInserted = -1;
                    }
                }
                switch (moneyInserted)//Makes sure that user enters a coin that the machine will accept
                {
                    case 10:
                        break;
                    case 20:
                        break;
                    case 50:
                        break;
                    case 100:
                        break;
                    case 200:
                        break;
                    case 0:
                        if (totalMoneyInserted > 0)//Returns money given if user decides that they did not have enough money part way through
                        {
                            Console.Clear();
                            Change(totalMoneyInserted * -1);
                            Console.WriteLine("Press Any Key to Continue");
                            Console.ReadKey();
                        }
                        Welcome(VendingMachine);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid coin.");
                        moneyInserted = 0;
                        break;
                }
                moneyTP -= moneyInserted;
                totalMoneyInserted += moneyInserted;
                moneyInserted = -1;
                if (moneyTP > 0)
                {
                    Console.WriteLine("Amount to pay:{0}", (moneyTP / 100).ToString("C", CultureInfo.CurrentCulture));//Outputs the amount of money needed to pay
                }

            }



            if (moneyTP < 0)
            {
                Change(moneyTP);//If change is needed to be given, it will calculate the change 

            }
            Console.WriteLine("\nThank you for your service.\nPress Any Key to Continue.");
            VendingMachine[choice].stock--;//Removes the stock of the item that was bought
            Console.ReadKey();
            Welcome(VendingMachine);//Goes back to the welcome screen 
        }

        static void Stock(ref Vending[] VendingMachine)
        {
            //assigns the code of each item
            for (int i = 0; i < VendingMachine.Length; i++)
            {
                VendingMachine[i].code = i;
            }
            //assigns the names of each item
            VendingMachine[0].item = "Galaxy";
            VendingMachine[1].item = "Mars";
            VendingMachine[2].item = "Twix";
            VendingMachine[3].item = "KitKat";
            VendingMachine[4].item = "Wispa";
            VendingMachine[5].item = "Coke";
            VendingMachine[6].item = "Fanta";
            VendingMachine[7].item = "Sprite";
            VendingMachine[8].item = "Water";
            VendingMachine[9].item = "Oasis";
            //assigns the price of each item
            VendingMachine[0].price = 0.5;
            VendingMachine[1].price = 0.5;
            VendingMachine[2].price = 0.5;
            VendingMachine[3].price = 0.6;
            VendingMachine[4].price = 1;
            VendingMachine[5].price = 2.5;
            VendingMachine[6].price = 2.5;
            VendingMachine[7].price = 2;
            VendingMachine[8].price = 1.5;
            VendingMachine[9].price = 1.75;
            //assigns the stock of each item
            for (int i = 0; i < VendingMachine.Length; i++)
            {
                VendingMachine[i].stock = 10;
            }

        }

        static void Change(double moneyTP)
        {
            double TotalChange = moneyTP * -1;//calculates total change to be given
            Console.WriteLine("Your Change is:");
            //Gives change in most efficient coin order
            while (TotalChange > 0)
            {
                if (TotalChange >= 200)
                {
                    TotalChange -= 200;
                    Console.WriteLine("£2");
                }
                else if (TotalChange >= 100)
                {
                    TotalChange -= 100;
                    Console.WriteLine("£1");
                }
                else if (TotalChange >= 50)
                {
                    TotalChange -= 50;
                    Console.WriteLine("50p");
                }
                else if (TotalChange >= 20)
                {
                    TotalChange -= 20;
                    Console.WriteLine("20p");
                }
                else if (TotalChange >= 10)
                {
                    TotalChange -= 10;
                    Console.WriteLine("10p");
                }
                else if (TotalChange >= 5)
                {
                    TotalChange -= 5;
                    Console.WriteLine("5p");
                }
            }

        }

        static void login(Vending[] VendingMachine)
        {
            //Login for the admin panel (password is adminPassword)
            string password = "adminPassword";

            int attempts = 3;
            //User gets 3 attempts to enter the correct password before kicking them back to the welcome screen
            while (attempts > 0)
            {

                Console.WriteLine("Enter the Admin Password");
                string input = Console.ReadLine();
                if (input != password)
                {
                    Console.WriteLine("This is an incorrect password.");
                    attempts--;

                }
                else if (input == password)
                {
                    adminPanel(VendingMachine);//Moves user to the admin panel
                }
            }
            Console.WriteLine("You have failed the password too many times.\nPress Any Key to Continue");
            Console.ReadKey();
            Welcome(VendingMachine);

        }

        static void adminPanel(Vending[] VendingMachine)
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine("What would you like to do?\n" +
                "1. Add Stock\n" +
                "2. Change Price\n" +
                "3. Change an item\n" +
                "4. Exit Admin Panel");
            while (choice == 0)//User selects what they would like to do
            {
                try//Validation for user input
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please entera valid code.");
                    choice = 0;
                }
            }
            switch (choice)
            {
                case 1:
                    adminStock(ref VendingMachine);//Moves on to stock
                    break;
                case 2:
                    priceChange(ref VendingMachine);//Moves on to price change
                    break;
                case 3:
                    itemChange(ref VendingMachine);
                    break;
                case 4:
                    Welcome(VendingMachine);//Goes back to the welcome screen
                    break;

                default:
                    break;
            }
        }

        static void adminStock(ref Vending[] VendingMachine)
        {
            Console.Clear();
            menu(VendingMachine);

            
            int choice = -1;
            while (choice == -1)//Validation for user input
            {
                Console.WriteLine("What is the code of the item that you would like to add stock to?");//User selects what item that they would like to add stock to
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid code");
                    choice = -1;
                }
            }

            int stock = 0;
            while (stock == 0)//Validation for user input
            {
                Console.Write("Enter how much stock you would like to add to {0}:", VendingMachine[choice].item);//User enters how much stock they would like to add to existing stock
                try
                {
                    stock = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid value.");
                    stock = 0;
                }
            }
            VendingMachine[choice].stock += stock;//Adds the stock

            string decision = "";
            while (decision == "")
            {
                Console.Write("Would you like to continue adding stock? y/n:");//User decides whether they would like to continue adding stock
                try
                {
                    decision = Console.ReadLine().ToLower();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }

            if (decision == "y")
            {
                adminStock(ref VendingMachine);
            }
            else
            {
                adminPanel(VendingMachine);//Moves back to the admin panel
            }

        }

        static void priceChange(ref Vending[] VendingMachine)
        {
            Console.Clear();
            menu(VendingMachine);

            
            int choice = -1;
            while (choice == -1)//Validation for user input
            {
                Console.Write("What is the code of the item that you would like to change the price of:");//User selects what item they would like to change the price of
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid code");
                    choice = -1;
                }
            }


            double price = 0;

            while (price == 0)
            {
                Console.Write("What would you like to change the price to (in £):");//User selects what the new price of the item will be
                try
                {
                    price = double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid value.");
                    price = 0;
                }
            }


            VendingMachine[choice].price = price;//Changes the price of the item chosen

            string decision = "";
            while (decision == "")
            {
                Console.Write("Would you like to continue changing the prices of items? y/n:");//User chooses if they would like to continue changing the prices
                try//Validation for user input
                {
                    decision = Console.ReadLine().ToLower();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }

            if (decision == "y")
            {
                priceChange(ref VendingMachine);
            }
            else
            {
                adminPanel(VendingMachine);//Goes back to the admin panel
            }

        }

        static void menu(Vending[] VendingMachine)//Displays the items of the vending machine
        {
            Console.WriteLine("Code\tItem\tPrice\tItems Remaining");//Outputs the vending machine items
            for (int i = 0; i < VendingMachine.Length; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", VendingMachine[i].code, VendingMachine[i].item, VendingMachine[i].price.ToString("C", CultureInfo.CurrentCulture), VendingMachine[i].stock);
            }
            //Culture.Info.CurrentCulture converts the double to a currency format
        }


        static void itemChange(ref Vending[] VendingMachine)
        {
            Console.Clear();
            menu(VendingMachine);

            
            int choice = -1;
            while (choice == -1)//Validation for user input
            {
                Console.Write("What is the code of the item that you would like to change:");//User selects what item they would like to change 
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid code");
                    choice = -1;
                }
            }

            string newItem = "";
            while (newItem == "")
            {
                Console.WriteLine("What would you like to change {0} to (name must be between 4 and 6 characters long, you can add spaces if necessary):", VendingMachine[choice].item);
                newItem = Console.ReadLine();
                if ((newItem.Length < 4) ||(newItem.Length > 6))//Makes sure that the new item is between 4 and 6 characters (inclusive) otherwise the formatting of the vending machine will be broken.
                {
                    Console.WriteLine("Name of the new item must be between 4 and 6 characters.");
                    newItem = "";
                }
            }

            VendingMachine[choice].item = newItem;

            string decision = "";
            while (decision == "")
            {
                Console.Write("Would you like to continue changing the items? y/n:");//User chooses if they would like to continue changing the items
                try//Validation for user input
                {
                    decision = Console.ReadLine().ToLower();
                }
                catch
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }

            if (decision == "y")
            {
                itemChange(ref VendingMachine);
            }
            else
            {
                adminPanel(VendingMachine);//Goes back to the admin panel
            }




        }




    }
}
