using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Containes data for each player sent by the model
    /// - _range                Bomb explosion range
    /// - _points               Current score
    /// - _maxBombs             Maximum amount of placable bombs
    /// - _currentBombs         Currently placed bombs
    /// - _speed                Speed of the player
    /// - _invincibility        Whether the player is invincible to monsters
    /// - _detonator            Whether the player has picked up the power up enabling manual detonation of bombs
    /// - _ghostMode            Wheteher the player is able to pass through walls and boxes
    /// - _numberOfObstacles    Amount of placable boxes
    /// - _instantPlacement     Debuff making the player place all their bombs immediately when they are available to be placed
    /// - _pu3 - _pu11          Controls the visibility of text under player scores
    /// _ cooldowns             Time remaining from power up ability
    /// </summary>
    public class Player : ViewModelBase
    {
        #region Fields
        private int _range;
        private int _points;
        private int _maxBombs;
        private int _currentBombs;
        private int _speed;
        private string _invicibility = "Red";
        private string? _invincibilityCoolDown;
        private string _detonator = "Red";
        private string _ghostMode = "Red";
        private string? _ghostModeCoolDown;
        private int _numberOfObstacles;
        private string? _slowDownCoolDown;
        private string? _rangeDecreaseCoolDown;
        private string? _bombPlacementDisabledCoolDown;
        private string _instantPlacement = "Red";
        private string? _instantPlacementCoolDown;
        private string _pu3 = "Collapsed";
        private string _pu4 = "Collapsed";
        private string _pu5 = "Collapsed";
        private string _pu6 = "Collapsed";
        private string _pu7 = "Collapsed";
        private string _pu8 = "Collapsed";
        private string _pu9 = "Collapsed";
        private string _pu10 = "Collapsed";
        private string _pu11 = "Collapsed";
        #endregion

        #region Properties
        /// <summary>
        /// Property for player id
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Property the above mentioned attributes and some extra ones
        /// </summary>
        public int Range
        {
            get { return _range; }
            set
            {
                _range = value;
                OnPropertyChanged();
            }
        }
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged();
            }
        }

        public int MaxBombs
        {
            get { return _maxBombs;}
            set
            {
                _maxBombs = value;
                OnPropertyChanged();
            }
        }
        public int CurrentBombs
        {
            get { return _currentBombs; }
            set
            {
                _currentBombs = value;
                OnPropertyChanged();
            }
        }

        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged();
            }
        }

        public string Invincibility
        {
            get { return _invicibility; }
            set
            {
                _invicibility = value;
                OnPropertyChanged();
            }
        }

        public string InvincibilityCoolDown
        {
            get { return _invincibilityCoolDown!; }
            set
            {
                _invincibilityCoolDown = value;
                OnPropertyChanged();
            }
        }

        public string Detonator
        {
            get { return _detonator; }
            set
            {
                _detonator = value;
                OnPropertyChanged();
            }
        }

        public string GhostMode
        {
            get { return _ghostMode; }
            set
            {
                _ghostMode = value;
                OnPropertyChanged();
            }
        }

        public string GhostModeCoolDown
        {
            get { return _ghostModeCoolDown!; }
            set
            {
                _ghostModeCoolDown = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfObstacles
        {
            get { return _numberOfObstacles; }
            set
            {
                _numberOfObstacles = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for time remainging for slowed effect
        /// </summary>
        public string SlowDownCoolDown
        {
            get { return _slowDownCoolDown!; }
            set
            {
                _slowDownCoolDown = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for time remainging for range decrease effect
        /// </summary>
        public string RangeDecreaseCoolDown
        {
            get { return _rangeDecreaseCoolDown!; }
            set
            {
                _rangeDecreaseCoolDown = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for time remainging for disabled bomb placement
        /// </summary>
        public string BombPlacementDisabledCoolDown
        {
            get
            {
                return _bombPlacementDisabledCoolDown!;
            }
            set
            {
                _bombPlacementDisabledCoolDown = value;
                OnPropertyChanged();
            }
        }

        public string InstantPlacement
        {
            get { return _instantPlacement; }
            set
            {
                _instantPlacement = value;
                OnPropertyChanged();
            }
        }

        public string InstantPlacementCoolDown
        {
            get { return _instantPlacementCoolDown!; }
            set
            {
                _instantPlacementCoolDown = value;
                OnPropertyChanged();

            }
        }

        public string PU3
        {
            get { return _pu3; }
            set
            {
                _pu3 = value;
                OnPropertyChanged();
            }
        }
        public string PU4
        {
            get { return _pu4; }
            set
            {
                _pu4 = value;
                OnPropertyChanged();
            }
        }
        public string PU5
        {
            get { return _pu5; }
            set
            {
                _pu5 = value;
                OnPropertyChanged();
            }
        }
        public string PU6
        {
            get { return _pu6; }
            set
            {
                _pu6 = value;
                OnPropertyChanged();
            }
        }
        public string PU7
        {
            get { return _pu7; }
            set
            {
                _pu7 = value;
                OnPropertyChanged();
            }
        }
        public string PU8
        {
            get { return _pu8; }
            set
            {
                _pu8 = value;
                OnPropertyChanged();
            }
        }
        public string PU9
        {
            get { return _pu9; }
            set
            {
                _pu9 = value;
                OnPropertyChanged();
            }
        }
        public string PU10
        {
            get { return _pu10; }
            set
            {
                _pu10 = value;
                OnPropertyChanged();
            }
        }
        public string PU11
        {
            get { return _pu11; }
            set
            {
                _pu11 = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
