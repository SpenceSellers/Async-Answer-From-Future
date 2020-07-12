using System;
using System.Threading.Tasks;

namespace TimeTravel
{
    public class Program
    {
        public static void Main()
        {
            var answer = FindN().Result;
            
            Console.Out.WriteLine($"Answer is {answer}");
            // Prints "Answer is 60"
        }
        
        private static async Task<int> FindN()
        {
            var answerFromFuture = new AnswerFromFuture();
            
            // Pull a number that's going to magically avoid hitting .Nope()
            var n = await answerFromFuture;

            if (n % 10 != 0)
            {
                answerFromFuture.Nope();
            }

            if (n < 55)
            {
                answerFromFuture.Nope();
            }

            return n;
        }
    }
}