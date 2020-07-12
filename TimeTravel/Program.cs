using System;
using System.Threading.Tasks;

namespace TimeTravel
{
    public class Program
    {
        public static async Task Main()
        {
            var answerFromFuture = new AnswerFromFuture();
            
            // Resolve a number that's going to magically avoid hitting .Nope()
            var n = await answerFromFuture;

            if (n % 10 != 0)
            {
                answerFromFuture.Nope();
            }

            if (n < 55)
            {
                answerFromFuture.Nope();
            }

            Console.Out.WriteLine($"Answer is {n}");
            // Prints "Answer is 60"
        }
    }
}