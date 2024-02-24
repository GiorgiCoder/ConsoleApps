using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects
{
    public class GuessTheNumber
    {
        public void GuessNumber(int start, int end)
        {
            int numberOfTries = 0;
            Random random = new Random();
            int number = random.Next(start, end);
            int userNumber = start - 1;

            Console.WriteLine($"Enter a number from {start} to {end}:");
            while (userNumber != number)
            {
                try
                {
                    userNumber = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Enter a valid number");
                    continue;
                }
                
                if (userNumber > number)
                {
                    Console.WriteLine("Lower");
                }
                else if (userNumber < number)
                {
                    Console.WriteLine("Higher");
                }
                else
                {
                    Console.WriteLine("Congrats!");
                }
                numberOfTries++;
            }

            Console.WriteLine($"You guessed the number in {numberOfTries} tries!");
        }
    }
}
