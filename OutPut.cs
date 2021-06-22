using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class OutPut
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors


        #endregion

        #region Methods

        #endregion
        public void PrintFlightPlan(int i)//Method with argument
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Fligtnumber {MainServer.flightPlans[i].FlightNumber} " +
                $"with destination {MainServer.flightPlans[i].Destination} " +
                $"with {MainServer.flightPlans[i].Seats} " +
                $"departure from gate: {MainServer.flightPlans[i].GateNumber} " +
                $"at: {MainServer.flightPlans[i].DepartureTime}");
            Console.ResetColor();
        }

        public void PrintLuggage(int i)//Method with argument
        {
            Console.ResetColor();
            Console.WriteLine($"Luggagge nr.: {MainServer.luggageBuffer[i].LuggageNumber} " +
                $"passenger nr.:{MainServer.luggageBuffer[i].PassengerNumber} " +
                $"Name: {MainServer.luggageBuffer[i].PassengerName}\t\t" +
                $"For flight: {MainServer.luggageBuffer[i].FlightNumber} has been created.");
            Console.ResetColor();
        }

        public void PrintArrivedToTheAirport(Luggage luggage) //MainEntrance Arrival
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Passenger: {luggage.PassengerName}, " +
                $"with Passenger number: {luggage.PassengerNumber} " +
                $"and luggage number: {luggage.LuggageNumber} " +
                $"for flight number: {luggage.FlightNumber}, has arrived to the airport. ");
            Console.ResetColor();
        }

        public void PrintCheckInBufferCapacity(int checkInNumber, int luggageInBuffer)//Printing the capacity of the checkIn buffer
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"CheckIn conveyor belt: {checkInNumber} now have {luggageInBuffer} / {MainServer.checkInBufferSize} Suitcases on the band ");
            Console.ResetColor();
        }

        public void PrintCheckInBufferWorkerOutput(int checkInNumber)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Reordered CheckIn buffer {checkInNumber}");
            Console.ResetColor();
        }

        public void PrintCheckInArrival(Luggage luggage) //Printing when luggage arrives to checkin
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Luggage number: {luggage.LuggageNumber} " +
                $"for flight number: {luggage.FlightNumber}, has arrived checkIn counter. " +
                $"at: {luggage.CheckInTimeStamp}.");
            Console.ResetColor();
        }

        public void PrintCheckInOut(Luggage luggage)//Not active at the moment. but can be added for debugging
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
        }

        public void PrintSortingBufferCapacity(int luggageInBuffer)//Printing the capacity of the sorting buffer
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Conveyor belt for the sorting unit now have {luggageInBuffer} / {MainServer.sortBufferSize} Suitcases on the band ");
            Console.ResetColor();
        }

        public void PrintSortingArrival(Luggage luggage)//Not finish
        {

            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"Luggage number: {luggage.LuggageNumber} " +
                $"for flight number: {luggage.FlightNumber}," +
                $" has arrived to the sortingUnit at {luggage.SortInTimeStmap}");
            Console.ResetColor();
        }

        public void PrintSortingDeparting(Luggage luggage)//Not finish
        {

            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"Luggage number: {luggage.LuggageNumber} " +
                $"for flight number: {luggage.FlightNumber}," +
                $" has left the the sortingUnit at {luggage.SortOutTimeStamp}");
            Console.ResetColor();
        }
        public void PrintSortedToGate(Luggage luggage, int gateNumber)//Not finish
        {
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Luggage number: {luggage.LuggageNumber} " +
                $"for flight number: {luggage.FlightNumber}," +
                $" has left the the sortingUnit at {luggage.SortOutTimeStamp}" +
                $"and is now in queue at gate: {gateNumber} ");
            Console.ResetColor();
        }

    }
}
