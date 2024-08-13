using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Represents each square on the board
    /// </summary>
    public class Field: ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Properties for coordinates and the type of the square which dictates what assett should be rendered.
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }
        private string? type;
        public string Type
        {
            get { return type!; }
            set { type = value; OnPropertyChanged(nameof(type)); }
        }
        #endregion
    }
}
