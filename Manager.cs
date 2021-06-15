using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bogus;

namespace luggageSortingPlant
{
    class Manager
    {
        //Global attributes adjustable from the Gui
        public static int amountOfCheckIns = 20;//Adjustable from WPF if possible
        public static int amountOfGates = 10;//Adjustable from WPF if possible
        public static int maxPendingFlights = 20;//Adjustable from WPF if possible
        public static int MaxLuggageBuffer = 500;

        //Global attributes for use in the Threads
        public static Random random = new Random();
        public static string[] destinations = new string[15] {
        "London, Storbritanien", "Amsterdam, Holland", "Berlin, Tyskland",
        "Stockholm, SemiDanmark", "Paris, Frankrig", "Reykjavik, Island",
        "Palma Mallorca, Spanien", "Frankfurt, Tyskland", "Aalborg, Jydeland",
        "Manchester, Storbritanien", "Bornholm, Danmark", "Zurich, Schweiz",
        "Oslo, Norge", "Riga, Letland", "Beograd, Serbien" };
        public static int[] numberOfSeats = new int[5] { 150, 200, 250, 300, 350 };

        public static FlightPlan[] flightPlans;
        public static Luggage[] luggageBuffer;

        public static CheckInBuffer[] checkInBuffers;
        public static CheckIn[] checkIns;
        public static Thread[] checkInWorkers;

        public static GateBuffer[] gateBuffers;
        public static Gate[] gates;
        public static Thread[] gateWorkers;

        public static Luggage[] log;


        //Initializing Classes
        public static Luggage luggage = new();
        public static CleaningLady cleaningLady = new();

        #region Fields
        private string name;
        private DateTime currentTime;
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


        //public CheckIn[] CheckIns
        //{
        //    get { return checkIns; }
        //    set { checkIns = value; }
        //}

        //public CheckInBuffer[] CheckInBuffers
        //{
        //    get { return checkInBuffers; }
        //    set { checkInBuffers = value; }
        //}
        //public GateBuffer[] GateBuffers
        //{
        //    get { return gateBuffers; }
        //    set { gateBuffers = value; }
        //}
        //public Luggage[] Log
        //{
        //    get { return log; }
        //    set { log = value; }
        //}
        #endregion



        #region Constructors
        public Manager()
        {

        }

        public Manager(string name)
        {
            this.name = name;
        }

        //public Manager(string name, DateTime currentTime,)
        //{
        //    this.name = name;
        //    this.currentTime = currentTime;
        //    this.checkIns = checkIns;
        //    this.gates = gates;
        //    this.gateBuffers = gateBuffers;
        //    this.log = log;
        //}
        #endregion



        #region Methods
        public void RunSimulation()
        {
            CurrentTime = DateTime.Now;//Setting the current time
            CheckIn checkIn = new();//Initializing CheckIn 
            checkIn.CreateCheckIns();//Run the CreateCheckIns method

            //Initializing the classes
            FlightPlan createFlights = new("Flightplanner");
            Luggage createLuggage = new("LuggageCreater");
            MainEntrance mainEntrance = new("Main Entrance");

            //Initializing the workers
            Thread flightPlanner = new(createFlights.AddFlightToFlightPlan);
            Thread luggageSpawner = new(createLuggage.CreateLuggage);
            //Initializing checkinWorkers to the checkInWorker Array using a loop
            for (int i = 0; i < checkIns.Length; i++)
            {
                Thread checkInWorker = new(checkIns[i].CheckInLuggage);
                checkInWorkers.Append(checkInWorker);
            }
            //Initializing gateWorkers to the gateWorker Array using a loop
            for (int i = 0; i < gates.Length; i++)
            {
                Thread gateWorker = new(gates[i].Boarding);
                gateWorkers.Append(gateWorker);
            }
            Thread mainEntranceSplitter = new(mainEntrance.SendLuggageToCheckIn);


            //-------------------------------------------------------------
            //Starting the threads
            //-------------------------------------------------------------

            flightPlanner.Start();

            // luggageSpawner.Start();

            //foreach (Thread worker in checkInWorkers)
            //{
            //    worker.Start();
            //} 

            //foreach (Thread worker in gateWorkers)
            //{
            //    worker.Start();
            //}

            //mainEntranceSplitter.Start();


            //-------------------------------------------------------------
            //-------------------------------------------------------------
        }





        #endregion
    }
}
