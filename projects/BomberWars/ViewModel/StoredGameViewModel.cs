using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Viewmodel of previous saves to display
    /// - _name                 Name of the save file
    /// - _loadButtonEnabled    Whether the playe has the ability to load a save. Only available if the playe wanted to load.
    /// </summary>
    public class StoredGameViewModel : ViewModelBase
    {
        #region Fields
        private string _name = string.Empty;
        private bool _loadButtonEnabled = false;
        #endregion

        #region Properties
        /// <summary>
        /// Property for name of the save
        /// </summary>
        public string Name { get { return _name; } set { if (_name != value) { _name = value; OnPropertyChanged(); } } }

        /// <summary>
        /// Property for load command
        /// </summary>
        public DelegateCommand? LoadGameCommand { get; set; }

        /// <summary>
        /// Property for _loadButtonEnabled
        /// </summary>
        public bool LoadButtonEnabled
        {
            get
            {
                return _loadButtonEnabled;
            }
            set
            {
                _loadButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
