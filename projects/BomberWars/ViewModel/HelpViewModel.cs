using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Viewmodel for the key bindings screen containing information for users about controlling the game.
    /// </summary>
    public class HelpViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Only contains a command and event for returning back to the game screen.
        /// </summary>
        public DelegateCommand? ReturnCommand { get; set; }
        public event EventHandler? Return;
        #endregion

        #region Constructor
        public HelpViewModel()
        {
            ReturnCommand = new DelegateCommand(x => ReturnToGame());
        }
        #endregion

        #region Functions
        private void ReturnToGame()
        {
            Return?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
