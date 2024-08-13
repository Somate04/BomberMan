using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Represents all the power ups in the game.
    /// </summary>
    public enum PowerUpEnum
    {
        None,                               //Default
        BombNumberIncreasePowerUp,          //Increments the number of bombs of a player
        BombRangeIncreasePowerUp,           //Increments the range of the player's bombs
        DetonatorPowerUp,                   //Enables the detonator, the player can detonates all it's bombs
        RollerSkatePowerUp,                 //Player can move faster
        InvincibilityPowerUp,               //Player cannot die during these period, when invincibility available
        GhostPowerUp,                       //Player can go through walls or boxes
        ObstaclePowerUp                     //Player can place boxes on the board
    }
}
