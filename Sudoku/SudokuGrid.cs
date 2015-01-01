using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace Sudoku
{
    public class SudokuGrid : IEnumerable<SudokuGridLocation>
    {
        public const int SudokuSize = 9;
        public const int SudokuSubSquareSize = 3;


        private SudokuGridLocation[,] _grid;

        protected SudokuGridLocation[,] Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }


        public int TotalUncovers
        {
            get
            {
                int count = 0;
                for (int x = 0; x < SudokuSize; x++)
                {
                    for (int y = 0; y < SudokuSize; y++)
                    {
                        if (this[x, y] != 0)
                            count++;
                    }
                }

                return count;
            }
        }

        // virtual method -- must be filled in by subclass
        protected virtual void EliminateGuesses(int row, int col) { }



        #region constructors

        public SudokuGrid()
        {
            Grid = new SudokuGridLocation[SudokuSize, SudokuSize];
            // initialize array
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    Grid[x, y] = new SudokuGridLocation();
                }
            }
        }

        #endregion


        #region indexer

        // TODO: I want it to work like this
        // puzzle[x, y].GuessesAvailable
        // Rather then puzzle.GuessesAvailable[x, y]
        public int this[int row, int col]
        {
            get { return Grid[row, col].Value; }
            set 
            { 
                Grid[row, col].Value = value;
                //Grid[row, col].OnPropertyChanged("Value");
                
                if (value != 0)
                    EliminateGuesses(row, col);
                else // if (value == 0) if value being set to is zero, then reinitialize guesses also
                {
                    // reset guesses
                    Grid[row, col].InitializeGuesses();
                }

            }
        }

        #endregion


        #region methods

        // make backup copy of grid by cloning it
        public SudokuGrid MakeCopy()
        {
            SudokuGrid tempGrid = new SudokuGrid(); ;

            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    tempGrid[x, y] = this[x, y];
                }
            }

            return tempGrid;
        }



        public override string ToString()
        {
            string output = "";
            for (int x = 0; x < SudokuSize; x++)
            {
                for (int y = 0; y < SudokuSize; y++)
                {
                    output = output + this[x, y].ToString();
                }
                output = output + "\r\n";
            }
            return output;
        }

        public void WriteOutSudokuGrid()
        {

            #if DEBUG
                Debug.WriteLine(this.ToString());
            #else
                Console.Write(this.ToString());
            #endif

        }


        // Sets the grid line using a string of numbers
        public void SetSudokuLine(int line, string rowValues)
        {
            if (rowValues.Length != 9)
                throw new Exception("SetSudokuLine must have a exactly 9 values in rowValues");

            for (int i = 0; i < rowValues.Length; i++)
            {
                if (rowValues[i] != '0')
                    this[line, i] = Convert.ToInt16(rowValues[i].ToString());
            }
        }

        #endregion

        
        #region enumerable

        private IEnumerable<SudokuGridLocation> Enumerate()
        {
            foreach (var item in Grid)
            {
                yield return item;
            }
        }

        public IEnumerator<SudokuGridLocation> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        #endregion

    }
}
