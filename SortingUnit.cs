using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class SortingUnit
    {
        #region Fields
        private int sortingUnitNumber;

        #endregion



        #region Properties
        public int SortingUnitNumber
        {
            get { return sortingUnitNumber; }
            set { sortingUnitNumber = value; }
        }

        #endregion

        #region Constructors

        public SortingUnit(int sortingUnitNumber)
        {
            this.sortingUnitNumber = sortingUnitNumber;
        }

        #endregion

        #region Methods
        public void SortLuggage()
        {

        }
        #endregion
    }
}
