using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Sudoku
{

    public class SudokuPuzzleOld : SudokuGrid
    {
        // constants
        private const int numSudokuSquares = 9;
        private const int minimumUncovered = 15;

        // private variables
        public SudokuSolution Solution { get; private set; }
        private Random rand = new Random();

        private int[,] UncoverCountBySubSquare { get; set; }

        public SudokuSolver.Difficulty DifficultyLevel { get; set; } // TODO: Shouldn't this be in this class and the solver has to reference it? 


        // constructor
        public SudokuPuzzleOld()
        {
            ConstructPuzzle(false);
        }


        // allows for setup of puzzle but without any uncovers yet
        public SudokuPuzzleOld(bool empty)
        {
            ConstructPuzzle(empty);
        }

        
        // This function does the real work of the constructor
        private void ConstructPuzzle(bool empty)
        {
            // create a puzzle
            // set to empty
            InitializePuzzle();

            if (empty == false)
            {
                // create a solution
                Solution = new SudokuSolution();
                Solution.FillSudokuSolution();

                // See if this is a solveable puzzle
                FindSolveablePuzzle();
            }

        }

        public void SetSolution(SudokuSolution solution)
        {
            // check that this is a valid solution
            for (int x=0; x < SudokuSize; x++)
                for (int y = 0; y < SudokuSize; y++)
                {
                    if (solution[x, y] < 1 || solution[x, y] > 9)
                        throw new Exception("Solution passed to SetSolution is not valid.");
                }

            Solution = solution;            
        }



        // Write out the solution. Usually used for Debugging purposes.
        public void WriteOutSudokuSolution()
        {
            Solution.WriteOutSudokuGrid();
        }


        // This function will loop using various random strategies until it finds a solveable puzzle 
        // with an assessible difficulty level
        private void FindSolveablePuzzle()
        {
            // select initial# of items to uncover
            int initialUncovered = rand.Next(minimumUncovered, minimumUncovered+minimumUncovered);
            UncoverPuzzle(initialUncovered);
            // loop through assessing difficulty and adding additional uncovers;
            do
            {
                //Debug.WriteLine("Before Assess Difficulty:");
                //WriteOutSudokuGrid();

                AssessDifficulty();
                if (DifficultyLevel == SudokuSolver.Difficulty.UNSOLVEABLE)
                {
                    // the problem is unsolveable so far, so pick a strategy to fix it
                    // TODO: use all strategies and not just 1 and 2
                    int strategy = rand.Next(1, 3);
                    switch(strategy)
                    {
                        case 1:
                        case 2:
                            // strategy 1 - just add one uncover and try again
                            UncoverPuzzle(1);
                        break;
                            
                        case 3:
                            // strategy 2 - add 5 uncovers evenly distributed across subsquares
                            UncoverPuzzle(5);
                        break;

                        case 4:
                            // strategy 3 - uncover 2 to 6 additional squares, but just randomly across the whole puzzle
                            UncoverPuzzle(rand.Next(2, 6), false);
                        break;
                        
                    }
                }
            }
            while (DifficultyLevel == SudokuSolver.Difficulty.UNSOLVEABLE);
        }



        // Initialize puzzle (fill in all zeros)
        private void InitializePuzzle()
        {
            if (UncoverCountBySubSquare == null)
                UncoverCountBySubSquare = new int[SudokuSubSquareSize, SudokuSubSquareSize];


            // initialize SudokuGrid
            for (int x=0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    this[x, y] = 0;
                }
            }

            // initialize count of uncovers
            for (int x = 0; x < SudokuSubSquareSize; x++)
            {
                for (int y = 0; y < SudokuSubSquareSize; y++)
                {
                    UncoverCountBySubSquare[x, y] = 0;
                }
            }
        }


        private void UncoverPuzzle(int newUncovers)
        {
            UncoverPuzzle(newUncovers, true);
        }


        // evenly distribute uncovers into sub-squares
        private void UncoverPuzzle(int newUncovers, bool distributeEvenly)
        {
            // count the number of actual uncovers
            int initialTotalCount, totalUncovers, requestedTotalUncovers;
            initialTotalCount = totalUncovers = TotalUncovers;
            requestedTotalUncovers = initialTotalCount + newUncovers;

            // get an approximate number of uncovers per square already
            int averagePerSquare = requestedTotalUncovers / numSudokuSquares;
            bool success = false;

            // only distribute evenly if requested (or by default)
            if (distributeEvenly == true)
            {

                // loop through each sub square -- if the total uncovers in this subsquare is less than 1 or
                // under the expected average (+/- 1 at random) then add enough to get it up to the average (+/- 1)
                for (int subSquareX = 0; subSquareX < SudokuSubSquareSize; subSquareX++)
                {
                    for (int subSquareY = 0; subSquareY < SudokuSubSquareSize; subSquareY++)
                    {

                        // Break out prematurely if we have reached our total number of requested uncovers.
                        // This can happen if the random effect causes us to head towards going slightly over
                        if (totalUncovers >= requestedTotalUncovers)
                            break;

                        // uncover "perSquare" number for square. Note: due to round errors, this might not be quite all of them

                        int uncoversInSubSquare = UncoverCountBySubSquare[subSquareX, subSquareY];

                        int target = averagePerSquare - uncoversInSubSquare;

                        // TODO: target can be negative and it always fills in subsquare 1 - bad all around
                        if (target <= 0)
                            continue;

                        // apply random factor 50% of the time
                        if (rand.Next(0, 2) == 1)
                            target = target + rand.Next(-1, 2);


                        // minimum of 1
                        if (target <= 0)
                            target = 1;

                        // but do not go over the requested amount
                        if (target + totalUncovers > requestedTotalUncovers)
                            target = requestedTotalUncovers - totalUncovers;

                        UncoverInSubSquare(target, subSquareX, subSquareY);

                        totalUncovers += target;

                    }
                }
            }

            // We have either evenly distributed our uncovers by sub squares and have a few left
            // or the call specifically requested we make this a random distrubtion instead 
            // of an even one.  
            if (totalUncovers < requestedTotalUncovers)
            {
                for (; totalUncovers < requestedTotalUncovers; totalUncovers++)
                // uncover from solution -- loop attempting to uncover until a blank square is found                    
                do
                {
                    // select an x,y placement for this sub square
                    int x = rand.Next(0, SudokuSize);
                    int y = rand.Next(0, SudokuSize);

                    success = UncoverSquare(x, y);
                }
                while (success == false);
            }
        }


        
        
        private void UncoverInSubSquare(int target, int subSquareX, int subSquareY)
        {

            bool success = false;

            for (int i=0; i < target; i++)
            {
                // uncover from solution -- loop attempting to uncover until a blank square is found                    
                do
                {
                    // select an x,y placement for this sub square
                    int subX = rand.Next(0, SudokuSubSquareSize);
                    int subY = rand.Next(0, SudokuSubSquareSize);

                    // convert to absolute coordinates for the whole grid
                    int x = (subSquareX * SudokuSubSquareSize) + subX;
                    int y = (subSquareY * SudokuSubSquareSize) + subY;

                    success = UncoverSquare(x, y);
                }
                while (success == false);
            }
        }


        private bool UncoverSquare(int x, int y)
        {
            if (Solution == null)
                throw new Exception("Do not attempt to uncover squares if you have no Solution specified.");

            // if this square is covered, then uncover it. Otherwise report that we failed to uncover it
            if (this[x, y] == 0)
            {
                this[x, y] = Solution[x, y];

                // keep count of how many are in this sub square
                // convert x, y to which square we are in
                int subSquareX = x / SudokuSubSquareSize;
                int subSquareY = y / SudokuSubSquareSize;
                UncoverCountBySubSquare[subSquareX, subSquareY]++;

                return true;
            }
            else
                return false;
        }


        public void AssessDifficulty()
        {            
            // TODO: assess difficulty on a clone so that I don't get side effects


            SudokuSolver solver = new SudokuSolver(this.Clone());
            
/*            
#if DEBUG
                Debug.WriteLine("Solution:");
                Solution.WriteOutSudokuGrid();
                Debug.WriteLine("Puzzle:");
                this.WriteOutSudokuGrid();
#endif
*/            

            solver.GetSolution();            
            DifficultyLevel = solver.DifficultyLevel;
        }


        // make a copy of this puzzle so that the solver (or whoever) can work on it without side effects
        public SudokuPuzzleOld Clone()
        {
            SudokuPuzzleOld newPuzzle = new SudokuPuzzleOld(true);

            for (int x=0; x < numSudokuSquares; x++)
                for (int y = 0; y < numSudokuSquares; y++)
                {
                    newPuzzle[x, y] = this[x, y];
                }

            return newPuzzle;
        }



        public SudokuGrid GetSolution()
        {

            SudokuSolver solver = new SudokuSolver(this);

            return solver.GetSolution();
        }

    }
}
