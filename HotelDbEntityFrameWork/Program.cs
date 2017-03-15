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

            Console.WriteLine("\n1. Alle Hotels\n2. Alle gæster\n3. Hotel samt værelse information\n4. Bookings på hvert værelse\n5. Ny gæst\n6. Ny Booking\n7. Rediger hotel\n8. Slet booking og Gæst\n9. Gæst bookings\n0. Exit");
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
                        SelectAllGuests();
                        break;
                    case 3:
                        SelectHotelandRoomInfo();
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
                    case 8:
                        DelBookOrUser();
                        break;
                    case 9:
                        TotalBookingsPlus();
                        break;
                    case 0:
                        Console.WriteLine("Bye bye!");
                        break;
                    default:
                        Console.WriteLine("Laters..!!");
                        break;
                }

                Console.WriteLine("\n1. Alle Hotels\n2. Alle gæster\n3. Hotel samt værelse information\n4. Bookings på hvert værelse\n5. Ny gæst\n6. Ny Booking\n7. Rediger hotel\n8. Slet booking og Gæst\n9. Gæst bookings\n0. Exit");
                input = int.Parse(Console.ReadLine());
                Console.Clear();
            }

        }   

        // method

            // lamdba expression til at vælge alle hoteller og cw write dem
        public static void SelectAllHotel()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                Console.WriteLine(" \nAlle hoteller \n");

                var allHotels = context.Hotels;

                foreach (var hotelObj in allHotels)
                {
                    Console.WriteLine(hotelObj);
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

            // lamdba expression til at vælge alle kunder og cw write dem
        public static void SelectAllGuests()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                Console.WriteLine(" \nAlle Gæster \n");

                var allKunder = context.Guests;

                foreach (var kundeObj in allKunder)
                {
                    Console.WriteLine(kundeObj);
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

                // simple include som samligner hotel.no med et input og giver bruger list med de værelser der er på hotel.no
        public static void SelectHotelandRoomInfo()
        {
            Console.Clear();
            Console.WriteLine("");

            using (var context = new HotelContext())
            {
                var allHotels = context.Hotels;
                foreach (var hotelObj in allHotels)
                {
                    Console.WriteLine($"Hotel nr. {hotelObj.Hotel_No}, {hotelObj.Name}, {hotelObj.Address}");
                }

                Console.Write("\n Vælg hotel nr: ");
                int input = int.Parse(Console.ReadLine());

                var selectedHotel = context.Hotels.Find(input);
                var infoHotel = context.Rooms.Where(x => x.Hotel_No == selectedHotel.Hotel_No);

                Console.WriteLine("\n" + selectedHotel.Name + ", " + selectedHotel.Address + "\n");

                foreach (var item in infoHotel)
                {
                    Console.WriteLine("Room nr. " + item.Room_No + ", Type: " + item.Types + ", Price: " + item.Price);
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void BookingsEachRoom()
        {
            Console.Clear();
            Console.WriteLine("");

            using (var context = new HotelContext())
            {
                var allHotels = context.Hotels;
                foreach (var hotelObj in allHotels)
                {
                    Console.WriteLine($"Hotel nr. {hotelObj.Hotel_No}, {hotelObj.Name}, {hotelObj.Address}");
                }

                Console.Write("\n Vælg hotel nr: ");
                int input = int.Parse(Console.ReadLine());

                var selectedHotel = context.Hotels.Find(input);
                var infoRoom = context.Rooms.Where(x => x.Hotel_No == selectedHotel.Hotel_No);
                var roomNr = infoRoom.Select(x => x.Room_No);

                var bookingInfo = context.Bookings.Where(x => roomNr.Contains(x.Room_No)).ToList(); // same room nrs on each hotel, so they multiply. 
                
                foreach (var item in bookingInfo)
                {
                    if (item.Hotel_No == selectedHotel.Hotel_No)
                    {
                        Console.WriteLine($"Gæst Nr. {item.Guest_No}, Booking Nr. {item.Booking_id}, Værelse Nr. {item.Room_No}, Date: [{item.Date_From} - {item.Date_To}]");
                    }
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void InsertNewGuest()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                Console.WriteLine(" Book en ny gæst \n");

                Guest newGuest = new Guest();

                // database does not auto set gæst nr, the next 3 lines selects the gæst entries 
                // and order them by descending and selects the last one + 1 for new nr. 
                var selectGuest = context.Guests.Select(x => x).ToList();
                var lastGuestNr = selectGuest.Select(x => x.Guest_No).OrderByDescending(y => y).First();
                int newGuestNr = lastGuestNr + 1;

                Console.Write("Guest name: ");
                newGuest.Name = Console.ReadLine();
                Console.Write("Guest adress: ");
                newGuest.Address = Console.ReadLine();
                newGuest.Guest_No = newGuestNr;

                context.Guests.Add(newGuest);
                context.SaveChanges();

                Console.WriteLine("\nDen følgende gæst er blevet tilføjet\n");
                Console.WriteLine(newGuest);
            }

            Console.ReadLine();
            Console.Clear();
        }

                // opret booking
        public static void OpretBooking()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                Console.WriteLine(" Book ny gæst \n");
                Booking newBooking = new Booking();

                Console.Write("Insert Guest nr: ");
                newBooking.Guest_No = int.Parse(Console.ReadLine());
                Console.Write("Insert Hotel nr: ");
                newBooking.Hotel_No = int.Parse(Console.ReadLine());
                Console.Write("Start Date");
                newBooking.Date_From = DateTime.Parse(Console.ReadLine());
                Console.Write("End Date");
                newBooking.Date_To = DateTime.Parse(
                    Console.ReadLine());
                Console.Write("Insert Room nr: ");
                newBooking.Room_No = int.Parse(Console.ReadLine());

                context.Bookings.Add(newBooking);
                context.SaveChanges();
            }

            Console.ReadLine();
            Console.Clear();
        }

                // takes user input and .finds hotel which is equal to that, then takes that var and uses it to edit htoel specified by user input.
        public static void EditHotelInfo()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                var allHotels = context.Hotels;
             
                Console.WriteLine(" \n Select Hotel nr to Edit \n");

                foreach (var item in allHotels)
                {
                    Console.WriteLine($"{item.Hotel_No}, {item.Name}");
                }

                int input1 = int.Parse(Console.ReadLine());
                var original = context.Hotels.Find(input1);

                Console.WriteLine("\n" + original + "\n");

                Console.WriteLine("\n1. Change Hotel Adress\n2. Change Hotel Name");
                string input2 = Console.ReadLine();

                switch (input2)
                {
                    case "1":
                        Console.WriteLine("\n Change Adress to ");
                        string adress = Console.ReadLine();
                        original.Address = adress;        
                        break;
                    case "2":
                        Console.WriteLine("\n Change Hotel Name to ");
                        string name = Console.ReadLine();
                        original.Name = name;
                        break;
                    default:
                        break;
                }

                context.SaveChanges();

            }

            Console.ReadLine();
            Console.Clear();

        }

        public static void DelBookOrUser()
        {
            Console.Clear();
            Console.WriteLine("");

            using (var context = new HotelContext())
            {
                Console.WriteLine("\n1. Slet gæst \n2. Delete Booking");
                int input1 = int.Parse(Console.ReadLine());

                switch (input1)
                {
                    case 1: // delete gæst

                        var hotelContext = context.Hotels;
                        var guestContext = context.Guests;

                        foreach (var hotelObj in hotelContext)
                        {
                            Console.WriteLine($"Hotel nr. {hotelObj.Hotel_No}, {hotelObj.Name}, {hotelObj.Address}");
                        }

                        Console.Write("\n Vælg hotel nr: ");
                        int input2 = int.Parse(Console.ReadLine());

                        var selectedHotel = context.Hotels.Find(input2);
                        var infoBooking = context.Bookings.Where(x => x.Hotel_No == selectedHotel.Hotel_No);

                        var allGuests = infoBooking.Select(x => x);
                        foreach (var item in allGuests)
                        {
                            Console.WriteLine($"Gæst Nr. {item.Guest_No}, Værelse Nr. {item.Room_No}");
                        }

                        Console.Write("\n Vælg gæst nr. Som skal slettes: ");

                        int delGuest = int.Parse(Console.ReadLine());
                        var originalGæst = context.Guests.Find(delGuest);
                        guestContext.Remove(originalGæst);

                        break;
                    case 2:
                        
                        var allBookings = context.Bookings;
                        foreach (var item in allBookings)
                        {
                            Console.WriteLine($"ID: {item.Booking_id}, Guest nr. {item.Guest_No}");
                        }

                        Console.Write("\nVælg ID som skal slettes: ");

                        int delBooking = int.Parse(Console.ReadLine());
                        var originalBooking = context.Bookings.Find(delBooking);
                        allBookings.Remove(originalBooking);

                        break;
                    default:
                        break;
                }

                context.SaveChanges();

            }

            Console.ReadLine();
            Console.Clear();

        }

        public static void TotalBookingsPlus()
        {
            Console.Clear();

            using (var context = new HotelContext())
            {
                var query = from a in context.Bookings
                            from b in context.Rooms.Where(x => x.Hotel_No == a.Hotel_No && x.Room_No == a.Room_No).DefaultIfEmpty()
                            from c in context.Guests.Where(x => x.Guest_No == a.Guest_No).DefaultIfEmpty()
                            select new { bookingID = a.Booking_id, name = c.Name, bookStart = a.Date_From, bookEnd = a.Date_To, guestNr = a.Guest_No, hotelNr = a.Hotel_No, roomType = b.Types, roomPrice = b.Price };


                foreach (var item in query.OrderBy(x => x.guestNr))
                {
                    int count = (item.bookEnd - item.bookStart).Days;
                    double? sumBooking = count * item.roomPrice;

                    Console.WriteLine("\n" + item.name + "\n Guest nr. " + item.guestNr + "\n Hotel Nr. " + item.hotelNr + "\n Start: " + item.bookStart.ToShortDateString()
                                           + "\n End: " + item.bookEnd.ToShortDateString() + "\n Days booked: " + count + "\n Room type: " + item.roomType + "\n Price Per day: "
                                           + item.roomPrice + "\n Sum: " + sumBooking + "\n BookingId: " + item.bookingID);
                }

        }

            Console.ReadLine();
            Console.Clear();
        }

    }
}
