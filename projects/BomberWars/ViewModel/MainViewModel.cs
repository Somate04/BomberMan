using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberWars.Store;
using BomberWars.ViewModel;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Responsible for deciding which screen to display
    /// - _ns        Stores the desired screen's viewmodel
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly NavigationStore _ns;
        #endregion

        #region Properties
        /// <summary>
        /// Property for stored viewmodel
        /// </summary>
        public ViewModelBase CurrentViewModel => _ns.CurrentVM;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor which initializes _ns and subscribes an event enabling switching between screens.
        /// </summary>
        /// <param name="ns"></param>
        public MainViewModel(NavigationStore ns)
        {
            _ns = ns;
            _ns.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        #endregion

        #region Functions
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        #endregion
    }
}
