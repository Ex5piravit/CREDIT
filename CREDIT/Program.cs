using System;
using System.Collections.Generic;

namespace CREDIT
{
    class Citizen
    {
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Debt { get; set; }
        public int PaymentHistory { get; set; }
        public bool Eligible { get; set; }
        public decimal DisposableIncome { get; set; }
        public decimal DebtPercentage { get; set; }
        public int CreditScore { get; set; }
        public string Rating { get; set; }
    }
    class Program
    {
        static List<Citizen> citizens = new List<Citizen>();

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("       CREDIT SCORE DETERMINATION");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Add Citizen");
                Console.WriteLine("2. Display All Records");
                Console.WriteLine("3. Search Citizen");
                Console.WriteLine("4. Update Citizen");
                Console.WriteLine("5. Exit");
                Console.WriteLine("======================================");
                choice = ReadInt("Enter your choice: ");
                switch (choice)
                {
                    case 1:
                        AddCitizen();
                        break;

                    case 2:
                        DisplayAllRecords();
                        break;
                    case 3:
                        SearchCitizen();
                        break;
                    case 4:
                        UpdateCitizen();
                        break;
                    case 5:
                        Console.WriteLine("Program terminated.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }

            } while (choice != 5);
        }
        static void AddCitizen()
        {
            Console.Clear();
            Console.WriteLine("========== ADD CITIZEN ==========");
            Citizen person = new Citizen();
            Console.Write("Enter ID number: ");
            person.IdNumber = Console.ReadLine();
            Console.Write("Enter full name: ");
            person.Name = Console.ReadLine();
            person.Age = ReadInt("Enter age: ");
            Console.Write("Enter geographical location: ");
            person.Location = Console.ReadLine();
            person.Income =
                ReadDecimal("Enter monthly income: R");
            person.Expenses =
                ReadDecimal("Enter monthly expenses: R");
            person.Debt =
                ReadDecimal("Enter outstanding debt: R");
            Console.WriteLine();
            Console.WriteLine("Payment History");
            Console.WriteLine("1. Excellent");
            Console.WriteLine("2. Good");
            Console.WriteLine("3. Average");
            Console.WriteLine("4. Poor");
            person.PaymentHistory =
                ReadInt("Select payment history: ");
            person.Eligible = CheckEligibility(person);
            if (!person.Eligible)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Citizen is NOT eligible for a credit score.");
                Pause();
                return;
            }
            CalculateCreditScore(person);
            person.Rating =
                DetermineRating(person.CreditScore);
            citizens.Add(person);
            Console.WriteLine();
            Console.WriteLine("Citizen successfully added.");
            DisplayCitizen(person);
            Pause();
        }
        static bool CheckEligibility(Citizen person)
        {
            if (person.Age < 18)
            {
                return false;
            }
            if (person.Income < 0)
            {
                return false;
            }
            if (person.Expenses < 0)
            {
                return false;
            }
            if (person.Debt < 0)
            {
                return false;
            }
            return true;
        }
        static void CalculateCreditScore(Citizen person)
        {
            int score = 100;

            switch (person.PaymentHistory)
            {
                case 1:
                    score += 300;
                    break;
                case 2:
                    score += 240;
                    break;
                case 3:
                    score += 180;
                    break;
                case 4:
                    score += 100;
                    break;
                default:
                    score += 0;
                    break;
            }
            person.DisposableIncome =
                person.Income - person.Expenses;

            if (person.Income > 0)
            {
                decimal disposablePercentage =
                    (person.DisposableIncome /
                    person.Income) * 100;
                if (disposablePercentage >= 50)
                {
                    score += 200;
                }
                else if (disposablePercentage >= 30)
                {
                    score += 150;
                }
                else if (disposablePercentage >= 10)
                {
                    score += 100;
                }
                else
                {
                    score += 50;
                }
            }
            if (person.Income > 0)
            {
                decimal annualIncome =
                    person.Income * 12;
                person.DebtPercentage =
                    (person.Debt / annualIncome) * 100;
                if (person.DebtPercentage <= 20)
                {
                    score += 150;
                }
                else if (person.DebtPercentage <= 40)
                {
                    score += 120;
                }
                else if (person.DebtPercentage <= 60)
                {
                    score += 80;
                }
                else
                {
                    score += 40;
                }
            }
            if (person.Age >= 25 &&
                person.Age <= 55)
            {
                score += 50;
            }
            else
            {
                score += 30;
            }
            if (score > 850)
            {
                score = 850;
            }
            if (score < 100)
            {
                score = 100;
            }
            person.CreditScore = score;
        }
        static string DetermineRating(int score)
        {
            if (score >= 750)
            {
                return "Excellent";
            }
            else if (score >= 650)
            {
                return "Good";
            }
            else if (score >= 550)
            {
                return "Fair";
            }
            else if (score >= 450)
            {
                return "Poor";
            }
            else
            {
                return "Very Poor";
            }
        }
        static void DisplayAllRecords()
        {
            Console.Clear();
            Console.WriteLine(
                "========== ALL CREDIT RECORDS ==========");
            if (citizens.Count == 0)
            {
                Console.WriteLine("No records available.");
                Pause();
                return;
            }
            foreach (Citizen person in citizens)
            {
                DisplayCitizen(person);
                Console.WriteLine(
                    "----------------------------------------");
            }
            Pause();
        }
        static void DisplayCitizen(Citizen person)
        {
            Console.WriteLine();
            Console.WriteLine("========== CREDIT REPORT ==========");
            Console.WriteLine(
                "ID Number        : " + person.IdNumber);
            Console.WriteLine(
                "Name             : " + person.Name);
            Console.WriteLine(
                "Age              : " + person.Age);
            Console.WriteLine(
                "Location         : " + person.Location);
            Console.WriteLine(
                "Monthly Income   : R" + person.Income);
            Console.WriteLine(
                "Monthly Expenses : R" + person.Expenses);
            Console.WriteLine(
                "Outstanding Debt : R" + person.Debt);

            Console.WriteLine(
                "Disposable Income: R" +
                person.DisposableIncome);
            Console.WriteLine(
                "Payment History  : " +
                GetPaymentDescription(
                    person.PaymentHistory));
            Console.WriteLine(
                "Debt Percentage  : " +
                person.DebtPercentage.ToString("F2") + "%");
            Console.WriteLine(
                "Credit Score     : " +
                person.CreditScore);
            Console.WriteLine(
                "Credit Rating    : " +
                person.Rating);
            Console.WriteLine(
                "Eligible         : " +
                (person.Eligible ? "Yes" : "No"));
        }
        static void SearchCitizen()
        {
            Console.Clear();

            Console.WriteLine(
                "========== SEARCH CITIZEN ==========");

            Console.Write("Enter ID number: ");
            string id = Console.ReadLine();

            Citizen found = FindCitizen(id);

            if (found == null)
            {
                Console.WriteLine("Citizen not found.");
            }
            else
            {
                DisplayCitizen(found);
            }

            Pause();
        }
        static Citizen FindCitizen(string id)
        {
            foreach (Citizen person in citizens)
            {
                if (person.IdNumber == id)
                {
                    return person;
                }
            }

            return null;
        }
        static void UpdateCitizen()
        {
            Console.Clear();
            Console.WriteLine(
                "========== UPDATE CITIZEN ==========");
            Console.Write("Enter ID number: ");
            string id = Console.ReadLine();
            Citizen found = FindCitizen(id);
            if (found == null)
            {
                Console.WriteLine("Citizen not found.");
                Pause();
                return;
            }
            Console.WriteLine();
            Console.WriteLine("CURRENT RECORD");
            DisplayCitizen(found);
            Console.WriteLine();
            Console.WriteLine("Enter new information.");
            found.Income =
                ReadDecimal("New monthly income: R");
            found.Expenses =
                ReadDecimal("New monthly expenses: R");
            found.Debt =
                ReadDecimal("New outstanding debt: R");
            Console.WriteLine();
            Console.WriteLine("Payment History");
            Console.WriteLine("1. Excellent");
            Console.WriteLine("2. Good");
            Console.WriteLine("3. Average");
            Console.WriteLine("4. Poor");

            found.PaymentHistory =
                ReadInt("New payment history: ");
            CalculateCreditScore(found);
            found.Rating =
                DetermineRating(found.CreditScore);

            Console.WriteLine();
            Console.WriteLine(
                "Record successfully updated.");

            DisplayCitizen(found);

            Pause();
        }
        static string GetPaymentDescription(
            int paymentHistory)
        {
            switch (paymentHistory)
            {
                case 1:
                    return "Excellent";

                case 2:
                    return "Good";

                case 3:
                    return "Average";

                case 4:
                    return "Poor";

                default:
                    return "Unknown";
            }
        }
        static int ReadInt(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(
                    Console.ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine(
                    "Please enter a valid number.");
            }
        }
        static decimal ReadDecimal(string message)
        {
            decimal value;

            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(
                    Console.ReadLine(), out value))
                {
                    if (value >= 0)
                    {
                        return value;
                    }
                }

                Console.WriteLine(
                    "Please enter a valid positive amount.");
            }
        }
        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}