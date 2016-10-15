/*
 * ARTHUR: EBUBE ABARA
 * CODE LANGUAGE: C#
 * DATE: SEPTEMBER 2016
 * TASK: Program accepts user location as a pair of coordinates, and returns a list
 * of the five closest events, along with the cheapest ticket price for each event
 */

using System;
using System.Linq;

namespace Simple_Geo_Location
{
    class Program
    {
        private static readonly Random ran = new Random();

        static void Main(string[] args)
        {
            try
            {
                //initialising variables, single and multidimensional arrays
                int x = 0;
                int y = 0;
                int[] xArray = new int[21] { -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int[] yArray = new int[21] { -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int[,] coordXY = new int[30, 30];
                int[] distance = new int[30];
                int[] eventID = new int[30];
                string eventIDString = "";
                double[] cheapestTicket = new double[30];
                string cheapestTicketString = "";
                string[] splitMoney = new string[2];
                string money = "";
                string dollar = "";
                string cent = "";

                //input dialog to enter x and y coordinates
                Console.WriteLine("HINT: First add x coordinate, press ENTER button then finally add y coordinate.");
                Console.WriteLine("Please Input Coordinates:");
                x = int.Parse(Console.ReadLine());
                y = int.Parse(Console.ReadLine());

                //check for invalid input
                if (!Enumerable.Range(-10, 21).Contains(x) || !Enumerable.Range(-10, 21).Contains(y))
                {
                    Console.WriteLine("You are only allowed x and y coordinates from -10 to +10");
                    Environment.Exit(0);
                }

                for (int i = 0; i <= 29; i++)
                {
                    //creating eventID and adding random numbers from -10 to +10 in x and y array into xy multidimensional array
                    eventID[i] = i + 1;
                    coordXY[i, 0] = xArray[ran.Next(0, 21)];
                    coordXY[i, 1] = yArray[ran.Next(0, 21)];

                    //event tickets and cheapest ticket
                    double[] ticketType = new double[3];
                    for (int j = 0; j <= ticketType.Length - 1; j++)
                    {
                        ticketType[j] = RandomGenerator(0.01, 99.99);
                    }
                    cheapestTicket[i] = TicketBubbleSort(ticketType);

                    //calculating distance using Manhattan Distance
                    distance[i] = Math.Abs(((coordXY[i, 0] - x) + (coordXY[i, 1] - y)));
                }

                Console.WriteLine();
                Console.WriteLine("Closest Events to (" + x + "," + y + "):");
                Console.WriteLine();

                //sorting and printing the 5 nearest events
                EventsBubbleSort(eventID, cheapestTicket, distance);
                for (int i = 0; i <= 4; i++)
                {
                    eventIDString = eventID[i].ToString();
                    cheapestTicketString = (Math.Round(cheapestTicket[i], 2)).ToString();
                    splitMoney = cheapestTicketString.Split('.');
                    dollar = splitMoney[0].PadLeft(2, '0');
                    cent = splitMoney[1].PadRight(2, '0');
                    money = dollar + "." + cent;
                    Console.WriteLine("Event " + eventIDString.PadLeft(3, '0') + " - $" + money + ", Distance " + distance[i]);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message + ". Try again");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message + ". Only numbers are allowed. Specifically from -10 to +10. Try again");
            }
        }


        //Method: ticket price random genetator from 0.01 to 99.99
        public static double RandomGenerator(double minVal, double maxVal)
        {
            double tpRan = 0;
            var num = ran.NextDouble();
            tpRan = minVal + (num * (maxVal - minVal));
            return tpRan;
        }


        //Method: BubbleSort algorithm to sort and find nearest events
        public static void EventsBubbleSort(int[] inputEventID, double[] inputCheapestTicket, int[] inputDistance)
        {
            int tempEventID = 0;
            double tempCheapestTicket = 0;
            int tempDistance = 0;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                for (int i = 0; i < inputDistance.Length - 1; i++)
                {
                    if (inputDistance[i] > inputDistance[i + 1])
                    {
                        tempEventID = inputEventID[i + 1];
                        inputEventID[i + 1] = inputEventID[i];
                        inputEventID[i] = tempEventID;

                        tempCheapestTicket = inputCheapestTicket[i + 1];
                        inputCheapestTicket[i + 1] = inputCheapestTicket[i];
                        inputCheapestTicket[i] = tempCheapestTicket;

                        tempDistance = inputDistance[i + 1];
                        inputDistance[i + 1] = inputDistance[i];
                        inputDistance[i] = tempDistance;

                        sorted = false;
                    }
                }
            }
        }


        //Method: BubbleSort algorithm to sort and get cheapest ticket
        public static double TicketBubbleSort(double[] inputVal)
        {
            double tempVal = 0;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                for (int i = 0; i < inputVal.Length - 1; i++)
                {
                    if (inputVal[i] > inputVal[i + 1])
                    {
                        tempVal = inputVal[i + 1];
                        inputVal[i + 1] = inputVal[i];
                        inputVal[i] = tempVal;
                        sorted = false;
                    }
                }
            }
            return inputVal[0];
        }
    }
}
