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
    public class SudokuSolutionTest
    {


        /// <summary>
        ///A test for SudokuSolution Constructor
        ///</summary>
        [TestMethod()]
        public void SudokuGridConstructorTest()
        {
            SudokuGrid target = new SudokuGrid();
            Assert.AreNotEqual(null, target); 
        }

        /// <summary>
        ///A test for AttemptSetSudokuArray
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void AttemptSetSudokuArrayTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor(); 
            target.SetSudokuArray(0, 0, 1);
            bool actual;
            
            actual = target.AttemptSetSudokuArray(0, 1, 1);
            Assert.IsFalse(actual);

            actual = target.AttemptSetSudokuArray(1, 0, 1);
            Assert.IsFalse(actual);

            actual = target.AttemptSetSudokuArray(1, 1, 1);
            Assert.IsFalse(actual);

            actual = target.AttemptSetSudokuArray(0, 1, 2);
            Assert.IsTrue(actual);

            actual = target.AttemptSetSudokuArray(1, 0, 3);
            Assert.IsTrue(actual);

            actual = target.AttemptSetSudokuArray(1, 1, 4);
            Assert.IsTrue(actual);

            actual = target.AttemptSetSudokuArray(0, 1, 4);
            Assert.IsFalse(actual);

        }

        
        /// <summary>
        ///A test for BackSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void BackSquareTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor(); 
            
            // test simple movement back one square
            target.currentX = 1;
            target.currentY = 3;
            target.BackSquare();
            Assert.AreEqual(1, target.currentX);
            Assert.AreEqual(2, target.currentY);

            // test going back one square at border
            target.currentX = 1;
            target.currentY = 0;
            target.BackSquare();
            Assert.AreEqual(0, target.currentX);
            Assert.AreEqual(8, target.currentY);
 

            /* -- had to remove this test because when using 'friend' assemblies, a test for an error thrown stops testing
             * and reports the error. 
            // test going back one square at zero
            target.currentX = 0;
            target.currentY = 0;
            bool errorThrown = false;
            try
            {
                target.BackSquare();
            }
            catch
            {
                errorThrown = true;
            }
            Assert.IsTrue(errorThrown);
            */
             
            // test going back one square at end
            target.currentY = 8;
            target.currentX = 8;
            target.BackSquare();
            Assert.AreEqual(8, target.currentX);
            Assert.AreEqual(7, target.currentY);
            
        }


        
        /// <summary>
        ///A test for FindValidNumberFor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void FindValidNumberForTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor(); 
            bool actual;

            // make this non-random so that the test is consistent
            target.NoRandomize = true;
            actual = target.FindValidNumberFor(0, 0);
            Assert.AreEqual(1, target[0, 0]);
            Assert.IsTrue(actual);
            actual = target.FindValidNumberFor(0, 1);
            Assert.AreEqual(2, target[0, 1]);
            Assert.IsTrue(actual);
            actual = target.FindValidNumberFor(0, 2);
            Assert.AreEqual(3, target[0, 2]);
            Assert.IsTrue(actual);

            // Setup my own test - this also tests the private setter for the SudokuSolution class
            target = new SudokuSolution_Accessor();
            target[0, 0] = 9;
            target[0, 1] = 1;
            target[0, 2] = 4;
            target[0, 3] = 3;
            target[0, 4] = 2;
            target[0, 5] = 7;
            target[0, 6] = 6;
            target[0, 7] = 5;

            actual = target.FindValidNumberFor(0, 8);
            Assert.AreEqual(8, target[0, 8]);
            Assert.IsTrue(actual);
        }


        
        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void IsValidTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor();
        
            target.SetSudokuArray(0, 0, 1);
            
            target.SetSudokuArray(0, 1, 1);
            Assert.IsFalse(target.IsValid(0, 1));
            target.SetSudokuArray(0, 1, 0);

            target.SetSudokuArray(1, 0, 1);
            Assert.IsFalse(target.IsValid(1, 0));
            target.SetSudokuArray(1, 0, 0);

            target.SetSudokuArray(1, 1, 1);
            Assert.IsFalse(target.IsValid(1, 1));
            target.SetSudokuArray(1, 1, 0);

            target.SetSudokuArray(0, 1, 2);
            Assert.IsTrue(target.IsValid(0, 1));

            target.SetSudokuArray(1, 0, 3);
            Assert.IsTrue(target.IsValid(1, 0));

            target.SetSudokuArray(1, 1, 4);
            Assert.IsTrue(target.IsValid(1, 1));

        }


       
        /// <summary>
        ///A test for IsValidArray
        ///</summary>
        [TestMethod()]
        public void IsValidArrayTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor();
        
            target.SetSudokuArray(0, 0, 1);
            target.SetSudokuArray(0, 1, 2);            
            target.SetSudokuArray(1, 0, 3);
            target.SetSudokuArray(1, 1, 4);

            Assert.IsFalse(target.IsValidArray());
            Assert.IsFalse(target.IsValidArray(true));
            Assert.IsTrue(target.IsValidArray(false));
        }
        
         
        
        
        /// <summary>
        ///A test for MakeCopy
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void MakeCopyTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor(); 
            SudokuGrid actual;
            target.FillSudokuSolution();
            actual = target.MakeCopy();

            for (int x=0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.AreEqual(target[x, y], actual[x, y]);
                }

            Assert.AreNotEqual(target, actual);
        }
         
         
        
        /// <summary>
        ///A test for NextSquare
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void NextSquareTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor();

            // test simple movement next one square
            target.currentX = 1;
            target.currentY = 3;
            target.NextSquare();
            Assert.AreEqual(1, target.currentX);
            Assert.AreEqual(4, target.currentY);

            // test going next one square at border
            target.currentX = 0;
            target.currentY = 8;
            target.NextSquare();
            Assert.AreEqual(1, target.currentX);
            Assert.AreEqual(0, target.currentY);

            // test starting at beginning
            target.currentX = 0;
            target.currentY = 0;
            target.NextSquare();
            Assert.AreEqual(0, target.currentX);
            Assert.AreEqual(1, target.currentY);

        }

         
        
        /// <summary>
        ///A test for SetSudokuArray
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Sudoku.dll")]
        public void SetSudokuArrayTest()
        {
            SudokuSolution_Accessor target = new SudokuSolution_Accessor(); 
            target.SetSudokuArray(1, 3, 2);
            Assert.AreEqual(2, target[1, 3]);

            target[1, 2] = 3;
            Assert.AreEqual(3, target[1, 2]);
        }

    }
}
