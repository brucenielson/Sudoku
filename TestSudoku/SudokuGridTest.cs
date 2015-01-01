using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSudoku
{
    
    
    /// <summary>
    ///This is a test class for SudokuGridTest and is intended
    ///to contain all SudokuGridTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudokuGridTest
    {

        /// <summary>
        ///A test for WriteOutSudokuGrid
        ///</summary>
        [TestMethod()]
        public void WriteOutSudokuGridTest()
        {
            Random rand = new Random();

            SudokuGrid target = new SudokuGrid(); // TODO: Initialize to an appropriate value
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    target[x, y] = rand.Next(1, 10);
                }
            }
            string s = "";
            s = target.ToString();
            Assert.IsTrue(s.Length > 0);
            //target.WriteOutSudokuGrid();
        }
    }
}
