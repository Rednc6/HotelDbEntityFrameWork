using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDbEntityFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;

            Console.WriteLine("\n1. List All Hotels\n2. List All Guests");
            input = int.Parse(Console.ReadLine());
                     
            switch (input)
            {
                case 1:
                    SelectAllHotel();
                    break;
                case 2:
                    SelectAllKunder();
                    break;
                default:
                    Console.WriteLine("Me no understand...");
                    break;
            }

            Console.ReadLine();

        }   

        // method

        public static void SelectAllHotel()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Alle hoteller");

                var allHotels = context.Hotels;

                foreach (var hotelObj in allHotels)
                {
                    Console.WriteLine(hotelObj);
                }

            }
        }

        public static void SelectAllKunder()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Alle kunder");

                var allKunder = context.Guests;

                foreach (var kundeObj in allKunder)
                {
                    Console.WriteLine(kundeObj);
                }
            }
        }


    }
}
