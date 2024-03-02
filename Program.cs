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
        private int iNumber;
        private string sPassword;
        private int iAccount_number;
        private double dAccount_balance;

        public string name
        {
            get { return sName; }
            set { name = value; }
        }

        public int number
        {
            get { return iNumber; }
            set { number = value; }
        }

        public string password
        {
            get { return sPassword; }
            set { password = value; }
        }

        public int account_number
        {
            get { return iAccount_number; }
            set { account_number = value; }
        }

        public double account_balance
        {
            get { return dAccount_balance; }
            set { account_balance = value; }
        }

        public User(string sName, int iNumber, string sPassword, int iAccountNumber, double dAccount_balance)
        {

            this.name = sName;
            this.number = iNumber;
            this.password = sPassword;
            this.account_number = iAccountNumber;
            this.account_balance = dAccount_balance = 0;
        }

    }

    /// <summary>
    /// we ned to save information to the file so that we can be able to read and write information to theh files
    /// </summary>

    internal class Program
    {

        static string userInfoFilePath = @"C:\Users\Ismail\Desktop\C#\Banking Sytem\UserInfo.txt", userIdFilePath2 =
            @"C:\Users\Ismail\Desktop\C#\Banking Sytem\userId.txt";

        static bool InfoExistsInFile(string info, string filePath)
        {
            bool exists = false;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine();
                    bool found = false;

                    while (line != null)
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
            }
            return exists ? true : false;
        }

        static long GenerateRandomNumber(int length)
        {
            Random random = new Random();
            long randomNumber = (long)(random.NextDouble() * 9000000000) + 1000000000;
            return randomNumber;
        }

        static string CreateUserId(string name)
        {

            string ID = InfoExistsInFile(GenerateRandomNumber(10) + name, userInfoFilePath) ? GenerateRandomNumber(10) + name : GenerateRandomNumber(10) + name;
            return ID;
        }

        static void SaveUserInfoToFile(string name, string phone_number, string password)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(userInfoFilePath, true))
                {
                    writer.WriteLine($"{name}, {phone_number}, {password}, {CreateUserId(name)}");
                }
                Console.WriteLine("data has been written to the file");

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

            SaveUserInfoToFile(user_answers[0], user_answers[1], user_answers[2]);

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
        }

        static string CreateWelcomeText()
        {
            string border = "#=======================================#",
                mainWelcomeText = " The UFS Liberty Bank  ---> By Ismail";

            return border + "\n" + mainWelcomeText + "\n" + border + "\n";
        }


        static void Test()
        {

            for (int i = 0; i < 6; i++)
            {
                for (int k = 0; k < 6 - i - 1; k++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

        }

        static void ShowNavigationMenu()
        {

            Console.WriteLine("1. Login" + "\n" + "2. Register" + "\n");

            Console.Write("Please select an option(1 or 2): ");

            string selectedOption = Console.ReadLine();

            // selectedOption == "1" ? Login() : Register(); --> find out why ternary operators do not work for these..

            if (selectedOption == "1") { Login(); } else if (selectedOption == "2") { Register(); }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine(CreateWelcomeText());

            //ShowNavigationMenu();

            Test();

        }
    }
}
