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

        static bool InfoExistsInFile(string info, string filePath) // change this from long to string
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
                        if (line.Contains(info)) // or change this from string to long
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
                {writer.WriteLine($"{name}, {phone_number}, {password}, {CreateUserId(name)}, {balance}");}
                
            }
            catch (IOException e)
            {
                Console.WriteLine("An error has occured while trying to write to the file:", e.Message);
            }
        }

        static void ClearConsole()
        {
            Console.Clear();
        }
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

            SaveUserInfoToFile(user_answers[0], user_answers[1], user_answers[2], "0,00");
            User newUser = new User(user_answers[0], user_answers[1], user_answers[2], CreateUserId(user_answers[0]), "0,00");
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

            if (allUsers.Any(user => user.Number == credentials[0]) && allUsers.Any(user => user.Number == credentials[1]))
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Incorrect email or password");
                
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


        static void MainMenu()
        {
            
            Console.WriteLine($" \n {CreateWelcomeText()} \n {ShowUserInfo()} \n Please Select A Service: \n \n 1. Balance \n 2. Deposit  \n 3. Withdraw");

            Console.WriteLine("\n Please select an option: ");
            string menuOption = Console.ReadLine();

            switch (menuOption)
            {
                case "1": Console.WriteLine(); break;

                case "2": Console.WriteLine(); break;

                case "3": Console.WriteLine(); break;
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
            Console.WriteLine("1. Login" + "\n" + "2. Register" + "\n" + "\n" + "Please select an option(1 or 2): ");
            string selectedOption = Console.ReadLine();
            
            // selectedOption == "1" ? Login() : Register(); --> find out why ternary operators do not work for function calling..

            if (selectedOption == "1") { Login(); } else if (selectedOption == "2") { Register(); }
        }

        static void Main(string[] args)
        {
            LoadALlInformation();
            Console.WriteLine(CreateWelcomeText());
            ShowNavigationMenu();
            //MainMenu();
        }
    }
}
