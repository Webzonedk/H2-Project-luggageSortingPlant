using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class GateBuffer
    {
        #region Fields
        private int gateNumber;


        private Luggage[] buffer = new Luggage[MainServer.gateBufferSize];

        #endregion



        #region Properties
        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }

        public Luggage[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        #endregion


        #region Constructors
        public GateBuffer()
        {

        }
        //Initializing
        public GateBuffer(int gateNumber)
        {
            this.gateNumber = gateNumber;
        }
        #endregion

        #region Methods
        public void ReorderingGateBuffer(int gateNumber)//Not yet adjusted to fit gatebuffer
        {
            while (true)
            {
                if (MainServer.luggageBuffer[0] == null)
                {
                    for (int i = 1; i < MainServer.luggageBuffer.Length; i++)
                    {
                        MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    }
                }
            }
        }
        #endregion
    }
}

