using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// THe event raised, when the match (best of three system) has ended. Only one player has 3 points, and the player won
    /// </summary>
    public class EndGameEventArgs : EventArgs
    {
        public bool _end;
        public int _firstPlayerPoints;
        public int _secondPlayerPoints;
        public int _thirdPlayerPoints;

        /// <summary>
        /// Constructor of the event.
        /// </summary>
        /// <param name="end">The game has ended or not.</param>
        /// <param name="firstPlayerPoints">First player points</param>
        /// <param name="secondPlayerPoints">Second player points</param>
        /// <param name="thirdPlayerPoints">Third player points</param>
        public EndGameEventArgs(bool end, int firstPlayerPoints, int secondPlayerPoints, int thirdPlayerPoints)
        {
            _end = end;
            _firstPlayerPoints = firstPlayerPoints;
            _secondPlayerPoints = secondPlayerPoints;
            _thirdPlayerPoints = thirdPlayerPoints;
        }
    }
}
