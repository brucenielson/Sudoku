using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace TestSudoku
{
    
    
    /// <summary>
    ///This is a test class for SudokuPuzzleTest and is intended
    ///to contain all SudokuPuzzleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudokuPuzzleTest
    {
        
        /// <summary>
        ///A test for SudokuPuzzle Constructor
        ///</summary>
        [TestMethod()]
        public void SudokuPuzzleConstructorTest()
        {
            SudokuPuzzleOld target = new SudokuPuzzleOld(true);
            Assert.IsNotNull(target);

            target = new SudokuPuzzleOld();
            Assert.IsNotNull(target);
            // this is a real puzzle, so its difficulty should be neither undetermined nor unsolveable
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            // now do a non-empty puzzle and nothing should be empty
            target = new SudokuPuzzleOld();
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
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNSOLVEABLE, target.DifficultyLevel);
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);
            Assert.IsTrue(target.TotalUncovers >= 15);
        }


        
        
        /// <summary>
        ///A test for AssessDifficulty
        ///</summary>
        [TestMethod()]
        public void AssessDifficultyTest()
        {
            SudokuPuzzleOld target = new SudokuPuzzleOld(true);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);
            target.AssessDifficulty();
            Assert.AreNotEqual(SudokuSolver.Difficulty.EASY, target.DifficultyLevel);

            target = new SudokuPuzzleOld(true);            
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);
        }
        

        /// <summary>
        ///A test for InitializePuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void InitializePuzzleTest()
        {
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
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
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.UncoverPuzzle(20);
            Assert.AreEqual(20, target.TotalUncovers);
            int count = 0;
            
            for (int x=0; x < 3; x++)
            {   
                for (int y=0; y < 3; y++)
                {
                    count = count + target.UncoverCountBySubSquare[x,y];
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
                target = new SudokuPuzzleOld_Accessor(true);
                SudokuSolution newSolution = new SudokuSolution();
                newSolution.FillSudokuSolution();
                target.SetSolution(newSolution);
                target.UncoverPuzzle(20);
                Assert.AreEqual(20, target.TotalUncovers);
            }
        }



        
        /// <summary>
        ///A test for UncoverInSubSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverInSubSquareTest()
        {
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.UncoverInSubSquare(5, 1, 1);
            Assert.AreEqual(5, target.TotalUncovers);
        }


               
        /// <summary>
        ///A test for UncoverPuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverPuzzleTest()
        {
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.UncoverPuzzle(20);
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

            target = new SudokuPuzzleOld_Accessor();
            Assert.AreEqual(20, count);

        }
        

        
        /// <summary>
        ///A test for UncoverSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverSquareTest()
        {
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            bool actual;
            actual = target.UncoverSquare(5, 6);
            Assert.IsTrue(actual);
            Assert.AreEqual(target.Solution[5,6], target[5,6]);
        }



        /// <summary>
        ///A test for UncoverCountBySubSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void UncoverCountBySubSquareTest()
        {
            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor(true);
            SudokuSolution solution = new SudokuSolution();
            solution.FillSudokuSolution();
            target.SetSolution(solution);
            target.UncoverInSubSquare(5, 1, 1);
            Assert.AreEqual(5, target.UncoverCountBySubSquare[1, 1]);
        }


        
        /// <summary>
        ///A test for FindSolveablePuzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void FindSolveablePuzzleTest()
        {
            bool actual = false;

            SudokuPuzzleOld_Accessor target = new SudokuPuzzleOld_Accessor();
            target.FindSolveablePuzzle();
            SudokuGrid solveableGrid = target.MakeCopy();
            SudokuGrid solutionGrid;

            // Create a solver with a filled in solution
            // TODO: When the solver really works, this should be changed to not start with a solution
            SudokuSolver_Accessor solver = new SudokuSolver_Accessor(solveableGrid);

            // find a solution
            actual = solver.FindSolution();
            Assert.IsTrue(actual);
            Assert.AreEqual(target.DifficultyLevel, solver.DifficultyLevel);
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.IsTrue(target.Solution[x, y] == solver.Puzzle[x, y]);
                }

            // Check that the solution is valid
            actual = solver.CheckForSolution();
            Assert.IsTrue(actual);

            // return the solution found
            solutionGrid = solver.GetSolution();
            // Test solution
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.IsTrue(target.Solution[x, y] == solutionGrid[x, y]);
                }


            // Prove it's a valid array for Sudoku
            SudokuSolution solution = new SudokuSolution(solutionGrid);
            Assert.IsTrue(solution.IsValidArray());



            // Test target.GetSolution() as well    
            solutionGrid = target.GetSolution();
            // Test solution
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.IsTrue(target.Solution[x, y] == solutionGrid[x, y]);
                }


            // Prove it's a valid array for Sudoku
            solution = new SudokuSolution(solutionGrid);
            Assert.IsTrue(solution.IsValidArray());                      

        }




        /// <summary>
        ///A test for TestUnsolveableToTrivialTransition
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void TestUnsolveableToTrivialTransition()
        {

            // I am bothered by the fact that when I don't use Steps 3 and 4 that the end difficulty is seemingly 
            // always 'Trivial'. This implies that the breaking point between solveable and unsolveable -- which 
            // should only be a single uncovered location -- is somehow jumping immediately to not needing Singletons
            // even though just one uncover ago it used them but it was unable to solve the puzzle. This seems odd to me.
            // So this test simply allows me to convince myself this isn't a problem
            /*
            Unsolveable puzzle:
            520840091
            038905400
            004032085
            083461059
            450020006
            600000104
            200083047
            070204518
            849007020

            One uncover later, only step 1 is required to solve it:
            520840091
            038905400
            004032085
            083461059
            450020006
            600300104
            200083047
            070204518
            849007020
            */

            SudokuPuzzleOld unsolveableGrid = new SudokuPuzzleOld(true);

            unsolveableGrid.SetSudokuLine(0, "520840091");
            unsolveableGrid.SetSudokuLine(1, "038905400");
            unsolveableGrid.SetSudokuLine(2, "004032085");
            unsolveableGrid.SetSudokuLine(3, "083461059");
            unsolveableGrid.SetSudokuLine(4, "450020006");
            unsolveableGrid.SetSudokuLine(5, "600000104");
            unsolveableGrid.SetSudokuLine(6, "200083047");
            unsolveableGrid.SetSudokuLine(7, "070204518");
            unsolveableGrid.SetSudokuLine(8, "849007020");

            SudokuPuzzleOld trivalGrid = new SudokuPuzzleOld(true);

            trivalGrid.SetSudokuLine(0, "520840091");
            trivalGrid.SetSudokuLine(1, "038905400");
            trivalGrid.SetSudokuLine(2, "004032085");
            trivalGrid.SetSudokuLine(3, "083461059");
            trivalGrid.SetSudokuLine(4, "450020006");
            trivalGrid.SetSudokuLine(5, "600300104"); // added a 3 at fourth column (i.e. column 3 when zero based)
            trivalGrid.SetSudokuLine(6, "200083047");
            trivalGrid.SetSudokuLine(7, "070204518");
            trivalGrid.SetSudokuLine(8, "849007020");

            // Test this
            Assert.IsTrue(unsolveableGrid.TotalUncovers == (trivalGrid.TotalUncovers - 1));

            unsolveableGrid.AssessDifficulty();
            Assert.AreEqual(SudokuSolver.Difficulty.UNSOLVEABLE, unsolveableGrid.DifficultyLevel);

            trivalGrid.AssessDifficulty();
            Assert.AreEqual(SudokuSolver.Difficulty.TRIVIAL, trivalGrid.DifficultyLevel);

        }    



        /// <summary>
        ///A test because when I changed the Singleton code to fill in the puzzle, everything broke
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void TestSingletonsToFillInPuzzle()
        {
            SudokuPuzzleOld puzzle = new SudokuPuzzleOld(true);
            SudokuSolver_Accessor target;

            puzzle.SetSudokuLine(0, "500000000");
            puzzle.SetSudokuLine(1, "800200069");
            puzzle.SetSudokuLine(2, "070034510");
            puzzle.SetSudokuLine(3, "130402095");
            puzzle.SetSudokuLine(4, "020005070");
            puzzle.SetSudokuLine(5, "000107000");
            puzzle.SetSudokuLine(6, "200008307");
            puzzle.SetSudokuLine(7, "704010000");
            puzzle.SetSudokuLine(8, "000706401");

            target = new SudokuSolver_Accessor(puzzle);
            Assert.IsNotNull(target);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, puzzle.DifficultyLevel);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            //target.GetSolution();
            // unrolling GetSolution();
            bool actual = false;
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsFalse(actual);
            actual = target.Step2UseSingletons();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsFalse(actual);
            actual = target.Step2UseSingletons();
            Assert.IsFalse(actual);


        }


    }

}
