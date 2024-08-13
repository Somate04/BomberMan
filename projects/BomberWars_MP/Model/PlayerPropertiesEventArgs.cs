using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// This event sends all the player properties to the model, than this data will be sended to the View
    /// Model layer, to show the power ups on the UI.
    /// </summary>
    public class PlayerPropertiesEventArgs : EventArgs
    {
        public int _playerId;
        public int _maxNumberOfBombs;
        public int _currentNumberBombs;
        public int _range;
        public int _speed;
        public bool _invincibility;
        public int _invincibilityCoolDown;
        public bool _detonator;
        public bool _ghostMode;
        public int _ghostModeCoolDown;
        public int _numberOfObstacles;
        public int _slowDownCoolDown;
        public int _rangeDecreasedCoolDown;
        public int _disableBombPlacementCoolDown;
        public bool _instantPlacingBombs;
        public int _instantPlacingBombsCoolDown;

        /// <summary>
        /// Constructor of the event args.
        /// </summary>
        /// <param name="playerId">Id of the player</param>
        /// <param name="maxNumberOfBombs">Maximum number of bombs</param>
        /// <param name="currentNumberOfBombs">Current bombs on the board</param>
        /// <param name="range">The range of the bombs</param>
        /// <param name="speed">The speed of the player</param>
        /// <param name="invincibility">The player has the invincibility power up or not</param>
        /// <param name="invincibilityCoolDown">Time left of the invincibility power up, if the player has it</param>
        /// <param name="detonator">The player has the detonator power up or not</param>
        /// <param name="ghostmode">The player has the ghostMode power up or not</param>
        /// <param name="ghostModeCoolDown">Time left of the ghostMode power up, if the player has it</param>
        /// <param name="numberOfObstackles">Number of the obstackles, that the player has</param>
        /// <param name="slowDownCoolDown">Time left of the slow mode power up, if the player has it</param>
        /// <param name="rangeDecreaseCoolDown">Time left of the range decrease power up, if the player has it</param>
        /// <param name="disableBombPlacementCoolDown">Time left of the disable placing bombs power up, if the player has it</param>
        /// <param name="intantPlacingBombs">The player has the instant placing bombs power up or not</param>
        /// <param name="instantPlacingBombsCoolDown">Time left of the instant placing bombs power up, if the player has it</param>
        public PlayerPropertiesEventArgs(int playerId, int maxNumberOfBombs, int currentNumberOfBombs, int range, int speed,
                                         bool invincibility, int invincibilityCoolDown, bool detonator, bool ghostmode,
                                         int ghostModeCoolDown, int numberOfObstackles, int slowDownCoolDown,
                                         int rangeDecreaseCoolDown, int disableBombPlacementCoolDown, bool intantPlacingBombs,
                                         int instantPlacingBombsCoolDown)
        {
            _playerId = playerId;
            _maxNumberOfBombs = maxNumberOfBombs;
            _currentNumberBombs = currentNumberOfBombs;
            _range = range;
            _speed = speed;
            _invincibility = invincibility;
            _invincibilityCoolDown = invincibilityCoolDown;
            _detonator = detonator;
            _ghostMode = ghostmode;
            _ghostModeCoolDown = ghostModeCoolDown;
            _numberOfObstacles = numberOfObstackles;
            _slowDownCoolDown = slowDownCoolDown;
            _rangeDecreasedCoolDown = rangeDecreaseCoolDown;
            _disableBombPlacementCoolDown = disableBombPlacementCoolDown;
            _instantPlacingBombs = intantPlacingBombs;
            _instantPlacingBombsCoolDown = instantPlacingBombsCoolDown;
        }
    }
}
