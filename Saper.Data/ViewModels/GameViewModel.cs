using Saper.Data.Logics;
using Saper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Saper.Data.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public GameViewModel()
        {
            IsEnabled = true;
            Columns = 15;
            Rows = 10;
            Mines = 15;
            Board = new Board(Columns, Rows, Mines);
        }
        /// <summary>
        /// Plansza
        /// </summary>
        public Board Board
        {
            get { return _Board; }
            set
            {
                _Board = value;
                OnPropertyChanged();
            }
        }
        private Board _Board;
        /// <summary>
        /// Ilość przykrytych min
        /// </summary>
        public int CoverdFields
        {
            get { return _CoverdFields; }
            set
            {
                _CoverdFields = value;
                OnPropertyChanged();
            }
        }
        private int _CoverdFields;
        /// <summary>
        /// Komunikat po zakończonej grze
        /// </summary>
        public string EndMessage
        {
            get { return _EndMessage; }
            set
            {
                _EndMessage = value;
                OnPropertyChanged();
            }
        }
        private string _EndMessage;
        /// <summary>
        /// Kolor komunikatu końcowego
        /// </summary>
        public Brush MessageForeground
        {
            get { return _MessageForeground; }
            set
            {
                _MessageForeground = value;
                OnPropertyChanged();
            }
        }
        private Brush _MessageForeground;
        /// <summary>
        /// Czy plansza jest odblokowana
        /// </summary>
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnabled;
        /// <summary>
        /// Ilośc kolumn w nowej grze
        /// </summary>
        public int Columns
        {
            get { return _Columns; }
            set
            {
                _Columns = value;
                OnPropertyChanged();
            }
        }
        private int _Columns;
        /// <summary>
        /// Ilość wierszy w nowej grze
        /// </summary>
        public int Rows
        {
            get { return _Rows; }
            set
            {
                _Rows = value;
                OnPropertyChanged();
            }
        }
        private int _Rows;
        /// <summary>
        /// Ilość min w nowej grze
        /// </summary>
        public int Mines
        {
            get { return _Mines; }
            set
            {
                _Mines = value;
                OnPropertyChanged();
            }
        }
        private int _Mines;

        /// <summary>
        /// Komenda od odłonięcia zaznaczonego pola
        /// </summary>
        public Command<Field> ShowCommand
        {
            get => new Command<Field>(async field =>
            {
                await Task.Run(() =>
                {
                    if (field.State == Enums.FieldState.Showed || field.State == Enums.FieldState.Coverd)
                        return;
                    BoardLogic logic = new BoardLogic();
                    if (Board.IsEmpty)
                        logic.Fill(Board, field.X, field.Y);
                    try
                    {
                        logic.Show(Board, field.X, field.Y);
                        if (logic.CheckWin(Board))
                        {
                            IsEnabled = false;
                            EndMessage = "WYGRANA!";
                            MessageForeground = Brushes.Green;
                        }
                    }
                    catch (MineException)
                    {
                        IsEnabled = false;
                        EndMessage = "PRZEGRANA!";
                        MessageForeground = Brushes.Red;
                    }
                });
            });
        }
        /// <summary>
        /// Komenda od zasłonięcia zaznaczonego pola
        /// </summary>
        public Command<Field> CoverCommand
        {
            get => new Command<Field>(async param =>
            {
                await Task.Run(() =>
                {
                    switch (param.State)
                    {
                        case Enums.FieldState.Defalut:
                            CoverdFields++;
                            param.State = Enums.FieldState.Coverd;
                            break;
                        case Enums.FieldState.Coverd:
                            CoverdFields--;
                            param.State = Enums.FieldState.Ask;
                            break;
                        case Enums.FieldState.Ask:
                            param.State = Enums.FieldState.Defalut;
                            break;
                    }
                });
            });
        }
        /// <summary>
        /// Komenda rozpoczęcia nowej gry
        /// </summary>
        public Command<Field> NewGameCommand
        {
            get => new Command<Field>(async param =>
            {
                await Task.Run(() =>
                {
                    if (Columns * Rows * 0.8 < Mines)
                        Mines = (int)(Columns * Rows * 0.4);
                    Board = new Board(Columns, Rows, Mines);
                    EndMessage = null;
                    IsEnabled = true;
                });
            });
        }
    }
}
