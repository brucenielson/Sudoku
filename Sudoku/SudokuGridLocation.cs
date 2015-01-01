using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace Sudoku
{
    public class SudokuGridLocation : INotifyPropertyChanged
    {
        private int cellValue;
        private int solutionValue;
        private List<int> guesses;

        public SudokuGridLocation()
        {
            Valid = true;
        }

        public int Value
        {
            get
            {
                return cellValue;
            }

            set
            {
                HighestStep = CurrentStep;
                cellValue = value;
                OnPropertyChanged("HighestStep");
                OnPropertyChanged("Value");
                //if (PropertyChanged != null)
                //    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }


        public bool Enabled
        {
            get { return !(Value != 0 && HighestStep == Step.OnUncovered); } // Value == 0; }
        }


        public bool Valid { get; set; }

        public int SolutionValue
        {
            get
            {
                return solutionValue;
            }

            set
            {
                solutionValue = value;
                OnPropertyChanged("SolutionValue");
            }
        }

        private List<int> GuessesAvailable
        {
            get
            {
                return guesses;
            }

            set
            {
                guesses = value;
                HighestStep = CurrentStep;
                OnPropertyChanged("");
            }
        }

        private bool _isChanged;
        public bool IsChanged 
        {
            get
            {
                return _isChanged;
            }

            set 
            {
                _isChanged = value;
                if (_isChanged == true)
                    AvoidUncover = true;
            } 
        }

        private bool _avoidUncover;
        public bool AvoidUncover 
        {
            get
            {
                return _avoidUncover | (Value != 0);
            }

            set 
            {
                _avoidUncover = value;
            } 
        }

        public string Color
        {
            get
            {
                if (IsChanged == true)
                    return "Azure";
                else
                    return "White";
            }
        }


        public string ColorValid
        {
            get
            {
                if (Valid == true)
                    return "White";
                else
                    return "Pink";
            }
        }


        public bool Top { get; set; }
        public bool Bottom { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }


        public string Tickness
        {
            get
            {
                string result = "";

                if (Left == true)
                    result = result + "3, ";
                else
                    result = result + "1, ";

                if (Top == true)
                    result = result + "3, ";
                else
                    result = result + "1, ";

                if (Right == true)
                    result = result + "3, ";
                else
                    result = result + "1, ";

                if (Bottom == true)
                    result = result + "3";
                else
                    result = result + "1";
                
                return result;
            }
        }


        private Step _highestStep = 0;
        public Step HighestStep
        {
            get
            {
                return _highestStep;
            }

            set
            {
                if (value > _highestStep)
                    _highestStep = value;
            }
        }

        public enum Step : int { None, OnUncovered, SingleGuesses, Singletons, Pairs, TwoOfAKind };
        public static Step CurrentStep { get; set; }

        public override string ToString()
        {
            string result="";

            //result = "Solution: " + SolutionValue + "\r\n";
            result = result + string.Join(", ", GuessesAvailable);
            return result;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                if (name == "Color" || name == "AvoidUncover")
                    handler(this, new PropertyChangedEventArgs(name));
                else
                {
                    IsChanged = true;
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        public void DoUpdate()
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(""));
            }
        }

        public bool RemoveGuess(int value)
        {
            if (GuessesAvailable.Remove(value))
            {
                HighestStep = CurrentStep;
                OnPropertyChanged("");
                return true;
            }
            return false;
        }


        public void RemoveAllGuesses()
        {
            if (GuessesAvailable == null)
            {
                GuessesAvailable = new List<int>();
            }
            else
                GuessesAvailable.Clear();
            OnPropertyChanged("");
        }

        public void InitializeGuesses()
        {

            if (GuessesAvailable == null)
            {
                GuessesAvailable = new List<int>();
            }
            else
                GuessesAvailable.Clear();

            GuessesAvailable = new List<int>();
            for (int i = 1; i <= SudokuPuzzle.SudokuSize; i++)
            {
                GuessesAvailable.Add(i);
            }
            OnPropertyChanged("");
        }


        public int GuessesCount()
        {
            return GuessesAvailable.Count();
        }



        public int GetGuess(int i)
        {
            return GuessesAvailable[i];
        }


        public bool ContainsGuess(int guess)
        {
            return GuessesAvailable.Contains(guess);
        }

        public List<int> GetAllGuesses()
        {
            List<int> guesses = new List<int>(GuessesAvailable.Count());
            for (int i = 0; i < guesses.Capacity; i++)
            {
                guesses.Add(GuessesAvailable[i]);
            }

            return guesses;
        }


        public void SetGuesses(List<int> guesses)
        {
            bool isSame = true;

            // compare guesses
            if (guesses.Count() == GuessesAvailable.Count())
            {
                // Might be identical
                for (int i = 0; i < guesses.Count(); i++)
                {
                    if (guesses[i] != GuessesAvailable[i])
                    {
                        isSame = false;
                        break;
                    }
                }

                if (!isSame)
                    GuessesAvailable = guesses;
            }
            else // else they counts are different
                isSame = false;
           
            if (!isSame)
            {
                GuessesAvailable = guesses;
                OnPropertyChanged("");
            }
        }


        public bool RemoveAllGuessesBut(List<int> listToNotRemove)
        {
            bool result = false;
            for(int i=1; i <= SudokuPuzzle.SudokuSize; i++)
            {
                if (! listToNotRemove.Contains(i))
                    result = result | GuessesAvailable.Remove(i);
            }

            // if we removed any
            if (result == true)
            {
                HighestStep = SudokuGridLocation.CurrentStep;
                OnPropertyChanged("");
            }

            return result;
        }

        public void ResetChangedIndicator()
        {
            if (IsChanged == true)
            {
                IsChanged = false;
                OnPropertyChanged("Color");
            }
        }


        // This function does a number of checks to see if we've entered an invalid state
        public void DebugChecks()
        {
            // Can't have a final value and guesses remaining
            if (Value != 0 && GuessesAvailable.Count() > 0)
                throw new Exception("Can't have a final value and guesses remaining");

            if (SolutionValue != 0)
            {
                // Value must be same as Solution
                if (Value != 0 && Value != SolutionValue)
                    throw new Exception("Final value must be same as Solution");

                // If Guesses don't contain solution, then something has gone wrong
                if (Value == 0 && !GuessesAvailable.Contains(SolutionValue))
                    throw new Exception("Guesses Available does not contain the Solution");
            }
        }


        public void CopyLocation(SudokuGridLocation location)
        {
            if (Value != location.Value)
                Value = location.Value;
            if (SolutionValue != location.solutionValue) 
                SolutionValue = location.SolutionValue;

            SetGuesses(location.GetAllGuesses());

            if (_isChanged != location._isChanged) { _isChanged = location._isChanged; OnPropertyChanged("Color"); }
                
            if (_highestStep != location._highestStep) { _highestStep = location._highestStep; OnPropertyChanged("HighestStep"); }
            if (_avoidUncover != location._avoidUncover) { _avoidUncover = location._avoidUncover; OnPropertyChanged("AvoidUncover"); }

        }

        public void ResetLocation()
        {
            Value = 0;
            SolutionValue = 0;

            InitializeGuesses();

            _isChanged = false; 
            OnPropertyChanged("Color");

            _highestStep = Step.None; 
            OnPropertyChanged("HighestStep"); 
            
            _avoidUncover = false; 
            OnPropertyChanged("AvoidUncover");

            CurrentStep = Step.None;


        }

    }
}
