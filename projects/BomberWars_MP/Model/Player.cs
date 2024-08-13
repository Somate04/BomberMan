using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// This class represents a player in Model. Handles moving, power ups, downs, player's bombs.
    /// Raises events based on board changing for Model.
    /// </summary>
    public class Player : Unit
    {
        #region Fields
        private int _id;
        private int _points;
        private int _speed;
        private int _moveCoolDown;
        private bool _canMove;
        private int _numberOfBombs;
        private bool _placedOneBomb;
        private List<Bomb>? _bombs;
        private int _range;
        private PowerUpEnum[] _availablePowerUps;
        private PowerDownEnum[] _availablePowerDowns;
        private int _baseCoolDownValue;
        private BlockTypeEnum _previousBlock;

        private bool _invincibility;
        private int _invincibilityCoolDown;
        private bool _detonator;
        private bool _ghostMode;
        private int _ghostModeCoolDown;
        private int _numberOfObstacles;
        private bool _placedAnObstacle;
        private int _slowDownCoolDown;
        private int _rangeDecreasedCoolDown;
        private bool _canPlaceBomb;
        private int _disableBombPlacementCoolDown;
        private bool _instantPlacingBombs;
        private int _instantPlacingBombsCoolDown;
        #endregion

        #region Events
        public EventHandler<PlayerPropertiesEventArgs>? OnPlayerPropertiesEventHandler;

        /// <summary>
        /// Invokes a PlayerPropertiesEvent, that contains all the information about a player to display for users.
        /// </summary>
        protected void OnPlayerProperties()
        {
            OnPlayerPropertiesEventHandler?.Invoke(this, new PlayerPropertiesEventArgs(_id,
                                                                                        _numberOfBombs,
                                                                                        _bombs!.Count,
                                                                                        _range,
                                                                                        _speed,
                                                                                        _invincibility,
                                                                                        _invincibilityCoolDown,
                                                                                        _detonator,
                                                                                        _ghostMode,
                                                                                        _ghostModeCoolDown,
                                                                                        _numberOfObstacles,
                                                                                        _slowDownCoolDown,
                                                                                        _rangeDecreasedCoolDown,
                                                                                        _disableBombPlacementCoolDown,
                                                                                        _instantPlacingBombs,
                                                                                        _instantPlacingBombsCoolDown));
        }

        public EventHandler<ExplodeEventArgs>? OnExplodeEventHandler;

        /// <summary>
        /// Invokes a ExplodeEventArgs, that contains all the information about positions explodde by a bomb.
        /// </summary>
        /// <param name="blockType">The type of the block</param>
        /// <param name="posX">x position of the block</param>
        /// <param name="posY">y position of the block</param>
        protected void OnBombExploded(BlockTypeEnum blockType, int posX, int posY)
        {
            OnExplodeEventHandler?.Invoke(this, new ExplodeEventArgs(blockType, posX, posY));
        }

        #endregion

        #region Properties
        public int Points { get { return _points; } set { _points = value; } }
        public List<Bomb>? Bombs { get { return _bombs; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for player. Player needs an id and all the available power ups. C'tor sets the default speed, points
        /// and calls the SetPropertiesForNewGame function.
        /// </summary>
        /// <param name="id">Id of the player</param>
        /// <param name="availablePowerUps">Array of the available power ups in the match </param>
        public Player(int id, PowerUpEnum[]? availablePowerUps = null, PowerDownEnum[]? availablePowerDowns = null)
        {
            if (availablePowerUps == null)
            {
                availablePowerUps = new PowerUpEnum[] { PowerUpEnum.None };
            }
            if (availablePowerDowns == null)
            {
                availablePowerDowns = new PowerDownEnum[] { PowerDownEnum.None };
            }

            _id = id;
            _availablePowerUps = availablePowerUps;
            _availablePowerDowns = availablePowerDowns;
            _speed = 10;
            _points = 0;
            SetPropertiesForNewGame();
        }

        /// <summary>
        /// COnstructor for Player, when the player is loaded from Data.
        /// </summary>
        /// <param name="data">Contains all imoprtant data of the player</param>
        /// <param name="availablePowerUps">Array of the available power ups in the match </param>
        public Player(int[] data, PowerUpEnum[]? availablePowerUps = null, PowerDownEnum[]? availablePowerDowns = null)
        {
            if (availablePowerUps == null)
            {
                availablePowerUps = new PowerUpEnum[] { PowerUpEnum.None };
            }
            if (availablePowerDowns == null)
            {
                availablePowerDowns = new PowerDownEnum[] { PowerDownEnum.None };
            }

            _availablePowerUps = availablePowerUps;
            _availablePowerDowns = availablePowerDowns;
            _id = data[0];
            _posX = data[1];
            _posY = data[2];
            _points = data[3];
            _speed = data[4];
            _moveCoolDown = data[5];

            if (data[6] == 0)
            {
                _canMove = false;
            }
            else
            {
                _canMove = true;
            }

            _numberOfBombs = data[7];

            if (data[8] == 0)
            {
                _placedOneBomb = false;
            }
            else
            {
                _placedOneBomb = true;
            }

            _range = data[9];
            _baseCoolDownValue = data[10];
            _previousBlock = (BlockTypeEnum)data[11];

            if (data[12] == 0)
            {
                _invincibility = false;
            }
            else
            {
                _invincibility = true;
            }

            _invincibilityCoolDown = data[13];

            if (data[14] == 0)
            {
                _detonator = false;
            }
            else
            {
                _detonator = true;
            }

            if (data[15] == 0)
            {
                _ghostMode = false;
            }
            else
            {
                _ghostMode = true;
            }
            _ghostModeCoolDown = data[16];

            _numberOfObstacles = data[17];

            if (data[18] == 0)
            {
                _placedAnObstacle = false;
            }
            else
            {
                _placedAnObstacle = true;
            }

            _slowDownCoolDown = data[19];
            _rangeDecreasedCoolDown = data[20];

            if (data[21] == 0)
            {
                _canPlaceBomb = false;
            }
            else
            {
                _canPlaceBomb = true;
            }
            _disableBombPlacementCoolDown = data[22];

            if (data[23] == 0)
            {
                _instantPlacingBombs = false;
            }
            else
            {
                _instantPlacingBombs = true;
            }

            _instantPlacingBombsCoolDown = data[24];

            if (data[25] == 0)
            {
                _alive = false;
            }
            else
            {
                _alive = true;
            }
            _bombs = new List<Bomb>();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Searches the player's occurence on the current board. Sets the player's coordinates if the location found.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="boardHeight">Gameboard's height</param>
        /// <param name="boardWidth">Gameboard's width</param>
        /// <returns>True if the function finds the player location, otherwise false.</returns>
        public bool FindPosition(BlockTypeEnum[,] board, int boardHeight, int boardWidth)
        {
            if (board == null) return false;

            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    switch (_id)
                    {
                        case 0:
                            if (board[i, j] == BlockTypeEnum.PlayerA || board[i, j] == BlockTypeEnum.GhostPlayerAOnWall || board[i, j] == BlockTypeEnum.GhostPlayerAOnBox)
                            {
                                _posX = i;
                                _posY = j;
                                return true;
                            }
                            break;
                        case 1:
                            if (board[i, j] == BlockTypeEnum.PlayerB || board[i, j] == BlockTypeEnum.GhostPlayerBOnWall || board[i, j] == BlockTypeEnum.GhostPlayerBOnBox)
                            {
                                _posX = i;
                                _posY = j;
                                return true;
                            }
                            break;
                        case 2:
                            if (board[i, j] == BlockTypeEnum.PlayerC || board[i, j] == BlockTypeEnum.GhostPlayerCOnWall || board[i, j] == BlockTypeEnum.GhostPlayerCOnBox)
                            {
                                _posX = i;
                                _posY = j;
                                return true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        #region Move functions

        /// <summary>
        /// FInds the current player, who is moving. Then calls the appropriate move function due to direction.
        /// </summary>
        /// <param name="direction">Direction of the movement.</param>
        /// <param name="board">Current gameboard</param>
        public void Move(DirectionEnum direction, BlockTypeEnum[,] board)
        {
            if (!_alive) return;
            if (!_canMove) return;

            BlockTypeEnum currentPlayer;
            switch (_id)
            {
                case 0:
                    currentPlayer = BlockTypeEnum.PlayerA;
                    break;
                case 1:
                    currentPlayer = BlockTypeEnum.PlayerB;
                    break;
                case 2:
                    currentPlayer = BlockTypeEnum.PlayerC;
                    break;
                default:
                    return;
            }

            try
            {

                switch (direction)
                {
                    case DirectionEnum.Left:
                        MoveLeft(board, currentPlayer);
                        break;
                    case DirectionEnum.Right:
                        MoveRight(board, currentPlayer);
                        break;
                    case DirectionEnum.Up:
                        MoveUp(board, currentPlayer);
                        break;
                    case DirectionEnum.Down:
                        MoveDown(board, currentPlayer);
                        break;
                    default:
                        return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            if (_instantPlacingBombs)
            {
                this.PlaceBomb();
            }

            _moveCoolDown = _speed;
            _canMove = false;
        }

        /// <summary>
        /// Moves the current player left, if it is possible. Raise a BoardChangeEventArgs after movement.
        /// Sets the player's prevoius block, and positions.
        /// Places a Bomb or a BoxPlacedByPlayer if the player previously called the PlaceBomb or PlaceObstacle functions.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="currentPlayer">Current player enum</param>
        private void MoveLeft(BlockTypeEnum[,] board, BlockTypeEnum currentPlayer)
        {
            if (board[_posX, _posY - 1] == BlockTypeEnum.Path || board[_posX, _posY - 1] == BlockTypeEnum.PowerUp || board[_posX, _posY - 1] == BlockTypeEnum.PowerDown || board[_posX, _posY - 1] == BlockTypeEnum.Explosion || board[_posX, _posY - 1] == BlockTypeEnum.Monster || _ghostMode)
            {
                if (board[_posX, _posY - 1] == BlockTypeEnum.PowerUp) { AddPowerUp(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX, _posY - 1] == BlockTypeEnum.PowerDown) { AddPowerDown(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX, _posY - 1] == BlockTypeEnum.Explosion) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }
                if (board[_posX, _posY - 1] == BlockTypeEnum.Monster) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }

                if (_placedOneBomb)
                {
                    Bomb bomb = new Bomb(_range, _posX, _posY);
                    _bombs?.Add(bomb);
                    _previousBlock = BlockTypeEnum.Bomb;
                    _placedOneBomb = false;
                    bomb.ExplosionEventHandler += OnExplode!;
                }
                else if (_placedAnObstacle)
                {
                    _previousBlock = BlockTypeEnum.BoxPlacedByPlayer;
                    _placedAnObstacle = false;
                }
                BlockTypeEnum currentPreviousBlock = _previousBlock;
                if (board[_posX, _posY - 1] == BlockTypeEnum.PowerUp || board[_posX, _posY - 1] == BlockTypeEnum.PowerDown)
                {
                    _previousBlock = BlockTypeEnum.Path;
                }
                else
                {
                    _previousBlock = board[_posX, _posY - 1];
                }
                OnBoardChange(currentPreviousBlock, _posX, _posY, SetPlayerEnum(currentPlayer, board, _posX, _posY - 1), _posX, _posY - 1);
                _posY--;
            }
        }

        /// <summary>
        /// Moves the current player right, if it is possible. Raise a BoardChangeEventArgs after movement.
        /// Places a Bomb or a BoxPlacedByPlayer if the player previously called the PlaceBomb or PlaceObstacle functions.
        /// Sets the player's prevoius block, and positions.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="currentPlayer">Current player enum</param>
        private void MoveRight(BlockTypeEnum[,] board, BlockTypeEnum currentPlayer)
        {
            if (board[_posX, _posY + 1] == BlockTypeEnum.Path || board[_posX, _posY + 1] == BlockTypeEnum.PowerUp || board[_posX, _posY + 1] == BlockTypeEnum.PowerDown || board[_posX, _posY + 1] == BlockTypeEnum.Explosion || board[_posX, _posY + 1] == BlockTypeEnum.Monster || _ghostMode)
            {
                if (board[_posX, _posY + 1] == BlockTypeEnum.PowerUp) { AddPowerUp(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX, _posY + 1] == BlockTypeEnum.PowerDown) { AddPowerDown(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX, _posY + 1] == BlockTypeEnum.Explosion) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }
                if (board[_posX, _posY + 1] == BlockTypeEnum.Monster) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }

                if (_placedOneBomb)
                {
                    Bomb bomb = new Bomb(_range, _posX, _posY);
                    _bombs?.Add(bomb);
                    _previousBlock = BlockTypeEnum.Bomb;
                    _placedOneBomb = false;
                    bomb.ExplosionEventHandler += OnExplode!;
                }
                else if (_placedAnObstacle)
                {
                    _previousBlock = BlockTypeEnum.BoxPlacedByPlayer;
                    _placedAnObstacle = false;
                }
                BlockTypeEnum currentPreviousBlock = _previousBlock;
                if (board[_posX, _posY + 1] == BlockTypeEnum.PowerUp || board[_posX, _posY + 1] == BlockTypeEnum.PowerDown)
                {
                    _previousBlock = BlockTypeEnum.Path;
                }
                else
                {
                    _previousBlock = board[_posX, _posY + 1];
                }
                OnBoardChange(currentPreviousBlock, _posX, _posY, SetPlayerEnum(currentPlayer, board, _posX, _posY + 1), _posX, _posY + 1);
                _posY++;
            }
        }


        /// <summary>
        /// Moves the current player up, if it is possible. Raise a BoardChangeEventArgs after movement.
        /// Sets the player's prevoius block, and positions.
        /// Places a Bomb or a BoxPlacedByPlayer if the player previously called the PlaceBomb or PlaceObstacle functions.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="currentPlayer">Current player enum</param>
        private void MoveUp(BlockTypeEnum[,] board, BlockTypeEnum currentPlayer)
        {
            if (board[_posX - 1, _posY] == BlockTypeEnum.Path || board[_posX - 1, _posY] == BlockTypeEnum.PowerUp || board[_posX - 1, _posY] == BlockTypeEnum.PowerDown || board[_posX - 1, _posY] == BlockTypeEnum.Explosion || board[_posX - 1, _posY] == BlockTypeEnum.Monster || _ghostMode)
            {
                if (board[_posX - 1, _posY] == BlockTypeEnum.PowerUp) { AddPowerUp(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX - 1, _posY] == BlockTypeEnum.PowerDown) { AddPowerDown(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX - 1, _posY] == BlockTypeEnum.Explosion) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }
                if (board[_posX - 1, _posY] == BlockTypeEnum.Monster) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }

                if (_placedOneBomb)
                {
                    Bomb bomb = new Bomb(_range, _posX, _posY);
                    _bombs?.Add(bomb);
                    _previousBlock = BlockTypeEnum.Bomb;
                    _placedOneBomb = false;
                    bomb.ExplosionEventHandler += OnExplode!;
                }
                else if (_placedAnObstacle)
                {
                    _previousBlock = BlockTypeEnum.BoxPlacedByPlayer;
                    _placedAnObstacle = false;
                }
                BlockTypeEnum currentPreviousBlock = _previousBlock;
                if (board[_posX - 1, _posY] == BlockTypeEnum.PowerUp || board[_posX - 1, _posY] == BlockTypeEnum.PowerDown)
                {
                    _previousBlock = BlockTypeEnum.Path;
                }
                else
                {
                    _previousBlock = board[_posX - 1, _posY];
                }
                OnBoardChange(currentPreviousBlock, _posX, _posY, SetPlayerEnum(currentPlayer, board, _posX - 1, _posY), _posX - 1, _posY);
                _posX--;
            }
        }


        /// <summary>
        /// Moves the current player down, if it is possible. Raise a BoardChangeEventArgs after movement.
        /// Sets the player's prevoius block, and positions.
        /// Places a Bomb or a BoxPlacedByPlayer if the player previously called the PlaceBomb or PlaceObstacle functions.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="currentPlayer">Current player enum</param>
        private void MoveDown(BlockTypeEnum[,] board, BlockTypeEnum currentPlayer)
        {
            if (board[_posX + 1, _posY] == BlockTypeEnum.Path || board[_posX + 1, _posY] == BlockTypeEnum.PowerUp || board[_posX + 1, _posY] == BlockTypeEnum.PowerDown || board[_posX + 1, _posY] == BlockTypeEnum.Explosion || board[_posX + 1, _posY] == BlockTypeEnum.Monster || _ghostMode)
            {
                if (board[_posX + 1, _posY] == BlockTypeEnum.PowerUp) { AddPowerUp(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX + 1, _posY] == BlockTypeEnum.PowerDown) { AddPowerDown(); _previousBlock = BlockTypeEnum.Path; }
                if (board[_posX + 1, _posY] == BlockTypeEnum.Explosion) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }
                if (board[_posX + 1, _posY] == BlockTypeEnum.Monster) { Kill(); OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Path, _posX, _posY); return; }

                if (_placedOneBomb)
                {
                    Bomb bomb = new Bomb(_range, _posX, _posY);
                    _bombs?.Add(bomb);
                    _previousBlock = BlockTypeEnum.Bomb;
                    _placedOneBomb = false;
                    bomb.ExplosionEventHandler += OnExplode!;
                }
                else if (_placedAnObstacle)
                {
                    _previousBlock = BlockTypeEnum.BoxPlacedByPlayer;
                    _placedAnObstacle = false;
                }
                BlockTypeEnum currentPreviousBlock = _previousBlock;
                if (board[_posX + 1, _posY] == BlockTypeEnum.PowerUp || board[_posX + 1, _posY] == BlockTypeEnum.PowerDown)
                {
                    _previousBlock = BlockTypeEnum.Path;
                }
                else
                {
                    _previousBlock = board[_posX + 1, _posY];
                }
                OnBoardChange(currentPreviousBlock, _posX, _posY, SetPlayerEnum(currentPlayer, board, _posX + 1, _posY), _posX + 1, _posY);
                _posX++;
            }
        }

        /// <summary>
        /// Sets the player's appearance on the enum board.
        /// For example if the ghost mode is on, then it won't put a player on a wall's posistion,but also
        /// a player on a wall. With box the phenomena will be simillar.
        /// </summary>
        /// <returns>The block type that will be on the board.</returns>
        private BlockTypeEnum SetPlayerEnum(BlockTypeEnum currentPlayer, BlockTypeEnum[,] board, int posX, int posY)
        {
            if (!_ghostMode) { return currentPlayer; }

            switch (_id)
            {
                case 0:
                    if (board[posX, posY] == BlockTypeEnum.Wall)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerAOnWall;
                    }
                    else if (board[posX, posY] == BlockTypeEnum.Box || board[posX, posY] == BlockTypeEnum.BoxPlacedByPlayer)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerAOnBox;
                    }
                    break;
                case 1:
                    if (board[posX, posY] == BlockTypeEnum.Wall)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerBOnWall;
                    }
                    else if (board[posX, posY] == BlockTypeEnum.Box || board[posX, posY] == BlockTypeEnum.BoxPlacedByPlayer)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerBOnBox;
                    }
                    break;
                case 2:
                    if (board[posX, posY] == BlockTypeEnum.Wall)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerCOnWall;
                    }
                    else if (board[posX, posY] == BlockTypeEnum.Box || board[posX, posY] == BlockTypeEnum.BoxPlacedByPlayer)
                    {
                        currentPlayer = BlockTypeEnum.GhostPlayerCOnBox;
                    }
                    break;
                default:
                    break;
            }
            return currentPlayer;
        }
        #endregion

        /// <summary>
        /// Sets the _placeedOneBomb variable true, if the player is alive, the number of bombs is higher than the _bombs list count, 
        /// and the player haven't placed an obstacle previously.
        /// The actual bomb placing executed in one of the move functions.
        /// </summary>
        public void PlaceBomb()
        {
            if (!_alive) return;
            if (!_canPlaceBomb) return;
            if (_placedOneBomb) return;
            if (_numberOfBombs == 0) return;
            if (_bombs is null) return;
            if (_numberOfBombs == _bombs.Count) return;
            if (_placedAnObstacle) return;
            _placedOneBomb = true;
        }

        /// <summary>
        /// Sets the _placedAnObstacle variable true, if the player is alive, the number of obstackles is higher than zero, 
        /// and the player haven't placed a bomb previously.
        /// The actual obstackle placing executed in one of the move functions.
        /// </summary>
        public void PlaceObstacle()
        {
            if (!_alive) return;
            if (_numberOfObstacles <= 0) return;
            if (_placedOneBomb) return;
            if (_placedAnObstacle) return;

            _placedAnObstacle = true;
            _numberOfObstacles--;
        }

        /// <summary>
        /// Detonates all the player's bombs. Only happens if the player have collected the detonator power up.
        /// </summary>
        public void DetonateBombs()
        {
            if (!_alive) return;
            if (!_detonator) return;
            if (_bombs is null) return;

            foreach (Bomb bomb in _bombs)
            {
                bomb.Time = 1;
            }
        }

        #region Power Ups/Downs
        /// <summary>
        /// If the player steps on a power up, this function choose a powerup for the player randomly from the available
        /// power ups. Then executes the appropriate functions for the power up.
        /// </summary>
        private void AddPowerUp()
        {
            if (!_alive) return;
            if (_availablePowerUps.Count() == 0) return;
            int powerUpIdx = random.Next(_availablePowerUps.Count());
            PowerUpEnum currentPowerUp = _availablePowerUps[powerUpIdx];

            switch (currentPowerUp)
            {
                case PowerUpEnum.None:
                    return;
                case PowerUpEnum.BombNumberIncreasePowerUp:
                    BombNumberIncreasePowerUp();
                    break;
                case PowerUpEnum.BombRangeIncreasePowerUp:
                    BombRangeIncreasePowerUp();
                    break;
                case PowerUpEnum.DetonatorPowerUp:
                    DetonatorPowerUp();
                    break;
                case PowerUpEnum.RollerSkatePowerUp:
                    RollerSkatePowerUp();
                    break;
                case PowerUpEnum.InvincibilityPowerUp:
                    InvincibilityPowerUp();
                    break;
                case PowerUpEnum.GhostPowerUp:
                    GhostPowerUp();
                    break;
                case PowerUpEnum.ObstaclePowerUp:
                    ObstaclePowerUp();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// If the player steps on a power up, this function choose a powerup for the player randomly from the available
        /// power ups. Then executes the appropriate functions for the power up.
        /// </summary>
        private void AddPowerDown()
        {
            if (!_alive) return;
            if (_availablePowerDowns.Count() == 0) return;
            int powerDownIdx = random.Next(_availablePowerDowns.Count());
            PowerDownEnum currentPowerUp = _availablePowerDowns[powerDownIdx];

            switch (currentPowerUp)
            {
                case PowerDownEnum.None:
                    return;
                case PowerDownEnum.SlowDownPowerDown:
                    SlowDownPowerDown();
                    break;
                case PowerDownEnum.BombRangeDecreasePowerDown:
                    BombRangeDecreasePowerDown();
                    break;
                case PowerDownEnum.DisableBombPlacementPowerDown:
                    DisableBombPlacementPowerDown();
                    break;
                case PowerDownEnum.InstantBombPlacementPowerDown:
                    InstantBombPlacementPowerDown();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// On BombNumberIncreasePowerUp power up, increase the available number of bumbs by one.
        /// Max number of bombs is 10.
        /// </summary>
        private void BombNumberIncreasePowerUp()
        {
            if (_numberOfBombs < 10)
            {
                _numberOfBombs++;
            }
        }

        /// <summary>
        /// On BombRangeIncreasePowerUp power up, this increments the bomb's range by one.
        /// Max range is 5.
        /// </summary>
        private void BombRangeIncreasePowerUp()
        {
            if (_range < 5)
            {
                _range++;
            }
        }

        /// <summary>
        /// On DetonatorPowerUp power up, enables the _detonator for the player.
        /// Can detonate bombs intantly.
        /// </summary>
        private void DetonatorPowerUp()
        {
            if (!_detonator) _detonator = true;
        }

        /// <summary>
        /// On RollerSkatePowerUp, increase the speed of the player, till the end of the match.
        /// </summary>
        private void RollerSkatePowerUp()
        {
            _moveCoolDown = 0;
            _speed = 5;
            _canMove = true;
        }

        /// <summary>
        /// On InvincibilityPowerUp, enables _invincibility for the player. For a while the player won't die in explosions,
        /// or catched be monsters.
        /// </summary>
        private void InvincibilityPowerUp()
        {
            _invincibility = true;
            _invincibilityCoolDown = _baseCoolDownValue;
        }

        /// <summary>
        /// On GhostPowerUp, enables _ghostMode for the player. For a while the player can move through walls, boxes.
        /// </summary>
        private void GhostPowerUp()
        {
            _ghostMode = true;
            _ghostModeCoolDown = _baseCoolDownValue;
        }

        /// <summary>
        /// On ObstaclePowerUp, player can place three obstackles on the map.
        /// </summary>
        private void ObstaclePowerUp()
        {
            _numberOfObstacles = _numberOfObstacles + 3;
        }

        /// <summary>
        /// On SlowDownPowerDown, decrease the player's speed, for a while.
        /// </summary>
        private void SlowDownPowerDown()
        {
            _speed = 15;
            _moveCoolDown = _speed;
            _slowDownCoolDown = _baseCoolDownValue;
            _canMove = false;
        }

        /// <summary>
        /// On BombRangeDecreasePowerDown, decreases the bomb's range to 0 for a while.
        /// </summary>
        private void BombRangeDecreasePowerDown()
        {
            _range = 0;
            _rangeDecreasedCoolDown = _baseCoolDownValue;
        }

        /// <summary>
        /// On DisableBombPlacementPowerDown, player can't place any bombs on the map, for a while.
        /// </summary>
        private void DisableBombPlacementPowerDown()
        {
            _canPlaceBomb = false;
            _disableBombPlacementCoolDown = _baseCoolDownValue;
        }

        /// <summary>
        /// On InstantBombPlacementPowerDown, player will playce a bomb after every movement, whenever it's possible.
        /// </summary>
        private void InstantBombPlacementPowerDown()
        {
            _instantPlacingBombs = true;
            _instantPlacingBombsCoolDown = _baseCoolDownValue;
        }
        #endregion

        /// <summary>
        /// Called in model on every timer tick.
        /// Calls bombs' OnTimerTick.
        /// Calls FindPositon, if it returns false, executes the Kill function.
        /// Sets every power ups property and their cool downs. If a cool down reach zero, then turns off the prower up.
        /// </summary>
        /// <param name="board">Current gameboard</param>
        /// <param name="boardHeight">Gameboard's height</param>
        /// <param name="boardWidth">Gameboard's width</param>
        public void OnTimerTick(BlockTypeEnum[,] board, int boardHeight, int boardWidth)
        {
            if (_bombs != null)
            {
                int bombnum = _bombs.Count - 1;
                for (int b = bombnum; b >= 0; b--)
                {
                    if (_bombs[b] is null) continue;

                    _bombs[b].OnTimerTick(board, boardHeight, boardWidth);
                    if (!_bombs[b].Alive)
                    {
                        _bombs.Remove(_bombs[b]);
                    }
                }
            }
            OnPlayerProperties();


            if (!_alive) return;

            if (!FindPosition(board, boardHeight, boardWidth))
            {
                if (_invincibility)
                {
                    BlockTypeEnum currentPlayer;
                    switch (_id)
                    {
                        case 0:
                            currentPlayer = BlockTypeEnum.PlayerA;
                            break;
                        case 1:
                            currentPlayer = BlockTypeEnum.PlayerB;
                            break;
                        case 2:
                            currentPlayer = BlockTypeEnum.PlayerC;
                            break;
                        default:
                            return;
                    }
                    OnBoardChange(BlockTypeEnum.None, _posX, _posY, currentPlayer, _posX, _posY);
                }
                else
                {
                    Kill();
                    return;
                }
            }

            if (_moveCoolDown > 0)
            {
                _moveCoolDown--;
                if (_moveCoolDown == 0) { _canMove = true; }
                else { _canMove = false; }
            }

            if (_invincibility)
            {
                _invincibilityCoolDown--;
                if (_invincibilityCoolDown <= 0) { _invincibility = false; }
            }

            if (_ghostMode)
            {
                _ghostModeCoolDown--;
                if (_ghostModeCoolDown == 0)
                {
                    _ghostMode = false;
                    if (_previousBlock == BlockTypeEnum.Wall || _previousBlock == BlockTypeEnum.Box || _previousBlock == BlockTypeEnum.BoxPlacedByPlayer)
                    {
                        Kill();
                        board[_posX, _posY] = _previousBlock;
                    }
                }
            }

            if (_slowDownCoolDown > 0)
            {
                _slowDownCoolDown--;
                if (_slowDownCoolDown <= 0)
                {
                    _speed = 10;
                    _moveCoolDown = 0;
                    _canMove = true;
                }
            }

            if (_rangeDecreasedCoolDown > 0)
            {
                _rangeDecreasedCoolDown--;
                if (_rangeDecreasedCoolDown == 0) { _range = 2; }
            }

            if (_disableBombPlacementCoolDown > 0)
            {
                _disableBombPlacementCoolDown--;
                if (_disableBombPlacementCoolDown == 0) _canPlaceBomb = true;
            }

            if (_instantPlacingBombsCoolDown > 0)
            {
                _instantPlacingBombsCoolDown--;
                if (_instantPlacingBombsCoolDown <= 0)
                {
                    _instantPlacingBombs = false;
                }
            }
        }

        /// <summary>
        /// If the bomb was placed, this function subscribes to the effects on the board caused by the bomb. 
        /// Then sends the information to the model. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExplode(object sender, ExplodeEventArgs e)
        {
            if (e is null) return;
            OnBombExploded(e.BlockType, e.PosX, e.PosY);
        }

        /// <summary>
        /// If the player does not have _invincibility power up, this kills the player (sets the _alive false).
        /// </summary>
        public void Kill()
        {
            if (_invincibility) return;
            _alive = false;
        }

        /// <summary>
        /// Sets players' all properties and fields to their default value. This called every new match.
        /// </summary>
        public void SetPropertiesForNewGame()
        {
            _placedOneBomb = false;
            _placedAnObstacle = false;
            _speed = 10;
            _moveCoolDown = _speed;
            _canMove = true;
            _alive = true;
            _numberOfBombs = 2;
            _bombs = new List<Bomb>();
            _range = 2;
            _baseCoolDownValue = 500;
            _invincibility = false;
            _invincibilityCoolDown = 0;
            _detonator = false;
            _ghostMode = false;
            _ghostModeCoolDown = 0;
            _numberOfObstacles = 0;
            _canPlaceBomb = true;
            _disableBombPlacementCoolDown = 0;
            _instantPlacingBombs = false;
            _instantPlacingBombsCoolDown = 0;
            _previousBlock = BlockTypeEnum.Path;
            OnPlayerProperties();
        }

        /// <summary>
        /// Collects all important data from player. On properties 1 - means True, 0 - means False.
        /// </summary>
        /// <returns>An int array, that contains all data for saving</returns>
        public int[] SaveData()
        {
            int[] returnData = new int[26];

            returnData[0] = _id;
            returnData[1] = _posX;
            returnData[2] = _posY;
            returnData[3] = _points;
            returnData[4] = _speed;
            returnData[5] = _moveCoolDown;
            if (_canMove)
            {
                returnData[6] = 1;
            }
            else
            {
                returnData[6] = 0;
            }
            returnData[7] = _numberOfBombs;
            if (_placedOneBomb)
            {
                returnData[8] = 1;
            }
            else
            {
                returnData[8] = 0;
            }

            returnData[9] = _range;
            returnData[10] = _baseCoolDownValue;
            returnData[11] = (int)_previousBlock;

            if (_invincibility)
            {
                returnData[12] = 1;
            }
            else
            {
                returnData[12] = 0;
            }
            returnData[13] = _invincibilityCoolDown;

            if (_detonator)
            {
                returnData[14] = 1;
            }
            else
            {
                returnData[14] = 0;
            }

            if (_ghostMode)
            {
                returnData[15] = 1;
            }
            else
            {
                returnData[15] = 0;
            }

            returnData[16] = _ghostModeCoolDown;

            returnData[17] = _numberOfObstacles;
            if (_placedAnObstacle)
            {
                returnData[18] = 1;
            }
            else
            {
                returnData[18] = 0;
            }

            returnData[19] = _slowDownCoolDown;
            returnData[20] = _rangeDecreasedCoolDown;

            if (_canPlaceBomb)
            {
                returnData[21] = 1;
            }
            else
            {
                returnData[21] = 0;
            }
            returnData[22] = _disableBombPlacementCoolDown;

            if (_instantPlacingBombs)
            {
                returnData[23] = 1;
            }
            else
            {
                returnData[23] = 0;
            }

            returnData[24] = _instantPlacingBombsCoolDown;
            if (_alive)
            {
                returnData[25] = 1;
            }
            else
            {
                returnData[25] = 0;
            }

            return returnData;
        }

        /// <summary>
        /// Collects the player's bombs all data. Returns an array of arrays. Each inner arrays contain data of the Bomb,
        /// this equal Bomb.SaveData function's return value.
        /// </summary>
        /// <returns>The array of arrays, that contains a player's _bombs data.</returns>
        public int[][] SaveBombs()
        {
            if (_bombs is null) return new int[0][];
            int[][] returnData = new int[_bombs.Count][];

            for (int i = 0; i < _bombs.Count; ++i)
            {
                returnData[i] = _bombs[i].SaveData(_id);
            }

            return returnData;
        }

        /// <summary>
        /// Creates bombs from saved data. Calls the Bombs loading constructor
        /// </summary>
        /// <param name="data">Array that contains all data from the saved bomb</param>
        public void LoadBomb(int[] data)
        {
            Bomb bomb = new Bomb(data[2], data[3], data[4]);
            if (data[1] <= 0)
            {
                bomb.Time = 1;
            }
            else
            {
                bomb.Time = data[1];
            }
            _bombs?.Add(bomb);
            bomb.ExplosionEventHandler += OnExplode!;
        }
        #endregion
    }
}
