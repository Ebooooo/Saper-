using Saper.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Data.Models
{
    /// <summary>
    /// Pole planszy
    /// </summary>
    public class Field : INotifyPropertyChanged
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x">Współrzędne pola</param>
        /// <param name="y">Współrzędne pola</param>
        public Field(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Współrzędna X
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Współrzędna Y
        /// </summary>
        public int Y { get; }
        /// <summary>
        /// Stan pola
        /// </summary>
        public FieldState State
        {
            get { return _State; }
            set
            {
                _State = value;
                OnPropertyChanged();
            }
        }
        private FieldState _State;
        /// <summary>
        /// Wartość pola
        /// </summary>
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged();
            }
        }
        private int _Value;
        /// <summary>
        /// Tekst pola
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                OnPropertyChanged();
            }
        }
        private string _Text;
        #region PropertyChanged
        /// <summary>
        /// Zdarzenie obsługujące zmianę wartości właściwości (implementowane przez INotifyPropertyChanged).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Podnosi zdarzenie PropertyChanged dla konkretnej wałaściwości.
        /// </summary>
        /// <param name="name">Nazwa właściwości.</param>
        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
