using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Contains which player sent data is for
    /// - _playerId     Number of the player
    /// </summary>
    public class PlayerEventArgs : EventArgs
    {
        #region Fields
        public int _playerId;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor setting the player id
        /// </summary>
        /// <param name="playerId">Number of player</param>
        public PlayerEventArgs(int playerId)
        {
            _playerId = playerId;
        }
        #endregion
    }
}
