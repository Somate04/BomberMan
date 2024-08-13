using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars.ViewModel
{
    /// <summary>
    /// Contains the name and the file path of the current save sent by the model.
    /// </summary>
    public class StoredGameEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Name        name of the save
        /// FullPath    full file path of the save file
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        #endregion
    }
}
