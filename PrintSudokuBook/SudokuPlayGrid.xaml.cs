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
    public partial class SudokuPlayGrid : Window
    {
        public SudokuPuzzle Puzzle { get; set; }


        public SudokuPlayGrid()
        {
            InitializeComponent();
            this.DataContext = this;

            Puzzle = new SudokuPuzzle();
            Puzzle.FindAnySolveablePuzzle(SudokuPuzzle.Difficulty.HARD);
        }

        public SudokuPlayGrid(SudokuPuzzle.Difficulty difficulty)
        {
            InitializeComponent();
            this.DataContext = this;

            Puzzle = new SudokuPuzzle();
            Puzzle.FindSolveablePuzzle(difficulty);
        }



        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (Puzzle.CheckForSolution())
            {
                Message.Text = "Finished! Congratulations!";
                Puzzle.UpdateAll();
            }
            else
            {
                Message.Text = "Sudoku Not Valid";
                //Puzzle.UpdateAll();
                Puzzle.MarkInvalids();
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            /*
            TextBox thisBox = (TextBox)sender;
            if (thisBox.Text == "")
                thisBox.Background = Brushes.White; 
            */
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (Puzzle.CheckForSolution())
            {
                Message.Text = "Finished! Congratulations!";
                Puzzle.UpdateAll();
            }
            else
            {
                //Message.Text = "Sudoku Not Valid";
                //Puzzle.UpdateAll();
                Puzzle.MarkInvalids();
            }
            /*
            TextBox thisBox = (TextBox)sender;
            SudokuSolution checkValid = new SudokuSolution(Puzzle);
            if (thisBox.Text != "" && !checkValid.IsValidArray(false))
                thisBox.Background = new LinearGradientBrush(Colors.LightPink, Colors.Red, 90);
            */
        }

        private void Cheat_Click(object sender, RoutedEventArgs e)
        {
            Puzzle.FillInSolution();
        }






    }
}
