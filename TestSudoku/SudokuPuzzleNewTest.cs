using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace TestSudoku
{
    
    
    /// <summary>
    ///This is a test class for SudokuPuzzleNewTest and is intended
    ///to contain all SudokuPuzzleNewTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudokuPuzzleNewTest
    {

        /// <summary>
        ///A test for SudokuPuzzle Constructor
        ///</summary>
        [TestMethod()]
        public void SudokuPuzzleNewConstructorTest()
        {
            SudokuPuzzle target = new SudokuPuzzle(true);
            Assert.IsNotNull(target);

            target = new SudokuPuzzle();
            Assert.IsNotNull(target);
            
            // this is a real puzzle, so its difficulty should be neither undetermined nor unsolveable
            // Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            // now do a non-empty puzzle and nothing should be empty
            target = new SudokuPuzzle();
            Assert.AreNotEqual(null, target);

            // the puzzle should not be entirely empty
            bool empty = true;

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (target[x, y] != 0)
                        empty = false;
                }
            }

            Assert.IsFalse(empty);
            //Assert.AreNotEqual(SudokuSolver.Difficulty.UNSOLVEABLE, target.DifficultyLevel);
            //Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);
            Assert.IsTrue(target.TotalUncovers >= 15);
        }


        
        /// <summary>
        ///A test for InitializePuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void InitializePuzzleTest()
        {
            SudokuPuzzle_Accessor target = new SudokuPuzzle_Accessor(true);
            target.InitializePuzzle();
            Assert.AreNotEqual(null, target);

            // the puzzle should be empty
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Assert.AreEqual(0, target[x, y]);
                }
            }
            
        }


        
        /// <summary>
        ///A test for TotalUncovers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void TotalUncoversTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.RandomlyUncoverSquares(20);
            Assert.AreEqual(20, target.TotalUncovers);
            int count = 0;
            
            for (int x=0; x < 3; x++)
            {   
                for (int y=0; y < 3; y++)
                {
                    count = count + target.UncoverCountBySubSquare(x,y);
                }
            }
            Assert.AreEqual(count, target.TotalUncovers);


            count = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (target[x, y] != 0)
                        count++;
                }
            }
            Assert.AreEqual(count, target.TotalUncovers);

            for (int i = 0; i < 25; i++)
            {
                // because there is a random element, it's good to test this a few times to be sure it really works
                target = new SudokuPuzzleNew_Accessor(true);
                SudokuSolution newSolution = new SudokuSolution();
                newSolution.FillSudokuSolution();
                target.SetSolution(newSolution);
                target.RandomlyUncoverSquares(20);
                Assert.AreEqual(20, target.TotalUncovers);
            }
             */
        }



  
               
        /// <summary>
        ///A test for UncoverPuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverPuzzleTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.RandomlyUncoverSquares(20);
            Assert.AreEqual(20, target.TotalUncovers);

            int count = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (target[x, y] != 0)
                        count++;
                }
            }
            Assert.AreEqual(20, count);

            target = new SudokuPuzzleNew_Accessor();
            Assert.AreEqual(20, count);
            */
        }
        

        
        /// <summary>
        ///A test for UncoverSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverSquareTest()
        {
            SudokuPuzzle_Accessor target = new SudokuPuzzle_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            bool actual;
            actual = target.UncoverSquare(5, 6);
            Assert.IsTrue(actual);
            Assert.AreEqual(target.Solution[5,6], target[5,6]);
        }

        

        /// <summary>
        ///A test for ClearGuesses
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ClearGuessesTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            int x = 0; // TODO: Initialize to an appropriate value
            int y = 0; // TODO: Initialize to an appropriate value
            target.ClearGuesses(value, x, y);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod()]
        public void CloneTest()
        {
            /*
            SudokuPuzzleNew target = new SudokuPuzzleNew(); // TODO: Initialize to an appropriate value
            SudokuPuzzleNew expected = null; // TODO: Initialize to an appropriate value
            SudokuPuzzleNew actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for ConstructPuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ConstructPuzzleTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            bool empty = false; // TODO: Initialize to an appropriate value
            target.ConstructPuzzle(empty);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for EliminateGuesses
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void EliminateGuessesTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int x = 0; // TODO: Initialize to an appropriate value
            int y = 0; // TODO: Initialize to an appropriate value
            target.EliminateGuesses(x, y);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for FindSolveablePuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void FindSolveablePuzzleTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            target.FindSolveablePuzzle();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for ProcessSingletonByColumn
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ProcessSingletonByColumnTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int column = 0; // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ProcessSingletonByColumn(column, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for ProcessSingletonByRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ProcessSingletonByRowTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int row = 0; // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ProcessSingletonByRow(row, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for ProcessSingletonBySubSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ProcessSingletonBySubSquareTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int subSquareX = 0; // TODO: Initialize to an appropriate value
            int subSquareY = 0; // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ProcessSingletonBySubSquare(subSquareX, subSquareY, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for RandomlyUncoverSquares
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void RandomlyUncoverSquaresTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int numberToUncover = 0; // TODO: Initialize to an appropriate value
            target.RandomlyUncoverSquares(numberToUncover);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for SetSolution
        ///</summary>
        [TestMethod()]
        public void SetSolutionTest()
        {
            /*
            SudokuPuzzleNew target = new SudokuPuzzleNew(); // TODO: Initialize to an appropriate value
            SudokuSolution solution = null; // TODO: Initialize to an appropriate value
            target.SetSolution(solution);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for SingletonsBySubSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SingletonsBySubSquareTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            int subSquareX = 0; // TODO: Initialize to an appropriate value
            int subSquareY = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SingletonsBySubSquare(subSquareX, subSquareY);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for Step1FindSingleGuesses
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step1FindSingleGuessesTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Step1FindSingleGuesses();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for Step2UseSingletons
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step2UseSingletonsTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Step2UseSingletons();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }


        /// <summary>
        ///A test for WriteOutSudokuSolution
        ///</summary>
        [TestMethod()]
        public void WriteOutSudokuSolutionTest()
        {
            /*
            SudokuPuzzleNew target = new SudokuPuzzleNew(); // TODO: Initialize to an appropriate value
            target.WriteOutSudokuSolution();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
             */
        }

        /// <summary>
        ///A test for DifficultyLevel
        ///</summary>
        [TestMethod()]
        public void DifficultyLevelTest()
        {
            /*
            SudokuPuzzleNew target = new SudokuPuzzleNew(); // TODO: Initialize to an appropriate value
            SudokuSolver.Difficulty expected = new SudokuSolver.Difficulty(); // TODO: Initialize to an appropriate value
            SudokuSolver.Difficulty actual;
            target.DifficultyLevel = expected;
            actual = target.DifficultyLevel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for MaxDifficulty
        ///</summary>
        [TestMethod()]
        public void MaxDifficultyTest()
        {
            /*
            SudokuPuzzleNew target = new SudokuPuzzleNew(); // TODO: Initialize to an appropriate value
            SudokuSolver.Difficulty expected = new SudokuSolver.Difficulty(); // TODO: Initialize to an appropriate value
            SudokuSolver.Difficulty actual;
            target.MaxDifficulty = expected;
            actual = target.MaxDifficulty;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }

        /// <summary>
        ///A test for Solution
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SolutionTest()
        {
            /*
            SudokuPuzzleNew_Accessor target = new SudokuPuzzleNew_Accessor(); // TODO: Initialize to an appropriate value
            SudokuSolution expected = null; // TODO: Initialize to an appropriate value
            SudokuSolution actual;
            target.Solution = expected;
            actual = target.Solution;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
             */
        }
    }
}
