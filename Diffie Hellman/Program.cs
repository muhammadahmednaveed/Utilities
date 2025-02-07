using System.Numerics;

namespace Diffie_Hellman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = 111111111111111111;
            BigInteger b = 1111111111111;
            BigInteger g = 11111111111111;
            BigInteger n = 1111111111111111111;

            BigInteger ag = BigInteger.ModPow(g, a, n);
            BigInteger bg = BigInteger.ModPow(g, b, n);

            BigInteger abg1 = BigInteger.ModPow(ag, b, n);
            BigInteger abg2 = BigInteger.ModPow(bg, a, n);

            Console.WriteLine(abg1);
            Console.WriteLine(abg2);
        }
    }
}
