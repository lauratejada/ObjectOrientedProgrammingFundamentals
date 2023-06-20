using OOP_Lab03.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

try
{
    Console.WriteLine();
    VendingMachine newVendingMachine = new VendingMachine("0100");

    Product product1 = new Product("CHIPS", 2, 101);
    Product product2 = new Product("CHOCOLATE", 3, 102);
    Product product3 = new Product("CHEESE STICKS", 4, 103);

    // inventory of coins
    Console.WriteLine(newVendingMachine.StockFloat(1, 10));
    Console.WriteLine(newVendingMachine.StockFloat(2, 5));
    //Console.WriteLine(newVendingMachine.StockFloat(3, 5));
    Console.WriteLine(newVendingMachine.StockFloat(5, 5));
    Console.WriteLine(newVendingMachine.StockFloat(10, 5));
    Console.WriteLine(newVendingMachine.StockFloat(20, 5));

    // inventory of products
    Console.WriteLine(newVendingMachine.StockItem(product1, 10));
    Console.WriteLine(newVendingMachine.StockItem(product2, 5));
    Console.WriteLine(newVendingMachine.StockItem(product3, 10));
    Console.WriteLine(newVendingMachine.StockItem(product2, 2));

    //Console.WriteLine($"Serial Number: {newVendingMachine.SerialNumber}");
    //VendingMachine newVendingMachine2 = new VendingMachine("02000");
   // Console.WriteLine($"Serial Number: {newVendingMachine2.SerialNumber}");
    
    Console.Clear();

    Console.WriteLine("-------- WELCOME TO THE VENDING MACHINE SYSTEM V.2 -----------");
    Console.WriteLine();
    // purchase process start in vending machine
    List<int> userMoneyAmountList = new List<int>();
    bool validMoneyInput = false; 

    //STEP 1: Ask for amout and validate input
    // use while to check the type of input and ask again for input type
    validMoneyInput = false;
    int userMoneyAmount = 0;
    while (!validMoneyInput)
    {
        Console.Write(">> Please enter coin ($1, $2, $5, $10, $20): $");
        string userInputOfMoney = Console.ReadLine();
        //try to parse the user input to an int variable
        validMoneyInput = Int32.TryParse(userInputOfMoney, out userMoneyAmount);
        if (validMoneyInput) 
        {
            if (userMoneyAmount == 1 ||
                userMoneyAmount == 2 ||
                userMoneyAmount == 5 ||
                userMoneyAmount == 10 ||
                userMoneyAmount == 20)
            {
                userMoneyAmountList.Add(userMoneyAmount);
                break; // break this while and continue when amount >0 and valid coin
            }
            else
            {
                validMoneyInput = false;
                // iterate inside this while until amout > 0 or valid coin
            }
        }
    }
    ////ask for insert another coin
    string continueAskingForCoins = "";
    while (continueAskingForCoins != "Y" || continueAskingForCoins != "N")
    {
        Console.Write("Do you want to insert another coin? (Y/N): ");
        continueAskingForCoins = Console.ReadLine().ToUpper();
        if (continueAskingForCoins == "Y")
        {
            validMoneyInput = false;
            userMoneyAmount = 0;
            while (!validMoneyInput)
            {
                Console.Write(">> Please enter coin ($1, $2, $5, $10, $20): $");
                string userInputOfMoney = Console.ReadLine();
                //try to parse the user input to an int variable
                validMoneyInput = Int32.TryParse(userInputOfMoney, out userMoneyAmount);
                if (validMoneyInput)
                {
                    if (userMoneyAmount == 1 ||
                        userMoneyAmount == 2 ||
                        userMoneyAmount == 5 ||
                        userMoneyAmount == 10 ||
                        userMoneyAmount == 20)
                    {
                        userMoneyAmountList.Add(userMoneyAmount);
                        break; // break this while and continue when amount >0 and valid coin
                    }
                    else
                    {
                        validMoneyInput = false;
                        // iterate inside this while until amout > 0 or valid coin
                    }
                }
            }
        }
        else if (continueAskingForCoins == "N")
        {
            break; // continue outside the loop
        }
    }
    // STEP 2: Ask for product code
    Console.WriteLine();

    // STEP 2: Ask for an item with a list of options and validate input
    Console.WriteLine("List of products");
    Console.WriteLine(" Code\tName\tPrice");
    Console.WriteLine(" ---- \t-----\t-----");
    foreach (KeyValuePair<Product, int> item in newVendingMachine.Inventory)
    {
        Console.WriteLine($" {item.Key.Code}\t{item.Key.Name}\t{item.Key.Price}");
    }
    Console.WriteLine();
    bool validItemEntryCode = false;
    int userItemCode = 0;
    while (!validItemEntryCode)
    {
        Console.Write(">> Enter item code: ");
        string userItemInput = Console.ReadLine().ToUpper().Trim();
        //try to parse the user input to an int variable
        validItemEntryCode = Int32.TryParse(userItemInput, out userItemCode);
        if (validItemEntryCode) // 
        {
            if (userItemCode > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Procesing your purchase...");
                Console.WriteLine(newVendingMachine.VendItem(userItemCode.ToString(), userMoneyAmountList));
                break;
            }
            else
            {
                validItemEntryCode = false;
            }
        }
        else
        {
            Console.WriteLine($"Sorry, the code is invalid. Try again.");
        }
    }
    Console.WriteLine("Thanks for using vending machine system");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}