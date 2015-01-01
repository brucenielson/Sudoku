using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Sudoku
{

 /*
 * How do I want to redesign this? 
 * Init: Start with a number of uncovers 15-30
 * Uncovers automatically change guesses which synch with puzzle from now on
 * Use strategies to solve puzzle - limit to max difficulty
 * Strategy 1 (Easy) - Singletons
 * Strategy 2 (Medium) - Two of a kind (Pairs part 1)
 * Strategy 3 (Medium) - Pairs (pairs part 2)
 * Strategy 4 (Hard) - Eliminate Bad Guesses
 * Exception - After each step, check the number of remaining unfilled squares
 * If passed some threshold (perhaps half uncovered), then allow a number of elimination of bad guesses even if 
 * Max difficulty is not set to hard
 * Easy - eliminate 1 bad guess - 1 per cycle
 * Medium - eliminate up to 3 bad guesses - 1 per cycle
 * Hard - eliminate as many bad guesses as possible
 * 
 * If puzzle is still unsolveable, then select a new square to uncover
 * Select square such that it minimizes guess elimination in squares already solved by a previous strategy
 * Uncover automatically eliminates guesses
 * Then repeat above strategies until the puzzle is solveable - Done
 * 
 * Set it up so that the puzzle can be solved and uncovered in steps that can be displayed in WPF
 * Or automatically makes a solveable puzzle
 * I will no longer have a solver class
 */

    public class SudokuPuzzle : SudokuGrid, INotifyPropertyChanged
    {
        // constants
        private const int numSudokuSquares = 9;
        //private const int minimumUncovered = 20;

        // enums
        public enum Difficulty : int { UNDETERMINED, TRIVIAL, EASY, MEDIUM, HARD, UNSOLVEABLE };

        // private variables
        private SudokuSolution solutionGrid;        
        private SudokuSolution Solution 
        {
            get
            {
                return solutionGrid;
            }
            set
            {
                solutionGrid = value;
                if (solutionGrid != null)
                {
                    for (int x = 0; x < SudokuSize; x++)
                    {
                        for (int y = 0; y < SudokuSize; y++)
                        {
                            this.Grid[x, y].SolutionValue = solutionGrid[x, y];
                        }
                    }
                }
            }
        }

        private SudokuGridLocation.Step CurrentStep { get; set; }
        public SudokuPuzzle LastPuzzleState { get; set; }
        private SudokuPuzzle ClonePuzzle { get; set; }
        private int UncoverX { get; set; }
        private int UncoverY { get; set; }

        private Random rand = new Random();

        public SudokuPuzzle.Difficulty DifficultyLevel { get; set; }
        //public SudokuPuzzleNew.Difficulty MaxDifficulty { get; set; }


        #region constructors

        // constructor
        public SudokuPuzzle()
        {
            ConstructPuzzle(false);
        }


        // allows for setup of puzzle but without any uncovers yet
        public SudokuPuzzle(bool empty)
        {
            ConstructPuzzle(empty);
        }


        #endregion


        #region methods

        // String status
        public string Status
        {
            get
            {
                string output = "";
                SudokuGridLocation.Step tempStep = CurrentStep;
                if (tempStep <= SudokuGridLocation.Step.TwoOfAKind)
                    output = output + "Current Step: " + CurrentStep.ToString() + " / ";
                else if (tempStep == SudokuGridLocation.Step.TwoOfAKind+1)
                    output = output + "Current Step: Smart Uncover (" + UncoverX.ToString() + "," + UncoverY.ToString() + ") / ";
                if (ClonePuzzle != null)
                    output = output + "Initial Uncovers: " + ClonePuzzle.TotalUncovers.ToString() + " / ";
                output = output + "Total Uncovers: " + TotalUncovers.ToString() + " / ";
                output = output + "Total Guesses: " + TotalGuesses().ToString() + " ";
                return output;
            }
        }

        public string PuzzleInfo
        {
            get
            {
                string output = "";
                output = output + "Difficulty: " + DifficultyLevel.ToString() + " / ";
                output = output + "Initial Uncovers: " + ClonePuzzle.TotalUncovers.ToString();
                return output;
            }
        }

        
        
        // This function does the real work of the constructor
        private void ConstructPuzzle(bool empty)
        {
            // create a puzzle
            // set to empty
            ClonePuzzle = null;
            LastPuzzleState = null;
            CurrentStep = SudokuGridLocation.Step.None;
            
            // initialize SudokuGrid
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    Grid[x, y].ResetLocation();
                }
            }
            InitializePuzzle();

            if (empty == false)
            {
                // create a solution
                SudokuSolution solution = new SudokuSolution();                
                solution.FillSudokuSolution();
                Solution = solution;

                // Fill in solution so it's easy to view in WPF


                // Make a puzzle 
                // FindSolveablePuzzle(); // i.e. do it all at once
                
                // Start making a puzzle
                //int min = minimumUncovered;
                //int max = minimumUncovered + (minimumUncovered / 2); // between 15 and 22

                bool retry;
                do
                {
                    retry = false;
                    try
                    {

                        // Initial Random Uncover that balances by row and column
                        //RandomlyUncoverSquares();
                        // Loop through and do smart uncovers until we have a pretty good mix
                        do
                        {
                            SmartUncover();
                        }
                        while (!ReadyToStart());
                    }
                    catch
                    {
                        // we failed to get to a start point, so try again
                        InitializePuzzle();
                        retry = true;
                    }
                }
                while (retry);

                // Make Reset Puzzle
                ClonePuzzle = Clone();
            }
        }



        private bool ReadyToStart()
        {
            // The grid is ready to start if it matches these conditions:
            // < 25 initial uncovers
            // < 200 remaining guesses
            if (TotalGuesses() < 180 && TotalUncovers < 25)
                return true;
            else if (TotalUncovers >= 25)
                throw new Exception("This Grid will never be ready.");
            else
                return false;
        }


        private bool Phase1()
        {
            try
            {
                ReadyToStart();
            }
            catch
            {
                return false;
            }

            return true;
        }


        private int TotalGuesses()
        {
            int count = 0;
            for (int i = 0; i < SudokuSize; i++)
            {
                for (int j = 0; j < SudokuSize; j++)
                {
                    count = count + Grid[i, j].GetAllGuesses().Count();
                }
            }

            return count;
        }


       
        private void RandomlyUncoverSquares()
        {
            // Handle rows and columns - evenly distribute an initial amount
            for (int i = 0; i < SudokuSize; i++)
            {
                int nbrUncovers = rand.Next(1, 1);
                int j=0;

                for(j=0; j < nbrUncovers; j++)
                    RowRandomUncover(i);

                nbrUncovers = rand.Next(1, 1);
                for (j = 0; j < nbrUncovers; j++)                
                    ColumnRandomUncover(i);
            }

        }


        private void RowRandomUncover(int row)
        {
            int tryCol, start;
            tryCol = start = rand.Next(0, SudokuSize);

            // Starting with tryUncover, go through the row until
            // we find an uncover to do that will not put the row and column 
            // uncover count above 4
            do
            {
                int rowUncoverCount = GetRowUncoverCount(row);
                int colUncoverCount = GetColumnUncoverCount(tryCol);

                if (rowUncoverCount < 2 && colUncoverCount < 2)
                {                   
                    UncoverSquare(row, tryCol);
                    return;
                }

                tryCol = (tryCol + 1) % SudokuSize;
            }
            while (tryCol != start);
        }


        private void ColumnRandomUncover(int column)
        {
            int tryRow, start;
            tryRow = start = rand.Next(0, SudokuSize);

            do
            {
                int rowUncoverCount = GetRowUncoverCount(tryRow);
                int colUncoverCount = GetColumnUncoverCount(column);

                if (rowUncoverCount < 2 && colUncoverCount < 2)
                {
                    UncoverSquare(tryRow, column);
                    return;
                }

                tryRow = (tryRow + 1) % SudokuSize;
            }
            while (tryRow != start);
        }



        private int GetRowUncoverCount(int row)
        {
            int count = 0;
            for (int i = 0; i < SudokuSize; i++)
            {
                if (Grid[row, i].Value != 0)
                    count++;
            }

            return count;
        }


        private int GetColumnUncoverCount(int column)
        {
            int count = 0;
            for (int i = 0; i < SudokuSize; i++)
            {
                if (Grid[i, column].Value != 0)
                    count++;
            }

            return count;
        }


        /*
        private void RandomUncover(List<SudokuGridLocation> container)
        {
            int countUncovers = 0;
            int uncoverCount = 0;
            int numUncovers = rand.Next(2, 4);
            // 25% chance of one less or one more
            if (rand.Next(1, 6) == 1)
                numUncovers--;
            else if (rand.Next(1, 6) == 1)
                numUncovers++;

            List<SudokuGridLocation> available;

            for (int i = 0; i < container.Count(); i++)
            {
                if (container[i].Value != 0)
                    countUncovers++;
            }

            if (countUncovers < random)
            {
                // loop through randomly uncovering items
                int neededUncovers = random - countUncovers;
                for (int i = 0; i < neededUncovers; i++)
                {
                    int pick;
                    available = container.Where((location) => location.Value == 0).ToList();
                    pick = rand.Next(0, available.Count());
                    UncoverSquare(container[pick]);
                }
            }
        }*/


        // Randomly Uncover a square -- used for initial uncovers
        private bool RandomlyUncoverSquare()
        {
            int x = rand.Next(0, SudokuSize - 1);
            int y = rand.Next(0, SudokuSize - 1);
            return UncoverSquare(x, y);
        }


        private void ResetGrid(SudokuPuzzle resetPuzzle)
        {
            for (int x=0; x < numSudokuSquares; x++)
                for (int y = 0; y < numSudokuSquares; y++)
                {
                    Grid[x, y].CopyLocation(resetPuzzle.Grid[x,y]);
                }

            /*
            Solution = resetPuzzle.Solution;
            LastPuzzleState = resetPuzzle.LastPuzzleState;
            ClonePuzzle = resetPuzzle.ClonePuzzle;
            CurrentStep = resetPuzzle.CurrentStep;
            DifficultyLevel = resetPuzzle.DifficultyLevel;
            MaxDifficulty = resetPuzzle.MaxDifficulty;
            UncoverX = resetPuzzle.UncoverX;
            UncoverY = resetPuzzle.UncoverY;
             */
        }


        // Do Smart Uncover: Find an uncover that won't affect previous efforts
        public void SmartUncover()
        {
            if (Phase1())
            {
                // The new approach to smart uncover is to run through every available square and count how many guesses we'd remove for uncovering that square
                // Then pick the uncover that eliminates the most guesses
                int currentGuessCount = TotalGuesses();
                int possible, max = 0;
                List<SudokuGridLocation> listOfMaxs = new List<SudokuGridLocation>();

                SudokuPuzzle tempPuzzle;

                for (int x = 0; x < SudokuSize; x++)
                {
                    for (int y = 0; y < SudokuSize; y++)
                    {
                        tempPuzzle = Clone();
                        tempPuzzle.UncoverSquare(x, y);
                        possible = currentGuessCount - tempPuzzle.TotalGuesses();
                        if (possible == max)
                        {
                            listOfMaxs.Add(Grid[x, y]);
                        }
                        if (possible > max)
                        {
                            listOfMaxs.Clear();
                            max = possible;
                            listOfMaxs.Add(Grid[x, y]);
                        }
                    }
                }


                int pick = rand.Next(0, listOfMaxs.Count());
                SudokuGridLocation location = listOfMaxs[pick];
                if (ClonePuzzle == null)
                    UncoverSquare(location);
                else
                {
                    ResetGrid(ClonePuzzle);
                    UncoverSquare(location);
                    // Make new reset point
                    ClonePuzzle = Clone();
                }
            }
            else
            {
                SudokuGridLocation location = GetBestUncoverCheap();
                if (location != null)
                {
                    int x, y;
                    GetXYCoordinates(location, out x, out y);
                    ResetGrid(ClonePuzzle);
                    UncoverSquare(x, y);
                    // Make new reset point
                    ClonePuzzle = Clone();

                    // For status, save off x and y that were uncovered
                    UncoverX = x;
                    UncoverY = y;
                }
            }
        }


        private SudokuGridLocation GetBestUncoverCheap()
        {
            List<SudokuGridLocation> list;
            List<SudokuGridLocation> availableSquares = Grid.Cast<SudokuGridLocation>().ToList(); // cool way to cast an array to a list!
            int minScore = 90;
            SudokuGridLocation minLocation = null;

            list = availableSquares.Where((location) => location.HighestStep < SudokuGridLocation.Step.Singletons && location.Value == 0).ToList();
            if (list.Count() == 0)
                list = availableSquares.Where((location) => location.HighestStep < SudokuGridLocation.Step.Pairs && location.Value == 0).ToList();

            if (list.Count() == 0)
                list = availableSquares.Where((location) => location.HighestStep < SudokuGridLocation.Step.TwoOfAKind && location.Value == 0).ToList();

            if (list.Count() == 0)
                list = availableSquares.Where((location) => location.Value == 0).ToList();

            foreach (SudokuGridLocation location in list)
            {
                int x, y;
                GetXYCoordinates(location, out x, out y);
                int score = HowManyNearBySingletons(x, y);
                if (score < minScore)
                {
                    minScore = score;
                    minLocation = location;
                }
            }
            return minLocation;
        }


        /*
        private SudokuGridLocation GetBestUncoverExpensive()
        {

        }
         */

        private void DoUncover(List<SudokuGridLocation> list)
        {
            int x, y;
            int toUncover = rand.Next(0, list.Count());
            SudokuGridLocation location = list[toUncover];
            GetXYCoordinates(location, out x, out y);
            ResetGrid(ClonePuzzle);
            UncoverSquare(x, y);
            // Make new reset point
            ClonePuzzle = Clone();

            // For status, save off x and y that were uncovered
            UncoverX = x;
            UncoverY = y;
        }



        /*
        private List<SudokuGridLocation> GetListOfLocationsNotNearSingletons(SudokuGridLocation.Step step)
        {
            int x, y;
            List<SudokuGridLocation> result = new List<SudokuGridLocation>();

            // If this is Uncovered, Single Guess, or Singleton, are there any that are not in the same row, subsquare, or column as a Pair or Two of a Kind?
            if (!(step == SudokuGridLocation.Step.Singletons || step == SudokuGridLocation.Step.Pairs || step == SudokuGridLocation.Step.TwoOfAKind))
            {
                // loop through all of this kind and see if any are not in a row, column, or subsquare of Pair or Two of a Kind
                for (x = 0; x < SudokuSize; x++)
                {
                    for (y = 0; y < SudokuSize; y++)
                    {
                        if (Grid[x, y].HighestStep == step || (step == SudokuGridLocation.Step.None && Grid[x,y].AvoidUncover == true))
                        {
                            if (Grid[x, y].Value == 0 && IsNotNearSingletons(x, y))
                            {
                                // we have a winner
                                result.Add(Grid[x, y]);
                            }
                        }
                    }
                }

                return result;
            }
            else
                throw new Exception("You can't ask to avoid pairs if 'step' is Pairs or TwoOfAKind");
        }*/


        private int HowManyNearBySingletons(int x, int y)
        {
            int score = 0;
            // check row and column
            for (int i=0; i < SudokuSize; i++)
            {
                if (i != y && Grid[x, i].HighestStep >= SudokuGridLocation.Step.Singletons)
                {
                    // we have near singleton or above
                    score++;
                }
                else if (i != x && Grid[i, y].HighestStep >= SudokuGridLocation.Step.Singletons)
                {
                    // we have near singleton or above
                    score++;
                }
            }

            // check subsquare
            int subSquareX = x / SudokuSubSquareSize;
            int subSquareY = y / SudokuSubSquareSize;

            // loop through this particular subsquare and return the result
            int startRow = subSquareX * SudokuSubSquareSize;
            int endRow = startRow + SudokuSubSquareSize;
            int startCol = subSquareY * SudokuSubSquareSize;
            int endCol = startCol + SudokuSubSquareSize;

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startCol; j < endCol; j++)
                {
                    // we have near singleton or above
                    score++;
                }
            }

            return score;
        }


        private void RandomlyUncoverInSubSquare(int subSquareX, int subSquareY)
        {

            bool isUncovered = false;

            // uncover from solution -- loop attempting to uncover until a blank square is found                    
            do
            {
                // select an x,y placement for this sub square
                int subX = rand.Next(0, SudokuSubSquareSize);
                int subY = rand.Next(0, SudokuSubSquareSize);

                // convert to absolute coordinates for the whole grid
                int x = (subSquareX * SudokuSubSquareSize) + subX;
                int y = (subSquareY * SudokuSubSquareSize) + subY;

                isUncovered = UncoverSquare(x, y);
            }
            while (isUncovered == false);
        }




        public void SetSolution(SudokuSolution solution)
        {
            // check that this is a valid solution
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    if (solution[x, y] < 1 || solution[x, y] > 9)
                        throw new Exception("Solution passed to SetSolution is not valid.");
                }
            }
            Solution = solution;            
        }



        private int UncoverCountBySubSquare(int subSquareX, int subSquareY)
        {
            // This function takes a row and column 1-3 representing each of 9 subsquares
            // and it returns the number of uncovers in that subsquare
            if (subSquareX > SudokuSubSquareSize - 1 || subSquareX < 0 || subSquareY > SudokuSubSquareSize - 1 || subSquareY < 0)
                throw new Exception("Sub Squares number only 0 to 2");

            // loop through this particular subsquare and return the result
            int startRow = subSquareX * SudokuSubSquareSize;
            int endRow = startRow + SudokuSubSquareSize;
            int startCol = subSquareY * SudokuSubSquareSize;
            int endCol = startCol + SudokuSubSquareSize;

            int count = 0;

            for (int x = startRow; x < endRow; x++)
            {
                for (int y = startCol; y < endCol; y++)
                {
                    if (this[x, y] != 0)
                        count++;
                }
            }

            return count;
        }

        
        // Write out the solution. Usually used for Debugging purposes.
        public void WriteOutSudokuSolution()
        {
            Solution.WriteOutSudokuGrid();
        }


        // This function will loop using various random strategies until it finds a solveable puzzle 
        // with an assessible difficulty level
        public Difficulty FindSolveablePuzzle()
        {
            // Loop through strategies until a solveable puzzle is found
            // If this function is called, there are no steps that can be viewed in WPF
            while(!CheckForSolution())
            {
                if (!SolveAsFarAsPossible())
                    DoARoundOfSolves(true);
            }

            // Assess difficulty
            List<SudokuGridLocation> list = Grid.Cast<SudokuGridLocation>().ToList();
            // How many singletons
            int singletons = list.Where((location) => location.HighestStep == SudokuGridLocation.Step.Singletons).Count();
            // How many pairs or two of a kind
            int pairs = list.Where((location) => location.HighestStep == SudokuGridLocation.Step.Pairs).Count();
            pairs = pairs + list.Where((location) => location.HighestStep == SudokuGridLocation.Step.TwoOfAKind).Count();

            Difficulty result=Difficulty.UNDETERMINED;
            if (singletons == 0 && pairs == 0)
                result = Difficulty.EASY;
            else if (singletons > 0 && pairs == 0)
                result = Difficulty.MEDIUM;
            else if (pairs > 0)
                result = Difficulty.HARD;

            // Reset to last point
            ResetGrid(ClonePuzzle);

            DifficultyLevel = result;

            return result;
        }



        public void FindSolveablePuzzle(Difficulty difficulty)
        {
            bool makeEasier = false;

            if (difficulty == Difficulty.TRIVIAL)
            {
                difficulty = Difficulty.EASY;
                makeEasier = true;
            }

            while (FindSolveablePuzzle() != difficulty)
            {
                ConstructPuzzle(false);
            }

            if (makeEasier == true)
            {
                SmartUncover();
                SmartUncover();
                SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.Singletons;
                DifficultyLevel = Difficulty.TRIVIAL;
            }
        }



        public void FindAnySolveablePuzzle(Difficulty desiredDifficulty)
        {
            while (FindSolveablePuzzle() < desiredDifficulty)
            {
                ConstructPuzzle(false);
            }
        }


        // Initialize puzzle (fill in all zeros)
        private void InitializePuzzle()
        {
            // initialize SudokuGrid
            for (int x=0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    // initialize grid
                    this[x, y] = 0;

                    
                    Grid[x, y].Top = Grid[x, y].Left = Grid[x, y].Right = Grid[x, y].Bottom = false;

                    if (x%3 == 0)
                        Grid[x, y].Top = true;

                    if (x%3 == 2)
                        Grid[x, y].Bottom = true;

                    if (y%3 == 0)
                        Grid[x, y].Left = true;

                    if (y%3 == 2)
                        Grid[x, y].Right = true;
                    
                }
            }
        }



             
        // Uncovers a specific square
        private bool UncoverSquare(int x, int y)
        {
            if (Solution == null)
                throw new Exception("Do not attempt to uncover squares if you have no Solution specified.");

            // if this square is covered, then uncover it. Otherwise report that we failed to uncover it
            if (this[x, y] == 0)
            {
                SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.OnUncovered;
                this[x, y] = Solution[x, y];
                //Grid[x, y].HighestStep = SudokuGridLocation.CurrentStep;
                //Grid[x, y].OnPropertyChanged("HighestStep");
                EliminateGuesses(x, y);

                return true;
            }
            else
                return false;
        }



        // Uncovers a specific square, but by SudokuGridLocation
        private bool UncoverSquare(SudokuGridLocation location)
        {
            if (Solution == null)
                throw new Exception("Do not attempt to uncover squares if you have no Solution specified.");

            // if this square is covered, then uncover it. Otherwise report that we failed to uncover it
            if (location.Value == 0)
            {
                SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.OnUncovered;
                location.Value = location.SolutionValue;
                EliminateGuesses(location);

                return true;
            }
            else
                return false;
        }



        // make a copy of this puzzle so that the solver (or whoever) can work on it without side effects
        public SudokuPuzzle Clone()
        {
            SudokuPuzzle newPuzzle = new SudokuPuzzle(true);

            for (int x=0; x < numSudokuSquares; x++)
                for (int y = 0; y < numSudokuSquares; y++)
                {
                    newPuzzle.Grid[x, y].CopyLocation(Grid[x, y]);
                }

            newPuzzle.Solution = Solution;
            newPuzzle.LastPuzzleState = LastPuzzleState;
            newPuzzle.ClonePuzzle = ClonePuzzle;
            newPuzzle.CurrentStep = CurrentStep;
            //newPuzzle.DifficultyLevel = DifficultyLevel;
            //newPuzzle.MaxDifficulty = MaxDifficulty;
            newPuzzle.UncoverX = UncoverX;
            newPuzzle.UncoverY = UncoverY;

            return newPuzzle;
        }


        #endregion


        // Eliminate Guesses after Uncovering a Square
        protected override void EliminateGuesses(int x, int y)
        {
            // This function eliminates potential guesses by simply looking at what numbers have already been uncovered
            // and then elimating those as possible guesses in the same row, column, and subsquare
            // This function is called every single time an uncover takes place
            Grid[x, y].RemoveAllGuesses();

            // now clear guesses in related row, column, subsquare
            int value = this[x, y];
            ClearGuesses(value, x, y);
        }



        // Eliminate Guesses after Uncovering a Square
        private void EliminateGuesses(SudokuGridLocation location)
        {
            // This function eliminates potential guesses by simply looking at what numbers have already been uncovered
            // and then elimating those as possible guesses in the same row, column, and subsquare
            // This function is called every single time an uncover takes place
            location.RemoveAllGuesses();

            // now clear guesses in related row, column, subsquare
            int x, y;
            GetXYCoordinates(location, out x, out y);
            int value = this[x, y];
            ClearGuesses(value, x, y);
        }


        // If you pass a SudokuGridLocation from Grid in, it will pass out (via out parameters) the X and Y Coodinate equivalent
        private bool GetXYCoordinates(SudokuGridLocation location, out int x, out int y)
        {
            x = y = 0;
            for(x=0; x < SudokuSize; x++)
                for(y=0; y < SudokuSize; y++)
                    if (Grid[x, y] == location)
                        return true;

            return false;
        }


        // Clear Guess for one value
        // The 'value' passed in is the value to clear.
        // The x, y is the grid location where the value was found, so it won't be cleared itself
        private void ClearGuesses(int value, int x, int y)
        {
            // remove in row
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
#if DEBUG
                int prev = Grid[i,y].GuessesCount();
#endif

                if (x != i)
                    Grid[i, y].RemoveGuess(value);

#if DEBUG
                if (Grid[i, y].GuessesCount() == 0 && this[i, y] == 0 && prev > 0)
                    throw new Exception("It should never be possible to eliminate all guesses, except by finding a value to fill in.");
#endif
            }
            // remove in column
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
#if DEBUG
                int prev = Grid[x, i].GuessesCount();
