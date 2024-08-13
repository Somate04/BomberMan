using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// THe event raised, when the round has ended. Only one player gains a point. After this new round must be started.
    /// </summary>
    public class EndRoundEventArgs : EventArgs
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
        public EndRoundEventArgs(bool end, int firstPlayerPoints, int secondPlayerPoints, int thirdPlayerPoints)
        {
            _end = end;
            _firstPlayerPoints = firstPlayerPoints;
            _secondPlayerPoints = secondPlayerPoints;
            _thirdPlayerPoints = thirdPlayerPoints;
        }
    }
}
