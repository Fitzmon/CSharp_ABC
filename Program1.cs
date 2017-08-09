using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deposit
{
    /* 人机交互部分 */
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("First, I'll build a new bank.\nHow many accounts do you like to build: ");
            int maxNum = int.Parse(Console.ReadLine());
            if (maxNum == 0)
            {
                Console.WriteLine("ERROR! The number is INVALID.");
                Console.ReadKey();
            }
            else
            {
                Bank aBank = new Bank("BANK OF CHINA", "95566", maxNum);
                Console.WriteLine("Welcome to BOC.");
                bool bExit = true;
                while (bExit)
                {
                    Console.WriteLine("");
                    Console.WriteLine(@"
---Customer Service---
1 Add Account
2 Save Money
3 Withdraw Money
4 Transfer Money
5 Show Balance
6 Modify Password
7 Loss Registration
8 Delete Account
0 Exit
----------------------");
                    Console.Write("Please select one business: ");
                    string seriveNum = Console.ReadLine();
                    switch (seriveNum)
                    {
                        case "1":
                            Console.Write("Please input your NAME: ");
                            string newName = Console.ReadLine();
                            Console.Write("Please input your ID: ");
                            string newID = Console.ReadLine();
                            aBank.AddAccount(new Person(newName, newID));
                            //Person zhang = new Person("zhangsan", "123456789012345");
                            //aBank.AddAccount(zhang);
                            //Person Li = new Person("Lisi", "223456789012345");
                            //aBank.AddAccount(Li);
                            break;
                        case "2":
                            Console.Write("Please input your ID: ");
                            string saveID = Console.ReadLine();
                            Console.Write("How much would you save: ");
                            double inMoney = Convert.ToDouble(Console.ReadLine());
                            aBank.getAccount(saveID).Save(inMoney);
                            break;
                        case "3":
                            Console.Write("Please input your ID: ");
                            string withdrawID = Console.ReadLine();
                            Console.Write("How much would you withdraw: ");
                            double outMoney = Convert.ToDouble(Console.ReadLine());
                            aBank.getAccount(withdrawID).Withdraw(outMoney);
                            break;
                        case "4":
                            Console.Write("Please input your ID: ");
                            string outID = Console.ReadLine();
                            Console.Write("Please input Transfer's ID: ");
                            string inID = Console.ReadLine();
                            Console.Write("How much would you transfer: ");
                            double funds = Convert.ToDouble(Console.ReadLine());
                            aBank.getAccount(outID).Transfer(aBank.getAccount(inID), funds);
                            break;
                        case "5":
                            Console.Write("Please input your ID: ");
                            string showID = Console.ReadLine();
                            Console.WriteLine("Your account balance: $"+aBank.getAccount(showID).ShowBalance());
                            break;
                        //case "6":
                        //    Console.Write("Please input your ID: ");
                        //    string ID = Console.ReadLine();
                        case "7":
                            Console.Write("Please input your ID: ");
                            string deleteID = Console.ReadLine();
                            aBank.DeleteAccount(aBank.getAccount(deleteID));
                            break;
                        default:
                            bExit = false;
                            break;
                    }
                }
            }
        }
    }

    /* 业务逻辑部分 */
    class Person
    {
        public string Name;
        public string ID;
        public string LinkagePhone;

        public Person(string name, string id)
        {
            Name = name;
            ID = id;
            LinkagePhone = "1234567";
        }
    }

    class Account
    {
        public Person Owner;
        private string Password;
        private string Number; //??
        private double Balance;
        public float InterestRate;
        public int startYear;
        public int startMonth;


        public Account(Person guest, string psw, int year, int month)
        {
            Owner = guest;
            Password = psw;
            startYear = year;
            startMonth = month;
            Random r = new Random();
            Number = r.Next().ToString(); //??
            Balance = 0.0f;
            InterestRate = 0.002f;
        }

        public void Save(double inNumber)
        {
            Balance += inNumber;
        }

        public bool Withdraw(double outNumber)
        {
            if (Balance < outNumber)
            {
                Console.WriteLine("Not sufficient funds");
                return false;
            }
            Balance -= outNumber;
            return true;
        }

        public double ShowBalance()
        {
            return Balance;
        }

        public bool Transfer(Account transfer,double money)
        {
            if (this.Balance < money)
            {
                return false;
            }
            Balance -= money;
            transfer.Balance += money;
            return true;
        }
    }

    class Bank
    {
        private string Name;
        private string OfficePhone;
        private Account[] Accounts;
        private int maxAccounts;
        private int curAccounts;

        public Bank(string bName, string phone, int numAccount)
        {
            Name = bName;
            OfficePhone = phone;
            Accounts = new Account[numAccount];
            maxAccounts = numAccount;
            curAccounts = 0;
        }

        public void AddAccount(Person guest)
        {
            if (curAccounts < maxAccounts)
            {
                bool setPsw = false;
                string originalPsw;
                do
                {
                    Console.Write("Please set your PASSWORD: ");
                    originalPsw = Console.ReadLine();
                    Console.Write("Please input your PASSWORD again: ");
                    string checkPsw = Console.ReadLine();
                    if (originalPsw.Equals(checkPsw))
                    {
                        setPsw = true;
                    }
                    else
                    {
                        Console.WriteLine("ERROR! Please try again.");
                    }
                } while (!setPsw && originalPsw != null);
                Account objAccount = new Account(guest, originalPsw, 2011, 4);
                Accounts[curAccounts] = objAccount;
                curAccounts++;
                Console.WriteLine("Add successfully.");
            }
            else
            {
                Console.WriteLine("Sorry, no any empty account fou you.");
            }
        }

        public void DeleteAccount(Account cancelAccount)
        {
            int which = -1;
            for (int i = 0; i < Accounts.Length; i++)
            {
                if (Accounts[i].Owner.Name == cancelAccount.Owner.Name && Accounts[i].Owner.ID == cancelAccount.Owner.ID)
                {
                    which = i;
                    break;
                }
            }
            if (which >= 0)
            {
                for (; which < Accounts.Length; which++)
                {
                    Accounts[which] = Accounts[which + 1];
                }
                curAccounts -= 1;
                Console.WriteLine("Delete Successfully");
            }
            else
            {
                Console.WriteLine("This account has not been found, please check the information.");
            }
        }

        public void ReportLoss(Account oneAccount)
        {

        }

        public double CalculateInterest(Account oneAccount, int curYear, int curMonth)
        {
            double interest = 0.0;
            if (curYear < oneAccount.startYear)
            {
                Console.WriteLine("The input year is wrong, please check it.");
                return 0;
            }
            else if (curYear == oneAccount.startYear)
            {
                if (curMonth < oneAccount.startMonth)
                {
                    Console.WriteLine("The input month is wrong, please check it");
                    return 0;
                }
            }
            else
            {
                interest = ((curYear - oneAccount.startYear) * 12 + curMonth - oneAccount.startMonth) * oneAccount.InterestRate;
            }
            return interest;
        }

        public Account getAccount(string id)
        {
            foreach (Account tempAccount in Accounts)
            {
                if (tempAccount.Owner.ID == id)
                {
                    return tempAccount;
                }
            }
            return null;
        }
    }
}
