using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Sudoku
{
    public class SudokuSolver
    {

        // Example: of how to make an integer comparer
        private class IntegerComparer : IComparer<int>
        {

            public int Compare(int x, int y)
            {
                if (x > y)
                    return 1;
                else if (y > x)
                    return -1;
                else
                    return 0;
            }
        }


        private struct GridLocation
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public GridLocation(int row, int column) : this()
            {
                Row = row;
                Column = column;
            }
        }

        private class FrequencyInfo
        {
            public int Number { get; set; }
            public int Frequency { get; set; }
            // TODO: only use one of these methods, not both -- whichever is more elegant
            public List<GridLocation> Locations { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
        }


        public class PairInfo : IEquatable<PairInfo>, IComparable, IEqualityComparer<PairInfo>  // implements comparable interface
        {
            public int First { get; set; }
            public int Second { get; set; }
            public int Location { get; set; }
            public bool ToDelete { get; set; }

            public bool Equals(PairInfo pair)
            {
                
                if (First == pair.First && Second == pair.Second && Location == pair.Location)
                    return true;
                else
                    return false;                
            }

            public bool SamePairValue(PairInfo pair)
            {
                return SamePairValue(pair.First, pair.Second);
            }


            public bool Equals(PairInfo pair1, PairInfo pair2)
            {
                
                if (pair1.First == pair2.First && pair1.Second == pair2.Second && pair1.Location == pair2.Location)
                    return true;
                else
                    return false;
            }

            
            public bool SamePairValue(int first, int second)
            {
                return (First == first) && (Second == second);
            }


            public override bool Equals(object obj)
            {
                if (obj == null) return base.Equals(obj);

                if (!(obj is PairInfo))
                    throw new InvalidCastException("The 'obj' argument is not a PairInfo object.");
                else
                    return Equals(obj as PairInfo);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
                       

            public int CompareTo(Object o)
            {
                return CompareTo((PairInfo)o);
            }

            public int CompareTo(PairInfo compare)
            {
                int result;
                result = First.CompareTo(compare.First);
                if (result != 0)
                    return result;
                else
                    return Second.CompareTo(compare.Second);
            }

            public PairInfo()
            {
                ToDelete = false;
            }

            public PairInfo(int first, int second, int location) : this()
            {
                First = first;
                Second = second;
                Location = location;
            }

            public override string ToString()
            {
                string s = "";

                s = "[" + Location.ToString() + "]: " + First.ToString() + Second.ToString();
                return s;
            }



            public int GetHashCode(PairInfo obj)
            {
                if (obj == null) return base.GetHashCode();

                if (!(obj is PairInfo))
                    throw new InvalidCastException("The 'obj' argument is not a PairInfo object.");
                else
                    // my hash code will be an integer in this format: First, Second, Row, Column
                    return First * 100 + Second * 10 + Location;
            }
        }



        public class GuessInfo : IEquatable<GuessInfo>, IComparable, IEqualityComparer<GuessInfo>  // implements comparable interface
        {
            public int Value { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
            public bool ToDelete { get; set; }

            public bool Equals(GuessInfo guess)
            {

                if (Value == guess.Value && Row == guess.Row && Column == guess.Column)
                    return true;
                else
                    return false;
            }

            
            public bool Equals(GuessInfo guess1, GuessInfo guess2)
            {

                if (guess1.Value == guess2.Value && guess1.Row == guess2.Row && guess1.Column == guess2.Column)
                    return true;
                else
                    return false;
            }

            
            public override bool Equals(object obj)
            {
                if (obj == null) return base.Equals(obj);

                // Example: of reflection "is" and "as"
                if (!(obj is GuessInfo))
                    throw new InvalidCastException("The 'obj' argument is not a GuessInfo object.");
                else
                    return Equals(obj as GuessInfo);
            }


            public bool SameValue(GuessInfo guess)
            {
                return SameValue(guess.Value);
            }

            public bool SameValue(int value)
            {
                return (Value == value);
            }



            
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }


            public int CompareTo(Object obj)
            {
                if (obj == null) return -1;

                // Example: of reflection "is" and "as"
                if (!(obj is GuessInfo))
                    throw new InvalidCastException("The 'obj' argument is not a GuessInfo object.");
                else
                    return CompareTo(obj as GuessInfo);
            }


            public int CompareTo(GuessInfo compare)
            {
                return Value.CompareTo(compare.Value);
            }


            public int CompareByLocation(GuessInfo compare)
            {
                int result;
                result = Row.CompareTo(compare.Row);
                if (result != 0)
                    return result;
                else
                    return Column.CompareTo(compare.Column);
            }


            public bool SameLocation(GuessInfo compare)
            {
                return (Row == compare.Row && Column == compare.Column);
            }

            public GuessInfo()
            {
                ToDelete = false;
            }

            public GuessInfo(int value, int row, int column)
                : this()
            {
                Value = value;
                Row = row;
                Column = column;
            }


            public override string ToString()
            {
                string s = "";

                s = "[" + Row.ToString() + ", " + Column.ToString() + "]: " + Value.ToString();
                return s;
            }



            public int GetHashCode(GuessInfo obj)
            {
                if (obj == null) return base.GetHashCode();

                if (!(obj is GuessInfo))
                    throw new InvalidCastException("The 'obj' argument is not a PairInfo object.");
                else
                    // my hash code will be an integer in this format: First, Second, Row, Column
                    return Value * 100 + Row * 10 + Column;
            }
        }


        
        // enums
        public enum Difficulty : int { UNDETERMINED, TRIVIAL, EASY, MEDIUM, HARD, UNSOLVEABLE };
        private enum Step : int { STEP1, STEP2, STEP3, STEP4, UNSOLVEABLE };

        private SudokuGrid Puzzle {get; set;}
        private SudokuGrid Solution { get; set; } // Use this as a way of storing the underlying solution (if avaiable) for checking of solutions

        // keep track of all possibilities by square
        private List<int>[,] GuessesAvailable { set; get; }
        
        // Properties
        public Difficulty DifficultyLevel { get; set; }
        private bool FoundSolution { get; set; }

        public SudokuSolver(SudokuPuzzleOld puzzle) : this(puzzle as SudokuGrid)
        {
            if (puzzle != null)
                Solution = puzzle.Solution;
        }

        // constructor
        public SudokuSolver(SudokuGrid puzzle)
        {
            FoundSolution = false;
            DifficultyLevel = Difficulty.UNDETERMINED;

            if (puzzle != null)
                Puzzle = puzzle;
            else
                Puzzle = new SudokuPuzzleOld();

            // initialize possible guess per square
            GuessesAvailable = new List<int>[SudokuGrid.SudokuSize, SudokuGrid.SudokuSize];
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
                    GuessesAvailable[x, y] = new List<int>();

                    // initial list of possibilities per square
                    for (int i = 1; i <= SudokuGrid.SudokuSize; i++)
                    {
                        GuessesAvailable[x, y].Add(i);
                    }
                }
            }
        }


        /// <summary>
        /// Attempts to find a solution -- returns false if no solution was found
        ///</summary>       
        private bool FindSolution()
        {
            // Work through each step: the steps call back words (i.e. step 2 also calls step 1) to reapply as new
            // possibilities are considered
            // TODO: Is this really the best way to go about this?
            //bool foundSolution = false;
            Step tryStep = Step.STEP1;
            Difficulty minDifficulty = Difficulty.UNDETERMINED;

            while (!FoundSolution && DifficultyLevel != Difficulty.UNSOLVEABLE)
            {
                bool updateMade = false; 
                switch (tryStep)
                {
                    case Step.STEP1:
                        // attempt to fill in results and check for a solution if there were any updates
                        updateMade = Step1ApplyNumbers();
                        if (updateMade)
                            minDifficulty = Difficulty.TRIVIAL;
                    break;

                    case Step.STEP2:
                        // attempt to fill in results and check for a solution if there were any updates
                        updateMade = Step2UseSingletons();
                        if (updateMade)
                            minDifficulty = Difficulty.EASY;
                    break;

                    case Step.STEP3:
                        // attempt to fill in results and check for a solution if there were any updates
                        updateMade = Step3UsePairs(); // TODO: put Step3 back
                        if (updateMade)
                            minDifficulty = Difficulty.MEDIUM;
                    break;

                    case Step.STEP4:
                        // attempt to fill in results and check for a solution if there were any updates
                        updateMade = Step4ElminateBadGuesses();
                        if (updateMade)
                            minDifficulty = Difficulty.HARD;
                    break;

                    case Step.UNSOLVEABLE:
                        // even step 4 failed, so the puzzle is unsolveable
                        DifficultyLevel = Difficulty.UNSOLVEABLE;
                    break;

                }

                // if it's not unsolveable yet, keep working at it
                if (DifficultyLevel != Difficulty.UNSOLVEABLE)
                {
                    // update Difficulty based on results;
                    if (DifficultyLevel < minDifficulty)
                        DifficultyLevel = minDifficulty;

                    // if we had no update move on to next step, otherwise fall back to beginning
                    // and start all over looking for more updates
                    if (updateMade)
                        tryStep = Step.STEP1;
                    else
                        tryStep++;

                    // Do we have a solution?
                    CheckForSolution();
                    // if there is a solution, we're done - FoundSolution property has been set by CheckForSolution();
                }                    
            }

            return FoundSolution;
        }



        /// <summary>
        /// Step1: Apply known numbers to eliminate possibilities
        ///</summary>       
        private bool Step1ApplyNumbers()
        {
            bool update = false;

            // eliminate guesses based on what numbers we already know
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
                    // Look for filled in items that haven't yet been cleared of all guesses
                    if (Puzzle[x, y] != 0 && GuessesAvailable[x, y].Count > 0) //TODO: This is what is causing FullSolverTest to fail!!! It never really does step1 again now that I am leaving 1 guess behind - need to rethink this
                    {

                        // Precondition: one of the guesses remaining needs to be what is filled in
#if DEBUG
                        Debug.Assert(GuessesAvailable[x, y].Contains(Puzzle[x, y]));
#endif
                        update = true;

                        //Debug.WriteLine("Current Grid:");
                        //Puzzle.WriteOutSudokuGrid();

                        // remove at that location
                        //GuessesAvailable[x, y].RemoveAll(parm => parm != Puzzle[x, y]);
                        GuessesAvailable[x, y].Clear();

                        
                        // now clear guesses in related row, column, subsquare
                        int value = Puzzle[x, y];
                        ClearGuesses(value, x, y);
                    }
                }
            }

            // Look for any guesses with only one possibility and fill in the puzzle
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {

                    if (GuessesAvailable[x, y].Count == 1 && Puzzle[x, y] == 0)
                    {
                        int value = GuessesAvailable[x, y][0];

#if DEBUG
                        if (Solution != null && GuessesAvailable[x, y][0] != Solution[x, y])
                            throw new Exception("Does not match solution.");
                        //Debug.WriteLine("Before Setting Value:");
                        //Puzzle.WriteOutSudokuGrid();
#endif
                        Puzzle[x, y] = value;
#if DEBUG
                        //Debug.WriteLine("After Setting Value:");
                        //Puzzle.WriteOutSudokuGrid();
                        SudokuSolution testSolution = new SudokuSolution(Puzzle);
                        if (!testSolution.IsValidArray(false))
                            throw new Exception("Puzzle is not a valid solution");
#endif
                        update = true;
                    }
                }
            }

