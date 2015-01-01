using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    // this class creates a Sudoku array pre-filled with valid numbers
    public class SudokuSolution : SudokuGrid
    {
        // keep track of what numbers we're tried at this square
        private List<int>[,] triesAvailable;
        private int currentX, currentY;

        // public methods
        public bool NoRandomize { get; set; }

        // private variables
        private Random rand = new Random();


        // Properties
        // If you try to set the SudokuArray via the "array interface" make sure you actually call SetSudokuArray instead
        new public int this[int row, int col]
        {
            get { return base[row, col]; }
            private set { SetSudokuArray(row, col, value); }
        }


        // constructor
        public SudokuSolution()
        {
            triesAvailable = new List<int>[SudokuSize, SudokuSize];
            NoRandomize = false;
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    SetSudokuArray(x, y, 0);
                }
            }
            currentX = 0;
            currentY = 0;
        }


        // constructor
        public SudokuSolution(SudokuGrid sudokuGrid) : this()
        {
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    SetSudokuArray(x, y, sudokuGrid[x, y]);
                }
            }
            
        }

        



        // Set an element of the array and, while we're at it, keep track of the numbers that have 
        // been tried so as to allow for backtracking if we get stuck
        private void SetSudokuArray(int x, int y, int value)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
                throw new Exception("Attempting to set a value out of Sudoku grid bounds");

            if (value < 0 || value > 9)
                throw new Exception("Attempting to set an illegal value: > 9 OR < 0");

            base[x, y] = value;
            // if we ever set value to 0, then initialize the tries back to starting with all possible options
            if (value == 0)
            {
                if (triesAvailable[x, y] == null)
                    triesAvailable[x, y] = new List<int>();
                else
                    triesAvailable[x, y].Clear();

                for (int i = 1; i <= SudokuSize; i++)
                {
                    triesAvailable[x, y].Add(i);
                }
            }
            else // else remove this value from the list of available numbers
            {
                triesAvailable[x, y].Remove(value);
            }
        }



        // Set an element of the array checking for conflicts. 
        // If the "set" was successful (i.e. no conflicts) returns true. Otherwise returns false.  
        private bool AttemptSetSudokuArray(int x, int y, int value)
        {
            SetSudokuArray(x, y, value);
            if (IsValid(x, y))
                return true;
            else
                return false;
        }


        // Advance one square. Returns true if you go past the grid boundaries
        private void NextSquare()
        {
            currentY++;

            if (currentY > 8)
            {
                currentY = 0;
                currentX++;
            }


            // TODO: need a better way to handle end of grid
            //if (currentX > 8)
            //    throw new Exception("Advanced beyond end of grid.");

        }


        // Backtrack one square. THrows error if you go past the grid boundaries
        private void BackSquare()
        {
            // reset current square
            SetSudokuArray(currentX, currentY, 0);

            // now go back one square
            currentY--;

            if (currentY < 0)
            {
                currentY = 8; //TODO: Magic number
                currentX--;
            }

            if (currentX < 0)
                throw new Exception("Advanced beyond beginning of grid.");
        }




        // This function kicks off filling up the SudokuArray
        public void FillSudokuSolution()
        {
            // TODO: should gracefully handle recalling this after it's filled in
            do
            {
                if (FindValidNumberFor(currentX, currentY)) // TODO: shouldn't need to pass these as parameters + this function should gracefully handle going over boundaries
                    NextSquare();
                else // couldn't find a valid number for current square, so go back one                    
                    BackSquare();
            }
            while (currentX < 9); // TODO magic number -- need a better way to detect we're filled in the grid and we're done


            if (IsValidArray() == false)
                throw new Exception("Could not find a valid configuration.");
        }



     
        // This function will attempt to find a valid number (out of the remaining valid numbers)
        // that could be put into x, y. If it fails to find a valid number because there wasn't one
        // it will return false. It returns True if it does find a valid number. 
        // If there are mulitple valid numbers, it picks one randomly. 
        private bool FindValidNumberFor(int x, int y)
        {
            bool succeed=false;
            int value, index;

            while (!succeed && triesAvailable[x, y].Count > 0)
            {
                // if we are out of tries, then we can't 
                // start with a randomly selected number that hasn't been tried yet            
                if (NoRandomize == true)
                    value = triesAvailable[x, y][0];
                else
                {
                    int upper = triesAvailable[x, y].Count;             
                    index = rand.Next(0, upper);
                    value = triesAvailable[x, y][index];
                }

                succeed = AttemptSetSudokuArray(x, y, value);
            }

            return succeed;
        }




        // this method will check if the x, y location contains a valid number (i.e. is not repeated in 
        // row, column, or 3x3 square
        public bool IsValid(int x, int y)
        {
            return IsValid(x, y, this[x, y]);
        }



        // this method will check if the x, y location contains a valid number (i.e. is not repeated in 
        // row, column, or 3x3 square
        private bool IsValid(int x, int y, int tryNumber)
        {
            // if tryNumber is not valid (i.e. 1 to 9) throw an error
            if (!(tryNumber >= 1 && tryNumber <= SudokuSize))
                throw new Exception("IsValid requires you try a number from 1 to 9");

            // look for repeat in row
            for (int i = 0; i < SudokuSize; i++)
            {
                // skip if this is the very row we are checking
                if (i == x)
                    continue;
                if (this[i, y] == tryNumber)
                    return false;
            }

            // look for repeat in column
            for (int j = 0; j < SudokuSize; j++)
            {
                // skip if this is the very column we are checking
                if (j == y)
                    continue;
                if (this[x, j] == tryNumber)
                    return false;
            }

            // look for repeat in 3x3 square
            // which 3x3 are we in
            int subSquareX = x / SudokuSubSquareSize;
            int subSquareY = y / SudokuSubSquareSize;

            for (int i = subSquareX * SudokuSubSquareSize; i < ( (subSquareX+1) * SudokuSubSquareSize); i++)
            {
                for (int j = subSquareY * SudokuSubSquareSize; j < ( (subSquareY+1) * SudokuSubSquareSize); j++)
                {
                    if (x == i && y == j)
                        continue;
                    if (this[i, j] == tryNumber)
                        return false;
                }
            }

            return true;
        }




        // This public method makes sure the Sudoku array is all filled in and every square is valid
        public bool IsValidArray()
        {
            return IsValidArray(true);
        }


        // This private method makes sure the Sudoku array is all valid so far (i.e. zeros are considered valid)
        public bool IsValidArray(bool isComplete)
        {
            // TODO: IsValidArray calls IsValid and therefore repeats checks many times more than needed. Fix this to be more efficient
            // and elegant

            int value;

            for (int x = 0; x < SudokuSize; x++)
                for (int y = 0; y < SudokuSize; y++)
                {
                    value = this[x, y];
                    // fail if not between 1 and 9 if this is supposed to be a complete grid
                    if (isComplete && !(value >= 1 && value <= SudokuSize))
                        return false;
                    else if (!isComplete && value == 0) // skip over zeros if not checking for completeness
                        continue;
                    else if (!IsValid(x, y))
                            return false;                   
                }

            return true;
        }

    }
}