#endif

                if (i != y)
                    Grid[x, i].RemoveGuess(value);

#if DEBUG
                if (Grid[x, i].GuessesCount() == 0 && this[x, i] == 0 && prev > 0)
                    throw new Exception("It should never be possible to eliminate all guesses, except by finding a value to fill in.");
#endif
            }

            // remove in sub square
            int subSquareX = x / SudokuGrid.SudokuSubSquareSize;
            int subSquareY = y / SudokuGrid.SudokuSubSquareSize;


            for (int i = subSquareX * SudokuGrid.SudokuSubSquareSize; i < ((subSquareX + 1) * SudokuGrid.SudokuSubSquareSize); i++)
            {
                for (int j = subSquareY * SudokuGrid.SudokuSubSquareSize; j < ((subSquareY + 1) * SudokuGrid.SudokuSubSquareSize); j++)
                {
                    int prev = Grid[i, j].GuessesCount();

                    if (!(x == i && y == j))
                        Grid[i, j].RemoveGuess(value);

#if DEBUG
                    if (Grid[i, j].GuessesCount() == 0 && this[i, j] == 0 && prev > 0)
                        throw new Exception("It should never be possible to eliminate all guesses, except by finding a value to fill in.");
#endif
                }
            }

        }


        // Try the next step in trying to uncover items - returns true if something changed
        public bool DoARoundOfSolves(bool includeUncovers)
        {
            bool result = false;
            // Back up before making step
            SudokuPuzzle tempClone = Clone();
            if (LastPuzzleState != null)
                tempClone.LastPuzzleState = LastPuzzleState.Clone();
            tempClone = Clone();

            if (includeUncovers == true)
            {
                // reset all changed properties
                SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.None;
                for (int x = 0; x < SudokuSize; x++)
                    for (int y = 0; y < SudokuSize; y++)
                        Grid[x, y].ResetChangedIndicator();
            }

            if (CurrentStep > SudokuGridLocation.Step.TwoOfAKind || CurrentStep < SudokuGridLocation.Step.SingleGuesses)
            {
                CurrentStep = SudokuGridLocation.Step.SingleGuesses;
            }


            do
            {
                switch (CurrentStep)
                {
                    case SudokuGridLocation.Step.SingleGuesses:
                        result = Step1SingleGuesses();
                        break;
                    case SudokuGridLocation.Step.Singletons:
                        result = Step2Singletons();
                        break;
                    case SudokuGridLocation.Step.Pairs:
                        result = Step3Pairs();
                        break;
                    case SudokuGridLocation.Step.TwoOfAKind:
                        result = Step4TwoOfAKind();
                        break;
                }

                if (result == false) // if nothing changed, move to next step
                    CurrentStep++;
                else // Because something changed, we start over next time
                {
                    CurrentStep = SudokuGridLocation.Step.SingleGuesses;
                    LastPuzzleState = tempClone;
                }
            }
            while (result == false && CurrentStep <= SudokuGridLocation.Step.TwoOfAKind);

            // if nothing changed (and we are out of options to try, do a smart uncover
            if (result == true)
            {
                // Update Status
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                return true;
            }
            else if (result == false && includeUncovers == true)
            {
                SmartUncover();
                LastPuzzleState = tempClone;
                // Update Status
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));

                return true;
            }
            else
                return false;
        }



        public bool SolveAsFarAsPossible()
        {
            bool result = false;
            
            // reset all changed properties
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.None;
            for (int x = 0; x < SudokuSize; x++)
                for (int y = 0; y < SudokuSize; y++)
                    Grid[x, y].ResetChangedIndicator();
            
            while (DoARoundOfSolves(false)) { result = true; }

            // Update Status
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));

            return result;
        }


        /*
        public void FindBestSolveablePuzzle()
        {
            // GetBestUncover has two methods for finding the best square to uncover.
            // The cheap way is to do a 'score' which simply counts the number of singletons or above in the row, column, and subsquare
            // (this double or triple counts in many cases.)
            // The expensive way is to explore out every possible uncover pattern to the end and pick the best one. Because this is exponential, 
            // the trip is to do the cheap way until we have 10 or fewer uncovers left, then use the expensive method.

            // First solve as far as possible
            SolveAsFarAsPossible();
            
            // Now count the number of uncovers remaining. If 10 or less, do expensive best uncover, if more than 10, do cheap best uncover
            if ( (SudokuSize*SudokuSize) - TotalUncovers <= 10)
                Do
        }*/

        public bool BackStep()
        {
            if (LastPuzzleState != null)
            {
                ResetGrid(LastPuzzleState);

                if (LastPuzzleState != null)
                {
                    CurrentStep = LastPuzzleState.CurrentStep;
                    if (LastPuzzleState.ClonePuzzle != null)
                        ClonePuzzle = LastPuzzleState.ClonePuzzle;
                    
                    LastPuzzleState = LastPuzzleState.LastPuzzleState;
                    
                    // Update Status
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
                
                return true;
            }
            else
                return false;
        }


        private void DebugChecks()
        {
            for (int x = 0; x < SudokuSize; x++)
                for (int y = 0; y < SudokuSize; y++)
                    Grid[x, y].DebugChecks();
        }


        // Try Step 1: Finding SingleGuesses
        private bool Step1SingleGuesses()
        {
            bool update = false;
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.SingleGuesses;

            // Look for any guesses with only one possibility and fill in the puzzle
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {

                    if (Grid[x, y].GuessesCount() == 1 && this[x, y] == 0)
                    {
                        int value = Grid[x, y].GetGuess(0);

#if DEBUG
                        if (Solution != null && Grid[x, y].GetGuess(0) != Solution[x, y])
                            throw new Exception("Does not match solution.");
#endif
                        //UncoverSquare(x, y);
                        this[x, y] = value;
                        //Grid[x, y].HighestStep = 1;
                        //Grid[x, y].OnPropertyChanged("HighestStep");
#if DEBUG
                        SudokuSolution testSolution = new SudokuSolution(this);
                        if (!testSolution.IsValidArray(false))
                            throw new Exception("Puzzle is not a valid solution");
#endif
                        update = true;
                    }
                }
            }

#if DEBUG
            SudokuSolution testSolution2 = new SudokuSolution(this);
            if (!testSolution2.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif


#if DEBUG
            DebugChecks();
#endif

            return update;
        }




        // Look for 'singletons', which are GuessesAvailable in a row, column, or subsquare that only appear once
        private bool Step2Singletons()
        {
            bool update = false;
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.Singletons; 

            // Look for singletons in a row
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
                // get frequencies by number
                for (int number = 1; number <= SudokuSize; number++)
                {
                    int rowCount = 0;
                    int columnCount = 0;
                    
                    // get frequencies (for a row or column for this number)
                    for (int j = 0; j < SudokuGrid.SudokuSize; j++)
                    {
                        if (Grid[i, j].ContainsGuess(number))
                            rowCount++;

                        if (Grid[j, i].ContainsGuess(number))
                            columnCount++;
                    }
                    
                    // Any singletons?
                    if (rowCount == 1)
                        update = update | ProcessSingletonByRow(i, number);

                    if (columnCount == 1)
                        update = update | ProcessSingletonByColumn(i, number); 

                }                
            }

            // look for singletons in subsquare
            for (int subSquareX = 0; subSquareX < SudokuGrid.SudokuSubSquareSize; subSquareX++)
            {
                for (int subSquareY = 0; subSquareY < SudokuGrid.SudokuSubSquareSize; subSquareY++)
                {
                    update = update | SingletonsBySubSquare(subSquareX, subSquareY);
                }
            }

#if DEBUG
            SudokuSolution testSolution = new SudokuSolution(this);
            if (!testSolution.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif

#if DEBUG
            DebugChecks();
#endif

            return update;
        }


        private bool ProcessSingletonByRow(int row, int value)
        {
            // For this row, value should be a singleton. So find it again and fill it in as an answer.
            for (int col = 0; col < SudokuGrid.SudokuSize; col++)
            {
                if (Grid[row, col].ContainsGuess(value))
                {
                    this[row, col] = value;
                    //Grid[row, col].HighestStep = 2;
                    //Grid[row, col].OnPropertyChanged("HighestStep");
                    return true;
                }
            }

            return false;
        }


        private bool ProcessSingletonByColumn(int column, int value)
        {
            // For this row, value should be a singleton. So find it again and fill it in as an answer.
            for (int row = 0; row < SudokuGrid.SudokuSize; row++)
            {
                if (Grid[row, column].ContainsGuess(value))
                {
                    this[row, column] = value;
                    //Grid[row, column].HighestStep = 2;
                    //Grid[row, column].OnPropertyChanged("HighestStep");
                    return true;
                }
            }

            return false;
        }


        // find singletons in a subsquare
        private bool SingletonsBySubSquare(int subSquareX, int subSquareY)
        {
            int count;

            bool update = false;


            // get frequencies
            for (int number = 1; number <= SudokuSize; number++)
            {
                count = 0;

                for (int x = subSquareX * SudokuGrid.SudokuSubSquareSize; x < ((subSquareX + 1) * SudokuGrid.SudokuSubSquareSize); x++)
                {
                    for (int y = subSquareY * SudokuGrid.SudokuSubSquareSize; y < ((subSquareY + 1) * SudokuGrid.SudokuSubSquareSize); y++)
                    {
                        // get frequencies
                        if (Grid[x, y].ContainsGuess(number))
                        {
                            count++;
                        }

                    }
                }
                // Any singletons?
                if (count == 1)
                    update = update | ProcessSingletonBySubSquare(subSquareX, subSquareY, number);
            }

            return update;
        }


        private bool ProcessSingletonBySubSquare(int subSquareX, int subSquareY, int value)
        {
            // For this subsquare, value should be a singleton. So find it again and fill it in as an answer.
            for (int x = subSquareX * SudokuGrid.SudokuSubSquareSize; x < ((subSquareX + 1) * SudokuGrid.SudokuSubSquareSize); x++)
            {
                for (int y = subSquareY * SudokuGrid.SudokuSubSquareSize; y < ((subSquareY + 1) * SudokuGrid.SudokuSubSquareSize); y++)
                {
                    if (Grid[x, y].ContainsGuess(value))
                    {
                        this[x, y] = value;
                        //Grid[x, y].HighestStep = 2;
                        //Grid[x, y].OnPropertyChanged("HighestStep");
                        return true;
                    }
                }
            }

            return false;
        }


        // look for two pairs of numbers that appear in only two squares
        // eliminate other possibilities repeat
        private bool Step3Pairs()
        {
            bool update = false;
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.Pairs; 

            SudokuGridLocation[] rowContainer, columnContainer, subSquareContainer;

            // one row and column at a time
            for (int i = 0; i < SudokuSize; i++)
            {
                // Restart row
                rowContainer = new SudokuGridLocation[SudokuSize];
                // Restart column
                columnContainer = new SudokuGridLocation[SudokuSize];

                for (int j = 0; j < SudokuSize; j++)
                {
                    // Get a row
                    rowContainer[j] = Grid[i, j];

                    // Get a column
                    columnContainer[j] = Grid[j, i];
                }

                update = update | FindPairs(rowContainer); // Find guesses where there are only two and they are in the same two cells               
                update = update | FindPairs(columnContainer);
            }
           
            // look for pairs in subsquare
            for (int subSquareX = 0; subSquareX < SudokuGrid.SudokuSubSquareSize; subSquareX++)
            {
                for (int subSquareY = 0; subSquareY < SudokuGrid.SudokuSubSquareSize; subSquareY++)
                {
                    // Restart column
                    subSquareContainer = new SudokuGridLocation[SudokuSize];
                    
                    // loop through this particular subsquare and return the result
                    int startRow = subSquareX * SudokuSubSquareSize;
                    int endRow = startRow + SudokuSubSquareSize;
                    int startCol = subSquareY * SudokuSubSquareSize;
                    int endCol = startCol + SudokuSubSquareSize;

                    int i = 0;

                    for (int x = startRow; x < endRow; x++)
                    {
                        for (int y = startCol; y < endCol; y++)
                        {
                            // Get a subsquare
                            subSquareContainer[i] = Grid[x, y];
                            i++;
                        }
                    }
                    update = update | FindPairs(subSquareContainer);
                }
            }

#if DEBUG
            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();
            SudokuSolution testSolution = new SudokuSolution(this);
            if (!testSolution.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif

#if DEBUG
            DebugChecks();
#endif

            return update;
        }


        // Find guesses where there are only two and they are in the same two cells
        private bool FindPairs(SudokuGridLocation[] container)
        {
            List<SudokuGridLocation> locations;
            bool result = false;

            List<int> allGuesses = new List<int>();
            for (int i = 0; i < SudokuSize; i++)
            {
                allGuesses.AddRange(container[i].GetAllGuesses());
            }

            // get frequencies by number and store locations in a node
            for (int number = 1; number <= SudokuSize; number++)
            {
                locations = new List<SudokuGridLocation>();

                locations.AddRange(container.Where((location) => location.ContainsGuess(number)));

                // only want ones with a count of two
                if (locations.Count() == 2)
                {
                    // Make a list of distinct values for this node
                    List<int> distinctValues = new List<int>();
                    distinctValues = locations[0].GetAllGuesses().Concat(locations[1].GetAllGuesses()).ToList();
                    distinctValues = distinctValues.Distinct().ToList();

                    foreach(int value in distinctValues)
                    {
                        // skip the specific value we're considering
                        if (value != number)
                        {
                            List<int> subGuesses = new List<int>();                            
                            for (int i = 0; i < locations.Count(); i++)
                            {
                                subGuesses.AddRange(locations[i].GetAllGuesses());
                            }

                            // To be a valid pair, it must match this criteria:
                            // 1) value != number (i.e. we already know the number we're considering has exactly a count of 2 at this point
                            // 2) count of value in locations (i.e. the store of all grid locations that contains "number" is exactly 2
                            // 3) count of value in all containers (i.e. the store of all grid locations for this row/column/subsquare is exactly 2
                            if (subGuesses.Count((i) => i == value) == 2 &&
                                allGuesses.Count((i) => i == value) == 2 )
                                //container.Where((location) => location.GuessesAvailable.Contains(value)).Count() == 2 )
                            {
                                // We have a pair! And the pair is 'number' and 'value'
                                // Eliminate all others Guesses for any node containing those two
                                // Drat! I know what values are the pair, but not where they are located!
                                // If I remove the extras out of "locations" will it also remove it from containers?                                
                                List<int> doNotRemove = new List<int>();
                                doNotRemove.Add(value);
                                doNotRemove.Add(number);

                                
                                result = result | locations[0].RemoveAllGuessesBut(doNotRemove);
                                /*
                                if (locations[0].GuessesAvailable.RemoveAll((guess) => !(guess == value || guess == number)) > 0)
                                {
                                    locations[0].HighestStep = SudokuGridLocation.CurrentStep;
                                    locations[0].OnPropertyChanged("HighestStep"); 
                                    locations[0].OnPropertyChanged("");

                                    result = true;
                                }*/

                                result = result | locations[1].RemoveAllGuessesBut(doNotRemove);                              
                                /*
                                if (locations[1].GuessesAvailable.RemoveAll((guess) => !(guess == value || guess == number)) > 0)
                                {
                                    locations[1].HighestStep = SudokuGridLocation.CurrentStep;
                                    locations[1].OnPropertyChanged("HighestStep"); locations[1].OnPropertyChanged("");

                                    result = true;
                                }*/
                            }
                        }
                    }
                }
            }

            return result;
        }


        private bool Step4TwoOfAKind()
        {
            bool update = false;
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.TwoOfAKind; 

            SudokuGridLocation[] rowContainer, columnContainer, subSquareContainer;

            // one row and column at a time
            for (int i = 0; i < SudokuSize; i++)
            {
                // Restart row
                rowContainer = new SudokuGridLocation[SudokuSize];
                // Restart column
                columnContainer = new SudokuGridLocation[SudokuSize];

                for (int j = 0; j < SudokuSize; j++)
                {
                    // Get a row
                    rowContainer[j] = Grid[i, j];

                    // Get a column
                    columnContainer[j] = Grid[j, i];
                }

                update = update | FindTwoOfAKind(rowContainer); // Find guesses where the cell contains only two guesses and another cells is the same way
                update = update | FindTwoOfAKind(columnContainer);
            }
           
            // look for pairs in subsquare
            for (int subSquareX = 0; subSquareX < SudokuGrid.SudokuSubSquareSize; subSquareX++)
            {
                for (int subSquareY = 0; subSquareY < SudokuGrid.SudokuSubSquareSize; subSquareY++)
                {
                    // Restart column
                    subSquareContainer = new SudokuGridLocation[SudokuSize];
                    
                    // loop through this particular subsquare and return the result
                    int startRow = subSquareX * SudokuSubSquareSize;
                    int endRow = startRow + SudokuSubSquareSize;
                    int startCol = subSquareY * SudokuSubSquareSize;
                    int endCol = startCol + SudokuSubSquareSize;

                    int i = 0;

                    for (int x = startRow; x < endRow; x++)
                    {
                        for (int y = startCol; y < endCol; y++)
                        {
                            // Get a subsquare
                            subSquareContainer[i] = Grid[x, y];
                            i++;
                        }
                    }
                    update = update | FindTwoOfAKind(subSquareContainer);
                }
            }

#if DEBUG
            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();
            SudokuSolution testSolution = new SudokuSolution(this);
            if (!testSolution.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif

#if DEBUG
            DebugChecks();
#endif

            return update;
        }


        #region SolverMethods


        // Find guesses where there are only two and they are in the same two cells
        private bool FindTwoOfAKind(SudokuGridLocation[] container)
        {
            bool result = false;
            List<SudokuGridLocation> locations;
            locations = new List<SudokuGridLocation>();

            // list of locations with exactly two guesses
            locations.AddRange(container.Where((location) => location.GuessesCount() == 2));

            // now do logic where I take the list of locations with two guesses and see if any two match
            int numberInContainer = locations.Count();
            int half = numberInContainer / 2;
            if (half < numberInContainer / 2)
                half++;
            // loop through half of the container -- once you do half, you've actually done all due to inner loop
            for (int i = 0; i < half; i++)
            {
                SudokuGridLocation node1 = locations[i];
                for (int j = 0; j < numberInContainer; j++)
                {
                    SudokuGridLocation node2 = locations[j];
                    // skip comparison if they are the same node
                    if (i == j)
                        break;


                    int value1 = node1.GetGuess(0);
                    int value2 = node1.GetGuess(1);
                    int value3 = node2.GetGuess(0);
                    int value4 = node2.GetGuess(1);

                    if ( (value1 == value3 && value2 == value4) ||
                            (value1 == value4 && value1 == value3)
                        )
                    {
                        // We have a match!
                        // Now clear guesses in same row, column, and sub square 
                        // if they have either of the two values that match
                        SudokuGridLocation[] locationsToSkip = new SudokuGridLocation[2];
                        locationsToSkip[0] = node1;
                        locationsToSkip[1] = node2;

                        ClearGuesses(value1, container, locationsToSkip);
                        ClearGuesses(value2, container, locationsToSkip);
                    }
                }

            }

#if DEBUG
            DebugChecks();
#endif

            return result;
        }



        // Clear Guess for one value but this is from within a container -- I should use this as the primary means of removing guesses
        // The 'value' passed in is the value to clear.
        // The locationsToSkip is the grid locations where the value was found (singletons or pairs), so it won't be cleared itself
        private void ClearGuesses(int value, SudokuGridLocation[] container, SudokuGridLocation[] locationsToSkip)
        {
            for (int i = 0; i < container.Count(); i++)
            {
                // remove in container (be it a row, column, or subsquare)
                if (! locationsToSkip.Contains(container[i]))
                {
                    container[i].RemoveGuess(value);
                }
            }
        }


        // returns true if all squares in grid are filled in. (i.e. only one possibility exists. Otherwise returns false
        public bool CheckForSolution()
        {
            // TODO: add an option to this function to also check it against the original solution
            // This will be especially useful during testing

            bool hasSolution = true;
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
#if DEBUG
                    if (Solution != null && Grid[x, y].Value != 0 && Grid[x, y].Value != Solution[x, y])
                        throw new Exception("Solution is invalid.");
#endif
                    //if (Grid[x, y].GuessesCount() > 0)
                    if (Grid[x,y].Value != Solution[x, y])
                        hasSolution = false;
                }
            }

            return hasSolution;
        }



        #endregion

        public void UpdateAll()
        {
            foreach (SudokuGridLocation loc in this)
            {
                loc.DoUpdate();
            }

        }


        public void MarkInvalids()
        {
            SudokuSolution checkValid = new SudokuSolution(this);
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
                    if (Grid[x, y].Enabled == true && Grid[x,y].Value != 0 && !checkValid.IsValid(x, y))
                        Grid[x, y].Valid = false;
                    else
                        Grid[x, y].Valid = true;

                    Grid[x, y].DoUpdate();
                }
            }
        }


        public void FillInSolution()
        {
            SudokuGridLocation.CurrentStep = SudokuGridLocation.Step.None;

            for (int x = 0; x < SudokuPuzzle.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuPuzzle.SudokuSize; y++)
                {
                    Grid[x, y].Value = Solution[x,y];
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}



