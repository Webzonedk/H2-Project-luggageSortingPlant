using System;

namespace luggageSortingPlant
{
    class Program
    {
        //Initializing the manager
        public static MainServer manager = new MainServer();
        static void Main(string[] args)
        {
            manager.RunSimulation();

        }
    }
}
