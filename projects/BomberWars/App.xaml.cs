using System.Configuration;
using System.Data;
using System.Windows;
using BomberWars.View;
using BomberWars.ViewModel;
using BomberWars_MP.Model;
using BomberWars_MP.DataAccess;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Controls;
using BomberWars.Store;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace BomberWars
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// - _mainViewModel                    Main viewmodel controlling which screen and viewmodel is needed
    /// - _menuViewModel                    Viewmodel of the menu screen
    /// - _gameViewModel                    Viewmodel of the game screen
    /// - _helpViewModel                    Viewmodel of the help screen
    /// - _storedGamesBrowserViewModel      Viewmodel of the save screen
    /// - _navigationStore                  Storage for the viewmodels
    /// - _mainWindow                       Main window tied to the main viewmodel
    /// - _model                            Project containg the business logic
    /// - _timer                            Timer controlling the render of the game
    /// - _isSelectAll                      Whether the toggle all button in the main menu is on or off
    /// - _isInGame                         Whether the user is in game or in the menu
    /// - _isPaused                         Whether the game is paused
    /// </summary>
    public partial class App : System.Windows.Application
    {
        #region Fields
        private ViewModel.MainViewModel? _mainViewModel;
        private ViewModel.MenuViewModel? _menuViewModel;
        private ViewModel.GameViewModel? _gameViewModel;
        private ViewModel.HelpViewModel? _helpViewModel;
        private ViewModel.StoredGamesBrowserViewModel? _storedGamesBrowserViewModel;
        private NavigationStore? _navigationStore;
        private MainWindow? _mainWindow;
        private Model? _model;
        private DispatcherTimer? _timer;
        private bool _isSelectAll = false;
        private bool _isInGame = false;

        private bool _isPaused = true;
        #endregion

        #region Constructor
        /// <summary>
        /// On startup the AppStartup function is called
        /// </summary>
        public App()
        {
            Startup += new StartupEventHandler(AppStartup);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Initializes the viewmodels apart from the game viewmodel and subsribes functions to events of the menu viewmodel
        /// Initializes the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppStartup(object sender, StartupEventArgs e)
        {
            _navigationStore = new NavigationStore();
            _mainViewModel = new MainViewModel(_navigationStore);
            _menuViewModel = new MenuViewModel();
            _helpViewModel = new HelpViewModel();
            _navigationStore.CurrentVM = _menuViewModel;
            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainWindow.Show();



            _menuViewModel.New += new EventHandler(ViewModel_NewGame);
            _menuViewModel.Load += ViewModel_LoadGame;
            _menuViewModel.Exit += new EventHandler(ViewModel_Exit);
            _menuViewModel.SelectAll += new EventHandler(ViewModel_SelectAll);
            _menuViewModel.Inc += new EventHandler(ViewModel_IncreaseGameLength);
            _menuViewModel.Dec += new EventHandler(ViewModel_DecreaseGameLength);

            _helpViewModel.Return += ReturnToGame;

            _timer = new DispatcherTimer(DispatcherPriority.DataBind);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            _timer.Tick += new EventHandler(Timer_Tick);
        }

        /// <summary>
        /// Decreases the length of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_DecreaseGameLength(object? sender, EventArgs e)
        {
            if(_menuViewModel!.SelectedGameLength > 1)
            {
                _menuViewModel.SelectedGameLength--;
            }
            else
            {
                _menuViewModel.SelectedGameLength = 1;
            }
        }

        /// <summary>
        /// Increases the length of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_IncreaseGameLength(object? sender, EventArgs e)
        {
            _menuViewModel!.SelectedGameLength++;
        }

        /// <summary>
        /// Funciton calling the function controlling the game in the model every time the timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (!_isPaused) { _model!.OnTimerTick(); }      
        }

        /// <summary>
        /// Starts new match
        /// Initializes game viewmodel, starts timer and subscribes functions to events of the game viewmodel and model 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            _model = new Model(_menuViewModel!.SelectedMap, _menuViewModel.SelectedPlayerCount, _menuViewModel.PowerUps.ToArray(), _menuViewModel.PowerDowns.ToArray(), _menuViewModel.SelectedGameLength);
            _gameViewModel = new GameViewModel(_model, _menuViewModel.SelectedPlayerCount);
            _gameViewModel.Model.NewGame();
            _navigationStore!.CurrentVM = _gameViewModel;
            
            _gameViewModel.GenerateFields();


            _timer!.Start();
            _isPaused = false;
            _isInGame = true;
            _gameViewModel.PauseIconPath = "Images/pause.png";

            _gameViewModel.New += new EventHandler(ViewModel_NewGame);
            _gameViewModel.Save += new EventHandler(ViewModel_SaveGame);
            _gameViewModel.Load += new EventHandler(ViewModel_LoadGame);
            _gameViewModel.Back += new EventHandler(ViewModel_Back);
            _gameViewModel.Pause += new EventHandler(ViewModel_Pause);
            _gameViewModel.Exit += ViewModel_Exit;
            _gameViewModel.Help += new EventHandler(ViewModel_ShowHelp);

            _gameViewModel.Down += new EventHandler<PlayerEventArgs>(ViewModel_Down);
            _gameViewModel.Up += new EventHandler<PlayerEventArgs>(ViewModel_Up);
            _gameViewModel.Left += new EventHandler<PlayerEventArgs>(ViewModel_Left);
            _gameViewModel.Right += new EventHandler<PlayerEventArgs>(ViewModel_Right);
            _gameViewModel.BombPlaced += new EventHandler<PlayerEventArgs>(ViewModel_PlaceBomb);
            _gameViewModel.DetonateBomb += new EventHandler<PlayerEventArgs>(ViewModel_Detonate);
            _gameViewModel.ObstaclePlaced += new EventHandler<PlayerEventArgs>(ViewModel_PlaceObstacle);

            _model.ModelOnMoveEventHandler += new EventHandler<BoardChangeEventArgs>(RefreshBoardMovement);
            _model.ModelOnExplodeEventHandler += new EventHandler<ExplodeEventArgs>(RefreshBoardExplosion);
            _model.ModelOnPlayerPropertiesEventHandler += new EventHandler<PlayerPropertiesEventArgs>(Model_PlayerProperties);
            _model.ModelOnEndRoundEventHandler += new EventHandler<EndRoundEventArgs>(Model_EndRound);
            _model.ModelOnEndGameEventHandler += new EventHandler<EndGameEventArgs>(Model_EndGame);

        }

        
        /// <summary>
        /// Pauses the timer and changes icon of the pause button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_Pause(object? sender, EventArgs e)
        {
            _isPaused = !_isPaused;
            if(_isPaused)
            {
                _gameViewModel!.PauseIconPath = "Images/play.png";
            }
            else
            {
                _gameViewModel!.PauseIconPath = "Images/pause.png";
            }
        }


        /// <summary>
        /// Calls function of the model placing a bomb if not paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_PlaceBomb(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerPlacesBomb(e._playerId); }
        }

        /// <summary>
        /// Calls function of the model detonating a bomb  if not paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_Detonate(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerDetonates(e._playerId); }     
        }

        /// <summary>
        /// Calls function of the model placing a box if not paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_PlaceObstacle(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerPlacesObstackles(e._playerId); }
        }

        /// <summary>
        /// Player is returned to the main menu screen
        /// Warning of loss of progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_Back(object? sender, EventArgs e)
        {
            if (_isInGame)
            {
                if (!_isPaused) { ViewModel_Pause(sender, e); }
                if (System.Windows.MessageBox.Show("Are you sure you want to quit?\n Any unsaved progress will be lost.", "BomberWars", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _isInGame = false;
                    _navigationStore!.CurrentVM = _menuViewModel!;
                }
            }
        }

        /// <summary>
        /// Exits the entire application
        /// Warning of loss of progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_Exit(object? sender, EventArgs e)
        {
            if (_isInGame)
            {
                if (!_isPaused) { ViewModel_Pause(sender, e); }
                if (System.Windows.MessageBox.Show("Are you sure you want to quit?\n Any unsaved progress will be lost.", "BomberWars", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Switches to the screen showing key bindings
        /// Also pauses the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_ShowHelp(object? sender, EventArgs e)
        {
            if (!_isPaused) { ViewModel_Pause(sender, e); }
            _navigationStore!.CurrentVM = _helpViewModel!;
        }

        /// <summary>
        /// Controlls the color and value of the power up selectors in the main menu
        /// Players can switch all power up on or off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_SelectAll(object? sender, EventArgs e)
        {
            _isSelectAll = !_isSelectAll;
            if(_isSelectAll)
            {
                _menuViewModel!.SelectAllColor = "#9ADE7B";
                _menuViewModel.PowerUp1 = true;
                _menuViewModel.PowerUp2 = true;
                _menuViewModel.PowerUp3 = true;
                _menuViewModel.PowerUp4 = true;
                _menuViewModel.PowerUp5 = true;
                _menuViewModel.PowerUp6 = true;
                _menuViewModel.PowerUp7 = true;
                _menuViewModel.PowerUp8 = true;
                _menuViewModel.PowerUp9 = true;
                _menuViewModel.PowerUp10 = true;
                _menuViewModel.PowerUp11 = true;
            }
            else
            {
                _menuViewModel!.SelectAllColor = "#FF8F8F";
                _menuViewModel.PowerUp1 = false;
                _menuViewModel.PowerUp2 = false;
                _menuViewModel.PowerUp3 = false;
                _menuViewModel.PowerUp4 = false;
                _menuViewModel.PowerUp5 = false;
                _menuViewModel.PowerUp6 = false;
                _menuViewModel.PowerUp7 = false;
                _menuViewModel.PowerUp8 = false;
                _menuViewModel.PowerUp9 = false;
                _menuViewModel.PowerUp10 = false;
                _menuViewModel.PowerUp11 = false;
            }
            
        }

        /// <summary>
        /// Initializes save viewmodel
        /// Subscribes return function to return button's event
        /// Player can't load saves
        /// Switches to save screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            if (_isPaused)
            {
                _storedGamesBrowserViewModel = new StoredGamesBrowserViewModel(_model!);
                _storedGamesBrowserViewModel.Return += new EventHandler(ReturnToGame);
                _storedGamesBrowserViewModel.SaveButtonVisibility = "Visible";
                foreach(StoredGameViewModel saves in _storedGamesBrowserViewModel.StoredGames)
                {
                    saves.LoadButtonEnabled = false;
                }
                _navigationStore!.CurrentVM = _storedGamesBrowserViewModel;
            }
        }


        /// <summary>
        /// Switches back to the game viewmodel if the player is not in the game, otherwise returns to main menu
        /// Used on save and game screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToGame(object? sender, EventArgs e)
        {
            if (_isInGame)
            {
            _navigationStore!.CurrentVM = _gameViewModel!;
            }
            else
            {
                _navigationStore!.CurrentVM = _menuViewModel!;
            }
        }


        /// <summary>
        /// Initializes save viewmodel
        /// Subscribes return function to return button's event
        /// Player can't make new saves
        /// Switches to load screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            if (_isPaused)
            {
                _storedGamesBrowserViewModel = new StoredGamesBrowserViewModel(_model!);
                _storedGamesBrowserViewModel.Return += new EventHandler(ReturnToGame);
                _storedGamesBrowserViewModel.GameLoading += new EventHandler<StoredGameEventArgs>(StoreViewModel_LoadGame);
                _storedGamesBrowserViewModel.SaveButtonVisibility = "Collapsed";
                foreach (StoredGameViewModel saves in _storedGamesBrowserViewModel.StoredGames)
                {
                    saves.LoadButtonEnabled = true;
                }
                _navigationStore!.CurrentVM = _storedGamesBrowserViewModel;
            }
        }

        /// <summary>
        /// Loads selected save
        /// Same as the new game function, but uses different constructor of the model, which only needs the file path of the save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoreViewModel_LoadGame(object? sender, StoredGameEventArgs e)
        {
            _model = new Model(e.FullPath);
            _gameViewModel = new GameViewModel(_model, _model.PlayerCount);
            _gameViewModel.Model.NewGame();
            _navigationStore!.CurrentVM = _gameViewModel;

            _gameViewModel.GenerateFields();


            _timer!.Start();
            _isPaused = false;
            _isInGame = true;
            _gameViewModel.PauseIconPath = "Images/pause.png";

            _gameViewModel.New += new EventHandler(ViewModel_NewGame);
            _gameViewModel.Save += new EventHandler(ViewModel_SaveGame);
            _gameViewModel.Load += new EventHandler(ViewModel_LoadGame);
            _gameViewModel.Back += new EventHandler(ViewModel_Back);
            _gameViewModel.Pause += new EventHandler(ViewModel_Pause);
            _gameViewModel.Exit += ViewModel_Exit;

            _gameViewModel.Down += new EventHandler<PlayerEventArgs>(ViewModel_Down);
            _gameViewModel.Up += new EventHandler<PlayerEventArgs>(ViewModel_Up);
            _gameViewModel.Left += new EventHandler<PlayerEventArgs>(ViewModel_Left);
            _gameViewModel.Right += new EventHandler<PlayerEventArgs>(ViewModel_Right);
            _gameViewModel.BombPlaced += new EventHandler<PlayerEventArgs>(ViewModel_PlaceBomb);
            _gameViewModel.DetonateBomb += new EventHandler<PlayerEventArgs>(ViewModel_Detonate);

            _model.ModelOnMoveEventHandler += new EventHandler<BoardChangeEventArgs>(RefreshBoardMovement);
            _model.ModelOnExplodeEventHandler += new EventHandler<ExplodeEventArgs>(RefreshBoardExplosion);
            _model.ModelOnPlayerPropertiesEventHandler += new EventHandler<PlayerPropertiesEventArgs>(Model_PlayerProperties);
            _model.ModelOnEndRoundEventHandler += new EventHandler<EndRoundEventArgs>(Model_EndRound);
            _model.ModelOnEndGameEventHandler += new EventHandler<EndGameEventArgs>(Model_EndGame);
        }

        /// <summary>
        /// Refreshes the squares representing the play area every time a player or monster moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBoardMovement(object? sender, BoardChangeEventArgs e)
        {
            Field prevField = _gameViewModel!.Fields!.Where(x => x.X == e._prevPosX && x.Y == e._prevPosY).First();
            Field currentField = _gameViewModel!.Fields!.Where(x => x.X == e._currentPosX && x.Y == e._currentPosY).First();
            switch (e._currenttype)
            {
                case BlockTypeEnum.Path:
                    currentField.Type = "Path";break;
                case BlockTypeEnum.PlayerA:
                    currentField.Type = "P1";break;
                case BlockTypeEnum.PlayerB:
                    currentField.Type = "P2"; break;
                case BlockTypeEnum.PlayerC:
                    currentField.Type = "P3"; break;
                case BlockTypeEnum.Bomb:
                    currentField.Type = "Bomb";break;
                case BlockTypeEnum.Box:
                    currentField.Type = "Box"; break;
                case BlockTypeEnum.Wall:
                    currentField.Type = "Wall"; break;
                case BlockTypeEnum.Monster:
                    currentField.Type = "Monster";break;
                case BlockTypeEnum.GhostMonsterOnPath:
                    currentField.Type = "GhostPath"; break;
                case BlockTypeEnum.GhostMonsterOnBox:
                    currentField.Type = "GhostBox"; break;
                case BlockTypeEnum.GhostMonsterOnWall:
                    currentField.Type = "GhostWall"; break;
                case BlockTypeEnum.DijkstraMonster:
                    currentField.Type = "DijkstraMonster"; break;
                case BlockTypeEnum.HeuristicMonster:
                    currentField.Type = "HeuristicMonster"; break;
                case BlockTypeEnum.GhostPlayerAOnBox:
                    currentField.Type = "P1Box"; break;
                case BlockTypeEnum.GhostPlayerBOnBox:
                    currentField.Type = "P2Box"; break;
                case BlockTypeEnum.GhostPlayerCOnBox:
                    currentField.Type = "P3Box"; break;
                case BlockTypeEnum.GhostPlayerAOnWall:
                    currentField.Type = "P1Wall"; break;
                case BlockTypeEnum.GhostPlayerBOnWall:
                    currentField.Type = "P2Wall"; break;
                case BlockTypeEnum.GhostPlayerCOnWall:
                    currentField.Type = "P3Wall"; break;
                case BlockTypeEnum.BoxPlacedByPlayer:
                    currentField.Type = "Box";break;

            }
            switch(e._prevtype)
            {
                case BlockTypeEnum.Path:
                    prevField.Type = "Path"; break;
                case BlockTypeEnum.PlayerA:
                    prevField.Type = "P1"; break;
                case BlockTypeEnum.PlayerB:
                    prevField.Type = "P2"; break;
                case BlockTypeEnum.PlayerC:
                    prevField.Type = "P3"; break;
                case BlockTypeEnum.Bomb:
                    prevField.Type = "Bomb"; break;
                case BlockTypeEnum.Box:
                    prevField.Type = "Box"; break;
                case BlockTypeEnum.Wall:
                    prevField.Type = "Wall"; break;
                case BlockTypeEnum.Monster:
                    prevField.Type = "Monster"; break;
                case BlockTypeEnum.GhostMonsterOnPath:
                    prevField.Type = "GhostPath"; break;
                case BlockTypeEnum.GhostMonsterOnBox:
                    prevField.Type = "GhostBox"; break;
                case BlockTypeEnum.GhostMonsterOnWall:
                    prevField.Type = "GhostWall"; break;
                case BlockTypeEnum.DijkstraMonster:
                    prevField.Type = "DijkstraMonster"; break;
                case BlockTypeEnum.HeuristicMonster:
                    prevField.Type = "HeuristicMonster"; break;
                case BlockTypeEnum.GhostPlayerAOnBox:
                    prevField.Type = "P1Box"; break;
                case BlockTypeEnum.GhostPlayerBOnBox:
                    prevField.Type = "P2Box"; break;
                case BlockTypeEnum.GhostPlayerCOnBox:
                    prevField.Type = "P3Box"; break;
                case BlockTypeEnum.GhostPlayerAOnWall:
                    prevField.Type = "P1Wall"; break;
                case BlockTypeEnum.GhostPlayerBOnWall:
                    prevField.Type = "P2Wall"; break;
                case BlockTypeEnum.GhostPlayerCOnWall:
                    prevField.Type = "P3Wall"; break;
                case BlockTypeEnum.BoxPlacedByPlayer:
                    prevField.Type = "Box"; break;
            }
        }

        /// <summary>
        /// Refreshes the squares representing the play area every time a bomb explodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBoardExplosion(object? sender, ExplodeEventArgs e)
        {
            Field field = _gameViewModel!.Fields!.Where(x => x.X == e.PosX && x.Y == e.PosY).First();
            switch (e.BlockType)
            {
                case BlockTypeEnum.Path:
                    field.Type = "Path"; break;
                case BlockTypeEnum.Explosion:
                    field.Type = "Explosion"; break;
                case BlockTypeEnum.PowerUp:
                    field.Type = "PowerUp";break;
                case BlockTypeEnum.PowerDown:
                    field.Type = "PowerDown"; break;
            }
        }

        /// <summary>
        /// Sets all the information under player scores based on the power ups selected in the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Model_PlayerProperties(object? sender, PlayerPropertiesEventArgs e)
        {
            TimeSpan t;
            ViewModel.Player player = _gameViewModel!.Players.Where(x => x.PlayerId == e._playerId).First();
            player.Range = e._range;
            player.MaxBombs = e._maxNumberOfBombs;
            player.CurrentBombs = e._currentNumberBombs;
            if (_menuViewModel!.PowerUp3 || _model!.PowerUps!.Contains(PowerUpEnum.DetonatorPowerUp))
            {
                player.Detonator = e._detonator ? "LightGreen" : "Red";
                player.PU3 = "Visible";
            }
            if (_menuViewModel.PowerUp4 || _model!.PowerUps!.Contains(PowerUpEnum.RollerSkatePowerUp))
            {
                player.Speed = e._speed == 10 ? 100 : 200;
                player.PU4 = "Visible";
            }
            if (_menuViewModel.PowerUp5 || _model!.PowerUps!.Contains(PowerUpEnum.InvincibilityPowerUp))
            {
                player.Invincibility = e._invincibility ? "LightGreen" : "Red";
                player.PU5 = "Visible";
                t = TimeSpan.FromSeconds(e._invincibilityCoolDown);
                player.InvincibilityCoolDown = t.ToString();
            }
            if(_menuViewModel.PowerUp6 || _model!.PowerUps!.Contains(PowerUpEnum.GhostPowerUp))
            {
                player.GhostMode = e._ghostMode ? "LightGreen" : "Red";
                t = TimeSpan.FromSeconds(e._ghostModeCoolDown);
                player.GhostModeCoolDown = t.ToString();
                player.PU6 = "Visible";
            }
            if (_menuViewModel.PowerUp7 || _model!.PowerUps!.Contains(PowerUpEnum.ObstaclePowerUp))
            {
                player.NumberOfObstacles = e._numberOfObstacles;
                player.PU7 = "Visible";
            }
            if(_menuViewModel.PowerUp8 || _model!.PowerDowns!.Contains(PowerDownEnum.SlowDownPowerDown))
            {
                t = TimeSpan.FromSeconds(e._slowDownCoolDown);
                player.SlowDownCoolDown = t.ToString();
                player.PU8 = "Visible";
            }
            if (_menuViewModel.PowerUp9 || _model!.PowerDowns!.Contains(PowerDownEnum.BombRangeDecreasePowerDown))
            {
                t = TimeSpan.FromSeconds(e._rangeDecreasedCoolDown);
                player.RangeDecreaseCoolDown = t.ToString();
                player.PU9 = "Visible";
            }
            if (_menuViewModel.PowerUp10 || _model!.PowerDowns!.Contains(PowerDownEnum.DisableBombPlacementPowerDown))
            {
                t = TimeSpan.FromSeconds(e._disableBombPlacementCoolDown);
                player.BombPlacementDisabledCoolDown = t.ToString();
                player.PU10 = "Visible";
            }
            if (_menuViewModel.PowerUp11 || _model!.PowerDowns!.Contains(PowerDownEnum.InstantBombPlacementPowerDown))
            {
                player.InstantPlacement = e._instantPlacingBombs ? "LightGreen" : "Red";
                t = TimeSpan.FromSeconds(e._instantPlacingBombsCoolDown);
                player.InstantPlacementCoolDown = t.ToString();
                player.PU11 = "Visible";
            }
        }

        /// <summary>
        /// The following four functions call the model's movement funcitons if the game is not paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_Up(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerMoves(e._playerId, DirectionEnum.Up); }
            
        }
        private void ViewModel_Down(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerMoves(e._playerId, DirectionEnum.Down); }               
        }
        private void ViewModel_Left(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerMoves(e._playerId, DirectionEnum.Left); }
            
        }
        private void ViewModel_Right(object? sender, PlayerEventArgs e)
        {
            if (!_isPaused) { _model!.PlayerMoves(e._playerId, DirectionEnum.Right); }
        }

        /// <summary>
        /// Called when a round is over
        /// Sets the scores of the players
        /// Resets the displayed information
        /// Warninga about starting a new game, which disappears after 5 seconds
        /// Then starts a new round and regenerates play area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Model_EndRound(object? sender, EndRoundEventArgs e)
        {
            ViewModel.Player player;
            for(int i = 0; i < _gameViewModel!.PlayerNumber; i++)
            {
                player = _gameViewModel.Players.Where(x => x.PlayerId ==  i).First();
                switch (i)
                {
                    case 0:
                        player.Points = e._firstPlayerPoints; break;
                    case 1:
                        player.Points = e._secondPlayerPoints; break;
                    case 2:
                        player.Points = e._thirdPlayerPoints; break;
                }
            }
            AutoClosingMessageBox.Show(text: "Starting new round!",
                                       caption:  "Round over!",
                                       timeout: 5000,
                                       buttons: MessageBoxButtons.OK,
                                       showCountDown: true);
            _model!.NewGame();
            _gameViewModel.GenerateFields();

        }


        /// <summary>
        /// Called when a player reaches the required score to end tha gama
        /// Scores are displayed in a popup window with the ability to start a new game with the same settings or return to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Model_EndGame(object? sender, EndGameEventArgs e)
        {
            if(_gameViewModel!.PlayerNumber == 2)
            {
                if(System.Windows.MessageBox.Show("Game Over" + "\n" + "Score: " + e._firstPlayerPoints + " - " + e._secondPlayerPoints + "\n" + "Do you want to start a new game with the same settings?", "BomberWars", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    ViewModel_NewGame(this,EventArgs.Empty);
                }
                else
                {
                    _navigationStore!.CurrentVM = _menuViewModel!;
                    _isInGame = false;
                }
            }
            else
            {
                if (System.Windows.MessageBox.Show("Game Over" + "\n" + "Score: " + e._firstPlayerPoints + " - " + e._secondPlayerPoints + " - " + e._thirdPlayerPoints + "\n" + "Do you want to start a new game with the same settings?", "BomberWars", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    ViewModel_NewGame(this, EventArgs.Empty);
                }
                else
                {
                    _navigationStore!.CurrentVM = _menuViewModel!;
                    _isInGame = false;
                }
            }
        }
        #endregion
    }

}
