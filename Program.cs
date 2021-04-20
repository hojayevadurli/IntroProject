using System;
using System.IO;

namespace Skiing_Amongst_Trees
{

    /* holds our character array of trees and the number of rows and columns in that array*/
    class DataNode
    {
        private char[,] dataArray;
        private int numberRows;
        private int numberColumns;

        public DataNode(char[,] array, int r, int c)
        {
            dataArray = array;
            numberRows = r;
            numberColumns = c;
        }

        public int getNumberRows()
        {
            return numberRows;
        }
        public int getNumberColumns()
        {
            return numberColumns;
        }
        public char[,] getDataArray()
        {
            return dataArray;
        }
    }

    /* holds the number of hits and misses */
    class hitMiss
    {
        private int hits = 0;
        private int miss = 0;

        //constructor
        public hitMiss(int h, int m)
        {
            hits = h;
            miss = m;
        }

        //Getters
        public int getHits() => hits;
        public int getMiss() => miss;

        //Setters
        public void updateHits(int h) => hits += h;
        public void updateMiss(int m) => miss += m;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Read from file into our dataMap array
            var dataMap = readIntoArray(@"C:\Users\hojay\Downloads\SkiWorking\Skiing\Skiing_Amongst_Trees\TreeMap.txt");


            int run = 3;
            int rise = 2;

            // call the ski function passing in the slope (over (run) 3 down (rise) 1 and the DataMap
            hitMiss hmcount = ski(dataMap, run, rise);

            //output the resutls to the viewer
            Console.WriteLine("With a slope of over " + run + " down " + rise + ", we hit " + hmcount.getHits() + " trees and missed the other " + hmcount.getMiss() + " trees.");

        }

        public static DataNode readIntoArray(string filename)
        {
            //Open the file 
            var fileAccessor = new StreamReader(File.OpenRead(filename));

            //Count the number of columns in our file
            var line1 = fileAccessor.ReadLine();
            var numCols = line1.Length;
            

            //Count how many rows are in our file
            int numRows = 1;
            while (!fileAccessor.EndOfStream) // credit https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
            {
                //read the next line
                fileAccessor.ReadLine();
                numRows++;
            }

            //allocate our Char array
            char[,] treeMap = new char[numRows, numCols];

            //Read the file into our array.
            fileAccessor.Close();
            fileAccessor = new StreamReader(File.OpenRead(filename));

            for (int r = 0; r < numRows; r++)
            {
                var line = fileAccessor.ReadLine();
                //var charArrayLine = line.ToCharArray();
                for (int c = 0; c < numCols; c++)
                {
                    treeMap[r, c] = line[c];
                }
            }

            //close the file
            fileAccessor.Close();


            //return our array
            return new DataNode(treeMap, numRows, numCols);
        }


        /* ski accepts the data node, run, and rise to calculate how many times the skiier hits a tree */
        public static hitMiss ski(DataNode dnode, int over, int down)
        {
            int hits = 0;
            var miss = 0;
            int colPosition = 0;
            var numRows = dnode.getNumberRows();
            var numCols = dnode.getNumberColumns();
            char[,] data = dnode.getDataArray();
            var hitMissCounts = new hitMiss(0, 0); // to count our number of hits and misses

            for (int rowPosition = 0; rowPosition < numRows; rowPosition += down, colPosition += over)
            {
                

                //did we hit a tree?
                if (data[rowPosition, (colPosition % numCols)] == '#')
                {
                    hitMissCounts.updateHits(1);
                }
                else
                {
                    hitMissCounts.updateMiss(1);
                }

                

            }

            return hitMissCounts;
        }
    }


}