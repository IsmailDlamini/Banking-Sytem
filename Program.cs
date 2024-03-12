using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Banking_Sytem
{
    public class User
    {
        private string sName;
        private string sNumber;
        private string sPassword;
        private string sAccount_number;
        private string sAccount_balance;

        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

        public string Number
        {
            get { return sNumber; }
            set { sNumber = value; }
        }

        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        public string AccountNumber
        {
            get { return sAccount_number; }
            set { sAccount_number = value; }
        }

        public string AccountBalance
        {
            get { return sAccount_balance; }
            set { sAccount_balance = value; }
        }

        public User(string sName, string sNumber, string sPassword, string sAccountNumber, string sAccount_balance)
        {
            this.Name = sName;
            this.Number = sNumber;
            this.Password = sPassword; 
            this.AccountNumber = sAccountNumber;
            this.AccountBalance = sAccount_balance;
        }
    }

    internal class Program
    {

         static string userInfoFilePath = @"C:\Users\Ismail\Desktop\C#\Banking Sytem\UserInfo.txt", userIdFilePath2 =
            @"C:\Users\Ismail\Desktop\C#\Banking Sytem\userId.txt";

         static List<User> currentUser = new List<User>();
         static List<User> allUsers = new List<User>();

        static bool InfoExistsInFile(string info, string filePath) 
        {
            bool exists = false;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {

                    string line;
                    bool found = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(info))
                        {
                            found = true;
                            break;
                        }
                    }
                    exists = found ? true : false;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("There was an error reading the information that is in the file:", e);
            }
            return exists ? true : false;
        }

        static string GenerateRandomNumber()
        {
            Random random = new Random();
            long randomNumber = (long)(random.NextDouble() * 9000000000) + 1000000000;
            return randomNumber.ToString();
        }

        static string CreateUserId(string name)
        {

            string ID = InfoExistsInFile(GenerateRandomNumber(), userInfoFilePath) ? GenerateRandomNumber(): GenerateRandomNumber();
            return ID;
        }

        static void SaveUserInfoToFile(string name, string phone_number, string password, string balance)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(userInfoFilePath, true))
                {writer.WriteLine($"\n{name},{phone_number},{password},{CreateUserId(name)},{balance}");}
                
            }
            catch (IOException e)
            {
                Console.WriteLine("An error has occured while trying to write to the file:", e.Message);
            }
        }

        static void ClearConsole(){Console.Clear();}

        static void Register()
        {
            ClearConsole();
            Console.WriteLine(CreateWelcomeText() + "\n" + "Welcome to the Registration page" + "\n");

            string[] questions = { "please enter your Name: ", "Please enter your phone number: ",
                "Please create a password: " };
            string[] answers = new string[3];

            List<string> user_answers = new List<string>();

            for (int i = 0; i < questions.Length; i++)
            {
                Console.Write(questions[i]);
                user_answers.Add(Console.ReadLine());
                Console.WriteLine();
            }

            SaveUserInfoToFile(user_answers[0], user_answers[1], user_answers[2], "0");
            User newUser = new User(user_answers[0], user_answers[1], user_answers[2], CreateUserId(user_answers[0]), "0");
            currentUser.Add(newUser);
            ClearConsole();
            MainMenu();
        }

        static void Login()
        {
            ClearConsole();
            Console.WriteLine(CreateWelcomeText() + "\n" + "Welcome to the Login page" + "\n");

            string[] prompts = { "Please enter your phone number: ", "Please enter your password: " };
            List<string> credentials = new List<string>();
            for (int i = 0; i < prompts.Length; i++)
            {
                Console.Write(prompts[i]);
                credentials.Add(Console.ReadLine());
            }

            if (allUsers.Any(user => user.Number == credentials[0]) && allUsers.Any(user => user.Password == credentials[1]))
            {
                User loggedInUser = allUsers.FirstOrDefault(user => user.Number == credentials[0]);
                currentUser.Add(loggedInUser);
                ClearConsole();
                MainMenu();
            }
            else
            {
                Console.WriteLine("Incorrect email or password");
                ClearConsole();
                Login();
            }
        }

        static string ShowUserInfo()
        {
            return $"Currently Logged in As: {currentUser[0].Name} \n Current Balance: R{currentUser[0].AccountBalance} \n";
        }

        static string CreateWelcomeText()
        {
            string border = "#=======================================#",
                mainWelcomeText = " The UFS Liberty Bank  ---> By Ismail";

            return border + "\n" + mainWelcomeText + "\n" + border + "\n";
        }


        static void LoadALlInformation()
        {
            try { 
                using(StreamReader reader = new StreamReader(userInfoFilePath))
                {
                    string line;

                    while((line = reader.ReadLine())  != null)
                    {

                        string[] separatedString = line.Split(',');
                        User obj1 = new User(separatedString[0], separatedString[1], separatedString[2], separatedString[3], separatedString[4]);
                        if (obj1 != null)
                        {
                            allUsers.Add(obj1 );
                        }
                    }       
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("There was an error reading the information from the file:", e);
            }

            foreach (User user in allUsers)
            {
               // Console.WriteLine($"Name: {user.Name}, Number: {user.Number}, Password: {user.Password}, AccountNumber: {user.AccountNumber}, Balance: {user.AccountBalance}");
            }
        }
        static void UpdateUserInformation(string searched, string information, string toChange)
        {
            string[] lines = File.ReadAllLines(userInfoFilePath);

            try
            {
                using (StreamReader reader = new StreamReader(userInfoFilePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
       
                        if (line.Contains(searched))
                        {
                            string[] userInformation = lines[lineNumber].Split(',');

                            string userName = toChange != "userName" ? userInformation[0] : information;
                            string phoneNumber = toChange != "phoneNumber" ? userInformation[1] : information;
                            string password = toChange != "password" ? userInformation[2] : information;
                            string balance = toChange != "balance" ? userInformation[4] : information;

                            string newInformation = $"{userName},{phoneNumber},{password},{currentUser[0].AccountNumber},{balance}";

                            lines[lineNumber] = newInformation;

                        }

                        lineNumber += 1;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("an error has occured trying to open the file {0}", e);
            }
            File.WriteAllLines(userInfoFilePath, lines);
        }

        static void cashProcess(string processType)
        {
            CreateWelcomeText();
            ClearConsole();
            CreateWelcomeText();

            Console.Write($"Please enter the amount of money you want to {processType}: ");
            decimal moneyToProcess = decimal.Parse(Console.ReadLine());

            decimal currentAmount = decimal.Parse(currentUser[0].AccountBalance);

            decimal newAmount = processType  == "Deposit" ? currentAmount + moneyToProcess : currentAmount - moneyToProcess;

            if(newAmount > 0)
            {
                currentUser[0].AccountBalance = newAmount.ToString("0.00");
                string addAmount = currentUser[0].AccountBalance = newAmount.ToString("0");
                UpdateUserInformation(currentUser[0].Number,addAmount, "balance");
                Prompts("Cash Process was successful" + "\n" + "Press any key to go back to the main menu");
                MainMenu();
            }
            else
            {
                Prompts("You do not have enough funds to complete the transaction:" + "\n" + "Press any key to go back to the main menu");
                MainMenu();
            }
        
        }

        static void Prompts(string prompt)
        {
            ClearConsole();
            Console.WriteLine(prompt);
            Console.ReadKey();
            ClearConsole();
        }

        static void TransferCash()
        {
            CreateWelcomeText();
            ClearConsole();
            CreateWelcomeText();

            string[] transferProcesses = { "Please enter the phone number of the receiver: ", "How much do you want to send: " };
            List<string> responses = new List<string>();

            foreach( string process in transferProcesses )
            {
                Console.Write(process);
                responses.Add(Console.ReadLine());
            }

            if( responses.Count > 0 )
            {
                if (InfoExistsInFile(responses[0], userInfoFilePath))
                {
                    User intendedReceiver = allUsers.FirstOrDefault(user => user.Number == responses[0]);

                    Console.Write($"\n are you sure you want to send R{responses[1]} to {intendedReceiver.Name}? (yes/no)");

                    string choice = Console.ReadLine();

                    if (choice != null)
                    {
                        if(choice == "yes")
                        {
                            if (int.Parse(currentUser[0].AccountBalance) - int.Parse(responses[1]) > 0)
                            {
                                int newReceiverAmount = int.Parse(intendedReceiver.AccountBalance) + int.Parse(responses[1]);
                                int newSenderAmount = int.Parse(currentUser[0].AccountBalance) - int.Parse(responses[1]);

                                UpdateUserInformation(intendedReceiver.Number, newReceiverAmount.ToString(), "balance");
                                UpdateUserInformation(currentUser[0].Number, newSenderAmount.ToString(), "balance");
                                LoadALlInformation();

                                Prompts($"Success: You have sent R{responses[1]} to {intendedReceiver.Name} \n  press any key to go back to the main menu");
                                MainMenu();
                            }
                            else
                            {
                                Prompts("Error 500: You do not have enough money in your account \n  press any key to go back to the main menu");
                                MainMenu();
                            }
                        }
                        else
                        {
                            Prompts("You have chosen not to send the cash \npress any key to go back to the main menu");
                            MainMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid responce");
                    }
                }
                else
                {
                    Prompts("Error 404: There is no user with the number you provided \n  press any key to re-enter the phone number");
                    TransferCash();
                }
            }
            else
            {
                Prompts("Error 500: Please provide The required Values \n  press any key to re-enter the values");
                TransferCash();
            }

        }

        static void MainMenu()
        {
            
            Console.WriteLine($" \n {CreateWelcomeText()} \n {ShowUserInfo()} \n Please Select A Service: \n \n 1. Deposit \n 2. Withdraw  \n 3. Transfer");

            Console.WriteLine("\n Please select an option: ");
            string menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1":
                    cashProcess("Deposit");
                    break;

                case "2":
                    cashProcess("Widthdraw");
                    break;

                case "3":
                    TransferCash();
                    break;
            }
        }

        static void Test()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int k = 0; k < 6 - i - 1; k++) Console.Write(" ");
                for (int j = 0; j < 2 * i + 1; j++) Console.Write("*");
                Console.WriteLine();
            }
        }

        static void ShowNavigationMenu()
        {
            try
            {
                Console.WriteLine("1. Login" + "\n" + "2. Register" + "\n" + "\n" + "Please select an option(1 or 2): ");
                string selectedOption = Console.ReadLine();

                if (selectedOption == "1") { Login(); } else if (selectedOption == "2") { Register(); }
            }
            catch () // you need to find the correct data type that you are going to be using for this exception
            {
                Prompts("an error has occured!!!! Please check inputs \nPress any key to go back to the main menu");
            }
            
        }

        static void Main(string[] args)
        {
            LoadALlInformation();
            Console.WriteLine(CreateWelcomeText());
            ShowNavigationMenu();
            MainMenu();

        }
    }
}
