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
            Console.Write("{0,-10}", $"{MainServer.flightPlans[i].FlightNumber}"); 
            Console.Write("{0,-35}", $"{MainServer.flightPlans[i].Destination}");
            Console.Write("{0,7}", $"{MainServer.flightPlans[i].Seats}");
            Console.Write("{0,10}", $"{MainServer.flightPlans[i].GateNumber}");
            Console.Write("{0,25}", $"{MainServer.flightPlans[i].DepartureTime}\n");
        }

        public void PrintLuggage(int i)//Method with argument
        {
            Console.Write("{0,-10}", $"{MainServer.luggageBuffer[i].LuggageNumber}");
            Console.Write("{0,-10}", $"{MainServer.luggageBuffer[i].PassengerNumber}");
            Console.Write("{0,-35}", $"{MainServer.luggageBuffer[i].PassengerName}");
            Console.Write("{0,10}", $"{MainServer.luggageBuffer[i].FlightNumber}");
            Console.Write("{0,25}", $"{MainServer.luggageBuffer[i].CheckInTimeStamp}");
            Console.Write("{0,25}", $"{MainServer.luggageBuffer[i].SortInTimeStmap}");
            Console.Write("{0,25}", $"{MainServer.luggageBuffer[i].SortOutTimeStamp}");
            Console.Write("{0,25}", $"{MainServer.luggageBuffer[i].GateArrivalTimeStamp}\n");
        }

        public void PrintArrivedToTheAirport(Luggage luggage) //MainEntrance Arrival
        {
            Console.Write($"Passenger: ");
            Console.Write($"{luggage.PassengerName}, ");
            Console.Write($"with Passenger number: ");
            Console.Write($"{luggage.PassengerNumber} ");
            Console.Write($"and luggage number: ");
            Console.Write($"{luggage.LuggageNumber}");
            Console.Write($"for flight number: ");
            Console.Write("{0,10}", $"{luggage.FlightNumber}, has arrived to the airport.\n");
        }

        public void PrintCheckInBufferCapacity(int checkInNumber, int luggageInBuffer)//Printing the capacity of the checkIn buffer
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"CheckIn conveyor belt: {checkInNumber} now have {luggageInBuffer} / {MainServer.checkInBufferSize} Suitcases on the band ");
            Console.ResetColor();
        }

        public void PrintCheckInBufferWorkerOutput(int checkInNumber)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Reordered CheckIn buffer {checkInNumber}");
            Console.ResetColor();
        }

        public void PrintCheckInArrival(Luggage luggage) //Printing when luggage arrives to checkin
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write($"Luggage number: ");
            Console.Write($"{luggage.LuggageNumber}");
            Console.Write($"for flight number: ");
            Console.Write("{0,10}", $"{luggage.FlightNumber}, has arrived checkIn counter. ");
            Console.Write($"at: ");
            Console.Write("{0,10}", $"{luggage.CheckInTimeStamp}, has arrived checkIn counter.\n");
            Console.ResetColor();
        }

        public void PrintCheckInOut(Luggage luggage)//Not active at the moment. but can be added for debugging
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write($"Luggage number: ");
            Console.Write($"{luggage.LuggageNumber}");
            Console.Write($"for flight number: ");
            Console.Write("{0,10}", $"{luggage.FlightNumber}, has arrived checkIn counter.\n");
            Console.ResetColor();
        }

        public void PrintSortingBufferCapacity(int luggageInBuffer)//Printing the capacity of the sorting buffer
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Conveyor belt for the sorting unit now have {luggageInBuffer + 1} / {MainServer.sortBufferSize} Suitcases on the band ");
            Console.ResetColor();
        }

        public void PrintSortingArrival(Luggage luggage)//Not finish
        {

            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write($"Luggage number: ");
            Console.Write($"{luggage.LuggageNumber}");
            Console.Write($"for flight number: ");
            Console.Write("{0,10}", $"{luggage.FlightNumber}, has arrived checkIn counter. ");
            Console.Write($"at: ");
            Console.Write("{0,10}", $"{luggage.CheckInTimeStamp}, has arrived checkIn counter.\n");
            Console.ResetColor();
        }

        public void PrintSortingDeparting(Luggage luggage)//Not finish
        {

            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write($"Luggage number: ");
            Console.Write($"{luggage.LuggageNumber}");
            Console.Write($"for flight number: ");
            Console.Write("{0,10}", $"{luggage.FlightNumber}, has arrived checkIn counter. ");
            Console.Write($"at: ");
            Console.Write("{0,10}", $"{luggage.CheckInTimeStamp}, has arrived checkIn counter.\n");
            Console.ResetColor();
        }

    }
}
