using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class Manager
    {
        //Global attributes
        public static Random random = new Random();
        public static int maxPendingFlights = 20;
        public static string[] destinations = new string[15] {
        "London, Storbritanien", "Amsterdam, Holland", "Berlin, Tyskland",
        "Stockholm, SemiDanmark", "Paris, Frankrig", "Reykjavik, Island",
        "Palma Mallorca, Spanien", "Frankfurt, Tyskland", "Aalborg, Jydeland",
        "Manchester, Storbritanien", "Bornholm, Danmark", "Zurich, Schweiz",
        "Oslo, Norge", "Riga, Letland", "Beograd, Serbien" };
        FlightPlan flightPlan = new FlightPlan();

        #region Fields
        private string name;
        private DateTime currentTime;
        private int amountOfCheckIns;
        private int amountOfGates;
        private FlightPlan[] flightPlans;
        private CheckIn[] checkIns;
        private Gate[] gates;
        private Luggage[] luggageBuffer;
        private CheckInBuffer[] checkInBuffers;
        private GateBuffer[] gateBuffers;
        private Luggage[] log;


        #endregion



        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; }
        }

        public int AmountOfCheckIns
        {
            get { return amountOfCheckIns; }
            set { amountOfCheckIns = value; }
        }
        public int AmountOfGates
        {
            get { return amountOfGates; }
            set { amountOfGates = value; }
        }
        public FlightPlan[] FlightPlans
        {
            get { return flightPlans; }
            set { flightPlans = value; }
        }
        public CheckIn[] CheckIns
        {
            get { return checkIns; }
            set { checkIns = value; }
        }
        public Gate[] Gates
        {
            get { return gates; }
            set { gates = value; }
        }
        public Luggage[] LuggageBuffer
        {
            get { return luggageBuffer; }
            set { luggageBuffer = value; }
        }

        public CheckInBuffer[] CheckInBuffers
        {
            get { return checkInBuffers; }
            set { checkInBuffers = value; }
        }
        public GateBuffer[] GateBuffers
        {
            get { return gateBuffers; }
            set { gateBuffers = value; }
        }
        public Luggage[] Log
        {
            get { return log; }
            set { log = value; }
        }

        public Manager()
        {

        }

        public Manager(string name)
        {
            this.name = name;
        }

        public Manager(string name, DateTime currentTime, int amountOfCheckIns, int amountOfGates, FlightPlan[] flightPlans, CheckIn[] checkIns, Gate[] gates, Luggage[] luggageBuffer, CheckInBuffer[] checkInBuffers, GateBuffer[] gateBuffers, Luggage[] log)
        {
            this.name = name;
            this.currentTime = currentTime;
            this.amountOfCheckIns = amountOfCheckIns;
            this.amountOfGates = amountOfGates;
            this.flightPlans = flightPlans;
            this.checkIns = checkIns;
            this.gates = gates;
            this.luggageBuffer = luggageBuffer;
            this.checkInBuffers = checkInBuffers;
            this.gateBuffers = gateBuffers;
            this.log = log;
        }

        #endregion



        #region Constructors

        #endregion

        #region Methods


        public void RunSimulation()
        {

        }
        public void AddFlightToFlightPlan()
        {

         
            if (FlightPlans.Length<maxPendingFlights)
            {
                
                FlightPlan flightPlan = new FlightPlan();
                flightPlan.FlightNumber++;
            }

        }
        //Checking in luggage to the CheckinBuffer
        public void CheckInLuggage()
        {

        }

        //Creating luggage and adding it to the checkInBuffer
        public void CreateLuggage()
        {

        }
        public void CreateCheckIns()
        {
            AmountOfCheckIns = 20;
            for (int i = 0; i < AmountOfCheckIns; i++)
            {
                CheckIn checkIn = new CheckIn();
                CheckIns.Append(checkIn);
            }
        }

        public void CreateGates()
        {
            AmountOfGates = 10;
            for (int i = 0; i < AmountOfGates; i++)
            {
                Gate gate = new Gate();
                Gates.Append(gate);
            }
        }

        #endregion
    }
}
