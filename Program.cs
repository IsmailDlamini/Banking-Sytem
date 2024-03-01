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

        public string name { 
            get { return sName;}
            set { name = value;}
        }

        public int number
        {
            get { return iNumber; }
            set {number = value;}
        }

        public string password
        {
            get { return sPassword; }
            set{ password = value;}
        }

        public int account_number
        {
            get { return iAccount_number; }
            set{ account_number = value;}
        }

        public double account_balance
        {
            get { return dAccount_balance; }
            set { account_balance = value; }
        } 

        public User(string sName, int iNumber, string sPassword, int iAccountNumber, double dAccount_balance) {
                
            this.name = sName;
            this.number = iNumber;
            this.password = sPassword;
            this.account_number = iAccountNumber;
            this.account_balance = dAccount_balance = 0;
        }

    }

    internal class Program
    {


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

            for(int i = 0; i < questions.Length; i++)
            {
                Console.Write(questions[i]);
                user_answers.Add(Console.ReadLine());
                Console.WriteLine();
            }
            
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

            return border + "\n" +  mainWelcomeText + "\n" + border + "\n";
        }

        static void ShowNavigationMenu()
        {

            Console.WriteLine("1. Login" + "\n" + "2. Register" + "\n");

            Console.Write("Please select an option(1 or 2): ");
            
            string selectedOption = Console.ReadLine();

           // selectedOption == "1" ? Login() : Register(); --> find out why ternary operators do not work for these..

            if(selectedOption == "1") { Login();} else if(selectedOption == "2"){Register();}
        }
            
        static void Main(string[] args)
        {
            Console.WriteLine(CreateWelcomeText());
            
            ShowNavigationMenu();
              
        }
    }
}
