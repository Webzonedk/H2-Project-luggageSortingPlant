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
        //Global attributes

        public static Random random = new Random();
        public static int maxPendingFlights = 20;
        public static string[] destinations = new string[15] {
        "London, Storbritanien", "Amsterdam, Holland", "Berlin, Tyskland",
        "Stockholm, SemiDanmark", "Paris, Frankrig", "Reykjavik, Island",
        "Palma Mallorca, Spanien", "Frankfurt, Tyskland", "Aalborg, Jydeland",
        "Manchester, Storbritanien", "Bornholm, Danmark", "Zurich, Schweiz",
        "Oslo, Norge", "Riga, Letland", "Beograd, Serbien" };
        public static int[] numberOfSeats = new int[5] { 150, 200, 250, 300, 350 };
        public static FlightPlan[] flightPlans;
        public static Luggage[] luggageBuffer;



        #region Fields
        private string name;
        private DateTime currentTime;
        private int amountOfCheckIns;
        private int amountOfGates;
        private CheckIn[] checkIns;
        private Gate[] gates;
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
        #endregion



        #region Constructors
        public Manager()
        {

        }

        public Manager(string name)
        {
            this.name = name;
        }

        public Manager(string name, DateTime currentTime, int amountOfCheckIns, int amountOfGates, CheckIn[] checkIns, Gate[] gates, CheckInBuffer[] checkInBuffers, GateBuffer[] gateBuffers, Luggage[] log)
        {
            this.name = name;
            this.currentTime = currentTime;
            this.amountOfCheckIns = amountOfCheckIns;
            this.amountOfGates = amountOfGates;
            this.checkIns = checkIns;
            this.gates = gates;
            this.checkInBuffers = checkInBuffers;
            this.gateBuffers = gateBuffers;
            this.log = log;
        }
        #endregion



        #region Methods
        public void RunSimulation()
        {
            CurrentTime = DateTime.Now;
            FlightPlan createFlights = new FlightPlan("Flightplanner");
            Manager createLuggage = new Manager("LuggageCreater");

            Thread flightPlanner = new Thread(createFlights.AddFlightToFlightPlan);
            Thread luggageSpawner = new Thread(createLuggage.CreateLuggage);
        }

        //Adding flights if the flightbuffer is not full
        //public void AddFlightToFlightPlan()
        //{
        //    while (true)
        //    {
        //        if (flightPlans[maxPendingFlights] == null)
        //        {
        //            int destinationIndex = random.Next(0, destinations.Length);
        //            int seats = random.Next(0, numberOfSeats.Length);
        //            FlightPlan flightPlan = new FlightPlan();
        //            flightPlan.FlightNumber++;
        //            flightPlan.Destination = destinations[destinationIndex];
        //            flightPlan.Seats = numberOfSeats[seats];
        //            flightPlan.GateNumber = random.Next(1, AmountOfGates);
        //            if (flightPlans[maxPendingFlights - 1] == null)
        //            {
        //                flightPlan.DepartureTime = currentTime.AddSeconds(random.Next(10, 20));
        //            }
        //            else
        //            {
        //                flightPlan.DepartureTime = flightPlans[maxPendingFlights - 1].DepartureTime.AddSeconds(random.Next(10, 20));
        //            }
        //            flightPlans[maxPendingFlights] = flightPlan;
        //        }
        //    }
        //}



        //Creating luggage and adding it to the checkInBuffer
        public void CreateLuggage()
        {
            int luggageCounter = 1;
            int paasengerNumber = 1;
            while (true)
                for (int i = 0; i < luggageBuffer.Length; i++)
                {
                    Luggage luggage = new Luggage();
                    luggage.LuggageNumber = luggageCounter;
                    luggageCounter++;
                    luggage.PassengerNumber = paasengerNumber;
                    paasengerNumber++;
                    Faker passengerName = new Faker();
                    luggage.PassengerName = passengerName.Name.FullName();

                    int randomFlightNumber = random.Next(0, maxPendingFlights);
                    int countLuggage = 0;
                    for (int j = 0; j < luggageBuffer.Length; j++)
                    {
                        if (luggageBuffer[j].FlightNumber == flightPlans[randomFlightNumber].FlightNumber)
                        {
                            countLuggage++;
                        }
                    }
                    if (countLuggage < flightPlans[randomFlightNumber].Seats)
                    {
                        luggage.FlightNumber = randomFlightNumber;
                    }
                }
        }



        //Checking in luggage to the CheckinBuffer
        public void CheckInLuggage()
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
