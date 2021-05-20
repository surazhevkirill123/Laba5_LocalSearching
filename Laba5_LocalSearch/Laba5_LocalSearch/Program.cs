using System;
using System.Collections.Generic;

namespace Laba5_LocalSearch
{
    class Program
    {
        public static int countOfNodes = 8;
        public static int minValue = 1;
        public static int maxValue = 40;
        public static int[,] matrixOfWeights = new int[countOfNodes, countOfNodes];
        public static Random random = new Random();


        public static void RandomizeMatrix(int[,] matrixOfWeights, int minValue, int maxValue)
        {
            for (int i = 0; i < matrixOfWeights.GetLength(0); i++)
            {
                for (int j = i; j < matrixOfWeights.GetLength(1); j++)
                {
                    matrixOfWeights[i, j] = random.Next(minValue, maxValue);
                }
            }
            for (int i = matrixOfWeights.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    matrixOfWeights[i, j] = matrixOfWeights[j, i];
                }
            }
        }

        public static int weightOfCycle(List<int> combination)
        {
            List<int> cycle = new List<int>();
            for (int i = 0; i < combination.Count; i++)
            {
                cycle.Add(combination[i]);
            }
            cycle.Add(combination[0]);            
            int weight = 0;
            for (int i = 0; i < cycle.Count - 1; i++)
            {
                weight += matrixOfWeights[cycle[i]-1, cycle[i + 1]-1];
                
            }
            return weight;
        }

        public static string WriteMatrixOfWeightsText(int[,] matrixOfWeights)
        {
            string matrixOfWeightsText = null;
            for (int i = 0; i < matrixOfWeights.GetLength(0); i++)
            {
                for (int j = 0; j < matrixOfWeights.GetLength(1); j++)
                {
                    matrixOfWeightsText += matrixOfWeights[i, j] + "\t";
                }
                matrixOfWeightsText += "\n";
            }
            return matrixOfWeightsText;
        }

        public static List<int> GetRandomCombination()
        {
            List<int> combination = new List<int>() { };
            for (int i = 1; i < countOfNodes + 1; i++)
            {
                combination.Add(i);
            }
            for (int i = combination.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = combination[j];
                combination[j] = combination[i];
                combination[i] = temp;
            }
            return combination;
        }

        public static string WriteListOfInt(List<int> numbers)
        {
            string ListOfInt = null;
            for (int i = 0; i < numbers.Count; i++)
            {
                ListOfInt += (numbers[i] + " ");
            }
            return ListOfInt;
        }

        public static List<int> SwapNodes(List<int> combination, int index1, int index2)
        {
            List<int> newCombination = new List<int>();
            for (int i = 0; i < combination.Count; i++)
            {
                newCombination.Add(combination[i]);
            }
            var buf = newCombination[index1];
            newCombination[index1] = newCombination[index2];
            newCombination[index2] = buf;
            return newCombination;
        }

        static void Main(string[] args)
        {
            RandomizeMatrix(matrixOfWeights, minValue, maxValue);
            List<int> combination = GetRandomCombination();
            int bestWeight = int.MaxValue;
            List<int> bestCombination = new List<int>();
            for (int i = 0; i < countOfNodes - 2; i++)
            {
                for (int j = i + 2; j < countOfNodes; j++)
                {
                    List<int> newCombination = SwapNodes(combination, i, j);
                    if (weightOfCycle(newCombination) < bestWeight)
                    {
                        bestWeight = weightOfCycle(newCombination);
                        bestCombination = newCombination;
                    }
                }
            }
            Console.WriteLine(WriteMatrixOfWeightsText(matrixOfWeights));
            Console.WriteLine($"bestWeight  : {bestWeight} \t\t bestCombination: {WriteListOfInt(bestCombination)}");
        }
    }
}
