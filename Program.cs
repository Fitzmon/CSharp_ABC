using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deposit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First, I'll build a new bank");
            Console.Read();
            Bank aBank = new Bank("ABC", "123456", 10);
            Person zhang = new Person("zhangsan", "123456789012345");
            aBank.AddAccount(zhang);
            Person Li = new Person("Lisi", "223456789012345");
            aBank.AddAccount(Li);
        }
    }

    class Person
    {
        public string Name;
        public string ID;
        public string LinkagePhone;

        public Person(string name,string id)
        {
            Name = name;
            ID = id;
        }
    }

    class Account
    {
        public Person Owner;
        public string Number; //??
        public double Balance;
        public float InterestRate;
        public int startYear;
        public int startMonth;

        public Account(Person guest,int year,int month)
        {
            Owner = guest;
            startYear = year;
            startMonth = month;
            Random r = new Random();
            Number = r.Next().ToString(); //??
            Balance = 0.0f;
            InterestRate = 0.002f;
        }

        public double Save(double inNumber)
        {
            Balance += inNumber;
            return Balance;
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
    }

    class Bank
    {
        public string Name;
        public string OfficePhone;
        public Account[] Accounts;
        public int curAccounts;

        public Bank(string bName, string phone, int numAccount)
        {
            Name = bName;
            OfficePhone = phone;
            Accounts = new Account[numAccount];
            curAccounts = 0;
        }
        
        public void AddAccount(Person guest)
        {
            Account objAccount = new Account(guest, 2011, 4);
            Accounts[curAccounts] = objAccount;
            curAccounts++;
        }
        
        public void DeleteAccount(Account cancelaccount)
        {
            int which = -1;
            for(int i = 0; i < Accounts.Length; i++)
            {
                if (Accounts[i].Owner.Name == cancelaccount.Owner.Name&&Accounts[i].Owner.ID==cancelaccount.Owner.ID)
                {
                    which = i;
                    break;
                }
            }
            if (which >=0)
            {
                for (; which < Accounts.Length; which++)
                {
                    Accounts[which] = Accounts[which + 1];
                }
                curAccounts -= 1;
            }
            else
            {
                Console.WriteLine("This account has not been found, please check the information.");
            }
        }

        public double CalculateInterest(Account oneAccount,int curYear,int curMonth)
        {
            double interest = 0.0;
            if (curYear < oneAccount.startYear)
            {
                Console.WriteLine("The input year is wrong, please check it");
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
            foreach(Account tempAccount in Accounts)
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
