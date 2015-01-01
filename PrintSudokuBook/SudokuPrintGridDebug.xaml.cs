using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sudoku;
using System.ComponentModel;
using System.Printing;


namespace SudokuGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SudokuPrintGrid2 : Window
    {
        public SudokuPuzzle Puzzle { get; set; }

        public SudokuPrintGrid2()
        {
            InitializeComponent();
            this.DataContext = this;

            Puzzle = new SudokuPuzzle();
            //Puzzle.FindSolveablePuzzle();
            Puzzle.FindSolveablePuzzle(SudokuPuzzle.Difficulty.MEDIUM);
            //Puzzle = new SudokuPuzzleNew(true);
            
            /*
             * How to bind in code
            Binding binding = new Binding();
            binding.Source = Puzzle.Solution;
            binding.Path = new PropertyPath("[0,0]");
            TestText.SetBinding(TextBlock.TextProperty, binding);
             */
            
            /*
            Puzzle.SetSudokuLine(0, "608000009");
            Puzzle.SetSudokuLine(1, "000008012");
            Puzzle.SetSudokuLine(2, "000340600");
            Puzzle.SetSudokuLine(3, "010400360");
            Puzzle.SetSudokuLine(4, "000030000");
            Puzzle.SetSudokuLine(5, "074006020");
            Puzzle.SetSudokuLine(6, "009017000");
            Puzzle.SetSudokuLine(7, "480900000");
            Puzzle.SetSudokuLine(8, "100000508");
            */
        }


        private void Next_Click(object sender, RoutedEventArgs e)
        {

            Puzzle.DoARoundOfSolves(true);

            Back.IsEnabled = true;

            if (Puzzle.CheckForSolution())
                Next.IsEnabled = false;

            /*
            if (!Puzzle.SolveAsFarAsPossible())
                Puzzle.DoARoundOfSolves(true);
            
            Back.IsEnabled = true;

            if (Puzzle.CheckForSolution())
                Next.IsEnabled = false;
            */

            /*
            if (Puzzle.DoARoundOfSolves())
            {
                //Puzzle = new SudokuPuzzleNew();
                Binding binding = new Binding();
                binding.Path = new PropertyPath("Puzzle");
                ItemsControl1.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            }*/
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Puzzle.BackStep())
                Next.IsEnabled = true;

            if (Puzzle.LastPuzzleState == null) // || Puzzle.LastPuzzleState.LastPuzzleState == null)
                Back.IsEnabled = false;
            else
                Back.IsEnabled = true;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            bool? result = printDlg.ShowDialog();

            if (result.GetValueOrDefault(false))
            {
                printDlg.PrintVisual(this.ItemsControl1, "The Sudoku Generator");
            }
        }





    }
}
