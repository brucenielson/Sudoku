using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestSudoku
{
    
    
    /// <summary>
    ///This is a test class for SudokuSolverTest and is intended
    ///to contain all SudokuSolverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudokuSolverTest
    {

        [TestMethod()]
        public void FullSolverTest()
        {
            SudokuGrid grid = new SudokuGrid();

            grid.SetSudokuLine(0, "000000200");
            grid.SetSudokuLine(1, "058006000");
            grid.SetSudokuLine(2, "000300085");
            grid.SetSudokuLine(3, "010470600");
            grid.SetSudokuLine(4, "906000507");
            grid.SetSudokuLine(5, "007039040");
            grid.SetSudokuLine(6, "760008000");
            grid.SetSudokuLine(7, "000900810");
            grid.SetSudokuLine(8, "009000000");

            SudokuSolver_Accessor target = new SudokuSolver_Accessor(grid);


            // set up target 2 so that I can test step 3 two ways
            SudokuGrid grid2 = new SudokuGrid();
            grid2.SetSudokuLine(0, "000000200");
            grid2.SetSudokuLine(1, "058006000");
            grid2.SetSudokuLine(2, "000300085");
            grid2.SetSudokuLine(3, "010470600");
            grid2.SetSudokuLine(4, "906000507");
            grid2.SetSudokuLine(5, "007039040");
            grid2.SetSudokuLine(6, "760008000");
            grid2.SetSudokuLine(7, "000900810");
            grid2.SetSudokuLine(8, "009000000");
            SudokuSolver_Accessor target2 = new SudokuSolver_Accessor(grid2);


            bool result = false;
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result);
            result = target.Step2UseSingletons();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result); 
            result = target.Step2UseSingletons();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result); 
            result = target.Step2UseSingletons();
            Assert.IsFalse(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result); 


            // First Row
            Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 0]));
            Assert.AreEqual("379", ConvertToString(target.GuessesAvailable[0, 1]));
            Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 2]));
            Assert.AreEqual("1578", ConvertToString(target.GuessesAvailable[0, 3]));
            Assert.AreEqual("14589", ConvertToString(target.GuessesAvailable[0, 4]));
            Assert.AreEqual("14", ConvertToString(target.GuessesAvailable[0, 5]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[0, 6]));
            Assert.AreEqual("2", target.Puzzle[0, 6].ToString());            
            Assert.AreEqual("679", ConvertToString(target.GuessesAvailable[0, 7]));
            Assert.AreEqual("13469", ConvertToString(target.GuessesAvailable[0, 8])); 

            // Second Row
            Assert.AreEqual("1234", ConvertToString(target.GuessesAvailable[1, 0]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 1]));
            Assert.AreEqual("5", target.Puzzle[1, 1].ToString()); 
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 2]));
            Assert.AreEqual("8", target.Puzzle[1, 2].ToString()); 
            Assert.AreEqual("127", ConvertToString(target.GuessesAvailable[1, 3]));
            Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[1, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 5]));
            Assert.AreEqual("6", target.Puzzle[1, 5].ToString());
            Assert.AreEqual("3479", ConvertToString(target.GuessesAvailable[1, 6]));            
            Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[1, 7]));
            Assert.AreEqual("1349", ConvertToString(target.GuessesAvailable[1, 8])); 

            // Third Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 0]));
            Assert.AreEqual("6", target.Puzzle[2, 0].ToString());
            Assert.AreEqual("279", ConvertToString(target.GuessesAvailable[2, 1]));
            Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 3]));
            Assert.AreEqual("3", target.Puzzle[2, 3].ToString());
            Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[2, 4]));
            Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 5]));
            Assert.AreEqual("479", ConvertToString(target.GuessesAvailable[2, 6]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 7]));
            Assert.AreEqual("8", target.Puzzle[2, 7].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 8]));
            Assert.AreEqual("5", target.Puzzle[2, 8].ToString()); 

            // Fourth Row
            Assert.AreEqual("238", ConvertToString(target.GuessesAvailable[3, 0]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 1]));
            Assert.AreEqual("1", target.Puzzle[3, 1].ToString());
            Assert.AreEqual("23", ConvertToString(target.GuessesAvailable[3, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 3]));
            Assert.AreEqual("4", target.Puzzle[3, 3].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 4]));
            Assert.AreEqual("7", target.Puzzle[3, 4].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 5]));
            Assert.AreEqual("5", target.Puzzle[3, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 6]));
            Assert.AreEqual("6", target.Puzzle[3, 6].ToString());
            Assert.AreEqual("29", ConvertToString(target.GuessesAvailable[3, 7]));
            Assert.AreEqual("289", ConvertToString(target.GuessesAvailable[3, 8])); 

            // Fifth Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 0]));
            Assert.AreEqual("9", target.Puzzle[4, 0].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 1]));
            Assert.AreEqual("4", target.Puzzle[4, 1].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 2]));
            Assert.AreEqual("6", target.Puzzle[4, 2].ToString());
            Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 3]));
            Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 4]));
            Assert.AreEqual("12", ConvertToString(target.GuessesAvailable[4, 5]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 6]));
            Assert.AreEqual("5", target.Puzzle[4, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 7]));
            Assert.AreEqual("3", target.Puzzle[4, 7].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 8]));
            Assert.AreEqual("7", target.Puzzle[4, 8].ToString());
            
            // Sixth Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 0]));
            Assert.AreEqual("5", target.Puzzle[5, 0].ToString());
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 1]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 2]));
            Assert.AreEqual("7", target.Puzzle[5, 2].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 3]));
            Assert.AreEqual("6", target.Puzzle[5, 3].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 4]));
            Assert.AreEqual("3", target.Puzzle[5, 4].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 5]));
            Assert.AreEqual("9", target.Puzzle[5, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 6]));
            Assert.AreEqual("1", target.Puzzle[5, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 7]));
            Assert.AreEqual("4", target.Puzzle[5, 7].ToString());
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 8])); 

            // Seventh Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 0]));
            Assert.AreEqual("7", target.Puzzle[6, 0].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 1]));
            Assert.AreEqual("6", target.Puzzle[6, 1].ToString());
            Assert.AreEqual("12345", ConvertToString(target.GuessesAvailable[6, 2]));
            Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[6, 3]));
            Assert.AreEqual("1245", ConvertToString(target.GuessesAvailable[6, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 5]));
            Assert.AreEqual("8", target.Puzzle[6, 5].ToString());
            Assert.AreEqual("349", ConvertToString(target.GuessesAvailable[6, 6]));
            Assert.AreEqual("259", ConvertToString(target.GuessesAvailable[6, 7]));
            Assert.AreEqual("2349", ConvertToString(target.GuessesAvailable[6, 8])); 

            // Eighth Row
            Assert.AreEqual("234", ConvertToString(target.GuessesAvailable[7, 0]));
            Assert.AreEqual("23", ConvertToString(target.GuessesAvailable[7, 1]));
            Assert.AreEqual("2345", ConvertToString(target.GuessesAvailable[7, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 3]));
            Assert.AreEqual("9", target.Puzzle[7, 3].ToString());
            Assert.AreEqual("2456", ConvertToString(target.GuessesAvailable[7, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 5]));
            Assert.AreEqual("7", target.Puzzle[7, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 6]));
            Assert.AreEqual("8", target.Puzzle[7, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 7]));
            Assert.AreEqual("1", target.Puzzle[7, 7].ToString());
            Assert.AreEqual("2346", ConvertToString(target.GuessesAvailable[7, 8])); 

            // Ninth Row
            Assert.AreEqual("1248", ConvertToString(target.GuessesAvailable[8, 0]));
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[8, 1]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 2]));
            Assert.AreEqual("9", target.Puzzle[8, 2].ToString());
            Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[8, 3]));
            Assert.AreEqual("12456", ConvertToString(target.GuessesAvailable[8, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 5]));
            Assert.AreEqual("3", target.Puzzle[8, 5].ToString());
            Assert.AreEqual("47", ConvertToString(target.GuessesAvailable[8, 6]));
            Assert.AreEqual("2567", ConvertToString(target.GuessesAvailable[8, 7]));
            Assert.AreEqual("246", ConvertToString(target.GuessesAvailable[8, 8])); 


            // catch target2 up
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsFalse(result);
            result = target2.Step2UseSingletons();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsFalse(result);
            result = target2.Step2UseSingletons();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsFalse(result);
            result = target2.Step2UseSingletons();
            Assert.IsFalse(result);
            result = target2.Step1ApplyNumbers();
            Assert.IsFalse(result); 



            // Now on to Step 3 -- first test it in parts            
            result = target.PairsByRowAndColumn(0);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(1);
            Assert.IsTrue(result);
            result = target.PairsByRowAndColumn(2);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(3);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(4);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(5);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(6);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(7);
            Assert.IsFalse(result);
            result = target.PairsByRowAndColumn(8);
            Assert.IsFalse(result);


            Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 0]));
            Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[0, 1]));
            Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 2]));
            Assert.AreEqual("1578", ConvertToString(target.GuessesAvailable[0, 3]));
            Assert.AreEqual("14589", ConvertToString(target.GuessesAvailable[0, 4]));
            Assert.AreEqual("14", ConvertToString(target.GuessesAvailable[0, 5]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[0, 6]));
            Assert.AreEqual("2", target.Puzzle[0, 6].ToString());
            Assert.AreEqual("679", ConvertToString(target.GuessesAvailable[0, 7]));
            Assert.AreEqual("13469", ConvertToString(target.GuessesAvailable[0, 8]));

            // Second Row
            Assert.AreEqual("1234", ConvertToString(target.GuessesAvailable[1, 0]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 1]));
            Assert.AreEqual("5", target.Puzzle[1, 1].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 2]));
            Assert.AreEqual("8", target.Puzzle[1, 2].ToString());
            Assert.AreEqual("127", ConvertToString(target.GuessesAvailable[1, 3]));
            Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[1, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 5]));
            Assert.AreEqual("6", target.Puzzle[1, 5].ToString());
            Assert.AreEqual("3479", ConvertToString(target.GuessesAvailable[1, 6]));
            Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[1, 7]));
            Assert.AreEqual("1349", ConvertToString(target.GuessesAvailable[1, 8]));

            // Third Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 0]));
            Assert.AreEqual("6", target.Puzzle[2, 0].ToString());
            Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[2, 1]));
            Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 3]));
            Assert.AreEqual("3", target.Puzzle[2, 3].ToString());
            Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[2, 4]));
            Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 5]));
            Assert.AreEqual("479", ConvertToString(target.GuessesAvailable[2, 6]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 7]));
            Assert.AreEqual("8", target.Puzzle[2, 7].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 8]));
            Assert.AreEqual("5", target.Puzzle[2, 8].ToString());

            // Fourth Row
            Assert.AreEqual("238", ConvertToString(target.GuessesAvailable[3, 0]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 1]));
            Assert.AreEqual("1", target.Puzzle[3, 1].ToString());
            Assert.AreEqual("23", ConvertToString(target.GuessesAvailable[3, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 3]));
            Assert.AreEqual("4", target.Puzzle[3, 3].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 4]));
            Assert.AreEqual("7", target.Puzzle[3, 4].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 5]));
            Assert.AreEqual("5", target.Puzzle[3, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 6]));
            Assert.AreEqual("6", target.Puzzle[3, 6].ToString());
            Assert.AreEqual("29", ConvertToString(target.GuessesAvailable[3, 7]));
            Assert.AreEqual("289", ConvertToString(target.GuessesAvailable[3, 8]));

            // Fifth Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 0]));
            Assert.AreEqual("9", target.Puzzle[4, 0].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 1]));
            Assert.AreEqual("4", target.Puzzle[4, 1].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 2]));
            Assert.AreEqual("6", target.Puzzle[4, 2].ToString());
            Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 3]));
            Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 4]));
            Assert.AreEqual("12", ConvertToString(target.GuessesAvailable[4, 5]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 6]));
            Assert.AreEqual("5", target.Puzzle[4, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 7]));
            Assert.AreEqual("3", target.Puzzle[4, 7].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 8]));
            Assert.AreEqual("7", target.Puzzle[4, 8].ToString());

            // Sixth Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 0]));
            Assert.AreEqual("5", target.Puzzle[5, 0].ToString());
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 1]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 2]));
            Assert.AreEqual("7", target.Puzzle[5, 2].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 3]));
            Assert.AreEqual("6", target.Puzzle[5, 3].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 4]));
            Assert.AreEqual("3", target.Puzzle[5, 4].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 5]));
            Assert.AreEqual("9", target.Puzzle[5, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 6]));
            Assert.AreEqual("1", target.Puzzle[5, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 7]));
            Assert.AreEqual("4", target.Puzzle[5, 7].ToString());
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 8]));

            // Seventh Row
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 0]));
            Assert.AreEqual("7", target.Puzzle[6, 0].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 1]));
            Assert.AreEqual("6", target.Puzzle[6, 1].ToString());
            Assert.AreEqual("12345", ConvertToString(target.GuessesAvailable[6, 2]));
            Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[6, 3]));
            Assert.AreEqual("1245", ConvertToString(target.GuessesAvailable[6, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 5]));
            Assert.AreEqual("8", target.Puzzle[6, 5].ToString());
            Assert.AreEqual("349", ConvertToString(target.GuessesAvailable[6, 6]));
            Assert.AreEqual("259", ConvertToString(target.GuessesAvailable[6, 7]));
            Assert.AreEqual("2349", ConvertToString(target.GuessesAvailable[6, 8]));

            // Eighth Row
            Assert.AreEqual("234", ConvertToString(target.GuessesAvailable[7, 0]));
            Assert.AreEqual("3", ConvertToString(target.GuessesAvailable[7, 1]));
            Assert.AreEqual("2345", ConvertToString(target.GuessesAvailable[7, 2]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 3]));
            Assert.AreEqual("9", target.Puzzle[7, 3].ToString());
            Assert.AreEqual("2456", ConvertToString(target.GuessesAvailable[7, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 5]));
            Assert.AreEqual("7", target.Puzzle[7, 5].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 6]));
            Assert.AreEqual("8", target.Puzzle[7, 6].ToString());
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 7]));
            Assert.AreEqual("1", target.Puzzle[7, 7].ToString());
            Assert.AreEqual("2346", ConvertToString(target.GuessesAvailable[7, 8]));

            // Ninth Row
            Assert.AreEqual("1248", ConvertToString(target.GuessesAvailable[8, 0]));
            Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[8, 1]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 2]));
            Assert.AreEqual("9", target.Puzzle[8, 2].ToString());
            Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[8, 3]));
            Assert.AreEqual("12456", ConvertToString(target.GuessesAvailable[8, 4]));
            Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 5]));
            Assert.AreEqual("3", target.Puzzle[8, 5].ToString());
            Assert.AreEqual("47", ConvertToString(target.GuessesAvailable[8, 6]));
            Assert.AreEqual("2567", ConvertToString(target.GuessesAvailable[8, 7]));
            Assert.AreEqual("246", ConvertToString(target.GuessesAvailable[8, 8]));


            
            
            // go back to step 1
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result);
            result = target.Step2UseSingletons();
            Assert.IsFalse(result); 
            


            for(int i=0; i < 2; i++)
            {
                Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 0]));
                Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[0, 1]));
                Assert.AreEqual("134", ConvertToString(target.GuessesAvailable[0, 2]));
                Assert.AreEqual("1578", ConvertToString(target.GuessesAvailable[0, 3]));
                Assert.AreEqual("14589", ConvertToString(target.GuessesAvailable[0, 4]));
                Assert.AreEqual("14", ConvertToString(target.GuessesAvailable[0, 5]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[0, 6]));
                Assert.AreEqual("2", target.Puzzle[0, 6].ToString());
                Assert.AreEqual("679", ConvertToString(target.GuessesAvailable[0, 7]));
                Assert.AreEqual("13469", ConvertToString(target.GuessesAvailable[0, 8]));

                // Second Row
                Assert.AreEqual("1234", ConvertToString(target.GuessesAvailable[1, 0]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 1]));
                Assert.AreEqual("5", target.Puzzle[1, 1].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 2]));
                Assert.AreEqual("8", target.Puzzle[1, 2].ToString());
                Assert.AreEqual("127", ConvertToString(target.GuessesAvailable[1, 3]));
                Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[1, 4]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[1, 5]));
                Assert.AreEqual("6", target.Puzzle[1, 5].ToString());
                Assert.AreEqual("3479", ConvertToString(target.GuessesAvailable[1, 6]));
                Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[1, 7]));
                Assert.AreEqual("1349", ConvertToString(target.GuessesAvailable[1, 8]));

                // Third Row
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 0]));
                Assert.AreEqual("6", target.Puzzle[2, 0].ToString());
                Assert.AreEqual("79", ConvertToString(target.GuessesAvailable[2, 1]));
                Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 2]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 3]));
                Assert.AreEqual("3", target.Puzzle[2, 3].ToString());
                Assert.AreEqual("1249", ConvertToString(target.GuessesAvailable[2, 4]));
                Assert.AreEqual("124", ConvertToString(target.GuessesAvailable[2, 5]));
                Assert.AreEqual("479", ConvertToString(target.GuessesAvailable[2, 6]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 7]));
                Assert.AreEqual("8", target.Puzzle[2, 7].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[2, 8]));
                Assert.AreEqual("5", target.Puzzle[2, 8].ToString());

                // Fourth Row
                Assert.AreEqual("238", ConvertToString(target.GuessesAvailable[3, 0]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 1]));
                Assert.AreEqual("1", target.Puzzle[3, 1].ToString());
                Assert.AreEqual("23", ConvertToString(target.GuessesAvailable[3, 2]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 3]));
                Assert.AreEqual("4", target.Puzzle[3, 3].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 4]));
                Assert.AreEqual("7", target.Puzzle[3, 4].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 5]));
                Assert.AreEqual("5", target.Puzzle[3, 5].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[3, 6]));
                Assert.AreEqual("6", target.Puzzle[3, 6].ToString());
                Assert.AreEqual("29", ConvertToString(target.GuessesAvailable[3, 7]));
                Assert.AreEqual("289", ConvertToString(target.GuessesAvailable[3, 8]));

                // Fifth Row
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 0]));
                Assert.AreEqual("9", target.Puzzle[4, 0].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 1]));
                Assert.AreEqual("4", target.Puzzle[4, 1].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 2]));
                Assert.AreEqual("6", target.Puzzle[4, 2].ToString());
                Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 3]));
                Assert.AreEqual("128", ConvertToString(target.GuessesAvailable[4, 4]));
                Assert.AreEqual("12", ConvertToString(target.GuessesAvailable[4, 5]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 6]));
                Assert.AreEqual("5", target.Puzzle[4, 6].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 7]));
                Assert.AreEqual("3", target.Puzzle[4, 7].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[4, 8]));
                Assert.AreEqual("7", target.Puzzle[4, 8].ToString());

                // Sixth Row
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 0]));
                Assert.AreEqual("5", target.Puzzle[5, 0].ToString());
                Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 1]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 2]));
                Assert.AreEqual("7", target.Puzzle[5, 2].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 3]));
                Assert.AreEqual("6", target.Puzzle[5, 3].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 4]));
                Assert.AreEqual("3", target.Puzzle[5, 4].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 5]));
                Assert.AreEqual("9", target.Puzzle[5, 5].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 6]));
                Assert.AreEqual("1", target.Puzzle[5, 6].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[5, 7]));
                Assert.AreEqual("4", target.Puzzle[5, 7].ToString());
                Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[5, 8]));

                // Seventh Row
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 0]));
                Assert.AreEqual("7", target.Puzzle[6, 0].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 1]));
                Assert.AreEqual("6", target.Puzzle[6, 1].ToString());
                Assert.AreEqual("1245", ConvertToString(target.GuessesAvailable[6, 2]));
                Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[6, 3]));
                Assert.AreEqual("1245", ConvertToString(target.GuessesAvailable[6, 4]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[6, 5]));
                Assert.AreEqual("8", target.Puzzle[6, 5].ToString());
                Assert.AreEqual("349", ConvertToString(target.GuessesAvailable[6, 6]));
                Assert.AreEqual("259", ConvertToString(target.GuessesAvailable[6, 7]));
                Assert.AreEqual("2349", ConvertToString(target.GuessesAvailable[6, 8]));

                // Eighth Row
                Assert.AreEqual("24", ConvertToString(target.GuessesAvailable[7, 0]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 1]));
                Assert.AreEqual("3", target.Puzzle[7, 1].ToString());
                Assert.AreEqual("245", ConvertToString(target.GuessesAvailable[7, 2]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 3]));
                Assert.AreEqual("9", target.Puzzle[7, 3].ToString());
                Assert.AreEqual("2456", ConvertToString(target.GuessesAvailable[7, 4]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 5]));
                Assert.AreEqual("7", target.Puzzle[7, 5].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 6]));
                Assert.AreEqual("8", target.Puzzle[7, 6].ToString());
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[7, 7]));
                Assert.AreEqual("1", target.Puzzle[7, 7].ToString());
                Assert.AreEqual("246", ConvertToString(target.GuessesAvailable[7, 8]));

                // Ninth Row
                Assert.AreEqual("1248", ConvertToString(target.GuessesAvailable[8, 0]));
                Assert.AreEqual("28", ConvertToString(target.GuessesAvailable[8, 1]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 2]));
                Assert.AreEqual("9", target.Puzzle[8, 2].ToString());
                Assert.AreEqual("125", ConvertToString(target.GuessesAvailable[8, 3]));
                Assert.AreEqual("12456", ConvertToString(target.GuessesAvailable[8, 4]));
                Assert.AreEqual("", ConvertToString(target.GuessesAvailable[8, 5]));
                Assert.AreEqual("3", target.Puzzle[8, 5].ToString());
                Assert.AreEqual("47", ConvertToString(target.GuessesAvailable[8, 6]));
                Assert.AreEqual("2567", ConvertToString(target.GuessesAvailable[8, 7]));
                Assert.AreEqual("246", ConvertToString(target.GuessesAvailable[8, 8]));

                // now repeat for target2, but this time use Step3
                if (target != target2)
                {
                    target = target2;

                    result = target.Step3UsePairs();
                    Assert.IsTrue(result);                                
                    result = target.Step1ApplyNumbers();
                    Assert.IsTrue(result);
                    result = target.Step1ApplyNumbers();
                    Assert.IsTrue(result);
                    result = target.Step1ApplyNumbers();
                    Assert.IsFalse(result);
                    result = target.Step2UseSingletons();
                    Assert.IsFalse(result);
                }

            }
            
        }

        public string ConvertToString(List<int> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s = s + list[i].ToString();
            }

            return s;
        }


        /// <summary>
        ///A test for SudokuSolver Constructor
        ///</summary>
        [TestMethod()]
        public void SudokuSolverConstructorTest()
        {
            // empty
            SudokuPuzzleOld puzzle = new SudokuPuzzleOld(true);
            SudokuSolver target = new SudokuSolver(puzzle);
            Assert.IsNotNull(target);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            target.GetSolution();
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);


            // not empty
            puzzle = new SudokuPuzzleOld();
            target = new SudokuSolver(puzzle);
            Assert.IsNotNull(target);
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, puzzle.DifficultyLevel);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            target.GetSolution();
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

        }
        

        /*
        /// <summary>
        ///A test for ClearPairGuesses
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ClearPairGuessesTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            // setup for row test
            target.GuessesAvailable[0, 0].Add(2);
            target.GuessesAvailable[0, 0].Add(5);

            target.GuessesAvailable[0, 6].Add(2);
            target.GuessesAvailable[0, 6].Add(5);

            target.GuessesAvailable[0, 4].Add(2);
            target.GuessesAvailable[0, 4].Add(3);
            target.GuessesAvailable[0, 5].Add(4);
            target.Puzzle[0, 8] = 1; // had to add this so that we'd not throw an error when the '5' below gets deleted.
            target.GuessesAvailable[0, 8].Add(5);

            target.ClearPairGuesses(2, 5, 0, 0, 0, 6);

            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(5));
            Assert.IsTrue(target.GuessesAvailable[0, 6].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 6].Contains(5));
            Assert.IsFalse(target.GuessesAvailable[0, 4].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(4));
            Assert.IsFalse(target.GuessesAvailable[0, 8].Contains(5));

        
            
            // TODO: setup for column test

            // TODO: setup for subsquare test


            // Crazy Test
            puzzle = new SudokuGrid();
            target = new SudokuSolver_Accessor(puzzle);

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.Puzzle[1,0] = 0;
target.GuessesAvailable[1,0].Add(1);
target.GuessesAvailable[1,0].Add(2);
target.GuessesAvailable[1,0].Add(3);
target.GuessesAvailable[1,0].Add(7);
target.GuessesAvailable[1,0].Add(8);
target.Puzzle[1,1] = 0;
target.GuessesAvailable[1,1].Add(1);
target.GuessesAvailable[1,1].Add(2);
target.GuessesAvailable[1,1].Add(3);
target.GuessesAvailable[1,1].Add(6);
target.Puzzle[1,2] = 0;
target.GuessesAvailable[1,2].Add(1);
target.GuessesAvailable[1,2].Add(2);
target.GuessesAvailable[1,2].Add(3);
target.GuessesAvailable[1,2].Add(6);
target.GuessesAvailable[1,2].Add(7);
target.GuessesAvailable[1,2].Add(8);

target.Puzzle[1,3] = 0;
target.GuessesAvailable[1,3].Add(1);
target.GuessesAvailable[1,3].Add(2);
target.GuessesAvailable[1,3].Add(3);
target.GuessesAvailable[1,3].Add(4);
target.GuessesAvailable[1,3].Add(5);
target.GuessesAvailable[1,3].Add(7);
target.GuessesAvailable[1,3].Add(8);
target.Puzzle[1,4] = 0;
target.GuessesAvailable[1,4].Add(1);
target.GuessesAvailable[1,4].Add(2);
target.GuessesAvailable[1,4].Add(3);
target.GuessesAvailable[1,4].Add(4);
target.GuessesAvailable[1,4].Add(5);
target.GuessesAvailable[1,4].Add(8);
target.Puzzle[1,5] = 0;
target.GuessesAvailable[1,5].Add(1);
target.GuessesAvailable[1,5].Add(2);
target.GuessesAvailable[1,5].Add(3);
target.GuessesAvailable[1,5].Add(4);
target.GuessesAvailable[1,5].Add(5);
target.GuessesAvailable[1,5].Add(8);
target.Puzzle[1,6] = 9;

target.Puzzle[1,7] = 0;
target.GuessesAvailable[1,7].Add(1);
target.GuessesAvailable[1,7].Add(3);
target.GuessesAvailable[1,7].Add(5);
target.GuessesAvailable[1,7].Add(6);
target.GuessesAvailable[1,7].Add(7);
target.GuessesAvailable[1,7].Add(8);
target.Puzzle[1,8] = 0;
target.GuessesAvailable[1,8].Add(1);
target.GuessesAvailable[1,8].Add(5);
target.GuessesAvailable[1,8].Add(6);
target.GuessesAvailable[1,8].Add(7);
/*
 *                 862743951

[1, 2]: 26*
[1, 1]: 26*
[1, 7]: 56*
[1, 8]: 56*
[1, 2]: 68*
[1, 7]: 68*
 revised
[1, 7]: 15
[1, 8]: 15
[1, 2]: 26
[1, 1]: 26
[1, 2]: 68
[1, 7]: 68

                431956287
                862743951
                759812634
                216378549
                974125863
                385469172
                127684395
                598237416
                643591728

            */
