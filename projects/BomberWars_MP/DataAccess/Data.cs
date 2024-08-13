using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.DataAccess
{
    /// <summary>
    /// Data class contains everthing to construct a Model object. Contains all importatn data for saving 
    /// or loading.
    /// _mapName                    - the name of the map
    /// _mapId                      - ID of the map
    /// _width                      - map's width
    /// _height                     - map's haight
    /// _onePlayerAlive             - 1: one player alive, 0: not one player alive
    /// _timeAfterOnePlayerALive    - time if only one player is alive
    /// _specialBlocks              - contains arrays, in an array there is an x, y coordinate and a Blocktype
    /// _powerUpEnums               - contains the power-ups for a game
    /// _powerUpEnums               - contains the power-downs for a game
    /// _numberOfPlayers            - the number of the current game players
    /// _playersData                - the array contains arrays, and each inner array contains all important data of a player
    /// _bombsData                  - array that contains arrays that contain data of each bomb
    /// _monsterData                - an array that contains arrays that contain data of each monsters
    /// </summary>
    public class Data
    {
        public string? _mapName { get; set; }
        public int _mapId { get; set; }
        public int _width { get; set; }
        public int _height { get; set; }
        public int _onePlayerAlive { get; set; }
        public int _timeAfterOnePlayerAlive { get; set; }
        public int[][]? _specialBlocks { get; set; }
        public int[]? _powerUpEnums { get; set; }
        public int[]? _powerDownEnums { get; set; }
        public int _numberOfPlayers { get; set; }
        public int[][]? _playersData { get; set; }
        public int[][]? _bombsData { get; set; }
        public int[][]? _monsterData { get; set; }
    }
}
