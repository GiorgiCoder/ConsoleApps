using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProjects
{
    public class GuessTheNumber
    {
        public void GuessNumber(int start, int end) // range of number
        {
            int numberOfTries = 0; // when it reaches 10, the game ends
            Random random = new Random();
            int number = random.Next(start, end); // pick number
            int userNumber = start - 1;

            Console.WriteLine($"Enter a number from {start} to {end}:");
            while (userNumber != number && numberOfTries < 10) // do while
            {
                try
                {
                    userNumber = int.Parse(Console.ReadLine()); // validation for number
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

            // loop can be exited in 2 ways, either use guessed number, or he used 10 tries.
            // we check it and return result

            if(numberOfTries == 10)
            {
                Console.WriteLine($"You lost. The number was {number}");
            }
            Console.WriteLine($"You guessed the number in {numberOfTries} tries!");
        }
    }
}
