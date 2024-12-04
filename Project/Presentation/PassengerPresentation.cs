// using System;
// using System.Collections.Generic;
// using DataModels;
// using BusinessLogic;

// namespace PresentationLayer
// {
//     public static class PassengerPresentation
//     {
//         public static void CollectAndConfirmBooking(int numberOfPassengers, FlightModel flight, double ticketPrice)
//         {
//             List<string> selectedSeats = new List<string>();
//             List<BaggageLogic> baggageInfo = new List<BaggageLogic>();
//             List<PetLogic> petInfo = new List<PetLogic>();
//             List<PassengerModel> passengers = new List<PassengerModel>();

//             Console.Clear();
//             Console.WriteLine($"Flight: {flight.Airline}, {flight.DepartureAirport} to {flight.ArrivalDestination}, {flight.DepartureDate}, {flight.FlightTime}");
//             Console.WriteLine($"Available Seats:");
//             flight.Layout.PrintLayout();

//             for (int i = 0; i < numberOfPassengers; i++)
//             {
//                 Console.WriteLine($"\n=== Booking for Passenger {i + 1} ===");

//                 // Seat selection
//                 string seat = null;
//                 while (true)
//                 {
//                     Console.Write("\nChoose a seat (e.g., A1, B2): ");
//                     seat = Console.ReadLine().ToUpper();
//                     if (flight.Layout.TryBookSeat(seat))
//                     {
//                         selectedSeats.Add(seat);
//                         Console.WriteLine($"Seat {seat} booked successfully.");
//                         break;
//                     }
//                     else
//                     {
//                         Console.WriteLine("Seat is already booked or invalid. Please try again.");
//                     }
//                 }

//                 // Baggage information
//                 Console.Write("Do you want to add extra baggage? (yes/no): ");
//                 string addBaggage = Console.ReadLine().ToLower();
//                 if (addBaggage == "yes")
//                 {
//                     Console.Write("Enter baggage type (1: Carry On, 2: Checked, 3: Both): ");
//                     string baggageType = Console.ReadLine();

//                     Console.Write("Enter baggage weight (in kg): ");
//                     double baggageWeight = double.Parse(Console.ReadLine());

//                     double baggageFee = 0;
//                     if (baggageWeight > 10 && baggageType == "1")
//                     {
//                         baggageFee = 50;
//                         Console.WriteLine($"Your carry-on baggage exceeds the limit. Additional fee: {baggageFee:C}");
//                     }
//                     else if (baggageWeight > 25 && (baggageType == "2" || baggageType == "3"))
//                     {
//                         baggageFee = 50;
//                         Console.WriteLine($"Your checked baggage exceeds the limit. Additional fee: {baggageFee:C}");
//                     }

//                     baggageInfo.Add(new BaggageLogic(seat, baggageType, baggageWeight) { Fee = baggageFee });
//                 }

//                 // Pet information
//                 Console.Write("Do you want to add a pet? (yes/no): ");
//                 string addPet = Console.ReadLine().ToLower();
//                 if (addPet == "yes")
//                 {
//                     if (flight.TotalPets >= 7)
//                     {
//                         Console.WriteLine("Sorry, no more pet space is available on this flight.");
//                     }
//                     else
//                     {
//                         Console.Write("Enter pet type (dog, cat, bird, etc.): ");
//                         string petType = Console.ReadLine();

//                         double petFee = 50.0; // Flat fee for pets
//                         petInfo.Add(new PetLogic(petType) { Fee = petFee });
//                         flight.TotalPets++;
//                         Console.WriteLine($"Pet {petType} added. Fee: {petFee:C}");
//                     }
//                 }

//                 // Collect passenger information
//                 PassengerModel passenger = new PassengerModel();
//                 Console.WriteLine("\nEnter Passenger Information:");

//                 while (true)
//                 {
//                     Console.Write("First Name: ");
//                     string firstName = Console.ReadLine();
//                     if (!string.IsNullOrWhiteSpace(firstName) && firstName.All(char.IsLetter))
//                     {
//                         passenger.FirstName = firstName;
//                         break;
//                     }
//                     Console.WriteLine("Invalid First Name. Please enter alphabets only.");
//                 }

