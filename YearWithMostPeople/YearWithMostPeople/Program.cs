using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//YearWithMostPeople
//by Trey Hookham
//this program uses the birth and end years between 1900 and 2000 of people provided by the user and calculates the year in which the most of those people were alive
//birth and end years are read from a txt file that the user provides a file path for
//the output gives the latest year with the most amount of people if there is a tie
//the program specifications asked for "the year with the most number of people alive", so only one year is given, but I could alter the code to return a list of years in the event of a tie, or return the first year instead of the last
//files containing the years are txt files with the birth and end years separated by commas, and each set of years on a new line

namespace YearWithMostPeople
{
    public class Program
    {
        static void Main()
        {
            string[] people_list = GetPeople();
            if (!people_list.Any()) { RunAgain("This file is empty."); }
            else
            {
                var people = new TupleList<int, int>();
                foreach (string person in people_list)
                {
                    //separates the birth and end years, then adds them into the TupleList as a tuple
                    try
                    {
                        int birth_year = int.Parse(person.Split(',')[0]);
                        int end_year = int.Parse(person.Split(',')[1]);
                        if (birth_year < 1900 || end_year < 1900 || birth_year > 2000 || end_year > 2000) { RunAgain("Not every year was from 1900 to 2000."); }
                        if (birth_year > end_year) { RunAgain("Birth years cannot be later than end years."); }
                        people.Add(birth_year, end_year);
                    }
                    catch (FormatException)
                    {
                        RunAgain("The contents of this file do not have the correct format.");
                    }
                }
                int YearWithMost = FindYearWithMost(people);
                RunAgain($"The year in which the most of the given people were alive was {YearWithMost}.");
            }
        }
        public static int FindYearWithMost(TupleList<int, int> people)
        {
            //takes a TupleList of birth and end years of people between 1900 and 2000 and returns the year with the most people
            //creates a dictionary with a key for each year from 1900 to 2000 with all the values set to 0
            Dictionary<int, int> Years = new Dictionary<int, int>();
            for (int year = 1900; year <= 2000; year++)
            {
                Years.Add(year, 0);
            }
            //goes through each tuple in people
            foreach (var person in people)
            {
                //for each set of years it increments the value of each year from their birth year to their end year in the Years dictionary by 1
                for (int year = person.Item1; year <= person.Item2; year++)
                {
                    Years[year]++;
                }
            }
            //a line of code i found online
            //uses the aggregate method to find the key of the max value in the Years dictionary
            int YearWithMost = Years.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return YearWithMost;
        }
        static string[] GetPeople()
        {
            //gets path for txt file containing birth and end years from user and returns the contents in a list
            Console.Write("Enter the file path: ");
            string path = Console.ReadLine();
            if (Path.GetExtension(path) != ".txt") { RunAgain("The file must be a text file."); }
            try { return File.ReadAllLines(path); }
            catch (FileNotFoundException)
            {
                RunAgain("This file does not exist.");
                return null;
            }
        }
        static void RunAgain(string message)
        {
            //gives the year with the most people or user error messages and asks if they would like to choose another file
            Console.WriteLine(message);
            Console.Write("\nWould you like to choose another file? ");
            string response = Console.ReadLine().ToLower();
            if (response == "yes" || response == "y") { Main(); }
            else if (response == "no" || response == "n")
            {
                Console.WriteLine("Thank you for using YearWithMostPeople.");
                Environment.Exit(0);
            }
            else { RunAgain("That is not a valid response. Please answer with yes or no."); }
        }
        public class TupleList<T1, T2> : List<Tuple<T1, T2>>
        {
            //a simple class for a list of tuples that I found online
            public void Add(T1 item1, T2 item2)
            {
                Add(new Tuple<T1, T2>(item1, item2));
            }
        }
    }
}