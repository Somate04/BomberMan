using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Represents all the power downs in the game.
    /// </summary>
    public enum PowerDownEnum
    {
        None,                               //Default
        SlowDownPowerDown,                  //Slows down tha player
        BombRangeDecreasePowerDown,         //PLayer's bomb's range decreased to zero
        DisableBombPlacementPowerDown,      //Disables bomb placement by a player
        InstantBombPlacementPowerDown       //Playces bombs after every move, if possible
    }
}
