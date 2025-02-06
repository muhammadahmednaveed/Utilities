using System.Numerics;

namespace Diffie_Hellman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long a = 111111111111111111;
            long b = 1111111111111;
            long g = 11111111111111;
            long n = 1111111111111111111;

            long ag = (g ^ a) % n;
            long bg = (g ^ b) % n;

            long abg1 = (ag ^ b) % n;
            long abg2 = (bg ^ a) % n;

            Console.WriteLine(abg1);
            Console.WriteLine(abg2);
        }
    }
}
