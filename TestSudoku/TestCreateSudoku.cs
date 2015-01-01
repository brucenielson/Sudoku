using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System.Diagnostics;


namespace TestSudoku
{
    [TestClass]
    public class TestCreateSudoku
    {
        [TestMethod]
        public void TestSudokuSolutionCreation()
        {
            SudokuSolution grid = new SudokuSolution();
            
            // first test without randomizing it
            grid.NoRandomize = true;
            grid.FillSudokuSolution();
            Assert.IsTrue(grid.IsValidArray());
           

            // now test 1000 times with randomized grids
            for (int i = 0; i < 100; i++)
            {
                grid = new SudokuSolution();
                grid.FillSudokuSolution();
                Assert.IsTrue(grid.IsValidArray());
            }
             
        }

        
        [TestMethod]
        public void TestSudokuPuzzleSolver()
        {
            SudokuPuzzleOld puzzle = new SudokuPuzzleOld(true);
            SudokuSolution solution = new SudokuSolution();
            SudokuSolver solver;
            int totalUncovers = 0;
            int i;
            int countTrivial=0, countEasy=0, countMedium=0, countHard=0;

            // Test 25-1000 times with randomized puzzles
            for (i = 0; i < 25; i++) // TODO: Very very slow now when running 1000 - what slowed it down? Seems to be the DEBUG code?!?
            {
                puzzle = new SudokuPuzzleOld();
                totalUncovers += puzzle.TotalUncovers;

                solver = new SudokuSolver(puzzle);
                // Test getting solution
                solution = new SudokuSolution(solver.GetSolution());
                // Is solution valid?
                Assert.IsTrue(solution.IsValidArray());
                Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, puzzle.DifficultyLevel);
                Assert.AreNotEqual(SudokuSolver.Difficulty.UNSOLVEABLE, puzzle.DifficultyLevel);

                switch (puzzle.DifficultyLevel)
                {
                    case SudokuSolver.Difficulty.TRIVIAL:
                        countTrivial++;
                    break;
                    
                    case SudokuSolver.Difficulty.EASY:
                        countEasy++;
                    break;
                    
                    case SudokuSolver.Difficulty.MEDIUM:
                        countMedium++;
                    break;

                    case SudokuSolver.Difficulty.HARD:
                        countHard++;
                    break;
                }

                // Test solution directly
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        Assert.IsTrue(puzzle.Solution[x, y] == solution[x, y]);
                    }

            }

#if DEBUG
            Debug.WriteLine("*****");
            Debug.WriteLine("Average Uncovers: " + (totalUncovers / i).ToString());
            Debug.WriteLine("% Trivial: " + ((countTrivial*100)/i).ToString());
            Debug.WriteLine("% Easy: " + ((countEasy*100)/i).ToString());
            Debug.WriteLine("% Medium: " + ((countMedium*100)/i).ToString());
            Debug.WriteLine("% Hard: " + ((countHard*100)/i).ToString());
            Debug.WriteLine("*****");
#endif
        }
    }
}
