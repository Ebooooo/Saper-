using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Data.Models
{
    /// <summary>
    /// Plansza
    /// </summary>
    public class Board 
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="columns">Ilość kolumn</param>
        /// <param name="rows">Ilość wierszy</param>
        /// <param name="mines">Ilość min</param>
        public Board(int columns, int rows, int mines)
        {
            Rows = rows;
            Mines = mines;
            Columns = columns;
            Fields = new Field[columns * rows];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    Fields[r * columns + c] = new Field(c, r);
            IsEmpty = true;
        }
        /// <summary>
        /// Ilość kolumn
        /// </summary>
        public int Columns { get; }
        /// <summary>
        /// Ilość wierszy
        /// </summary>
        public int Rows { get; }
        /// <summary>
        /// Ilość min
        /// </summary>
        public int Mines { get; }
        /// <summary>
        /// Ilość odkrytych pól
        /// </summary>
        public int ShowedFileds { get; set; }
        /// <summary>
        /// Czy plansza jest pusta
        /// </summary>
        public bool IsEmpty { get; set; }
        /// <summary>
        /// Tablica pól
        /// </summary>
        public Field[] Fields { get; }
        /// <summary>
        /// Pobieranie pola po współrzędnych
        /// </summary>
        /// <param name="x">Współrzędne</param>
        /// <param name="y">Współrzędne</param>
        /// <returns>Pole</returns>
        public Field this[int x, int y]
        {
            get => Fields[y * Columns + x];
        }
        /// <summary>
        /// Pobieranie otoczenia pola
        /// </summary>
        /// <param name="x">Współrzędne</param>
        /// <param name="y">Współrzędne</param>
        /// <returns>Kolekcja pól</returns>
        public IEnumerable<Field> GetSurroundings(int x, int y)
        {
            for (int j = 0, x0 = x - 1; j < 3; j++, x0++)
                for (int k = 0, y0 = y - 1; k < 3; k++, y0++)
                {
                    if (x0 < 0 || y0 < 0 || x0 >= Columns || y0 >= Rows || (x == x0 && y == y0))
                        continue;
                    yield return this[x0, y0];
                }
        }
        /// <summary>
        /// Pobieranie otoczenia pola wraz z polem
        /// </summary>
        /// <param name="x">Współrzędne</param>
        /// <param name="y">Współrzędne</param>
        /// <returns>Kolekcja pól</returns>
        public IEnumerable<Field> GetEnvironment(int x, int y)
        {
            for (int j = 0, x0 = x - 1; j < 3; j++, x0++)
                for (int k = 0, y0 = y - 1; k < 3; k++, y0++)
                {
                    if (x0 < 0 || y0 < 0 || x0 >= Columns || y0 >= Rows)
                        continue;
                    yield return this[x0, y0];
                }
        }
    }
}