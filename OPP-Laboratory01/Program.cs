// See https://aka.ms/new-console-template for more information
using OOP_Lab04;
using OOP_Lab04.Classes;

try
{
    Console.WriteLine("Welcome to our hotel reservation system!");
    Hotel newHotel = new Hotel("Hotel 2000","190 Main St");

    Console.WriteLine(newHotel.Name);
    Console.WriteLine(newHotel.Address);

    Client client1 = new Client("B ob", 1234567890987654);
    Client client2 = new Client("John", 1234567890981234);

    Room room1 = new Room("101", 1);
    Room room2 = new Room("202", 2);


    Reservation reservation1 = new Reservation(client1, room1, 1);

    Reservation reservation2 = new Reservation(client2, room2, 2);

    newHotel.RegisterANewReservation(reservation1);
    newHotel.RegisterANewReservation(reservation2);

    Console.WriteLine($"The reservation of Room: {reservation1.Room.Number} has been made by client {reservation1.Client.Name}");

    Console.WriteLine($"The reservation of Room: {reservation2.Room.Number} has been made by client {reservation2.Client.Name}");

    Console.WriteLine();
}
catch(Exception ex)
{ 
    Console.WriteLine(ex.Message); 
}