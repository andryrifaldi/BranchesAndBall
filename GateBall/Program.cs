using System;
using System.Collections.Generic;
using System.Linq;

namespace GateBall
{
    class Program
    {
        // 2 hour work
        //-----------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                       initial                                                                 //
        // node 1                                           0                                           1                                                //
        // node 2                                         00 01                                       10 11                                              //
        // node 3                                      000 001 010 011                           100 101 110 111                                         //
        // node 4                          0000 0001 0010 0011 0100 0101 0110 0111    1000 1001 1010 1011 1100 1101 1110 1111                            //
        //                                   A   B     C   D     E    F   G     H       I    J    K    L    M    N    O    P                             //


        public static void Main(string[] args)
        {
            string[] prevChoiceArray = { "", "", "", "" };
            List<string> gate = new List<string>();

            for (int j = 1; j <= 16; j++)
            {
                string prevChoice = "";
                string currentChoice = "";

                Console.WriteLine("---------------ball {0}------------------", j);
                for (int i = 1; i <= 4; i++)
                {
                    currentChoice = getChoice(prevChoiceArray, gate, ref prevChoice, i);
                }
            }

            Console.ReadLine();
        }


        /// <summary>
        /// Get choice left or right (0,1)
        /// </summary>
        /// <param name="prevChoiceArray">Previous choice array list</param>
        /// <param name="gate">ball pass gate</param>
        /// <param name="prevChoice">previous choice</param>
        /// <param name="i">counter</param>
        /// <returns></returns>
        private static string getChoice(string[] prevChoiceArray, List<string> gate, ref string prevChoice, int i)
        {

            string currentChoice;
            Random random = new Random();

            var n = random.Next(0, 2);
            currentChoice = prevChoice + n.ToString();

            IEnumerable<string> b = Combinations(new int[] { 0, 1 }, i);


            if (prevChoiceArray[i - 1] == "")
                prevChoiceArray[i - 1] = currentChoice;
            else
            {
                if (prevChoiceArray[i - 1] == currentChoice)
                {
                    n = (n == 0) ? 1 : 0;
                    currentChoice = prevChoice + n.ToString();
                }
                prevChoiceArray[i - 1] = currentChoice;
            }

            var c = b.Where(x => x == prevChoiceArray[i - 1] && !gate.Contains(x)).FirstOrDefault();
            if (c == null)
            {
                n = (n == 0) ? 1 : 0;
                currentChoice = prevChoice + n.ToString();
                prevChoiceArray[i - 1] = currentChoice;

                c = b.Where(x => x == prevChoiceArray[i - 1] && !gate.Contains(x)).FirstOrDefault();
                if(c == null)
                {
                    Console.Write("-");
                }

            }

            Console.WriteLine(c);

            prevChoice = currentChoice;
            if (i == 4)
            {
                gate.Add(prevChoice);
            }

            return currentChoice;
        }

        /// <summary>
        /// Get all posible combination
        /// </summary>
        /// <param name="input">input parameter</param>
        /// <param name="length">length</param>
        /// <returns></returns>
        static IEnumerable<String> Combinations(IEnumerable<int> input, int length)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in Combinations(input, length - 1))
                        yield return i.ToString() + c;
            }
        }

    }
}