/*target.PairsByRowAndColumn(1);



/*            
            // Another test
            // reset grid and guesses
            //puzzle = new SudokuPuzzle(true);
            SudokuGrid grid = new SudokuGrid();

            grid.SetSudokuLine(0, "400006200");
            grid.SetSudokuLine(1, "000000900");
            grid.SetSudokuLine(2, "050000004");
            grid.SetSudokuLine(3, "000070509");
            grid.SetSudokuLine(4, "070000003");
            grid.SetSudokuLine(5, "085069000");
            grid.SetSudokuLine(6, "000000090");
            grid.SetSudokuLine(7, "500007000");
            grid.SetSudokuLine(8, "600090008");

            target = new SudokuSolver_Accessor(grid);

            bool actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            target.DumpState();

            actual = target.Step2UseSingletons();
            Assert.IsFalse(actual);
            target.DumpState();

            /*
            target.PairsByRowAndColumn(0);
            target.DumpState();
            //target.PairsByRowAndColumn(1);
            
            // mimic part way through Step 3
            /*
            // one row and column at a time
            for (int i = 0; i < SudokuGrid.SudokuSubSquareSize; i++)
            {
                target.PairsByRowAndColumn(i);
            }

            
            // look for pairs in subsquare
            for (int subSquareX = 0; subSquareX < SudokuGrid.SudokuSubSquareSize; subSquareX++)
            {
                for (int subSquareY = 0; subSquareY < SudokuGrid.SudokuSubSquareSize; subSquareY++)
                {
                    target.PairsBySubSquare(subSquareX, subSquareY);
                }
            }

            // now here is the troubled line
            List<SudokuSolver_Accessor.PairInfo> listPairs = new List<SudokuSolver_Accessor.PairInfo>();
            SudokuSolver_Accessor.PairInfo pair;
            pair = new SudokuSolver_Accessor.PairInfo(1, 6, 3, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(1, 6, 4, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(2, 7, 8, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(2, 7, 6, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(2, 9, 4, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(2, 9, 7, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(4, 7, 6, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(4, 7, 8, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(4, 9, 7, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(4, 9, 4, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(7, 9, 0, 2);
            listPairs.Add(pair);
            pair = new SudokuSolver_Accessor.PairInfo(7, 9, 2, 2);
            listPairs.Add(pair);

            /*
            for (int i = 0; i < listPairs.Count - 1; i += 2)
            {
                // setup variables to make code more readable
                int first = listPairs[i].First;
                int second = listPairs[i].Second;
                int x1, y1, x2, y2;
                x1 = listPairs[i].Row1;
                y1 = listPairs[i].Column1;
                x2 = listPairs[i + 1].Row1;
                y2 = listPairs[i + 1].Column1;

                // For each pair in the grid, remove all guesses but the pair itself
                List<int> guesses = target.GuessesAvailable[x1, y1];
                foreach (int g in guesses)
                {
                    if (g != first && g != second)
                    {
                        target.GuessesAvailable[x1, y1].Remove(g);
                    }
                }
                guesses = target.GuessesAvailable[x2, y2];
                foreach (int g in guesses)
                {
                    if (g != first && g != second)
                    {
                        target.GuessesAvailable[x2, y2].Remove(g);
                    }
                }

                // Now, for each of the two matching pairs, loop through each row, column, and subsquare 
                // and remove all guesses that match the two members of the pair
                target.ClearPairGuesses(first, second, x1, y1, x2, y2);
            }
             

            // another crazy test


        }*/

        
        /// <summary>
        ///A test for CheckForSolution
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void CheckForSolutionTest()
        {
            bool actual = false;

            /*
                431956287
                862743951
                759812634
                216378549
                974125863
                385469172
                127684395
                598237416
                643591728
            */

            // test with everything filled in
            SudokuGrid grid = new SudokuGrid();

            grid.SetSudokuLine(0, "431956287");
            grid.SetSudokuLine(1, "862743951");
            grid.SetSudokuLine(2, "759812634");
            grid.SetSudokuLine(3, "216378549");
            grid.SetSudokuLine(4, "974125863");
            grid.SetSudokuLine(5, "385469172");
            grid.SetSudokuLine(6, "127684395");
            grid.SetSudokuLine(7, "598237416");
            grid.SetSudokuLine(8, "643591728");

            SudokuSolver_Accessor target = new SudokuSolver_Accessor(grid);

            // this should remove all guesses
            target.Step1ApplyNumbers();

            actual = target.CheckForSolution();

            Assert.IsTrue(actual);


            // now test with some missing
            grid = new SudokuGrid();

            grid.SetSudokuLine(0, "431956287");
            //grid.SetSudokuLine(1, "862743951");
            //grid.SetSudokuLine(2, "759812634");
            grid.SetSudokuLine(3, "216378549");
            grid.SetSudokuLine(4, "974125863");
            grid.SetSudokuLine(5, "385469172");
            grid.SetSudokuLine(6, "127684395");
            grid.SetSudokuLine(7, "598237416");
            grid.SetSudokuLine(8, "643591728");

            target = new SudokuSolver_Accessor(grid);

            target.Step1ApplyNumbers();

            actual = target.CheckForSolution();

            Assert.IsFalse(actual);

        }
        



        
        /// <summary>
        ///A test for DifficultyLevel
        ///</summary>
        [TestMethod()]
        public void DifficultyLevelTest()
        {
            SudokuPuzzleOld puzzle = null; 
            SudokuSolver target = new SudokuSolver(puzzle);
            SudokuSolver.Difficulty expected = SudokuSolver.Difficulty.UNDETERMINED;
            SudokuSolver.Difficulty actual;
            actual = target.DifficultyLevel;
            Assert.AreEqual(expected, actual);           

            
            // Now do a test after calling to find a solution and it should no longer be "UNSOLVEABLE"
            puzzle = new SudokuPuzzleOld();
            SudokuSolver_Accessor target2 = new SudokuSolver_Accessor(puzzle);
            target2.FindSolution();
            actual = target2.DifficultyLevel;
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, actual);
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNSOLVEABLE, actual);

             
        }
    


        
        
        /// <summary>
        ///A test for FindSolution
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void FindSolutionTest()
        {
            SudokuPuzzleOld puzzle = new SudokuPuzzleOld();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.FindSolution();
            SudokuSolver.Difficulty actual = target.DifficultyLevel;
            // With the defaults, the most likely thing is that it will be unsolveable. So just check that 
            // it is not undetermined any more
            Assert.AreNotEqual(SudokuSolver.Difficulty.UNDETERMINED, actual);
        }
        

        
        /// <summary>
        ///A test for GetSolution
        ///</summary>
        [TestMethod()]
        public void GetSolutionTest()
        {
            bool actual = false;

            // test with everything filled in
            SudokuGrid grid = new SudokuGrid();
            SudokuGrid solutionGrid;

            grid.SetSudokuLine(0, "431956287");
            grid.SetSudokuLine(1, "862743951");
            grid.SetSudokuLine(2, "759812634");
            grid.SetSudokuLine(3, "216378549");
            grid.SetSudokuLine(4, "974125863");
            grid.SetSudokuLine(5, "385469172");
            grid.SetSudokuLine(6, "127684395");
            grid.SetSudokuLine(7, "598237416");
            grid.SetSudokuLine(8, "643591728");

            SudokuSolver_Accessor target = new SudokuSolver_Accessor(grid);

            // find a solution -- since it's all filled in, should figure it out on step 1
            actual = target.FindSolution();
            Assert.IsTrue(actual);

            // Check that the solution is valid
            actual = target.CheckForSolution();
            Assert.IsTrue(actual);

            // return the solution found
            solutionGrid = target.GetSolution();

            // Prove it's a valid array for Sudoku
            SudokuSolution solution = new SudokuSolution(solutionGrid);
            Assert.IsTrue(solution.IsValidArray());

            // Put tests on solution here
            for (int x=0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.IsTrue(target.Puzzle[x, y] == solutionGrid[x, y]);
                }
        }



        /// <summary>
        ///A test for Step1ApplyNumbers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step1ApplyNumbersTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.Puzzle[3, 0] = 2;
            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(2));
            target.Step1ApplyNumbers();

            // test subsquare
            Assert.IsFalse(target.GuessesAvailable[3, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 1].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 2].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[4, 1].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[4, 2].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[5, 1].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[5, 2].Contains(2));
            
            // test column
            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[1, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[2, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[6, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[7, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[8, 0].Contains(2));

            // test row
            Assert.IsFalse(target.GuessesAvailable[3, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 1].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 2].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 3].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 4].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 5].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 6].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 7].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 8].Contains(2));

            // test some that should not have been removed
            Assert.IsTrue(target.GuessesAvailable[2, 2].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[8, 7].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[6, 5].Contains(2));

            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            // test if puzzle is getting updated as we go
            puzzle = new SudokuGrid();
            target = new SudokuSolver_Accessor(puzzle);
            target.Puzzle[0, 1] = 1;
            target.Puzzle[0, 2] = 2;
            target.Puzzle[0, 3] = 3;
            target.GuessesAvailable[0, 4].Remove(9);
            target.GuessesAvailable[0, 4].Remove(7);
            target.Puzzle[0, 5] = 5;
            target.Puzzle[0, 6] = 6;
            target.GuessesAvailable[0, 7].Remove(9);
            target.GuessesAvailable[0, 7].Remove(4);
            target.Puzzle[0, 8] = 8;

            Assert.AreEqual(0, target.Puzzle[0, 4]);
            Assert.AreEqual(7, target.GuessesAvailable[0, 4].Count);
            Assert.AreEqual(0, target.Puzzle[0, 7]);
            Assert.AreEqual(7, target.GuessesAvailable[0, 7].Count);
            Assert.AreEqual(0, target.Puzzle[0, 0]);
            Assert.AreEqual(9, target.GuessesAvailable[0, 0].Count);

            bool result;
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);

            Assert.AreEqual(4, target.Puzzle[0, 4]);
            Assert.AreEqual(1, target.GuessesAvailable[0, 4].Count);
            Assert.AreEqual(7, target.Puzzle[0, 7]);
            Assert.AreEqual(1, target.GuessesAvailable[0, 7].Count);
            Assert.AreEqual(0, target.Puzzle[0, 0]);
            Assert.AreEqual(3, target.GuessesAvailable[0, 0].Count);

            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsTrue(result);
            result = target.Step1ApplyNumbers();
            Assert.IsFalse(result);

            Assert.AreEqual(4, target.Puzzle[0, 4]);
            Assert.AreEqual(0, target.GuessesAvailable[0, 4].Count);
            Assert.AreEqual(7, target.Puzzle[0, 7]);
            Assert.AreEqual(0, target.GuessesAvailable[0, 7].Count);
            Assert.AreEqual(9, target.Puzzle[0, 0]);
            Assert.AreEqual(0, target.GuessesAvailable[0, 0].Count);
        }


        
        /// <summary>
        ///A test for Step2UseSingletons
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step2UseSingletonsTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.GuessesAvailable[0, 0].Remove(9);
            target.GuessesAvailable[0, 1].Remove(9);
            target.GuessesAvailable[0, 2].Remove(9);
            target.GuessesAvailable[0, 3].Remove(9);
            target.GuessesAvailable[0, 4].Remove(9);
            target.GuessesAvailable[0, 5].Remove(9);
            // [0, 6] has a singleton
            target.GuessesAvailable[0, 7].Remove(9);
            target.GuessesAvailable[0, 8].Remove(9);

            target.GuessesAvailable[0, 0].Remove(9);
            target.GuessesAvailable[1, 0].Remove(9);
            target.GuessesAvailable[2, 0].Remove(9);
            target.GuessesAvailable[3, 0].Remove(9);
            target.GuessesAvailable[4, 0].Remove(9);
            target.GuessesAvailable[5, 0].Remove(9);
            // [6, 0] has a singleton
            target.GuessesAvailable[7, 0].Remove(9);
            target.GuessesAvailable[8, 0].Remove(9);

            target.GuessesAvailable[3, 0].Remove(9);
            target.GuessesAvailable[3, 1].Remove(9);
            target.GuessesAvailable[3, 2].Remove(9);
            target.GuessesAvailable[4, 0].Remove(9);
            target.GuessesAvailable[4, 1].Remove(9);
            target.GuessesAvailable[4, 2].Remove(9);
            target.GuessesAvailable[5, 0].Remove(9);
            // [5, 1] has a singleton
            target.GuessesAvailable[5, 2].Remove(9);


            bool actual = target.Step2UseSingletons();
            Assert.IsTrue(actual); // an update took place
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);

            Assert.AreEqual(9, target.GuessesAvailable[0, 6][0]);
            Assert.AreEqual(9, target.GuessesAvailable[6, 0][0]);
            Assert.IsTrue(target.GuessesAvailable[7, 6].Count > 1);
            Assert.AreEqual(9, target.GuessesAvailable[5, 1][0]);
            Assert.IsTrue(target.GuessesAvailable[5, 2].Count > 1);
            Assert.IsTrue(target.GuessesAvailable[8, 8].Count > 1);
        }


 
        
        /// <summary>
        ///A test for Step3UsePairs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step3UsePairsTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            bool actual;

            
            // test by row

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(7);
            target.GuessesAvailable[0, 0].Add(9);

            target.GuessesAvailable[0, 2].Add(2);
            target.GuessesAvailable[0, 2].Add(7);
            target.GuessesAvailable[0, 2].Add(9);

            target.GuessesAvailable[0, 3].Add(3);
            target.GuessesAvailable[0, 4].Add(7);
            target.GuessesAvailable[0, 4].Add(8);
            target.GuessesAvailable[0, 5].Add(9);
            target.GuessesAvailable[0, 5].Add(5);

            actual = target.Step3UsePairs();
            Assert.IsTrue(actual);

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(9));

            Assert.IsFalse(target.GuessesAvailable[0, 2].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 3].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(5));

            actual = target.Step3UsePairs();
            Assert.IsTrue(actual);

            Assert.IsTrue(target.GuessesAvailable[0, 3].Contains(3));
            Assert.IsFalse(target.GuessesAvailable[0, 4].Contains(7));
            Assert.IsFalse(target.GuessesAvailable[0, 5].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(5));

            

            // now by column
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(7);
            target.GuessesAvailable[0, 0].Add(9);

            target.GuessesAvailable[2, 0].Add(2);
            target.GuessesAvailable[2, 0].Add(7);
            target.GuessesAvailable[2, 0].Add(9);

            target.GuessesAvailable[3, 0].Add(3);
            target.GuessesAvailable[4, 0].Add(7);
            target.GuessesAvailable[4, 0].Add(8);
            target.GuessesAvailable[5, 0].Add(9);
            target.GuessesAvailable[5, 0].Add(5);

            actual = target.Step3UsePairs();

            Assert.IsTrue(actual);

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(9));

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[2, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[2, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(5));


            actual = target.Step3UsePairs();
            Assert.IsTrue(actual);

            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(3));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Contains(7));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(5));

            

            // This was a trickier example, but now that I understand it better
            // it should really just do nothing. 
            
            // Setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(1);
            target.GuessesAvailable[0, 0].Add(2);
            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(5);

            target.GuessesAvailable[0, 4].Add(1);
            target.GuessesAvailable[0, 4].Add(2);
            target.GuessesAvailable[0, 4].Add(6);

            target.GuessesAvailable[0, 6].Add(1);
            target.GuessesAvailable[0, 6].Add(2);
            target.GuessesAvailable[0, 6].Add(3);
            target.GuessesAvailable[0, 6].Add(5);
            target.GuessesAvailable[0, 6].Add(6);

            target.GuessesAvailable[0, 7].Add(2);
            target.GuessesAvailable[0, 7].Add(3);

            target.GuessesAvailable[0, 8].Add(1);
            target.GuessesAvailable[0, 8].Add(3);
            target.GuessesAvailable[0, 8].Add(5);
            target.GuessesAvailable[0, 8].Add(6);

            actual = target.Step3UsePairs();

            Assert.IsFalse(actual);


            // Now do an example I know failed in the past -- it fills in an entire grid
            /*
                Solution:
                431956287
                862743951
                759812634
                216378549
                974125863
                385469172
                127684395
                598237416
                643591728

                Puzzle:
                400006200
                000000900
                050000004
                000070509
                070000003
                085069000
                000000090
                500007000
                600090008
            */


            // reset grid and guesses
            //puzzle = new SudokuPuzzle(true);
            SudokuGrid grid = new SudokuGrid();
            
            grid.SetSudokuLine(0, "400006200");
            grid.SetSudokuLine(1, "000000900");
            grid.SetSudokuLine(2, "050000004");
            grid.SetSudokuLine(3, "000070509");
            grid.SetSudokuLine(4, "070000003");
            grid.SetSudokuLine(5, "085069000");
            grid.SetSudokuLine(6, "000000090");
            grid.SetSudokuLine(7, "500007000");
            grid.SetSudokuLine(8, "600090008");

            target = new SudokuSolver_Accessor(grid);
            
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);

            actual = target.Step2UseSingletons();
            Assert.IsFalse(actual);

            actual = target.Step3UsePairs();
            Assert.IsFalse(actual);           
        }
        

        /*
        /// <summary>
        ///A test for Step4ElminateBadGuesses
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void Step4ElminateBadGuessesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(param0); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Step4ElminateBadGuesses();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        */

        /// <summary>
        ///A test for Puzzle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void PuzzleTest()
        {
            SudokuGrid param0 = null; 
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(param0); // TODO: Initialize to an appropriate value
            SudokuGrid expected = new SudokuGrid(); 
            SudokuGrid actual;
            target.Puzzle = expected;
            actual = target.Puzzle;
            Assert.AreEqual(expected, actual);            
        }
        

        /// <summary>
        ///A test for SingletonsByRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SingletonsByRowTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.GuessesAvailable[0, 0].Remove(9);
            target.GuessesAvailable[0, 1].Remove(9);
            target.GuessesAvailable[0, 2].Remove(9);
            target.GuessesAvailable[0, 3].Remove(9);
            target.GuessesAvailable[0, 4].Remove(9);
            target.GuessesAvailable[0, 5].Remove(9);
            // [0, 6] has a singleton
            target.GuessesAvailable[0, 7].Remove(9);
            target.GuessesAvailable[0, 8].Remove(9);

            bool actual = target.SingletonsByRow(0);
            Assert.IsTrue(actual);
            Assert.AreEqual(9, target.GuessesAvailable[0, 6][0]);

            actual = target.SingletonsByRow(6);
            Assert.IsFalse(actual);
            Assert.IsTrue(target.GuessesAvailable[6, 0].Count > 1);

        }

        /// <summary>
        ///A test for SingletonsByColumn
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SingletonsByColumnTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.GuessesAvailable[0, 0].Remove(9);
            target.GuessesAvailable[1, 0].Remove(9);
            target.GuessesAvailable[2, 0].Remove(9);
            target.GuessesAvailable[3, 0].Remove(9);
            target.GuessesAvailable[4, 0].Remove(9);
            target.GuessesAvailable[5, 0].Remove(9);
            // [6, 0] has a singleton
            target.GuessesAvailable[7, 0].Remove(9);
            target.GuessesAvailable[8, 0].Remove(9);
            
            bool actual = target.SingletonsByColumn(0);
            Assert.IsTrue(actual);
            Assert.AreEqual(9, target.GuessesAvailable[6, 0][0]);

            actual = target.SingletonsByColumn(6);
            Assert.IsFalse(actual);
            Assert.IsTrue(target.GuessesAvailable[0, 6].Count > 1);
        }
         

        
        /// <summary>
        ///A test for SingletonsBySubsquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SingletonsBySubsquareTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.GuessesAvailable[0, 0].Remove(9);
            target.GuessesAvailable[0, 1].Remove(9);
            target.GuessesAvailable[0, 2].Remove(9);
            target.GuessesAvailable[1, 0].Remove(9);
            target.GuessesAvailable[1, 1].Remove(9);
            target.GuessesAvailable[1, 2].Remove(9);
            target.GuessesAvailable[2, 0].Remove(9);
            // [2, 1] has a singleton
            target.GuessesAvailable[2, 2].Remove(9);
            
            bool actual = target.SingletonsBySubSquare(0, 0);
            Assert.IsTrue(actual);
            Assert.AreEqual(9, target.GuessesAvailable[2, 1][0]);
            Assert.IsTrue(target.GuessesAvailable[2, 2].Count > 1);

            actual = target.SingletonsBySubSquare(1, 1);
            Assert.IsFalse(actual);
            Assert.IsTrue(target.GuessesAvailable[8, 8].Count > 1);
        }

        
        /// <summary>
        ///A test for ClearGuess
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void ClearGuessesTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);
            target.ClearGuesses(2, 3, 0);

            // This only cleared a guess, it did not add something to the solution
            Assert.AreEqual(0, target.Puzzle[3, 0]);

            // Example: Use of lambda expression
            // My first use of a 'lambda expression' which is an 'inline' delegate. In this
            // case I'm passing into the Exists method a short function that returns True if there
            // is a value in the GuessesAvailable[x,y] == 2            
            // This is the location being cleared, so it should still have '2' as a guess. We just clear everything else around it
            Assert.IsTrue(target.GuessesAvailable[3, 0].Exists((int num) => { return num == 2; })); 

            // Or I can do it this way to make it more readable
            Predicate<int> compare = (int num) => { return num == 2; };

            // test subsquare
            Assert.IsTrue(target.GuessesAvailable[3, 0].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[3, 1].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[3, 2].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[4, 1].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[4, 2].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[5, 1].Exists(compare));
            Assert.IsFalse(target.GuessesAvailable[5, 2].Exists(compare));

            // test column
            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[1, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[2, 0].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[6, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[7, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[8, 0].Contains(2));

            // test row
            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 1].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 2].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 3].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 4].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 5].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 6].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 7].Contains(2));
            Assert.IsFalse(target.GuessesAvailable[3, 8].Contains(2));

            // test some that should not have been removed
            Assert.IsTrue(target.GuessesAvailable[2, 2].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[8, 7].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[6, 5].Contains(2));

            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);
        }
        

        
        


        
        /// <summary>
        ///A test for PairsByRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void PairsByRowAndColumnTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);

            // test by row

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(7);
            target.GuessesAvailable[0, 0].Add(9);

            target.GuessesAvailable[0, 2].Add(2);
            target.GuessesAvailable[0, 2].Add(7);
            target.GuessesAvailable[0, 2].Add(9);

            target.GuessesAvailable[0, 3].Add(3);
            target.GuessesAvailable[0, 4].Add(7);
            target.GuessesAvailable[0, 4].Add(8);
            target.GuessesAvailable[0, 5].Add(9);
            target.GuessesAvailable[0, 5].Add(5);

            bool actual;
            actual = target.PairsByRowAndColumn(0);
            Assert.IsFalse(actual);
            actual = target.PairsBySubSquare(0, 0);
            Assert.IsTrue(actual);          

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(9));

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 3].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(5));

            actual = target.PairsByRowAndColumn(0);
            Assert.IsTrue(actual);

            Assert.IsTrue(target.GuessesAvailable[0, 3].Contains(3));
            Assert.IsFalse(target.GuessesAvailable[0, 4].Contains(7));
            Assert.IsFalse(target.GuessesAvailable[0, 5].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[0, 4].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[0, 5].Contains(5));


            // now by column
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(7);
            target.GuessesAvailable[0, 0].Add(9);

            target.GuessesAvailable[2, 0].Add(2);
            target.GuessesAvailable[2, 0].Add(7);
            target.GuessesAvailable[2, 0].Add(9);

            target.GuessesAvailable[3, 0].Add(3);
            target.GuessesAvailable[4, 0].Add(7);
            target.GuessesAvailable[4, 0].Add(8);
            target.GuessesAvailable[5, 0].Add(9);
            target.GuessesAvailable[5, 0].Add(5);

            actual = target.PairsByRowAndColumn(0);
            Assert.IsFalse(actual);
            actual = target.PairsBySubSquare(0, 0);
            Assert.IsTrue(actual);          

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(9));

            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[2, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[2, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(5));

            actual = target.PairsByRowAndColumn(0);
            Assert.IsTrue(actual);

            Assert.IsTrue(target.GuessesAvailable[3, 0].Contains(3));
            Assert.IsFalse(target.GuessesAvailable[4, 0].Contains(7));
            Assert.IsFalse(target.GuessesAvailable[5, 0].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[4, 0].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[5, 0].Contains(5));

            // Problem example
            puzzle = new SudokuGrid();
            target = new SudokuSolver_Accessor(puzzle);

            // test by row

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }


            target.Puzzle[0, 1] = 0;
            target.GuessesAvailable[0, 1].Add(2);
            target.GuessesAvailable[0, 1].Add(6);
            target.GuessesAvailable[0, 1].Add(8);
            
            target.Puzzle[7, 1] = 0;
            target.GuessesAvailable[7, 1].Add(1);
            target.GuessesAvailable[7, 1].Add(2);
            target.GuessesAvailable[7, 1].Add(8);

            target.Puzzle[8, 1] = 0;
            target.GuessesAvailable[8, 1].Add(1);
            target.GuessesAvailable[8, 1].Add(3);
            target.GuessesAvailable[8, 1].Add(6);
            target.GuessesAvailable[8, 1].Add(8);


            target.PairsByRowAndColumn(1);

            
            // This was a trickier example. But now that I understand it fully, it should really do nothing.
            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(1);
            target.GuessesAvailable[0, 0].Add(2);
            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(5);

            target.GuessesAvailable[0, 4].Add(1);
            target.GuessesAvailable[0, 4].Add(2);
            target.GuessesAvailable[0, 4].Add(6);

            target.GuessesAvailable[0, 6].Add(1);
            target.GuessesAvailable[0, 6].Add(2);
            target.GuessesAvailable[0, 6].Add(3);
            target.GuessesAvailable[0, 6].Add(5);
            target.GuessesAvailable[0, 6].Add(6);

            target.GuessesAvailable[0, 7].Add(2);
            target.GuessesAvailable[0, 7].Add(3);

            target.GuessesAvailable[0, 8].Add(1);
            target.GuessesAvailable[0, 8].Add(3);
            target.GuessesAvailable[0, 8].Add(5);
            target.GuessesAvailable[0, 8].Add(6);

            actual = target.PairsByRowAndColumn(0);

            Assert.IsFalse(actual);

            
        }




        /// <summary>
        ///A test for PairsBySubSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void PairsBySubSquareTest()
        {
            SudokuGrid puzzle = new SudokuGrid();
            SudokuSolver_Accessor target = new SudokuSolver_Accessor(puzzle);

            // setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(7);
            target.GuessesAvailable[0, 0].Add(9);

            target.GuessesAvailable[0, 2].Add(2);
            target.GuessesAvailable[0, 2].Add(7);
            target.GuessesAvailable[0, 2].Add(9);

            target.GuessesAvailable[2, 0].Add(3);
            target.GuessesAvailable[2, 1].Add(7);
            target.GuessesAvailable[2, 1].Add(8);
            target.GuessesAvailable[2, 2].Add(9);
            target.GuessesAvailable[2, 2].Add(5);

            bool actual;
            actual = target.PairsBySubSquare(0, 0);
            Assert.IsFalse(actual);
            actual = target.PairsByRowAndColumn(0);
            Assert.IsTrue(actual);


            Assert.IsFalse(target.GuessesAvailable[0, 0].Contains(3));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 0].Contains(9));

            Assert.IsFalse(target.GuessesAvailable[0, 2].Contains(2));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(7));
            Assert.IsTrue(target.GuessesAvailable[0, 2].Contains(9));

            actual = target.PairsBySubSquare(0, 0);
            Assert.IsTrue(actual);

            Assert.IsTrue(target.GuessesAvailable[2, 0].Contains(3));
            Assert.IsFalse(target.GuessesAvailable[2, 1].Contains(7));
            Assert.IsFalse(target.GuessesAvailable[2, 2].Contains(9));

            Assert.IsTrue(target.GuessesAvailable[2, 1].Contains(8));
            Assert.IsTrue(target.GuessesAvailable[2, 2].Contains(5));


            
            // This was a trickier example, but now that I understand it properly, it should really do 
            // nothing at all
            
            // Setup the test
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    target.GuessesAvailable[x, y].Clear();
                }

            target.GuessesAvailable[0, 0].Add(1);
            target.GuessesAvailable[0, 0].Add(2);
            target.GuessesAvailable[0, 0].Add(3);
            target.GuessesAvailable[0, 0].Add(5);

            target.GuessesAvailable[0, 4].Add(1);
            target.GuessesAvailable[0, 4].Add(2);
            target.GuessesAvailable[0, 4].Add(6);

            target.GuessesAvailable[0, 6].Add(1);
            target.GuessesAvailable[0, 6].Add(2);
            target.GuessesAvailable[0, 6].Add(3);
            target.GuessesAvailable[0, 6].Add(5);
            target.GuessesAvailable[0, 6].Add(6);

            target.GuessesAvailable[0, 7].Add(2);
            target.GuessesAvailable[0, 7].Add(3);

            target.GuessesAvailable[0, 8].Add(1);
            target.GuessesAvailable[0, 8].Add(3);
            target.GuessesAvailable[0, 8].Add(5);
            target.GuessesAvailable[0, 8].Add(6);

            actual = target.PairsByRowAndColumn(0);

            Assert.IsFalse(actual);
            
        }

        /// <summary>
        ///A test to work on fixing the bugs I get when I introduce step3
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void TestProblemsWithStep3FindPairs()
        {
            SudokuPuzzleOld puzzle = new SudokuPuzzleOld(true);
            SudokuSolver_Accessor target;

            puzzle.SetSudokuLine(0, "300094500");
            puzzle.SetSudokuLine(1, "540700060");
            puzzle.SetSudokuLine(2, "000150043");
            puzzle.SetSudokuLine(3, "205009700");
            puzzle.SetSudokuLine(4, "060027039");
            puzzle.SetSudokuLine(5, "000000000");
            puzzle.SetSudokuLine(6, "020085006");
            puzzle.SetSudokuLine(7, "000000800");
            puzzle.SetSudokuLine(8, "903001050");


            SudokuGrid solutionGrid = new SudokuGrid();
            solutionGrid.SetSudokuLine(0, "316294587");
            solutionGrid.SetSudokuLine(1, "549738162");
            solutionGrid.SetSudokuLine(2, "872156943");
            solutionGrid.SetSudokuLine(3, "235469718");
            solutionGrid.SetSudokuLine(4, "168527439");
            solutionGrid.SetSudokuLine(5, "794813625");
            solutionGrid.SetSudokuLine(6, "421985376");
            solutionGrid.SetSudokuLine(7, "657342891");
            solutionGrid.SetSudokuLine(8, "983671254");

            SudokuSolution solution = new SudokuSolution(solutionGrid);
            puzzle.SetSolution(solution);

            target = new SudokuSolver_Accessor(puzzle);
            Assert.IsNotNull(target);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, puzzle.DifficultyLevel);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            bool actual = false;
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
            
            /*
            Solution:
            316294 587
            549738*162 : guesses - 1,2,9 - Pair 29
            872156*943 : guesses - 2,9 - Pair 29
            235469 718
            168527 439 : guesses - 1,4
            794813 625
            421985 376
            657342 891
            983671 254 : guesses - 2,4


            Puzzle
            300094 500
            540730*060 : guesses - 1,2,9 - Pair 29
            000150*043 : guesses - 2,9 - Pair 29
            205009 700
            060527 039 : guesses - 1,4
            000000 625
            020085 306
            050000 800
            983001 050 : guesses - 2,4
            
            My algorithm is producing a 'pair' 29 when really there should be no pairs because there is a '2' at row 8. I think
            */

            actual = target.PairsByRowAndColumn(6);
            Assert.IsFalse(actual);
            actual = target.Step3UsePairs(); ;
            Assert.IsFalse(actual);

            // Next problem:

            /*
            Before Setting Value:
            504083120
            030002578
            008710034
            000300000
            853100247
            069007300
            300000010
            940071863
            000030090

            After Setting Value:
            504083120
            030002578
            508710034
            000300000
            853100247
            069007300
            300000010
            940071863
            000030090

             */

            puzzle = new SudokuPuzzleOld(true);

            puzzle.SetSudokuLine(0, "004080120");
            puzzle.SetSudokuLine(1, "030002508");
            puzzle.SetSudokuLine(2, "008710004");
            puzzle.SetSudokuLine(3, "000300000");
            puzzle.SetSudokuLine(4, "800100207");
            puzzle.SetSudokuLine(5, "069007300");
            puzzle.SetSudokuLine(6, "300000010");
            puzzle.SetSudokuLine(7, "940071060");
            puzzle.SetSudokuLine(8, "000000090");

            
            solutionGrid = new SudokuGrid();
            solutionGrid.SetSudokuLine(0, "794583126");
            solutionGrid.SetSudokuLine(1, "631492578");
            solutionGrid.SetSudokuLine(2, "528716934");
            solutionGrid.SetSudokuLine(3, "172345689");
            solutionGrid.SetSudokuLine(4, "853169247");
            solutionGrid.SetSudokuLine(5, "469827351");
            solutionGrid.SetSudokuLine(6, "386954712");
            solutionGrid.SetSudokuLine(7, "945271863");
            solutionGrid.SetSudokuLine(8, "217638495");

            solution = new SudokuSolution(solutionGrid);
            puzzle.SetSolution(solution);

            target = new SudokuSolver_Accessor(puzzle);
            Assert.IsNotNull(target);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, puzzle.DifficultyLevel);
            Assert.AreEqual(SudokuSolver.Difficulty.UNDETERMINED, target.DifficultyLevel);

            actual = false;
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

            Debug.WriteLine("Current Grid:");
            target.Puzzle.WriteOutSudokuGrid();

            actual = target.Step2UseSingletons();
            Assert.IsFalse(actual);

            //actual = target.Step3UsePairs();
            //Assert.IsTrue(actual);

            /*
            Current Grid:
            004083120
            030002578
            008710034
            000300000
            853100247
            069007300
            300000010
            940071863
            000030090
            */
            
            //actual = target.PairsByRowAndColumn(1);
            //Assert.IsTrue(actual);
            //actual = target.PairsByRowAndColumn(8);
            //Assert.IsTrue(actual);
            //actual = target.PairsBySubSquare(0, 0);
            //Assert.IsTrue(actual);
            //actual = target.PairsBySubSquare(1, 2);
            //Assert.IsTrue(actual);
            

            //actual = target.Step1ApplyNumbers();
            //Assert.IsTrue(actual);
            //actual = target.Step1ApplyNumbers();
            //Assert.IsTrue(actual);
            //actual = target.Step1ApplyNumbers();
            //Assert.IsFalse(actual);
            
            
            actual = target.Step3UsePairs();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsTrue(actual);
            actual = target.Step1ApplyNumbers();
            Assert.IsFalse(actual);
            actual = target.Step2UseSingletons();
            Assert.IsFalse(actual);
            actual = target.Step3UsePairs();
            Assert.IsFalse(actual);
            

        }

    }
}
