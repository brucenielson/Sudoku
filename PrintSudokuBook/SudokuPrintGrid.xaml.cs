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
    public partial class SudokuPrintGrid : Window
    {
        public SudokuPuzzle Puzzle { get; set; }


        public SudokuPrintGrid()
        {
            InitializeComponent();
            this.DataContext = this;

            Puzzle = new SudokuPuzzle();
            Puzzle.FindAnySolveablePuzzle(SudokuPuzzle.Difficulty.EASY);
        }

        public SudokuPrintGrid(SudokuPuzzle.Difficulty difficulty)
        {
            InitializeComponent();
            this.DataContext = this;

            Puzzle = new SudokuPuzzle();
            Puzzle.FindSolveablePuzzle(difficulty);
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
