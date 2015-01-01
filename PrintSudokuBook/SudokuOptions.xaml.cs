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

namespace SudokuGame
{
    /// <summary>
    /// Interaction logic for SudokuPrintGrid.xaml
    /// </summary>
    public partial class SudokuOptions : Window
    {
        private int difficulty;

        public int Difficulty
        {
            get
            {
                return difficulty;
            }

            set
            {
                difficulty = value;
                if (Difficulty == 1)
                    DifficultyText.Text = "Trivial";
                else if (Difficulty == 2)
                    DifficultyText.Text = "Easy";
                else if (Difficulty == 3)
                    DifficultyText.Text = "Medium";
            }
        }
        

        public SudokuOptions()
        {
            InitializeComponent();
            // Must bind to the code behind
            this.DataContext = this;
            DifficultyText.Text = "Easy";
            Difficulty = 1;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            SudokuPuzzle.Difficulty desiredDifficulty;

            if (Difficulty == 1)
                desiredDifficulty = SudokuPuzzle.Difficulty.TRIVIAL;
            else if (Difficulty == 2)
                desiredDifficulty = SudokuPuzzle.Difficulty.EASY;
            else if (Difficulty == 3)
                desiredDifficulty = SudokuPuzzle.Difficulty.MEDIUM;
            else
                desiredDifficulty = SudokuPuzzle.Difficulty.UNDETERMINED;

            if (radioButtonPlay.IsChecked == true)
            {
                SudokuPlayGrid playGrid = new SudokuPlayGrid(desiredDifficulty);
                playGrid.Show();
            }
            else
            {
                //MessageBox.Show(Difficulty.ToString());
                SudokuPrintGrid printGrid = new SudokuPrintGrid(desiredDifficulty);
                printGrid.Show();
            }
            Close();
        }

    }
}
