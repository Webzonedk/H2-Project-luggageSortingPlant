using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

                try
                {
                    Monitor.Enter(MainServer.gateBuffers[GateNumber]);//Locking the thread

                    for (int i = 0; i < MainServer.gateBuffers[GateNumber].Buffer.Length - 1; i++)
                {
                    if (MainServer.gateBuffers[GateNumber].Buffer[i] == null)
                    {
                        MainServer.gateBuffers[GateNumber].Buffer[i] = MainServer.gateBuffers[GateNumber].Buffer[i + 1];
                        MainServer.gateBuffers[GateNumber].Buffer[i + 1] = null;
                    }
                }

                }
                finally
                {
                    Monitor.PulseAll(MainServer.gateBuffers[GateNumber]);//Sending signal to other thread
                    Monitor.Exit(MainServer.gateBuffers[GateNumber]);//Release the lock
                }



               
            }
        }
        #endregion
    }
}

