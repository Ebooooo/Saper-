using Saper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Data.Logics
{
    /// <summary>
    /// Klasa logiki gry Saper
    /// </summary>
    public class BoardLogic
    {
        /// <summary>
        /// Metoda wypełniająca planszę
        /// </summary>
        /// <param name="board">Plansza</param>
        /// <param name="x">Punkt wypełnienia, bez miny</param>
        /// <param name="y">Punkt wypełnienia, bez miny</param>
        public void Fill(Board board, int x, int y)
        {
            var random = new Random();
            var env = board.GetEnvironment(x, y);
            for (int i = 0; i < board.Mines; i++)
            {
                int x0, y0;
                do
                {
                    x0 = random.Next(0, board.Columns - 1);
                    y0 = random.Next(0, board.Rows - 1);
                } while (board[x0, y0].Value == -1 || env.Contains(board[x0, y0]));
                board[x0, y0].Value = -1;
                foreach (var field in board.GetSurroundings(x0, y0))
                {
                    if (field.Value != -1)
                        field.Value++;
                }
            }
            board.IsEmpty = false;
        }
        /// <summary>
        /// Metoda odkrywająca pole
        /// </summary>
        /// <param name="board">Plansza</param>
        /// <param name="x">Punkt odkrycia</param>
        /// <param name="y">Punkt odkrycia</param>
        public void Show(Board board, int x, int y)
        {
            try
            {
                if (board[x, y].Value == -1)
                {
                    board[x, y].State = Enums.FieldState.Showed;
                    throw new MineException();
                }
                if (board[x, y].Value > 0)
                {
                    board[x, y].State = Enums.FieldState.Showed;
                    board.ShowedFileds++;
                }
                foreach (var item in board.GetEnvironment(x, y))
                {
                    if (item.State == Enums.FieldState.Defalut)
                    {
                        board.ShowedFileds++;
                        item.State = Enums.FieldState.Showed;
                        if (item.Value == 0)
                            Show(board, item.X, item.Y);
                    }
                    item.Text = item.Value.ToString();
                }
            }
            finally
            {
                board[x, y].Text = board[x, y].Value.ToString();
            }
        }
        /// <summary>
        /// Metoda sprawdzająca wygraną
        /// </summary>
        /// <param name="board">Plansza</param>
        /// <returns>Czy gra jest wygrana</returns>
        public bool CheckWin(Board board)
        {
            if (board.ShowedFileds + board.Mines == board.Fields.Length)
                return true;
            return false;
        }
        /// <summary>
        /// Metoda zmieniająca stan pola
        /// </summary>
        /// <param name="field">Pole planszy</param>
        public void ChangeState(Field field)
        {
            switch (field.State)
            {
                case Enums.FieldState.Defalut:
                    field.State = Enums.FieldState.Coverd;
                    break;
                case Enums.FieldState.Coverd:
                    field.State = Enums.FieldState.Ask;
                    break;
                case Enums.FieldState.Ask:
                    field.State = Enums.FieldState.Defalut;
                    break;
            }
        }
    }
}
