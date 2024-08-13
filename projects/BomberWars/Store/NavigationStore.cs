using BomberWars.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.Store
{
    /// <summary>
    /// Stores the current viewmodel used to change screens.
    /// - CurretnViewModelChanged      event for changing viewmodels
    /// - _currentVM                   current viewmodel
    /// </summary>
    public class NavigationStore
    {
        #region Fields
        public event Action? CurrentViewModelChanged;
        private ViewModelBase? _currentVM;
        #endregion

        #region Properties
        /// <summary>
        /// Property for the current viewmodel
        /// </summary>
        public ViewModelBase CurrentVM
        {
            get { return _currentVM!; }
            set
            {
                _currentVM = value;
                OnCurrentVMChanged();
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Calls the event for changing the current viewmodel
        /// </summary>
        private void OnCurrentVMChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        #endregion
    }
}