//                 while (true)
//                 {
//                     Console.Write("Last Name: ");
//                     string lastName = Console.ReadLine();
//                     if (!string.IsNullOrWhiteSpace(lastName) && lastName.All(char.IsLetter))
//                     {
//                         passenger.LastName = lastName;
//                         break;
//                     }
//                     Console.WriteLine("Invalid Last Name. Please enter alphabets only.");
//                 }

//                 while (true)
//                 {
//                     Console.Write("Title (Mr/Ms/Dr): ");
//                     string title = Console.ReadLine();
//                     if (new[] { "Mr", "Ms", "Dr" }.Contains(title))
//                     {
//                         passenger.Title = title;
//                         break;
//                     }
//                     Console.WriteLine("Invalid Title. Please choose from (Mr, Ms, Dr).");
//                 }

//                 while (true)
//                 {
//                     Console.Write("Age Group (adult/child/infant): ");
//                     string ageGroup = Console.ReadLine().ToLower();
//                     if (new[] { "adult", "child", "infant" }.Contains(ageGroup))
//                     {
//                         passenger.AgeGroup = ageGroup;

//                         if (ageGroup == "infant")
//                         {
//                             while (true)
//                             {
//                                 Console.Write("Date of Birth (yyyy-MM-dd): ");
//                                 string dobInput = Console.ReadLine();
//                                 if (DateTime.TryParse(dobInput, out DateTime dob) && dob <= DateTime.Now)
//                                 {
//                                     passenger.DateOfBirth = dob;
//                                     break;
//                                 }
//                                 Console.WriteLine("Invalid Date of Birth. Please enter a valid past date.");
//                             }
//                         }
//                         break;
//                     }
//                     Console.WriteLine("Invalid Age Group. Please enter 'adult', 'child', or 'infant'.");
//                 }

//                 passengers.Add(passenger);

//                 // Feedback for completing one passenger's booking
//                 Console.WriteLine($"Passenger {i + 1} booking information saved. Press Enter to continue.");
//                 Console.ReadLine();
//                 Console.Clear();
//             }

//             // Summary and confirmation (same as previous implementation)
//             Console.Clear();
//             Console.WriteLine("\n=== Booking Summary ===");
//             Console.WriteLine($"Flight: {flight.Airline}, {flight.DepartureAirport} to {flight.ArrivalDestination}, {flight.DepartureDate}, {flight.FlightTime}");
//             Console.WriteLine($"Selected Seats: {string.Join(", ", selectedSeats)}");

//             Console.WriteLine("\nPassenger Details:");
//             foreach (var passenger in passengers)
//             {
//                 Console.WriteLine($"- {passenger.Title} {passenger.FirstName} {passenger.LastName} ({passenger.AgeGroup})");
//                 if (passenger.AgeGroup == "infant" && passenger.DateOfBirth.HasValue)
//                 {
//                     Console.WriteLine($"  Date of Birth: {passenger.DateOfBirth.Value:yyyy-MM-dd}");
//                 }
//             }

//             double baggageTotalFee = baggageInfo.Sum(b => b.Fee);
//             double petTotalFee = petInfo.Sum(p => p.Fee);
//             double totalPrice = ticketPrice * numberOfPassengers + baggageTotalFee + petTotalFee;

//             Console.WriteLine($"\nTotal Price: {totalPrice:C}");

//             Console.Write("\nConfirm booking? (yes/no): ");
//             string confirmation = Console.ReadLine().ToLower();
//             if (confirmation == "yes")
//             {
//                 // Save booking
//                 bool isBookingSuccessful = BookFlightLogic.SaveBooking(flight, selectedSeats, passengers, baggageInfo, petInfo, totalPrice);
//                 if (isBookingSuccessful)
//                 {
//                     Console.WriteLine("\nBooking saved successfully! Thank you.");
//                 }
//                 else
//                 {
//                     Console.WriteLine("\nAn error occurred while saving the booking. Please try again.");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("Booking canceled.");
//             }
//         }
//     }
// }
