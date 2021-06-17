using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class SortingUnitBuffer
    {
        #region Fields

        private Luggage[] buffer;

        #endregion



        #region Properties
        public Luggage[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        #endregion


        #region Constructors
        public SortingUnitBuffer()
        {

        }
        //Initializing
        public SortingUnitBuffer(Luggage[] buffer)
        {
            this.buffer = buffer;
        }
        #endregion

        #region Methods

        #endregion
    }
}