#if DEBUG
            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();
            SudokuSolution testSolution2 = new SudokuSolution(Puzzle);
            if (!testSolution2.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif

            return update;
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
                    int prev = GuessesAvailable[i, y].Count;
#endif

                if (x != i)
                    GuessesAvailable[i, y].Remove(value);

#if DEBUG
                    if (GuessesAvailable[i, y].Count == 0 && Puzzle[i, y]  == 0 && prev > 0)
                        throw new Exception("It should never be possible to eliminate all guesses, except by finding a value to fill in.");
#endif
            }
            // remove in column
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
                #if DEBUG
                    int prev = GuessesAvailable[x, i].Count;
                #endif

                if (i != y)
                    GuessesAvailable[x, i].Remove(value);

#if DEBUG
                if (GuessesAvailable[x, i].Count == 0 && Puzzle[x, i]  == 0 && prev > 0)
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
                    int prev = GuessesAvailable[i, j].Count;

                    if (!(x == i && y == j))
                        GuessesAvailable[i, j].Remove(value);
#if DEBUG
                    if (GuessesAvailable[i, j].Count == 0 && Puzzle[i, j] == 0 && prev > 0)
                        throw new Exception("It should never be possible to eliminate all guesses, except by finding a value to fill in.");
#endif
                }
            }

        }




        // returns true if all squares in grid are filled in. (i.e. only one possibility exists. Otherwise returns false
        private bool CheckForSolution()
        {
            // TODO: add an option to this function to also check it against the original solution
            // This will be especially useful during testing

            bool hasSolution = true;
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
                    if (GuessesAvailable[x, y].Count > 0)
                        hasSolution = false;
                }
            }


            /*
            // Fill in solution
            // TODO: This seems like an unnecessary step -- fix it to be filled in 
            // as a precondition of this call - i.e. there should be no blank puzzles with
            // 1 guess. Instead, the guesses should already be cleared
            if (hasSolution)
            {
                for (int x = 0; x < SudokuGrid.SudokuSize; x++)
                {
                    for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                    {
                        if (GuessesAvailable[x, y].Count == 1)
                        {
#if DEBUG
                            Debug.Assert(!(Solution != null && GuessesAvailable[x, y][0] != Solution[x, y]), "Does not match solution.");
#endif
                            Puzzle[x, y] = GuessesAvailable[x, y][0];
#if DEBUG
                            SudokuSolution testSolution = new SudokuSolution(Puzzle);
                            Debug.Assert(testSolution.IsValidArray(false), "Puzzle is not a valid solution");
#endif
                            GuessesAvailable[x, y].RemoveAll(parm => parm != Puzzle[x, y]);

                        }
                    }
                }
            }*/

            // update "FoundSolution" property
            FoundSolution = hasSolution;

            return hasSolution;
        }



        /// <summary>
        /// returns a copy of a SudkoGrid with the solution all filled in. 
        /// If no single solution was found, returns 'null'
        ///</summary>       
        public SudokuGrid GetSolution()
        {
            if (!FindSolution())
                return null;
            else
                return Puzzle.MakeCopy();
        }


        // Look for 'singletons', which are GuessesAvailable in a row, column, or subsquare that only appear once
        private bool Step2UseSingletons()
        {
            bool update = false;

            /*
            // Look for any guesses with only one possibility and fill in the puzzle
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {

                    if (GuessesAvailable[x, y].Count == 1 && Puzzle[x, y] == 0)
                    {
                        int value = GuessesAvailable[x, y][0];

#if DEBUG
                        if(Solution != null && GuessesAvailable[x, y][0] != Solution[x, y])
                            throw new Exception("Does not match solution.");
#endif
                        Puzzle[x, y] = value;
#if DEBUG
                        //Debug.WriteLine("Current Grid:");
                        //Puzzle.WriteOutSudokuGrid();
                        SudokuSolution testSolution = new SudokuSolution(Puzzle);
                        if(!testSolution.IsValidArray(false))
                            throw new Exception("Puzzle is not a valid solution");
#endif
                        GuessesAvailable[x, y].RemoveAll(parm => parm != value);
                        update = true;
                        //Step1ApplyNumbers();
                    }
                }
            }*/

            // one row and column at a time
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
                update = update | SingletonsByRow(i);
                update = update | SingletonsByColumn(i);
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
            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();
            SudokuSolution testSolution = new SudokuSolution(Puzzle);
            if (!testSolution.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif


            return update;
        }


        // find singletons in a row
        private bool SingletonsByRow(int x)
        {            
            FrequencyInfo[] frequencyTable = new FrequencyInfo[SudokuGrid.SudokuSize];
            for (int i = 0; i < frequencyTable.Length; i++)
                frequencyTable[i] = new FrequencyInfo();

            bool update = false;

            for (int y = 0; y < SudokuGrid.SudokuSize; y++)
            {
                if (GuessesAvailable[x, y].Count > 0)
                {
                    UpdateFrequencyTable(frequencyTable, x, y);
                }
            }

            update = ProcessSingletons(frequencyTable);

            return update;
        }



        // find singletons in a column
        private bool SingletonsByColumn(int y)
        {
            FrequencyInfo[] frequencyTable = new FrequencyInfo[SudokuGrid.SudokuSize];
            for (int i = 0; i < frequencyTable.Length; i++)
                frequencyTable[i] = new FrequencyInfo();

            bool update = false;

            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                if (GuessesAvailable[x, y].Count > 0)
                {
                    UpdateFrequencyTable(frequencyTable, x, y);
                }
            }

            /*
            // if we have only one occurance of something, then we know it's the right item, so elimate all other guesses
            foreach (FrequencyInfo f in frequencyTable)
            {
                if (f.Frequency == 1)
                {
                    // Column y has only one possible option for 'number'
                    Puzzle[f.Row, y] = f.Number;
                    update = true;
                }
            }*/

            update = ProcessSingletons(frequencyTable);

            return update;
        }



        // find singletons in a subsquare
        private bool SingletonsBySubSquare(int subSquareX, int subSquareY)
        {
            FrequencyInfo[] frequencyTable = new FrequencyInfo[SudokuGrid.SudokuSize];
            for (int i = 0; i < frequencyTable.Length; i++)
                frequencyTable[i] = new FrequencyInfo();

            bool update = false;

            for (int x = subSquareX * SudokuGrid.SudokuSubSquareSize; x < ((subSquareX + 1) * SudokuGrid.SudokuSubSquareSize); x++)
            {
                for (int y = subSquareY * SudokuGrid.SudokuSubSquareSize; y < ((subSquareY + 1) * SudokuGrid.SudokuSubSquareSize); y++)
                {
                    if (GuessesAvailable[x, y].Count > 0)
                    {
                        UpdateFrequencyTable(frequencyTable, x, y);
                    }
                }
            }

            update = ProcessSingletons(frequencyTable);            

            return update;
        }



        // update to frequency list
        private void UpdateFrequencyTable(FrequencyInfo[] frequencyTable, int x, int y)
        {
            foreach (int i in GuessesAvailable[x, y])                        
            {
                frequencyTable[i-1].Number = i; // which number appears
                frequencyTable[i-1].Frequency++; // number of times this number appears                
                frequencyTable[i-1].Row = x; 
                frequencyTable[i-1].Column = y;
                if (frequencyTable[i - 1].Locations == null)
                    frequencyTable[i - 1].Locations = new List<GridLocation>();
                frequencyTable[i - 1].Locations.Add(new GridLocation(x, y));
            }

        }



        // process each singleton
        private bool ProcessSingletons(FrequencyInfo[] frequencyTable)
        {
            bool update = false;

            // if we have only one occurance of something, then we know it's the right item, so elimate all other guesses
            foreach (FrequencyInfo f in frequencyTable)
            {
                if (f.Frequency == 1 && Puzzle[f.Row, f.Column] == 0)
                {
                    // Row i has only one possible option for 'number'
#if DEBUG
                    if (Solution != null && f.Number != Solution[f.Row, f.Column])
                        throw new Exception("Does not match solution.");
#endif
                    //Puzzle[f.Row, f.Column] = f.Number;
#if DEBUG
                    //Debug.WriteLine("Current Grid (Process Singletons):");
                    //Puzzle.WriteOutSudokuGrid();
                    SudokuSolution testSolution = new SudokuSolution(Puzzle);
                    if (!testSolution.IsValidArray(false))
                        throw new Exception("Puzzle is not a valid solution");
#endif
                    GuessesAvailable[f.Row, f.Column].RemoveAll(parm => parm != f.Number);


                    update = true;
                }
            }

            return update;
        }


        // look for two pairs of numbers that appear in only two squares
        // eliminate other possibilities repeat
        private bool Step3UsePairs()
        {
            bool update = false;

            // one row and column at a time
            for (int i = 0; i < SudokuGrid.SudokuSize; i++)
            {
                update = update | PairsByRowAndColumn(i);
            }
                       
            // look for pairs in subsquare
            for (int subSquareX = 0; subSquareX < SudokuGrid.SudokuSubSquareSize; subSquareX++)
            {
                for (int subSquareY = 0; subSquareY < SudokuGrid.SudokuSubSquareSize; subSquareY++)
                {
                    update = update | PairsBySubSquare(subSquareX, subSquareY);
                }
            }

#if DEBUG
            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();
            SudokuSolution testSolution = new SudokuSolution(Puzzle);
            if (!testSolution.IsValidArray(false))
                throw new Exception("Puzzle is not a valid solution");
#endif


            return update;
        }


        // Find pairs in a row
        private bool PairsByRowAndColumn(int i)
        {

            //Debug.WriteLine("Current Grid:");
            //Puzzle.WriteOutSudokuGrid();

            bool update = false;
            List<int>[] GuessesInSection = new List<int>[SudokuGrid.SudokuSize];

            // For each location in a row (or column or subsquare)

            // By row
            for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                GuessesInSection[y] = GuessesAvailable[i, y];

            update = update | ProcessPairs(GuessesInSection);


            // By column
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
                GuessesInSection[x] = GuessesAvailable[x, i];

            update = update | ProcessPairs(GuessesInSection);

            return update;
        }



        // Find pairs in subsquare
        private bool PairsBySubSquare(int subSquareX, int subSquareY)
        {
            bool update = false;
            List<PairInfo> listPairs = new List<PairInfo>();
            List<int>[] GuessesInSection = new List<int>[SudokuGrid.SudokuSize];
            int count = 0;

            for (int x = subSquareX * SudokuGrid.SudokuSubSquareSize; x < ((subSquareX + 1) * SudokuGrid.SudokuSubSquareSize); x++)
            {
                for (int y = subSquareY * SudokuGrid.SudokuSubSquareSize; y < ((subSquareY + 1) * SudokuGrid.SudokuSubSquareSize); y++)
                {
                    GuessesInSection[count++] = GuessesAvailable[x, y];
                }
            }

            update = update | ProcessPairs(GuessesInSection);

            return update;
        }


        // This function does the bulk of the work processing pairs for rows, columns, and subsquares
        private bool ProcessPairs(List<int>[] GuessesInSection)
        {
            bool update=false;
            List<PairInfo>[] potentialMatches = new List<PairInfo>[SudokuGrid.SudokuSize];

            potentialMatches = GetPotentialMatches(GuessesInSection);

            // We now have a list of potential matches for each and every location
            // To make this work, we now have to run through it again and this time look for matches that are at locations
            // That have no other matches
            for (int loc1 = 0; loc1 < SudokuGrid.SudokuSize; loc1++)
            {

                /* Example: of Sort
                // I am going to sort the list of potential Matches so that I have an example of how to do a sort
                // This isn't needed. But it does make stepping through the code interesting
                // But I'll keep it commented out after testing it.
                // Example: of sorting with a lambda expression -- note that I pass in two parameters here
                // TODO: I am not sure how I knew it took 2 parameters. I wonder if this a 'lambda' comparer. Need to experiment
                if (potentialMatches[loc1] != null)
                {
                    potentialMatches[loc1].Sort((match1, match2) => match1.CompareTo(match2) * -1); // reverse the order
                    potentialMatches[loc1].Sort((match1, match2) => match1.CompareTo(match2)); // put the order back
                }*/


                if (potentialMatches[loc1] != null && potentialMatches[loc1].Count == 1)
                {
                    PairInfo pair1 = potentialMatches[loc1][0];
                    int loc2 = pair1.Location;

                    // Only a match if the matching pair's location has no other matches
                    if (potentialMatches[loc2] != null && potentialMatches[loc2].Count == 1)
                    {
                        // But does it really match?
                        PairInfo pair2 = potentialMatches[loc2][0];
                        if (pair1.SamePairValue(pair2))
                        {
                            // Even if we have a match, it doesn't count unless these are the only two occurances of these integers
                            if (CountValues(GuessesInSection, pair1.First) == 2 && CountValues(GuessesInSection, pair1.Second) == 2)
                            {
                                // We have a match!

                                // Remove all guesses that aren't this pair. i.e. (2, 7, 9) and (3, 7, 9) become (7, 9) and (7, 9)
                                for (int loop = GuessesInSection[loc1].Count - 1; loop >= 0; loop--)
                                {
                                    int guess = GuessesInSection[loc1][loop];

                                    if (guess != pair1.First && guess != pair1.Second)
                                    {
                                        GuessesInSection[loc1].Remove(guess);
                                        update = true;

                                    }
                                }
                                for (int loop = GuessesInSection[loc2].Count - 1; loop >= 0; loop--)
                                {
                                    int guess = GuessesInSection[loc2][loop];

                                    if (guess != pair2.First && guess != pair2.Second)
                                    {
                                        GuessesInSection[loc2].Remove(guess);
                                        update = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            potentialMatches = GetPotentialMatches(GuessesInSection);

            // We have removed the excess guesses in the previous section of code
            // However, there is a part 2 to this. We still need to remove any other guesses
            // at other locations that contain values from this pair
            // For the record, the pair in question is GuessesInSection[loc1] and GuessesInSection[loc2]
            for (int loc1 = 0; loc1 < SudokuGrid.SudokuSize; loc1++)
            {
                if (potentialMatches[loc1] != null && potentialMatches[loc1].Count == 1)
                {
                    PairInfo pair1 = potentialMatches[loc1][0];
                    int loc2 = pair1.Location;

                    // Only a match if the matching pair's location has no other matches
                    if (potentialMatches[loc2] != null && potentialMatches[loc2].Count == 1)
                    {
                        // But does it really match?
                        PairInfo pair2 = potentialMatches[loc2][0];
                        if (pair1.SamePairValue(pair2) && GuessesInSection[loc1].Count == 2 && GuessesInSection[loc2].Count == 2)
                        {

                            // Precondition: At this point, we should always have two pair with nothing else in those locations
#if DEBUG
                            Debug.Assert(GuessesInSection[loc1].Count == 2);
                            Debug.Assert(GuessesInSection[loc2].Count == 2);
                            Debug.Assert(GuessesInSection[loc1][0] == GuessesInSection[loc2][0]);
                            Debug.Assert(GuessesInSection[loc1][1] == GuessesInSection[loc2][1]);
#endif

                            for (int loop = 0; loop < SudokuGrid.SudokuSize; loop++)
                            {

                                // skip over the two pairs we know are two pairs
                                if (loop != loc1 && loop != loc2) // TODO: I loop through these twice as often as needed -- do something to avoid running this for each pair of pairs
                                {
                                    // remove each guess that is part of the pair
                                    update = update | GuessesInSection[loop].Remove(pair1.First);
                                    update = update | GuessesInSection[loop].Remove(pair1.Second);
                                }
                            }
                            potentialMatches = GetPotentialMatches(GuessesInSection);

                        }
                    }
                }
            } // end of loop through loc1

            return update;
        }


        private int CountValues(List<int>[] GuessesInSection, int value)
        {
            // This is to remove all pairs that have a number that appears more than twice.
            // You pass it a value and it counds the number of times that value appears in GuessesInSection
            // remove guesses that appear more than twice
            int count = 0;
            
            for (int i = 0; i < GuessesInSection.Length; i++)
            {
                
                if (GuessesInSection[i].Contains(value))
                    count++;
            }

            return count;
        }



        private List<PairInfo>[] GetPotentialMatches(List<int>[] GuessesInSection)
        {
            List<PairInfo>[] potentialMatches = new List<PairInfo>[SudokuGrid.SudokuSize];

            // For each location in a row (or column or subsquare) we are going to look for pairs
            // in the sense that there are two numbers in two locations and no where else
            // If we find any, remove all other guesses for that location
            for (int loc1 = 0; loc1 < SudokuGrid.SudokuSize; loc1++)
            {
                // For each pair in a location (i.e. 1,3;  1,4;  3,4, etc.)
                List<int> guesses = new List<int>();
                guesses = GuessesInSection[loc1].ToList();


                // create all permutations of pairs
                for (int j = 0; j < guesses.Count - 1; j++)
                {
                    int first, second = 0;
                    first = guesses[j];

                    for (int h = j + 1; h < guesses.Count; h++)
                    {
                        second = guesses[h];
                        // Compare each pair with pairs in all other locations in this row (or column or subsquare)
                        for (int loc2 = 0; loc2 < SudokuGrid.SudokuSize; loc2++)
                        {
                            // skip over the location we are currently comparing everything to
                            if (loc2 == loc1)
                                continue;

                            // Count the number of matches and save their locations
                            if (GuessesInSection[loc2].Contains(first) && GuessesInSection[loc2].Contains(second))
                            {
                                if (potentialMatches[loc1] == null)
                                    potentialMatches[loc1] = new List<PairInfo>();

                                potentialMatches[loc1].Add(new PairInfo(first, second, loc2));  
                            }

                        } // end for loop for moving through each location loc2

                    } // end for loop for 'second' - go to next pair

                } // end for loop for 'first' - go to next pair

            } // end for loop for each location loc1

            return potentialMatches;
        }



        // now try making guesses and find contradictions to eliminate
        private bool Step4ElminateBadGuesses()
        {
            return false;
        }


        [Conditional("DEBUG")]
        private void DumpState()
        {
            Debug.WriteLine("Begin DumpState:");
            for (int x = 0; x < SudokuGrid.SudokuSize; x++)
            {
                for (int y = 0; y < SudokuGrid.SudokuSize; y++)
                {
                    Debug.WriteLine("target.Puzzle[" + x.ToString() + "," + y.ToString() + "] = " + Puzzle[x, y].ToString() + ";");
                    for (int i = 0; i < GuessesAvailable[x, y].Count(); i++)
                    {
                        Debug.WriteLine("target.GuessesAvailable[" + x.ToString() + "," + y.ToString() + "].Add(" + GuessesAvailable[x, y][i] + ");");
                    }
                }
            }
            Debug.WriteLine("End DumpState");
        }


        /*
            // Example: of using a comparer so that I can use Distinct
            IntegerComparer comparer = new IntegerComparer();
            listOfGuessesRow = listOfGuessesRow.Distinct<int>((IEqualityComparer<int>)comparer).ToList<int>();
         
        */

    }
}
