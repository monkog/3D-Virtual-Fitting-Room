using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectFittingRoom.View.Canvases.Events
{
    public sealed class CanvasesManager
    {
        #region Private Fields
        /// <summary>
        /// Only instance of CanvasesManager
        /// </summary>
        private static CanvasesManager _instance;
        #endregion Private Fields
        #region Public Properties
        #endregion
        #region .ctor
        //private CanvasesManager()
        //{
        //}
        #endregion
        /// <summary>
        /// Method with access to only instance of CanvasesManager
        /// </summary>
        public static CanvasesManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CanvasesManager();
                return _instance;
            }
        }
    }
}