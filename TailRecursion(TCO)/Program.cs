using System.Numerics;

namespace TailRecursion_TCO_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var a = Factorial(50000);
            var b = FactorialTailRecursive(9630,1);
            //Console.WriteLine(a);
            Console.WriteLine(b);
        }

        static BigInteger FactorialTailRecursive(int n, BigInteger product)
        {
            if (n < 2)
                return product;
            return FactorialTailRecursive(n - 1, n * product);
        }

        static BigInteger Factorial(int n)
        {
            if (n < 2)
                return 1;
            return n * Factorial(n - 1);
        }
    }
}
