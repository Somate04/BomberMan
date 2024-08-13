using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberWars.Store;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Viewmodel of the main menu screen
    /// - _selectedMap          Selected map, default first map
    /// - _selectAllColor       Color of the toggle all button switching between red and green, default red
    /// - _selectedPlayerCount  Player count, default 2
    /// - _selectedGameLength   Amount of wins required for victory, default 1
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        #region Fields
        private int _selectedMap = 0;
        private string _selectAllColor = "#FF8F8F";
        private int _selectedPlayerCount = 2;
        private int _selectedGameLength = 1;
        #endregion

        #region Properties
        /// <summary>
        /// Properties for the above menitoned attributes
        /// </summary>
        public int SelectedMap
        {
            get { return _selectedMap; }
            set
            {
                _selectedMap = value;
                OnPropertyChanged(nameof(SelectedMap));
            }
        }

        public int SelectedPlayerCount
        {
            get { return _selectedPlayerCount; }
            set
            {
                _selectedPlayerCount = value;
                OnPropertyChanged(nameof(SelectedPlayerCount));
            }
        }
        public int SelectedGameLength
        {
            get { return _selectedGameLength; }
            set
            {
                _selectedGameLength = value;
                OnPropertyChanged(nameof(SelectedGameLength));
            }
        }
        public string SelectAllColor
        {
            get { return _selectAllColor; }
            set
            {
                _selectAllColor = value;
                OnPropertyChanged(nameof(SelectAllColor));
            }
        }

        /// <summary>
        /// Properties for each power up selectable in the main menu containing a boolean value setting whether they are selected or not.
        /// </summary>
        #region PowerUps
        private List<PowerUpEnum> _powerUps = new List<PowerUpEnum>();
        public List<PowerUpEnum> PowerUps { get => _powerUps; }

        private List<PowerDownEnum> _powerDowns = new List<PowerDownEnum>();
        public List<PowerDownEnum> PowerDowns { get => _powerDowns; }

        private bool _powerUp1;
        public bool PowerUp1
        {
            get { return _powerUp1; }
            set
            {
                if (value)
                {
                    _powerUp1 = true;
                    PowerUps.Add(PowerUpEnum.BombNumberIncreasePowerUp);
                }
                else
                {
                    _powerUp1 = false;
                    if (PowerUps.Contains(PowerUpEnum.BombNumberIncreasePowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.BombNumberIncreasePowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp1));
            }
        }
        private bool _powerUp2;
        public bool PowerUp2
        {
            get { return _powerUp2; }
            set
            {
                if (value)
                {
                    _powerUp2 = true;
                    PowerUps.Add(PowerUpEnum.BombRangeIncreasePowerUp);
                }
                else
                {
                    _powerUp2 = false;
                    if (PowerUps.Contains(PowerUpEnum.BombRangeIncreasePowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.BombRangeIncreasePowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp2));
            }
        }
        private bool _powerUp3;
        public bool PowerUp3
        {
            get { return _powerUp3; }
            set
            {
                if (value)
                {
                    _powerUp3 = true;
                    PowerUps.Add(PowerUpEnum.DetonatorPowerUp);
                }
                else
                {
                    _powerUp3 = false;
                    if (PowerUps.Contains(PowerUpEnum.DetonatorPowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.DetonatorPowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp3));
            }
        }
        private bool _powerUp4;
        public bool PowerUp4
        {
            get { return _powerUp4; }
            set
            {
                if (value)
                {
                    _powerUp4 = true;
                    PowerUps.Add(PowerUpEnum.RollerSkatePowerUp);
                }
                else
                {
                    _powerUp4 = false;
                    if (PowerUps.Contains(PowerUpEnum.RollerSkatePowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.RollerSkatePowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp4));
            }
        }
        private bool _powerUp5;
        public bool PowerUp5
        {
            get { return _powerUp5; }
            set
            {
                if (value)
                {
                    _powerUp5 = true;
                    PowerUps.Add(PowerUpEnum.InvincibilityPowerUp);
                }
                else
                {
                    _powerUp5 = false;
                    if (PowerUps.Contains(PowerUpEnum.InvincibilityPowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.InvincibilityPowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp5));
            }
        }
        private bool _powerUp6;
        public bool PowerUp6
        {
            get { return _powerUp6; }
            set
            {
                if (value)
                {
                    _powerUp6 = true;
                    PowerUps.Add(PowerUpEnum.GhostPowerUp);
                }
                else
                {
                    _powerUp6 = false;
                    if (PowerUps.Contains(PowerUpEnum.GhostPowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.GhostPowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp6));
            }
        }
        private bool _powerUp7;
        public bool PowerUp7
        {
            get { return _powerUp7; }
            set
            {
                if (value)
                {
                    _powerUp7 = true;
                    PowerUps.Add(PowerUpEnum.ObstaclePowerUp);
                }
                else
                {
                    _powerUp7 = false;
                    if (PowerUps.Contains(PowerUpEnum.ObstaclePowerUp))
                    {
                        PowerUps.Remove(PowerUpEnum.ObstaclePowerUp);
                    }
                }
                OnPropertyChanged(nameof(PowerUp7));
            }
        }
        private bool _powerUp8;
        public bool PowerUp8
        {
            get { return _powerUp8; }
            set
            {
                if (value)
                {
                    _powerUp8 = true;
                    PowerDowns.Add(PowerDownEnum.SlowDownPowerDown);
                }
                else
                {
                    _powerUp8 = false;
                    if (PowerDowns.Contains(PowerDownEnum.SlowDownPowerDown))
                    {
                        PowerDowns.Remove(PowerDownEnum.SlowDownPowerDown);
                    }
                }
                OnPropertyChanged(nameof(PowerUp8));
            }
        }
        private bool _powerUp9;
        public bool PowerUp9
        {
            get { return _powerUp9; }
            set
            {
                if (value)
                {
                    _powerUp9 = true;
                    PowerDowns.Add(PowerDownEnum.BombRangeDecreasePowerDown);
                }
                else
                {
                    _powerUp9 = false;
                    if (PowerDowns.Contains(PowerDownEnum.BombRangeDecreasePowerDown))
                    {
                        PowerDowns.Remove(PowerDownEnum.BombRangeDecreasePowerDown);
                    }
                }
                OnPropertyChanged(nameof(PowerUp9));
            }
        }
        private bool _powerUp10;
        public bool PowerUp10
        {
            get { return _powerUp10; }
            set
            {
                if (value)
                {
                    _powerUp10 = true;
                    PowerDowns.Add(PowerDownEnum.DisableBombPlacementPowerDown);
                }
                else
                {
                    _powerUp10 = false;
                    if (PowerDowns.Contains(PowerDownEnum.DisableBombPlacementPowerDown))
                    {
                        PowerDowns.Remove(PowerDownEnum.DisableBombPlacementPowerDown);
                    }
                }
                OnPropertyChanged(nameof(PowerUp10));
            }
        }
        private bool _powerUp11;
        public bool PowerUp11
        {
            get { return _powerUp11; }
            set
            {
                if (value)
                {
                    _powerUp11 = true;
                    PowerDowns.Add(PowerDownEnum.InstantBombPlacementPowerDown);
                }
                else
                {
                    _powerUp11 = false;
                    if (PowerDowns.Contains(PowerDownEnum.InstantBombPlacementPowerDown))
                    {
                        PowerDowns.Remove(PowerDownEnum.InstantBombPlacementPowerDown);
                    }
                }
                OnPropertyChanged(nameof(PowerUp11));
            }
        }

        #endregion

        /// <summary>
        /// Properties for command calling event after user pressing given button.
        /// </summary>
        public DelegateCommand? NewGameCommand { get; set; }
        public DelegateCommand? LoadCommand { get; set; }
        public DelegateCommand? ExitCommand { get; set; }
        public DelegateCommand? ToggleAll { get; set; }
        public DelegateCommand? Increase { get; set; }
        public DelegateCommand? Decrease { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Events meant to get called by the commands
        /// </summary>
        public event EventHandler? New;
        public event EventHandler? Load;
        public event EventHandler? Exit;
        public event EventHandler? SelectAll;
        public event EventHandler? Inc;
        public event EventHandler? Dec;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor setting action for each command
        /// </summary>
        public MenuViewModel()
        {
            NewGameCommand = new DelegateCommand(x => NewGame());
            LoadCommand = new DelegateCommand(x => LoadGame());
            ExitCommand = new DelegateCommand(x => ExitGame());
            ToggleAll = new DelegateCommand(x => ToggleAllPowerups());
            Increase = new DelegateCommand(x => IncreaseLength());
            Decrease = new DelegateCommand(x => DecreaseLength());
        }
        #endregion

        #region Funcstions
        /// <summary>
        /// The following functions all call their corresponding event after a command is executed.
        /// </summary>
        private void LoadGame()
        {
            Load?.Invoke(this, EventArgs.Empty);
        }

        private void DecreaseLength()
        {
            Dec?.Invoke(this, EventArgs.Empty);
        }

        private void IncreaseLength()
        {
            Inc?.Invoke(this, EventArgs.Empty);
        }

        private void NewGame()
        {
            New?.Invoke(this, EventArgs.Empty);
        }
        private void ExitGame()
        {
            Exit?.Invoke(this, EventArgs.Empty);
        }
        private void ToggleAllPowerups()
        {
            SelectAll?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
