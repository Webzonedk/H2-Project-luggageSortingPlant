using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class SortingPlant
    {
        #region Fields
        private int luggageNumber;
        private DateTime timeIn;
        private DateTime timeOut;




        #endregion

        #region Properties
        public int LuggageNumber
        {
            get { return luggageNumber; }
            set { luggageNumber = value; }
        }

        public DateTime TimeIn
        {
            get { return timeIn; }
            set { timeIn = value; }
        }
        public DateTime TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void SortLuggage()
        {

        }
        #endregion
    }
}
