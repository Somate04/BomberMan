using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberWars_MP.DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Model handles the BomberWars' game loogic.
    /// Deals with all the inputs and returns outputs.
    /// The class uses the Persistence layer which handles the data sturcture and 
    /// IO operations.
    /// Saving and loading a game is possible.
    /// Handles the board and sets it's blocks.
    /// Moves the objects on the board, such as players, monsters.
    /// </summary>
    public class Model
    {
        private DataAccess.DataAccess? _dataAccess;
        private BlockTypeEnum[,]? _board;
        private int _boardWidth;
        private int _boardHeight;
        private int _numberOfPlayers;
        private Player[] _players;
        private List<MonsterBase> _monstersOnBoard;
        private bool _end;
        private PowerUpEnum[]? _availablePowerUps;
        private PowerDownEnum[]? _availablePowerDowns;
        private int _currentMap;
        private bool _onePlayerAlive;
        private int _timeAfterOnePlayerAlive;
        private int _numberOfWonRounds;
        private int _monsterSpawnsProbability;

        private bool _testMode;
        private bool _testMonsters = false; // new bool for testing

        #region Properties
        public int BoardWidth { get { return _boardWidth; } }
        public int BoardHeight { get { return _boardHeight; } }
        public BlockTypeEnum[,]? Board { get { return _board; } set { _board = value; } }
        public int PlayerCount { get { return _numberOfPlayers; } }
        public PowerUpEnum[]? PowerUps { get { return _availablePowerUps; } }
        public PowerDownEnum[]? PowerDowns { get { return _availablePowerDowns; } }
        #endregion

        #region Events
        public EventHandler<BoardChangeEventArgs>? ModelOnMoveEventHandler;

        /// <summary>
        /// Invokes a BoardChangeEventArgs, that contains all the information about moving objects effection on board.
        /// </summary>
        /// <param name="prevtype">previous block type</param>
        /// <param name="prevPosX">x position of the previous block</param>
        /// <param name="prevPosY">y position of the previous block</param>
        /// <param name="currenttype">current block type</param>
        /// <param name="currentPosX">x position of the current block</param>
        /// <param name="currentPosY">y position of the current block</param>
        protected void ModelOnMove(BlockTypeEnum prevtype, int prevPosX, int prevPosY, BlockTypeEnum currenttype, int currentPosX, int currentPosY)
        {
            ModelOnMoveEventHandler?.Invoke(this, new BoardChangeEventArgs(prevtype, prevPosX, prevPosY, currenttype, currentPosX, currentPosY));
        }

        public EventHandler<EndGameEventArgs>? ModelOnEndGameEventHandler;

        /// <summary>
        /// Invokes a EndGameEventArgs, that contains the game over information and standings
        /// </summary>
        /// <param name="end">The game has ended or not</param>
        /// <param name="firstPlayerPoints">number of the first player's points</param>
        /// <param name="secondPlayerPoints">number of the second player's points</param>
        /// <param name="thirdPlayerPoints">number of the third player's points</param>
        protected void ModelOnEndGame(bool end, int firstPlayerPoints, int secondPlayerPoints, int thirdPlayerPoints)
        {
            ModelOnEndGameEventHandler?.Invoke(this, new EndGameEventArgs(end, firstPlayerPoints, secondPlayerPoints, thirdPlayerPoints));
        }

        public EventHandler<EndRoundEventArgs>? ModelOnEndRoundEventHandler;

        /// <summary>
        /// Invokes a EndRoundEventArgs, that contains the round over information and standings
        /// </summary>
        /// <param name="end">The game has ended or not</param>
        /// <param name="firstPlayerPoints">number of the first player's points</param>
        /// <param name="secondPlayerPoints">number of the second player's points</param>
        /// <param name="thirdPlayerPoints">number of the third player's points</param>
        protected void ModelOnEndRound(bool end, int firstPlayerPoints, int secondPlayerPoints, int thirdPlayerPoints)
        {
            ModelOnEndRoundEventHandler?.Invoke(this, new EndRoundEventArgs(end, firstPlayerPoints, secondPlayerPoints, thirdPlayerPoints));
        }

        public EventHandler<PlayerPropertiesEventArgs>? ModelOnPlayerPropertiesEventHandler;

        /// <summary>
        /// Invokes a PlayerPropertiesEvent, that contains all the information about a player to display for users.
        /// </summary>
        /// <param name="eventArgs">The event args that contains all the information</param>
        protected void ModelOnPlayerProperties(PlayerPropertiesEventArgs eventArgs)
        {
            ModelOnPlayerPropertiesEventHandler?.Invoke(this, eventArgs);
        }

        public EventHandler<ExplodeEventArgs>? ModelOnExplodeEventHandler;

        /// <summary>
        /// Invokes a ExplodeEventArgs, that contains all the information about positions explodde by a bomb.
        /// </summary>
        /// <param name="blockType">The type of the block</param>
        /// <param name="posX">x position of the block</param>
        /// <param name="posY">y position of the block</param>
        protected void ModelOnExplode(BlockTypeEnum blockType, int posX, int posY)
        {
            ModelOnExplodeEventHandler?.Invoke(this, new ExplodeEventArgs(blockType, posX, posY));
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the Model class. Starts a new best of 3 matches system.
        /// </summary>
        /// <param name="map">The current map's number, that will be loaded every new match</param>
        /// <param name="players">The number of players of the current match</param>
        /// <param name="availablePowerUps">The available power ups in the current match</param>
        /// <exception cref="DataAccessExeption">Raised when something goes wrong in the Persistence layer.</exception>
        public Model(int map, int players, PowerUpEnum[] availablePowerUps, PowerDownEnum[] availablePowerDowns, int numberOfWonRounds, bool testMode = false)
        {
            if (players != 2 && players != 3) throw new DataAccessExeption();
            if (map != 0 && map != 1 && map != 2) throw new DataAccessExeption();
            if (numberOfWonRounds <= 0) throw new DataAccessExeption();
            if (availablePowerUps.Length == 0 && availablePowerDowns.Length == 0)
            {
                availablePowerUps = new PowerUpEnum[2];
                availablePowerUps[0] = PowerUpEnum.BombNumberIncreasePowerUp;
                availablePowerUps[1] = PowerUpEnum.BombRangeIncreasePowerUp;
            }
            
            _dataAccess = new DataAccess.DataAccess(testMode);
            _currentMap = map;
            _onePlayerAlive = false;
            _timeAfterOnePlayerAlive = 500;

            _availablePowerUps = availablePowerUps;
            _availablePowerDowns = availablePowerDowns;

            _numberOfPlayers = players;
            _players = new Player[_numberOfPlayers];
            for (int i = 0; i < _numberOfPlayers; ++i)
            {
                _players[i] = new Player(i, _availablePowerUps, _availablePowerDowns);
            }
            _monstersOnBoard = new List<MonsterBase>();
            _numberOfWonRounds = numberOfWonRounds;

            _testMode = testMode;

            _monsterSpawnsProbability = 1000;
        }

        /// <summary>
        /// Read the given JSON file, and construct the model object from the data of the file.
        /// </summary>
        /// <param name="file_path">Full path to the previous saves</param>
        public Model(string file_path, bool testMode = false)
        {
            _testMode = testMode;
            try
            {
                _dataAccess = new DataAccess.DataAccess(_testMode);
                Data data = _dataAccess.LoadModel(file_path);

                LoadBoard(data);
                LoadPowerUps(data);
                LoadPowerDowns(data);
                LoadPlayers(data);
                LoadBombs(data);
                LoadMonsters(data);

                //Non-nullable variable must contain a non-null value when exiting constructor - solution meaning if (_players == null) { create an empty array and assign it to _players }
                _players ??= Array.Empty<Player>();
                _monstersOnBoard ??= new List<MonsterBase>();
            }
            catch (Exception)
            {
                throw new DataAccessExeption();
            }

            _monsterSpawnsProbability = 750;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Creates the _board and all it's positions. Gets all the data from the Persistence.DataAccess class.
        /// Sets players all property, and handles it's events.
        /// </summary>
        /// <returns>True if generating was succesful, false otherwise.</returns>
        private bool GenBoard()
        {
            if (_dataAccess is null)
            {
                return false;
            }
            Data data = new Data();
            try
            {
                switch (_currentMap)
                {
                    case 0:
                        data = _dataAccess.GetFirstMap();
                        break;
                    case 1:
                        data = _dataAccess.GetSecondMap();
                        break;
                    case 2:
                        data = _dataAccess.GetThirdMap();
                        break;
                }
            }
            catch (DataAccessExeption)
            {
                return false;
            }
            if (data is null) return false;

            _boardWidth = data._width;
            _boardHeight = data._height;

            _board = new BlockTypeEnum[_boardHeight, _boardWidth];

            for (int i = 0; i < _boardHeight; i++)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    _board[i, j] = BlockTypeEnum.Wall;
                }
            }

            foreach (var coordinate in data._specialBlocks!)
            {
                if (_numberOfPlayers == 2)
                {
                    if ((BlockTypeEnum)coordinate[2] == BlockTypeEnum.PlayerC)
                    {
                        _board[coordinate[0], coordinate[1]] = BlockTypeEnum.Path;
                        continue;
                    }
                }
                _board[coordinate[0], coordinate[1]] = (BlockTypeEnum)coordinate[2];

            }

            return true;
        }

        /// <summary>
        /// Handles everything for a new match (one of the best of three system).
        /// </summary>
        public void NewGame()
        {
            _timeAfterOnePlayerAlive = 500;
            foreach (Player player in _players)
            {
                if (player.Points == _numberOfWonRounds) return;
            }

            _end = false;
            GenBoard();
            _monstersOnBoard.Clear();
            for (int i = 0; i < _numberOfPlayers; ++i)
            {
                _players[i].FindPosition(_board!, _boardHeight, _boardWidth);
                _players[i].SetPropertiesForNewGame();
                _players[i].OnBoardChangeEventHandler += OnMove!;
                _players[i].OnPlayerPropertiesEventHandler += OnPlayerProperties!;
                _players[i].OnExplodeEventHandler += OnExplode!;
                _players[i]?.Bombs?.Clear();
            }
        }

        /// <summary>
        /// Calls every timer tick in ViewModel.
        /// Calls players, monsters on timer tick functions.
        /// Calls GenerateMonster, for new monster objects.
        /// Handles end of a match / game.
        /// </summary>
        public void OnTimerTick()
        {
            if (_end) return;
            if (_board is null) return;

            for (int i = 0; i < _numberOfPlayers; ++i)
            {
                _players[i].OnTimerTick(_board, _boardHeight, _boardWidth);
            }

            GenerateMonster();
            for (int m = _monstersOnBoard.Count - 1; m >= 0; m--)
            {
                if (_monstersOnBoard[m].Alive) _monstersOnBoard[m].OnTimerTick(_board);
                if (!_monstersOnBoard[m].Alive)
                {
                    _monstersOnBoard.Remove(_monstersOnBoard[m]);
                    continue;
                }

            }

            SetOnePLayerAlive();
            if (_onePlayerAlive) CountDownToEndGame();

        }

        /// <summary>
        /// ViewModel layer calls this.
        /// Represents a player's movement. Requires the players id and the direction, after it tries to execute moving.
        /// </summary>
        /// <param name="playerId">The player's id (0,1,2)</param>
        /// <param name="direction">The player's movement direction</param>
        public void PlayerMoves(int playerId, DirectionEnum direction)
        {
            if (_end) return;
            if (_board is null) return;
            try
            {
                _players[playerId].Move(direction, _board);
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Called in ViewModel layer. Tries to execute player's PlaceBomb function. Players are represented with id.
        /// </summary>
        /// <param name="playerId">The player's id (0,1,2)</param>
        public void PlayerPlacesBomb(int playerId)
        {
            if (_end) return;
            if (_board is null) return;
            try
            {
                _players[playerId].PlaceBomb();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Called in ViewModel layer. Tries to execute player's DetonateBombs function. Players are represented with id.
        /// </summary>
        /// <param name="playerId">The player's id (0,1,2)</param>
        public void PlayerDetonates(int playerId)
        {
            if (_end) return;
            if (_board is null) return;
            try
            {
                _players[playerId].DetonateBombs();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Called in ViewModel layer. Tries to execute player's PlaceObstackles function. Players are represented with id.
        /// </summary>
        /// <param name="playerId">The player's id (0,1,2)</param>
        public void PlayerPlacesObstackles(int playerId)
        {
            if (_end) return;
            if (_board is null) return;
            try
            {
                _players[playerId].PlaceObstacle();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Generates a monster every timer tick if the number of monsters on board is less than 4
        /// </summary>
        private void GenerateMonster()
        {
            if (_end) return;
            if (_testMode && !_testMonsters) return;
            if (_monstersOnBoard.Count >= 4) return;

            Random rng = new Random();
            int spawnMonster = rng.Next(_monsterSpawnsProbability);

            switch (spawnMonster)
            {
                case 0:
                    SpawnMonster(0);
                    break;
                case 1:
                    SpawnMonster(1);
                    break;
                case 2:
                    SpawnMonster(2);
                    break;
                case 3:
                    SpawnMonster(3);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Sets a monster on board based on the monster type. 
        /// Monsters must be one block away from players.
        /// </summary>
        /// <param name="monsterType">type of the monster</param>
        private void SpawnMonster(int monsterType)
        {
            if (_end) return;
            if (_board == null) return;
            Random rng = new Random();
            int newPosX = rng.Next(_boardHeight);
            int newPosY = rng.Next(_boardWidth);
            while (true)
            {
                if (_board[newPosX, newPosY] == BlockTypeEnum.Path && !(IsPlayerNearBy(newPosX, newPosY))) break;
                newPosX = rng.Next(_boardHeight);
                newPosY = rng.Next(_boardWidth);
            }

            switch (monsterType)
            {
                case 0:
                    _board[newPosX, newPosY] = BlockTypeEnum.Monster;
                    ModelOnMove(BlockTypeEnum.Monster, newPosX, newPosY, BlockTypeEnum.Monster, newPosX, newPosY);
                    Monster monster = new Monster(newPosX, newPosY, BoardWidth, BoardHeight);
                    monster.OnBoardChangeEventHandler += OnMove!;
                    _monstersOnBoard.Add(monster);
                    break;
                case 1:
                    _board[newPosX, newPosY] = BlockTypeEnum.GhostMonsterOnPath;
                    ModelOnMove(BlockTypeEnum.GhostMonsterOnPath, newPosX, newPosY, BlockTypeEnum.GhostMonsterOnPath, newPosX, newPosY);
                    GhostMonster monster1 = new GhostMonster(newPosX, newPosY, BoardWidth, BoardHeight);
                    monster1.OnBoardChangeEventHandler += OnMove!;
                    _monstersOnBoard.Add(monster1);
                    break;
                case 2:
                    _board[newPosX, newPosY] = BlockTypeEnum.DijkstraMonster;
                    ModelOnMove(BlockTypeEnum.DijkstraMonster, newPosX, newPosY, BlockTypeEnum.DijkstraMonster, newPosX, newPosY);
                    DijkstraMonster monster2 = new DijkstraMonster(newPosX, newPosY, BoardWidth, BoardHeight);
                    monster2.OnBoardChangeEventHandler += OnMove!;
                    _monstersOnBoard.Add(monster2);
                    break;
                case 3:
                    _board[newPosX, newPosY] = BlockTypeEnum.HeuristicMonster;
                    ModelOnMove(BlockTypeEnum.HeuristicMonster, newPosX, newPosY, BlockTypeEnum.HeuristicMonster, newPosX, newPosY);
                    HeuristicMonster monster3 = new HeuristicMonster(newPosX, newPosY, BoardWidth, BoardHeight);
                    monster3.OnBoardChangeEventHandler += OnMove!;
                    _monstersOnBoard.Add(monster3);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Seacrhes for players near by an index. If found one returns true o
        /// </summary>
        /// <param name="x">X position on the board</param>
        /// <param name="y">Y position on the board</param>
        /// <returns>If found a player returns true, otherwise false</returns>
        private bool IsPlayerNearBy(int x, int y)
        {
            if (_board == null) return true;
            try
            {

                if (_board[x, y] == BlockTypeEnum.PlayerA || _board[x, y] == BlockTypeEnum.PlayerB || _board[x, y] == BlockTypeEnum.PlayerC) return true;

                if (_board[x + 1, y] == BlockTypeEnum.PlayerA || _board[x + 1, y] == BlockTypeEnum.PlayerB || _board[x + 1, y] == BlockTypeEnum.PlayerC) return true;
                if (_board[x - 1, y] == BlockTypeEnum.PlayerA || _board[x - 1, y] == BlockTypeEnum.PlayerB || _board[x - 1, y] == BlockTypeEnum.PlayerC) return true;

                if (_board[x, y + 1] == BlockTypeEnum.PlayerA || _board[x, y + 1] == BlockTypeEnum.PlayerB || _board[x, y + 1] == BlockTypeEnum.PlayerC) return true;
                if (_board[x, y - 1] == BlockTypeEnum.PlayerA || _board[x, y - 1] == BlockTypeEnum.PlayerB || _board[x, y - 1] == BlockTypeEnum.PlayerC) return true;

                if (_board[x + 1, y + 1] == BlockTypeEnum.PlayerA || _board[x + 1, y + 1] == BlockTypeEnum.PlayerB || _board[x + 1, y + 1] == BlockTypeEnum.PlayerC) return true;
                if (_board[x + 1, y - 1] == BlockTypeEnum.PlayerA || _board[x + 1, y - 1] == BlockTypeEnum.PlayerB || _board[x + 1, y - 1] == BlockTypeEnum.PlayerC) return true;

                if (_board[x - 1, y + 1] == BlockTypeEnum.PlayerA || _board[x - 1, y + 1] == BlockTypeEnum.PlayerB || _board[x - 1, y + 1] == BlockTypeEnum.PlayerC) return true;
                if (_board[x - 1, y - 1] == BlockTypeEnum.PlayerA || _board[x - 1, y - 1] == BlockTypeEnum.PlayerB || _board[x - 1, y - 1] == BlockTypeEnum.PlayerC) return true;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Decided if one player is alive or not.
        /// If one player alive, than a counter starts for endgame.
        /// If no one is alive end round is raised
        /// </summary>
        private void SetOnePLayerAlive()
        {
            int counter = 0;
            foreach (var player in _players)
            {
                if (player.Alive) counter++;
            }

            if (counter == 0)
            {
                _end = true;
                _onePlayerAlive = false;
                if (_numberOfPlayers == 2)
                {
                    ModelOnEndRound(_end, _players[0].Points, _players[1].Points, 0);
                }
                else
                {
                    ModelOnEndRound(_end, _players[0].Points, _players[1].Points, _players[2].Points);
                }
                return;
            }
            else if (counter == 1)
            {
                _end = false;
                _onePlayerAlive = true;
            }
            else
            {
                _end = false;
                _onePlayerAlive = false;
            }
        }

        /// <summary>
        /// Counts the time till the _timeAfterOnePlayerAlive is zero.
        /// If the counter hits zero the SetPoints is called.
        /// </summary>
        private void CountDownToEndGame()
        {
            if (_end) return;
            if (_timeAfterOnePlayerAlive > 0)
            {
                _timeAfterOnePlayerAlive--;
                if (_timeAfterOnePlayerAlive == 0)
                {
                    _end = true;
                    SetPoints();
                }
            }
        }

        /// <summary>
        /// Sets the players points.
        /// If a player's points equals the _numberOfWonRounds, than the End game event called.
        /// Else if the game ended the End round event called.
        /// </summary>
        private void SetPoints()
        {
            if (!_end) return;
            if (!_onePlayerAlive)
            {
                if (_numberOfPlayers == 2)
                {
                    ModelOnEndRound(_end, _players[0].Points, _players[1].Points, 0);
                }
                else
                {
                    ModelOnEndRound(_end, _players[0].Points, _players[1].Points, _players[2].Points);
                }
                return;
            }

            foreach (var player in _players)
            {
                if (player.Alive) player.Points++;

                if (player.Points == _numberOfWonRounds)
                {
                    if (_numberOfPlayers == 2)
                    {
                        ModelOnEndGame(_end, _players[0].Points, _players[1].Points, 0);
                    }
                    else
                    {
                        ModelOnEndGame(_end, _players[0].Points, _players[1].Points, _players[2].Points);
                    }
                    return;
                }
            }
            if (_numberOfPlayers == 2)
            {
                ModelOnEndRound(_end, _players[0].Points, _players[1].Points, 0);
            }
            else
            {
                ModelOnEndRound(_end, _players[0].Points, _players[1].Points, _players[2].Points);
            }
        }

        /// <summary>
        /// Changes the blocktype enum at the given position of the board and sends information to the viewmodel about the changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExplode(object sender, ExplodeEventArgs e)
        {
            if (_end) return;
            if (e is null) return;
            if (_board is null) return;

            if (e.BlockType == BlockTypeEnum.PowerUp)
            {
                if (_availablePowerUps!.Length != 0 && _availablePowerDowns!.Length != 0) //is _availablePowerDowns == null possible?
                {
                    Random rng = new Random();
                    int prob = rng.Next(0, 2);
                    if (prob > 0)
                    {
                        e.BlockType = BlockTypeEnum.PowerUp;
                    }
                    else
                    {
                        e.BlockType = BlockTypeEnum.PowerDown;
                    }
                }
                else if (_availablePowerUps.Length != 0 && _availablePowerDowns!.Length == 0) //is _availablePowerDowns == null possible?
                {
                    e.BlockType = BlockTypeEnum.PowerUp;
                }
                else if (_availablePowerUps.Length == 0 && _availablePowerDowns!.Length != 0) //is _availablePowerDowns == null possible?
                {
                    e.BlockType = BlockTypeEnum.PowerDown;
                }
                else
                {
                    e.BlockType = BlockTypeEnum.Path;
                }
            }
            _board[e.PosX, e.PosY] = e.BlockType;
            ModelOnExplode(e.BlockType, e.PosX, e.PosY);
        }

        /// <summary>
        /// Sets the the _board blocks after moving and sends the data to viewModel layer.
        /// Data got by moving objects.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">An instance of the BoardChangeEventArgs class containing the event data.</param>
        private void OnMove(object sender, BoardChangeEventArgs e)
        {
            if (_end) return;
            if (e is null) return;
            if (_board is null) return;
            ModelOnMove(e._prevtype, e._prevPosX, e._prevPosY, e._currenttype, e._currentPosX, e._currentPosY);

            _board[e._prevPosX, e._prevPosY] = e._prevtype;
            _board[e._currentPosX, e._currentPosY] = e._currenttype;
        }

        /// <summary>
        /// Sends the players' properties to viewModel layer
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">An instance of the PlayerPropertiesEventArgs class containing the event data.</param>
        private void OnPlayerProperties(object sender, PlayerPropertiesEventArgs e)
        {
            if (e is null) return;
            ModelOnPlayerProperties(e);
        }

        /// <summary>
        /// Creates a Data object that contains all important data from the Model, Player, Bomb, Monster objects.
        /// Tries to execute the DataAccess.SaveModel function, that writes all data to a JSON file.
        /// </summary>
        public void SaveModel()
        {
            if (_board == null) return;

            Data data = new Data();
            data._mapName = "saved_game";
            data._mapId = _currentMap;
            data._width = this._boardWidth;
            data._height = this._boardHeight;
            data._numberOfPlayers = _numberOfPlayers;
            data._powerUpEnums = SavePowerUps();

            data._powerDownEnums = SavePowerDowns();

            data._specialBlocks = SaveBoard();

            data._playersData = SavePlayers();

            data._bombsData = SaveBombs();

            data._monsterData = SaveMonsters();

            _dataAccess?.SaveModel(data);

        }

        /// <summary>
        /// Create an array that contains integers, that represent the powerups.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[] SavePowerUps()
        {
            int[] returnData = new int[_availablePowerUps!.Length];
            if (_availablePowerUps.Length == 0)
            {
                return new int[0];
            }

            for (int i = 0; i < _availablePowerUps.Length; ++i)
            {
                returnData[i] = (int)_availablePowerUps[i];
            }

            return returnData;
        }

        /// <summary>
        /// Create an array that contains integers, that represent the powerdowns.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[] SavePowerDowns()
        {
            int[] returnData = new int[_availablePowerDowns!.Length]; //is _availablePowerDowns == null possible?
            if (_availablePowerDowns.Length == 0)
            {
                return new int[0];
            }

            for (int i = 0; i < _availablePowerDowns.Length; ++i)
            {
                returnData[i] = (int)_availablePowerDowns[i];
            }

            return returnData;
        }

        /// <summary>
        /// Produce an array that contains arrays. Each elment of the outer is an array. This inner arrays contains x, y coordinates
        /// and an integer that represents a BlockTypeEnum. The whole outer array represents the _board's blocks, that is not 'WALL'.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[][] SaveBoard()
        {
            int[][] returnData = new int[1000][];
            int counter = 0;

            for (int i = 0; i < _boardHeight; i++)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    if (_board![i, j] != BlockTypeEnum.Wall)
                    {
                        returnData[counter] = new int[] { i, j, (int)_board[i, j] };
                        counter++;
                    }
                }
            }
            return returnData;
        }

        /// <summary>
        /// Produce an array that contains arrays. Each inner array will be a player's data. This data is the return value
        /// of the Player.SaveData function.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[][] SavePlayers()
        {
            int[][] returnData = new int[_numberOfPlayers][];
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                returnData[i] = _players[i].SaveData();
            }
            return returnData;
        }

        /// <summary>
        /// Produce an array that contains arrays. Each inner array contains data of a Bomb object. The inner arrays data
        /// equal the Bombs.SaveData function. The outer array will contain all players all bombs.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[][] SaveBombs()
        {
            int[][] returnData = new int[30][];
            int[][] firstPlayerBombs = _players[0].SaveBombs();
            int[][] secondPlayerBombs = _players[1].SaveBombs();

            int counter = 0;
            for (int i = 0; i < firstPlayerBombs.Length; ++i)
            {
                returnData[counter] = firstPlayerBombs[i];
                counter++;
            }

            for (int i = 0; i < secondPlayerBombs.Length; ++i)
            {
                returnData[counter] = secondPlayerBombs[i];
                counter++;
            }

            if (_numberOfPlayers == 3)
            {
                int[][] thirdPlayerBombs = _players[2].SaveBombs();
                for (int i = 0; i < thirdPlayerBombs.Length; ++i)
                {
                    returnData[counter] = thirdPlayerBombs[i];
                    counter++;
                }
            }
            return returnData;
        }

        /// <summary>
        /// Produce an array that contains arrays. Conatins every monsters' important data on board. Each inner array will
        /// equal the return data of the BaseMonster.SaveData function.
        /// </summary>
        /// <returns>The generated array that will be in the JSON.</returns>
        private int[][] SaveMonsters()
        {
            int[][] returnData = new int[_monstersOnBoard.Count][];

            for (int i = 0; i < _monstersOnBoard.Count; ++i)
            {
                returnData[i] = _monstersOnBoard[i].SaveData();
            }
            return returnData;
        }

        /// <summary>
        /// Constructs the _board from the data's special block nested arrays.
        /// Sets the base variables from the data.
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadBoard(Data data)
        {
            _currentMap = data._mapId;
            _boardWidth = data._width;
            _boardHeight = data._height;
            _numberOfPlayers = data._numberOfPlayers;
            if (data._onePlayerAlive == 0)
            {
                _onePlayerAlive = false;
            }
            else
            {
                _onePlayerAlive = true;
            }
            _timeAfterOnePlayerAlive = data._timeAfterOnePlayerAlive;

            _board = new BlockTypeEnum[_boardHeight, _boardWidth];

            for (int i = 0; i < _boardHeight; i++)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    _board[i, j] = BlockTypeEnum.Wall;
                }
            }

            foreach (var coordinate in data._specialBlocks!)
            {
                if (coordinate == null) continue;
                if (_numberOfPlayers == 2)
                {
                    if ((BlockTypeEnum)coordinate[2] == BlockTypeEnum.PlayerC)
                    {
                        _board[coordinate[0], coordinate[1]] = BlockTypeEnum.Path;
                        continue;
                    }
                }
                _board[coordinate[0], coordinate[1]] = (BlockTypeEnum)coordinate[2];

            }
        }

        /// <summary>
        /// Loads the available power ups from the Data's _powerUpEnums .
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadPowerUps(Data data)
        {
            if (data._powerUpEnums!.Length == 0) //is _powerUpEnums == null possible?
            {
                _availablePowerUps = new PowerUpEnum[0];
            }
            else
            {
                _availablePowerUps = new PowerUpEnum[data._powerUpEnums.Length];
                for (int i = 0; i < data._powerUpEnums.Length; i++)
                {
                    _availablePowerUps[i] = (PowerUpEnum)data._powerUpEnums[i];
                }
            }
        }

        /// <summary>
        /// Loads the available power ups from the Data's _powerDownEnums .
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadPowerDowns(Data data)
        {
            if (data._powerDownEnums?.Length == 0)
            {
                _availablePowerDowns = new PowerDownEnum[0];
            }
            else
            {
                _availablePowerDowns = new PowerDownEnum[data._powerDownEnums!.Length]; //is _powerDownEnums == null possible?
                for (int i = 0; i < data?._powerDownEnums?.Length; i++)
                {
                    _availablePowerDowns[i] = (PowerDownEnum)data._powerDownEnums[i];
                }
            }
        }

        /// <summary>
        /// Sets all data of the players from the Data's _playersData.
        /// Subscribe for the players's events as well.
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadPlayers(Data data)
        {
            _players = new Player[_numberOfPlayers];
            for (int i = 0; i < _numberOfPlayers; ++i)
            {
                if (data._playersData is null) continue;
                if (data._playersData[i] is null) continue;
                _players[i] = new Player(data._playersData[i], _availablePowerUps, _availablePowerDowns);
                _players[i].FindPosition(_board!, _boardHeight, _boardWidth);
                _players[i].OnBoardChangeEventHandler += OnMove!;
                _players[i].OnPlayerPropertiesEventHandler += OnPlayerProperties!;
                _players[i].OnExplodeEventHandler += OnExplode!;
            }
        }

        /// <summary>
        /// From Data's _bombsData this constructs all saved bombs, via the players.
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadBombs(Data data)
        {
            for (int i = 0; i < data._bombsData!.Length; i++)
            {
                if (data._bombsData[i] == null) continue;

                switch (data._bombsData[i][0])
                {
                    case 0:
                        _players[0].LoadBomb(data._bombsData[i]);
                        break;
                    case 1:
                        _players[1].LoadBomb(data._bombsData[i]);
                        break;
                    case 2:
                        _players[2].LoadBomb(data._bombsData[i]);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Loads the monsters from te saved Data's _monsterData.
        /// </summary>
        /// <param name="data">Data object that contains the important data</param>
        private void LoadMonsters(Data data)
        {
            _monstersOnBoard = new List<MonsterBase>();
            for (int i = 0; i < data._monsterData!.Length; i++)
            {
                if (data._monsterData[i] == null) continue;
                if (data?._monsterData[i][6] == 0) continue;

                switch (data!._monsterData[i][0])
                {
                    case 0:
                        Monster monster = new Monster(data._monsterData[i][1], data._monsterData[i][2], data._monsterData[i][3], (int)BlockTypeEnum.Path, data._monsterData[i][4], data._monsterData[i][5], BoardWidth, BoardHeight);
                        monster.OnBoardChangeEventHandler += OnMove!;
                        _monstersOnBoard.Add(monster);
                        break;
                    case 1:
                        GhostMonster ghostMonster = new GhostMonster(data._monsterData[i][1], data._monsterData[i][2], data._monsterData[i][3], (int)BlockTypeEnum.Path, data._monsterData[i][4], data._monsterData[i][5], BoardWidth, BoardHeight);
                        ghostMonster.OnBoardChangeEventHandler += OnMove!;
                        _monstersOnBoard.Add(ghostMonster);
                        break;
                    case 2:
                        DijkstraMonster dijkstraMonster = new DijkstraMonster(data._monsterData[i][1], data._monsterData[i][2], data._monsterData[i][3], (int)BlockTypeEnum.Path, data._monsterData[i][4], data._monsterData[i][5], BoardWidth, BoardHeight);
                        dijkstraMonster.OnBoardChangeEventHandler += OnMove!;
                        _monstersOnBoard.Add(dijkstraMonster);
                        break;
                    case 3:
                        HeuristicMonster heuristicMonster = new HeuristicMonster(data._monsterData[i][1], data._monsterData[i][2], data._monsterData[i][3], (int)BlockTypeEnum.Path, data._monsterData[i][4], data._monsterData[i][5], BoardWidth, BoardHeight);
                        heuristicMonster.OnBoardChangeEventHandler += OnMove!;
                        _monstersOnBoard.Add(heuristicMonster);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }
}
