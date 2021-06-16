using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class CheckInBuffer
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
        public CheckInBuffer()
        {

        }
        //Initializing
        public CheckInBuffer(Luggage[] buffer)
        {
            this.buffer = buffer;
        }

        #endregion

        #region Methods

        #endregion
    }
}

