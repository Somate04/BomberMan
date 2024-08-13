using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BomberWars_MP.Model;
using BomberWars_MP.DataAccess;
using System.Xml.Linq;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Viewmodel of the save browser screen listing all previous saves and gives the ability to make a new one
    /// - _model                        Model containing logic of the game
    /// - _saves                        List of previous saves
    /// - _saveButtonVisibility         Whether the save button is visible or not. Only visible when the use wants to save.
    /// </summary>
    public class StoredGamesBrowserViewModel : ViewModelBase
    {
        #region Fields
        private Model _model;
        private (List<string>, List<string>) _saves;
        private string? _saveButtonVisibility;
        #endregion

        #region Events
        public event EventHandler<StoredGameEventArgs>? GameLoading;
        public event EventHandler? Return;
        #endregion

        #region Properties
        /// <summary>
        /// Commands for making a new save and returning to the game
        /// </summary>
        public DelegateCommand NewSaveCommand { get; private set; }
        public DelegateCommand ReturnCommand { get; private set; }
        /// <summary>
        /// Collection of previous saves
        /// </summary>
        public ObservableCollection<StoredGameViewModel> StoredGames { get; private set; }
        /// <summary>
        /// Property for _saveButtonVisibility
        /// </summary>
        public string SaveButtonVisibility
        {
            get
            {
                return _saveButtonVisibility!;
            }
            set
            {
                _saveButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor initializing the model, list of previous saves, visibility of the save button, new save and return commands
        /// stored saves collection and updates the stored saves.
        /// </summary>
        /// <param name="model"></param>
        public StoredGamesBrowserViewModel(Model model)
        {
            _model = model;
            _saves = DataAccess.ListSaves();
            SaveButtonVisibility = string.Empty;
            NewSaveCommand = new DelegateCommand(param =>
            {
                _model.SaveModel();
                UpdateStoredGames();
            });
            ReturnCommand = new DelegateCommand(x => ReturnToGame());
            StoredGames = new ObservableCollection<StoredGameViewModel>();
            UpdateStoredGames();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Calls event after return to game command is executed
        /// </summary>
        private void ReturnToGame()
        {
            Return?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the list of previous saves from the model
        /// </summary>
        private void UpdateStoredGames()
        {
            StoredGames.Clear();
            _saves = DataAccess.ListSaves();
            foreach (string save in _saves.Item2)
            {
                StoredGames.Add(new StoredGameViewModel
                {
                    Name = save,
                    LoadGameCommand = new DelegateCommand(param => OnGameLoading(param?.ToString() ?? "")),
                });
            }
        }

        /// <summary>
        /// Finds the name and index of the save file selected for loading, then calls event
        /// </summary>
        /// <param name="name"></param>
        private void OnGameLoading(string name)
        {
            string filename = _saves.Item2.Where(x => x.EndsWith(name)).FirstOrDefault()!;
            int ind = _saves.Item2.IndexOf(filename!);
            GameLoading?.Invoke(this, new StoredGameEventArgs { FullPath = _saves.Item1[ind] }) ;
        }
        #endregion
    }
}
