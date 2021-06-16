using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class Gate
    {
        #region Fields
        private string workerName;



        private int gateNumber;

        #endregion

        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }


        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }


        #endregion

        #region Constructors
        public Gate()
        {

        }
        public Gate(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion

        #region Methods
        public void CreateGates()
        {
            for (int i = 0; i < MainServer.amountOfGates; i++)
            {
                Gate gate = new Gate($"Gate {i}");
                MainServer.gates[i]=gate;//Might be problematic
            }
        }

        public void Boarding()
        {

        }
        #endregion
    }
}
