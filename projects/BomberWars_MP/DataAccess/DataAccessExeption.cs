using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.DataAccess
{
    /// <summary>
    /// DataAccess object can throw DataAccessException, when IOException, UnicodeException or JSONException occurs.
    /// </summary>
    public class DataAccessExeption : Exception
    {
        /// <summary>
        /// Default Ctor of the exception.
        /// </summary>
        public DataAccessExeption() { }

        /// <summary>
        /// Ctro of exception, that has an error message.
        /// </summary>
        /// <param name="message">Error message</param>
        public DataAccessExeption(string message) { }
    }
}
