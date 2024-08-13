using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.DataAccess
{
    /// <summary>
    /// Represents all block, that occur on a game board.
    /// </summary>
    public enum BlockTypeEnum
    { 
        None = 0,                   //Default
        Wall = 1,                   //Wall on the board
        Box = 2,                    //Box on the board
        Path = 3,                   //Path on the board
        Monster = 4,                //Normal monster on board
        Bomb = 5,                   //Bomb on the board
        Explosion = 6,              //Explosion of the bomb
        PlayerA = 7,                //The first player accurence on Path block
        PlayerB = 8,                //The second player accurence on Path block
        PlayerC = 9,                //The third player accurence on Path block
        PowerUp = 10,               //Power up on the board
        BoxPlacedByPlayer = 11,     //A Box block placed be a player 
        GhostPlayerAOnWall = 12,    //First player in ghost mode, and player is on wall block
        GhostPlayerBOnWall = 13,    //Second player in ghost mode, and player is on wall block
        GhostPlayerCOnWall = 14,    //Third player in ghost mode, and player is on wall block
        GhostPlayerAOnBox = 15,     //First player in ghost mode, and player is on box block
        GhostPlayerBOnBox = 16,     //Second player in ghost mode, and player is on wall block
        GhostPlayerCOnBox = 17,     //Third player in ghost mode, and player is on wall block
        GhostMonsterOnPath = 18,    //GhostMonster on path block
        GhostMonsterOnWall = 19,    //GhostMonster on wall block
        GhostMonsterOnBox = 20,     //GhostMonster on box block
        PowerDown = 21,             //Power down on the board
        DijkstraMonster = 22,       //Dijkstra monster on board
        HeuristicMonster = 23       //Heuristic monster on board
    }

}
