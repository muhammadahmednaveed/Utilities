using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace Levenshtein_Distance_Algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string goAgain = "y";
            while(goAgain=="y")
            {
                Console.Write("First Word: ");
                string word1 = Console.ReadLine();
                Console.Write("Second Word: ");
                string word2 = Console.ReadLine();

                Console.WriteLine("Score: " + Score(word1, word2));
                Console.Write("Go again: ");
                goAgain = Console.ReadLine();
            }
           
        }

        static double Score(string word1, string word2)
        {
            double distance = LevenshteinAlgo(word1, word2);
            double wordLength = word1.Length > word2.Length ? word1.Length : word2.Length;
            //int firstwordLength = word1.Length > word2.Length ? word1.Length : word2.Length;
            //double wordLength = word2.Length;
            double score = (wordLength-distance) / wordLength;

            return score;
        }
        static int LevenshteinAlgo(string word1,string word2)
        {
            int[,] matrix = new int[word1.Length+1,word2.Length+1];

            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = i;
            }
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[0,i] = i;
            }
            for(int i = 1;i < matrix.GetLength(0); i++)
            {
                for(int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (word1[i-1] == word2[j-1])
                    {
                        matrix[i,j] = matrix[i-1,j-1];
                    }
                    else
                    {
                        matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j - 1], matrix[i, j - 1]), matrix[i - 1, j])+1;
                    }
                }
            }
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine(); 
            }
            Console.WriteLine("Edit Distance: " + matrix[word1.Length, word2.Length]);
            return matrix[word1.Length,word2.Length];
        }
    }
}
