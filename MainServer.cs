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
        public static int checkInBufferSize = 225;
        public static int sortBufferSize = 500;
        public static int randomSleepMin = 50;
        public static int randomSleepMax = 300;
        public static int gateBufferSize = 50;
        public static int logSize = 2000000;
        public static int flightPlanMinInterval = 30;//secunds
        public static int flightPlanMaxInterval = 60;//secunds
        public static int checkInOpenBeforeDeparture = 60;//secunds
        public static int gateOpenBeforeDeparture = 30;//secunds

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
        public static List<FlightPlan> tempFlightPlan = new List<FlightPlan>();

        public static Luggage[] luggageBuffer = new Luggage[MaxLuggageBuffer];

        public static CheckInBuffer[] checkInBuffers = new CheckInBuffer[amountOfCheckIns];
        public static Thread[] checkInBufferWorkers = new Thread[amountOfCheckIns];
        public static CheckIn[] checkIns = new CheckIn[amountOfCheckIns];
        public static Thread[] checkInWorkers = new Thread[amountOfCheckIns];


        public static SortingUnitBuffer sortingUnitBuffer = new SortingUnitBuffer();

        public static GateBuffer[] gateBuffers = new GateBuffer[amountOfGates];
        public static Gate[] gates = new Gate[amountOfGates];
        public static Thread[] gateWorkers = new Thread[amountOfGates];

        public static Luggage[] log = new Luggage[logSize];


        //Instantiating Classes
        public static OutPut outPut = new OutPut();//This class is only for printing in console.

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
                CheckInBuffer checkIn = new CheckInBuffer(i);
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
                                       // CheckIn checkIn = new();//Initializing CheckIn 

            CreateCheckIns();//Run the CreateCheckIns method
            CreateCheckInBuffers();
            CreateGates();//Creates the Gates
            CreateGateBuffers();//Creates the gate buffers


           // Thread.Sleep(200);


            //Instantiates the classes
            FlightPlanWorker createFlights = new("FlightplanWorker");
            FlightPlanQueueWorker sortFlightPlan = new();
            LuggageWorker createLuggage = new("LuggageWorker");
            LuggageQueueWorker sortLuggage = new();
            MainEntrance mainEntrance = new("Main Entrance");




            //Instantiates the flightPlan worker
            Thread flightPlanner = new(createFlights.AddFlightToFlightPlan);

            //Instantiates the FlightPlanSorter
            Thread flightPlanSorter = new(sortFlightPlan.ReorderingFlightPlan);

            //Instantiates the LuggaggeWorker worker
            Thread luggageSpawner = new(createLuggage.CreateLuggage);

            //Instantiates the LuggaggeWorker worker
            Thread luggageSorter = new(sortLuggage.ReorderingLuggageBuffer);

            //Instantiates mainEntranceSPlitter
            Thread mainEntranceSplitter = new(mainEntrance.SendLuggageToCheckIn);

            //Instantiates checkInBufferSortings to each checkInWorker Array using a loop
            for (int i = 0; i < checkInBufferWorkers.Length; i++)
            {
                Thread checkInBufferWorker = new(checkInBuffers[i].ReorderingCheckInBuffer);
                checkInBufferWorkers[i] = checkInBufferWorker;
            }
            //Instantiates checkinWorkers to the checkInWorker Array using a loop
            for (int i = 0; i < checkIns.Length; i++)
            {
                Thread checkInWorker = new(checkIns[i].CheckInLuggage);
                checkInWorkers[i] = checkInWorker;
            }

            //Instantiates gateWorkers to the gateWorker Array using a loop
            for (int i = 0; i < gates.Length; i++)
            {
                Thread gateWorker = new(gates[i].Boarding);
                gateWorkers[i] = gateWorker;
            }




            //-------------------------------------------------------------
            //Starting the threads
            //-------------------------------------------------------------

            flightPlanner.Start();

            flightPlanSorter.Start();

            luggageSpawner.Start();

            luggageSorter.Start();

            mainEntranceSplitter.Start();

            foreach (Thread worker in checkInBufferWorkers)
            {
                worker.Start();
            }

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