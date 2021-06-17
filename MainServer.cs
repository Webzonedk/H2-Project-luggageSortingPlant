using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bogus;

namespace luggageSortingPlant
{
    class MainServer
    {
        //Global attributes adjustable from the Gui
        public static int amountOfCheckIns = 20;//Adjustable from WPF if possible
        public static int amountOfGates = 10;//Adjustable from WPF if possible
        public static int maxPendingFlights = 20;//Adjustable from WPF if possible
        public static int MaxLuggageBuffer = 8000;
        public static int checkInBufferSize = 50;
        public static int sortBufferSize = 500;

        public static int gateBufferSize = 50;
        public static int logSize = 2000000;
        public static int flightPlanMinInterval = 10;
        public static int flightPlanMaxInterval = 30;
        public static int checkInOpenBeforeDeparture = 20;
        public static int gateOpenBeforeDeparture = 10;

        //Global attributes for use in the Threads
        public static Random random = new Random();
        public static string[] destinations = new string[15] {
        "London, Storbritanien", "Amsterdam, Holland", "Berlin, Tyskland",
        "Stockholm, SemiDanmark", "Paris, Frankrig", "Reykjavik, Island",
        "Palma Mallorca, Spanien", "Frankfurt, Tyskland", "Aalborg, Jydeland",
        "Manchester, Storbritanien", "Bornholm, Danmark", "Zurich, Schweiz",
        "Oslo, Norge", "Riga, Letland", "Beograd, Serbien" };
        public static int[] numberOfSeats = new int[5] { 150, 200, 250, 300, 350 };

        public static FlightPlan[] flightPlans = new FlightPlan[maxPendingFlights];
        public static Luggage[] luggageBuffer = new Luggage[MaxLuggageBuffer];

        public static CheckInBuffer[] checkInBuffers = new CheckInBuffer[amountOfCheckIns];
        public static CheckIn[] checkIns = new CheckIn[amountOfCheckIns];
        public static Thread[] checkInWorkers = new Thread[amountOfCheckIns];

        public static SortingUnitBuffer sortingUnitBuffer = new SortingUnitBuffer();

        public static GateBuffer[] gateBuffers = new GateBuffer[amountOfGates];
        public static Gate[] gates = new Gate[amountOfGates];
        public static Thread[] gateWorkers = new Thread[amountOfGates];

        public static Luggage[] log = new Luggage[logSize];


        //Instantiating Classes
      //  public static Luggage luggage = new Luggage();
        public static OutPut outPut = new OutPut();

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
        public MainServer()
        {

        }

        public MainServer(string name)
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
        public void CreateCheckIns()
        {
            for (int i = 0; i < amountOfCheckIns; i++)
            {
                CheckIn checkIn = new CheckIn($"Check in counter {i + 1}", false, i);
                checkIns[i] = checkIn;
            }
        }
        public void CreateCheckInBuffers()
        {
            for (int i = 0; i < amountOfCheckIns; i++)
            {
                CheckInBuffer checkIn = new CheckInBuffer();
                checkInBuffers[i] = checkIn;
            }
        }

        public void CreateGates()
        {
            for (int i = 0; i < amountOfGates; i++)
            {
                Gate gate = new Gate($"Gate {i}", false, i);
                gates[i] = gate;
            }
        }
        public void CreateGateBuffers()
        {
            for (int i = 0; i < amountOfGates; i++)
            {
                GateBuffer gate = new GateBuffer();
                gateBuffers[i] = gate;
            }
        }


        public void RunSimulation()
        {
            CurrentTime = DateTime.Now;//Setting the current time
            CheckIn checkIn = new();//Initializing CheckIn 
            CreateCheckIns();//Run the CreateCheckIns method
            CreateGates();//Creates the Gates

            //Initializing the classes
            FlightPlanWorker createFlights = new("FlightplanWorker");
            LuggageWorker createLuggage = new("LuggageWorker");
            MainEntrance mainEntrance = new("Main Entrance");
           CleaningLady cleaningLady = new();



        //Initializing the workers
        Thread flightPlanner = new(createFlights.AddFlightToFlightPlan);
            Thread luggageSpawner = new(createLuggage.CreateLuggage);

            //Initializing mainEntranceSPlitter
            Thread mainEntranceSplitter = new(mainEntrance.SendLuggageToCheckIn);

            //Initializing the FlightPlanner CleaningLady
            Thread flightPlanSorter = new(cleaningLady.ReorderingFlightPlan);

            //Initializing checkinBufferSortings to each checkInWorker Array using a loop
            for (int i = 0; i < checkIns.Length; i++)
            {
                Thread checkInWorker = new(checkIns[i].CheckInLuggage);
                checkInWorkers[i] = checkInWorker;
            } 
            //Initializing checkinWorkers to the checkInWorker Array using a loop
            for (int i = 0; i < checkIns.Length; i++)
            {
                Thread checkInWorker = new(checkIns[i].CheckInLuggage);
                checkInWorkers[i] = checkInWorker;
            }

            //Initializing gateWorkers to the gateWorker Array using a loop
            for (int i = 0; i < gates.Length; i++)
            {
                Thread gateWorker = new(gates[i].Boarding);
                gateWorkers[i] = gateWorker;
            }




            //-------------------------------------------------------------
            //Starting the threads
            //-------------------------------------------------------------
            flightPlanSorter.Start();

            flightPlanner.Start();

            luggageSpawner.Start();

           // mainEntranceSplitter.Start();

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



            #endregion
        }
    }
}