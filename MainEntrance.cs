using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class MainEntrance
    {
        #region Fields
        private string workerName;
        #endregion



        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }
        #endregion



        #region Constructors
        public MainEntrance(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion

        #region Methods

        #endregion
    }
}
