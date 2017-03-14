using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HotelDbEntityFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;

            Console.WriteLine("\n1. List All Hotels\n2. List All Guests\n3. List hotels + room information\n4. Bookings on each room\n5. Insert New Guest\n6. Opret Booking\n7. Edit Hotel");
            input = int.Parse(Console.ReadLine());
            Console.Clear();

            while (input != 0)
            {

                switch (input)
                {
                    case 1:
                        SelectAllHotel();
                        break;
                    case 2:
                        SelectAllKunder();
                        break;
                    case 3:
                        SelectHotelNARi();
                        break;
                    case 4:
                        BookingsEachRoom();
                        break;
                    case 5:
                        InsertNewGuest();
                        break;
                    case 6:
                        OpretBooking();
                        break;
                    case 7:
                        EditHotelInfo();
                        break;
                    default:
                        Console.WriteLine("Laters..!!");
                        break;
                }

                Console.WriteLine("\n1. List All Hotels\n2. List All Guests\n3. List hotels + room information\n4. Bookings on each room\n5. Insert New Guest\n6. Opret Booking\n7. Edit Hotel");
                input = int.Parse(Console.ReadLine());
                Console.Clear();
            }

        }   

        // method

            // lamdba expression til at vælge alle hoteller og cw write dem
        public static void SelectAllHotel()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Alle hoteller \n");

                var allHotels = context.Hotels;

                foreach (var hotelObj in allHotels)
                {
                    Console.WriteLine(hotelObj);
                }

            }
        }

            // lamdba expression til at vælge alle kunder og cw write dem
        public static void SelectAllKunder()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Alle kunder\n");

                var allKunder = context.Guests;

                foreach (var kundeObj in allKunder)
                {
                    Console.WriteLine(kundeObj);
                }
            }
        }

                // simple include som samligner hotel.no med et input og giver bruger list med de værelser der er på hotel.no
        public static void SelectHotelNARi()
        {
            using (var context = new HotelContext())
            {
                int input;
                Console.WriteLine(" hotelnavn samt værelses information, vælg hotel udfra hotel nr. ");
                input = int.Parse(Console.ReadLine());

                var specHotel = context.Hotels.Include(x => x.Rooms).Where(y => y.Hotel_No == input).ToList();

                foreach (var hotelObj in specHotel)
                {
                    Console.WriteLine(hotelObj);
                    Console.WriteLine(string.Join($"\n", hotelObj.Rooms));
                }                
            }
        }

                // 2 lamdba expressions, 1st vælger room.no fra rooms den anden samligner de room.no fra bookings og samligner dem aka "joiner" dem.
        public static void BookingsEachRoom()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Alle bookings på hvert room");

                var RoomRoomNo = context.Rooms.Select(x => x.Room_No);
                var bookingEachRoom = context.Bookings.Where(y => RoomRoomNo.Contains(y.Room_No));

                foreach (var roomObj in bookingEachRoom)
                {
                    Console.WriteLine($"Room nr: {roomObj.Room_No}, Booking nr: {roomObj.Booking_id}, Start: {roomObj.Date_From.ToString("dd/mm/yy")}, End: {roomObj.Date_To.ToString("dd/mm/yy")}");
                }
            }
        }

        public static void InsertNewGuest()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Insert ny guest i systemet");
                Guest newGuest = new Guest();

                Console.WriteLine("Guest nr: ");
                newGuest.Guest_No = int.Parse(Console.ReadLine());
                Console.WriteLine("Guest name: ");
                newGuest.Name = Console.ReadLine();
                Console.WriteLine("Guest adress: ");
                newGuest.Address = Console.ReadLine();

                context.Guests.Add(newGuest);
                context.SaveChanges();
            }
        }

        public static void OpretBooking()
        {
            using (var context = new HotelContext())
            {
                Console.WriteLine(" Opret ny Booking");
                Booking newBooking = new Booking();

                Console.WriteLine("Insert Guest nr: ");
                newBooking.Guest_No = int.Parse(Console.ReadLine());
                Console.WriteLine("Insert Hotel nr: ");
                newBooking.Hotel_No = int.Parse(Console.ReadLine());
                Console.WriteLine("Start Date");
                newBooking.Date_From = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("End Date");
                newBooking.Date_To = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Insert Room nr: ");
                newBooking.Room_No = int.Parse(Console.ReadLine());

                context.Bookings.Add(newBooking);
                context.SaveChanges();
            }
        }

        public static void EditHotelInfo()
        {

        }

    }
}
