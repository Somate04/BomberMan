using BomberWars.View;
using BomberWars_MP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Viewmodel of the game screen
    /// - _pauseIconPath        Path of the pause icon which changes between pause and play icons.
    /// - _fields               Collection of the squares making up the play area.
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        #region Fields
        private string? _pauseIconPath;
        private ObservableCollection<Field>? _fields;
        #endregion

        #region Properties
        /// <summary>
        /// Property for model.
        /// </summary>
        public Model Model { get; set; }

        /// <summary>
        /// Property for field collection which represents the play area
        /// </summary>
        public ObservableCollection<Field>? Fields
        {
            get { return _fields; }
            set
            {
                _fields = value!;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for players collection representing the player info under the play area.
        /// </summary>
        public ObservableCollection<Player> Players { get; set; }


        /// <summary>
        /// Properties for size of the board
        /// </summary>
        public int N 
        { 
            get { return Model.BoardWidth; }
        }
        public int M
        {
            get { return Model.BoardHeight; }
        }

        /// <summary>
        /// Property for the number of players selected
        /// </summary>
        public int PlayerNumber { get; set; }


        /// <summary>
        /// Properties for commands which invoke events after the user clicks a button
        /// </summary>
        public DelegateCommand? NewGameCommand { get; set; }
        public DelegateCommand? BackCommand { get; set; }
        public DelegateCommand? LoadGameCommand { get; set; }
        public DelegateCommand? SaveGameCommand { get; set; }
        public DelegateCommand? ExitCommand { get; set; }
        public DelegateCommand? PauseCommand { get; set; }
        public DelegateCommand? HelpCommand { get; set; }

        public DelegateCommand UpCommand {  get; set; }
        public DelegateCommand DownCommand { get; set; }
        public DelegateCommand LeftCommand { get; set; }
        public DelegateCommand RightCommand { get; set; }
        public DelegateCommand BombCommand { get; set; }
        public DelegateCommand DetonateCommand { get; set; }
        public DelegateCommand PlaceObstacle { get; set; }

        /// <summary>
        /// Property for pause icon path
        /// </summary>
        public string PauseIconPath
        {
            get { return _pauseIconPath!; }
            set
            {
                _pauseIconPath = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Events for the commands
        /// </summary>
        public event EventHandler? New;
        public event EventHandler? Load;
        public event EventHandler? Save;
        public event EventHandler? Exit;
        public event EventHandler? Pause;
        public event EventHandler? Back;
        public event EventHandler? Help;
        public event EventHandler<PlayerEventArgs>? Up;
        public event EventHandler<PlayerEventArgs>? Left;
        public event EventHandler<PlayerEventArgs>? Down;
        public event EventHandler<PlayerEventArgs>? Right;
        public event EventHandler<PlayerEventArgs>? BombPlaced;
        public event EventHandler<PlayerEventArgs>? DetonateBomb;
        public event EventHandler<PlayerEventArgs>? ObstaclePlaced;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Model containing logic</param>
        /// <param name="playerNumber">Number of players seleceted</param>
        public GameViewModel(Model model, int playerNumber)
        {
            this.Model = model;
            PlayerNumber = playerNumber;
            Players = new ObservableCollection<Player>();
            for(int i = 0; i < PlayerNumber; i++)
            {
                Players.Add(new Player
                {
                    PlayerId = i,
                    Range = 0,
                    MaxBombs = 2
                });
            }



            NewGameCommand = new DelegateCommand(x => NewGame());
            SaveGameCommand = new DelegateCommand(x => SaveGame());
            LoadGameCommand = new DelegateCommand(x => LoadGame());
            BackCommand = new DelegateCommand(x => BackToMenu());
            PauseCommand = new DelegateCommand(x => PauseGame());
            ExitCommand = new DelegateCommand(x => ExitGame());
            HelpCommand = new DelegateCommand(x => ShowHelp());
            UpCommand = new DelegateCommand(x => PlayerUp(x!));
            LeftCommand = new DelegateCommand(x => PlayerLeft(x!));
            RightCommand = new DelegateCommand(x => PlayerRight(x!));
            DownCommand = new DelegateCommand(x => PlayerDown(x!));
            BombCommand = new DelegateCommand(x => PlaceBomb(x!));
            DetonateCommand = new DelegateCommand(x => Detonate(x!));
            PlaceObstacle = new DelegateCommand(x => PlaceBox(x!));
        }
        #endregion

        #region Functions
        /// <summary>
        /// Loads the play area based on the model's enum array
        /// Used when a new game is started.
        /// </summary>
        public void GenerateFields()
        {
            Fields = new ObservableCollection<Field>();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    string t = "";
                    switch (Model.Board![i, j])
                    {
                        case BomberWars_MP.DataAccess.BlockTypeEnum.PlayerA:
                            t = "P1"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.PlayerB:
                            t = "P2"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.PlayerC:
                            t = "P3"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Wall:
                            t = "Wall"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Box:
                            t = "Box"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Path:
                            t = "Path"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Monster:
                            t = "Monster"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Bomb:
                            t = "Bomb"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.Explosion:
                            t = "Explosion"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.PowerUp:
                            t = "PowerUp"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.BoxPlacedByPlayer:
                            t = "Box"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostMonsterOnWall:
                            t = "GhostWall"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostMonsterOnBox:
                            t = "GhostBox"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostMonsterOnPath:
                            t = "GhostPath"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.DijkstraMonster:
                            t = "DijkstraMonster"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.HeuristicMonster:
                            t = "HeuristicMonster"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerAOnBox:
                            t = "P1Box"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerAOnWall:
                            t = "P1Wall"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerBOnBox:
                            t = "P2Box"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerBOnWall:
                            t = "P2Wall"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerCOnBox:
                            t = "P3Box"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.GhostPlayerCOnWall:
                            t = "P3Wall"; break;
                        case BomberWars_MP.DataAccess.BlockTypeEnum.PowerDown:
                            t = "PowerDown"; break;
                    }
                    Fields.Add(new Field
                    {
                        X = i,
                        Y = j,
                        Type = t
                    });
                }
            }
        }

        /// <summary>
        /// The following functions call their corresponding events after a command is executed.
        /// </summary>
        private void NewGame()
        {
            New?.Invoke(this, EventArgs.Empty);
        }
        private void SaveGame()
        {
            Save?.Invoke(this, EventArgs.Empty);
        }
        private void LoadGame()
        {
            Load?.Invoke(this, EventArgs.Empty);
        }
        private void ShowHelp()
        {
            Help?.Invoke(this, EventArgs.Empty);
        }
        private void PauseGame()
        {
            Pause?.Invoke(this, EventArgs.Empty);
        }

        #region MoveCommands
        private void BackToMenu()
        {
            Back?.Invoke(this, EventArgs.Empty);
        }

        private void ExitGame()
        {
            Exit?.Invoke(this, EventArgs.Empty);
        }
        private void PlayerDown(object parameter)
        {
            Down?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }

        private void PlayerRight(object parameter)
        {
            Right?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }

        private void PlayerLeft(object parameter)
        {
            Left?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }

        private void PlayerUp(object parameter)
        {
            Up?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }
        #endregion

        private void PlaceBomb(object parameter)
        {
            BombPlaced?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }
        private void Detonate(object parameter)
        {
            DetonateBomb?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }

        private void PlaceBox(object parameter)
        {
            ObstaclePlaced?.Invoke(this, new PlayerEventArgs(Convert.ToInt32(parameter)));
        }
        #endregion
    }
}
