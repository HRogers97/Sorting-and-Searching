/************************************************ID BLOCK***********************************************
 * Due Date:             October 22nd, 2018
 * Software Designer:    Hunter Rogers
 * Deliverable:          Assignment #3 (Version 5) --- Sorting and Searching
 * 
 * Description:          This program works with an array of string names, and an array
 *                       of corresponding interger weights. The program outputs a menu
 *                       and asks the user to select from one of three different search 
 *                       algorithms (Shell,Insertion, and Select), or to search for a 
 *                       name in the name array. The user must sort the array before 
 *                       searching. Upon selecting a sort algorithm, the program will
 *                       copy the name array into WKnam and sort WKnam. It will also copy
 *                       the weight array into WKwght and sort the data to correspond with
 *                       the now sorted data in the WKnam array. After performing a search
 *                       the program will either inform the user that their search cannot 
 *                       be found, or inform the user that their search has been found, 
 *                       along with it's position in the array, and the corresponding weight
 *******************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogeHa3v5
{
    class Program
    {
        const int NMAX = 10;          //maximum size of each name string
        const int LSIZE = 20;         //number of actual name strings in array
        static string[] WKnam;        //Copy of nam data
        static int[] WKwght;          //Copy of wght data



        static void Main()
        {
            //array of name strings
            string[] nam = new string[20] { "wendy", "ellen", "freddy", "tom", "susan",
                             "dick", "harry", "aloysius", "zelda", "sammy",
                             "mary", "hortense", "georgie", "ada", "daisy",
                             "paula", "alexander", "louis", "fiona", "bessie"  };

            //array of weights corresponding to these names
            int[] wght = new int[20] { 120, 115, 195, 235, 138, 177, 163, 150, 128, 142,
                       118, 134, 255, 140, 121, 108, 170, 225, 132, 148 };

            //Sort Section
            /*************DECLARATIONS*************/
            bool oneSort = false;   //Has the user sorted?
            char choice;            //User's menu choice
            bool done = false;      //Terminate program switch

            OutLists(nam, wght, "Unsorted Arrays", "Names", "Weights");                 //Print unsorted arrays

            while (!done)
            {
                WriteLine();
                PutMenu();                                                              //Output menu
                choice = GetChoice();                                                   //Get menu choice from user and validate

                if (choice < '4')                                                       //Has the user chosen to search?
                {
                    CopyLists(nam, wght);                                               //Copy the unsorted arrays
                    Clear();
                    DoSort(choice);                                                     //Perform the chosen sort algorithm
                    oneSort = true;                                                     //User has performed a sort
                }
                else if (oneSort)                                                       //User has sorted, allow user to search
                {
                    done = true;                                                        //Terminate program after searching
                    Clear();
                    SearchArray();                                                      //Search the now sorted array
                }
                else                                                                    //User attempts to search without sorting
                {
                    WriteLine("You must perform a sort before searching");              //Error
                    ReadLine();
                    Clear();
                }
            }
        }

        static void OutLists(string[] n, int[] w, string tableName, string col1Name, string col2Name)
        {
            WriteLine(tableName.PadLeft(30));
            WriteLine();

            Write(col1Name.PadLeft(15));
            WriteLine(col2Name.PadLeft(15));

            WriteLine("=======================================");

            for (int i = 0; i < LSIZE; i++)
            {
                WriteLine(n[i].PadLeft(15) + w[i].ToString().PadLeft(15));
            }

            WriteLine("=======================================");
        }

        static void PutMenu()
        {
            WriteLine("Menu");
            WriteLine();
            WriteLine("1. Shell Sort");
            WriteLine("2. Insertion Sort");
            WriteLine("3. Selection Sort");
            WriteLine("4. Binary Search");
        }

        static char GetChoice()
        {
            char c;                             //User's choice
            string inChoice;                    //User's input
			
            inChoice = ReadLine();
			
			if (inChoice != "")					//Invalidate choice if string is empty
			{
				c = inChoice[0];
            }
			else
		    {
				c = '0';
			}
			
            while (c < '1' || c > '4')          //Validate choice
            {
                WriteLine("Invalid choice");
                inChoice = ReadLine();
                c = inChoice[0];
            }

            return c;
        }

        static void DoSort(char c)
        {
            switch (c)
            {
                //Perform the correct sort based on user's input, then output the sorted array
                case '1':
                    ShellSort(LSIZE);
                    OutLists(WKnam, WKwght, "Shell Sort Output", "Names", "Weight");
                    ReadLine();
                    break;

                case '2':
                    InsertSort(LSIZE);
                    OutLists(WKnam, WKwght, "Insertion Sort Output", "Names", "Weight");
                    ReadLine();
                    break;

                case '3':
                    SelectSort(LSIZE);
                    OutLists(WKnam, WKwght, "Select Sort Output", "Names", "Weight");
                    ReadLine();
                    break;
            }

            Clear();
        }

        static void CopyLists(string[] n, int[] w)
        {
            //Store copy of unsorted arrays
            WKnam = new string[LSIZE];
            WKwght = new int[LSIZE];

            for (int i = 0; i < LSIZE; i++)       //Populate work arrays
            {
                WKnam[i] = n[i];
                WKwght[i] = w[i];
            }
        }


        static void ShellSort(int n)
        {
            int[] gapList = new int[n - 1];
            int numGaps = GetGaps(ref gapList);

            int i = numGaps - 1;

            while (i >= 0)
            {
                int gap = gapList[i];
                int j = gap;

                while (j <= n - 1)
                {
                    string y = string.Copy(WKnam[j]);
                    int yW = WKwght[j];
                    int k = j - gap;
                    bool found = false;

                    while (k >= 0 && !found)
                    {
                        if (string.Compare(y, WKnam[k]) < 0)
                        {
                            WKnam[k + gap] = WKnam[k];
                            WKwght[k + gap] = WKwght[k];

                            k -= gap;
                        }
                        else
                        {
                            found = true;
                        }
                    }

                    WKnam[k + gap] = y;
                    WKwght[k + gap] = yW;

                    j++;
                }

                i--;
            }
        }

        static void InsertSort(int n)
        {
            int k = 1;

            while (k <= n - 1)
            {
                string y = string.Copy(WKnam[k]);
                int yW = WKwght[k];
                int i = k - 1;
                bool found = false;

                while (i >= 0 && !found)
                {
                    if (string.Compare(y, WKnam[i]) < 0)
                    {
                        WKnam[i + 1] = WKnam[i];
                        WKwght[i + 1] = WKwght[i];
                        i--;
                    }
                    else
                    {
                        found = true;
                    }
                }

                WKnam[i + 1] = y;
                WKwght[i + 1] = yW;
                k++;
            }
        }

        static void SelectSort(int n)
        {
            int i = n - 1;                                  //Amount of unsorted elements in the array

            while (i > 0)                                   //Unsorted elements remain
            {
                int where = 0;                              //Index where big was found
                string big = string.Copy(WKnam[0]);         //Biggest element in unsorted section
                int bigW = WKwght[0];                       //Keep the weights corresponding to the names
                int j = 1;                                  //Element to compare with big

                while (j <= i)                              //Have we reached the end of unsorted section?
                {
                    if (string.Compare(WKnam[j], big) > 0)  //Does the current element belong after big?
                    {
                        big = WKnam[j];                     //Biggest element found
                        bigW = WKwght[j];                   //Keep the weights corresponding to the names

                        where = j;                          //Store index of biggest element
                    }

                    j++;                                    //Next element in array
                }

                //Swap biggest element with the last element of the unsorted section
                WKnam[where] = WKnam[i];
                WKwght[where] = WKwght[i];

                //Keep the weights corresponding to the names
                WKnam[i] = big;
                WKwght[i] = bigW;

                i--;                                        //Unsorted section decreased
            }
        }

        static int GetGaps(ref int[] g)
        {
            int low = 0;
            int high = LSIZE - 1;
            int gap = 1;
            int pos = 0;

            while (gap < (high - low) + 1)
            {
                g[pos] = gap;
                gap *= 3;
                pos++;
            }

            return pos;
        }

        static int Bsrch(string p, int n)
        {
            int q = -1;					//Store index where search found (-1 if not found)
            int a = 0;					//Lower limit of array
            int b = n - 1;				//Upper limit of array

            while (a <= b)
            {
                int mid = (a + b) / 2; //Check mid point of array
                
                if(string.Compare(p.ToLower(), WKnam[mid].ToLower()) == 0) //Match found
                {
                    q = mid; 			//Store index where match found
                    a = n;				//Force loop exit
                }
                else if(string.Compare(p.ToLower(), WKnam[mid].ToLower()) < 0) //Is search higher or lower than mid?
                {
                    b = mid - 1;		//Change search section to above mid
                }
                else
                {
                    a = mid + 1;		//Change search section to below mid
                }
            }

            return q;
        }

        static void SearchArray()
        {
            string xnam;                                      //Stores user's search

            do
            {
                WriteLine("Enter the name you want to search (Enter nothing to quit)");
                xnam = ReadLine();
                int pos = Bsrch(xnam, LSIZE);                //Get the position of the user's search (if found)

                if (xnam != "")                              //Only search if the user has entered a name
                {
                    if (pos != -1)                           //Was the name found?
                    {
                        WriteLine(xnam + " was found at position " + pos + " and their body weight is " + WKwght[pos]); //Print found message
                    }
                    else
                    {
                        WriteLine(xnam + " was not found");  //Print not found message
                    }
                }

                ReadLine();

            } while (xnam != "");
        }
    }
}
