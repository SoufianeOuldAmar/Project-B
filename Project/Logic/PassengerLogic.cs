// using System;
// using System.Collections.Generic;
// using System.Linq;
// using DataModels;

// namespace BusinessLogic
// {
//     public static class PassengerLogic
//     {
//         public static List<PassengerModel> CollectPassengerDetails(int numberOfPassengers)
//         {
//             var passengers = new List<PassengerModel>();

//             for (int i = 0; i < numberOfPassengers; i++)
//             {
//                 Console.WriteLine($"=== Enter details for Passenger {i + 1} ===");
//                 PassengerModel passenger = new PassengerModel();

//                 // Collect and validate first name
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

//                 // Collect and validate last name
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

//                 // Collect and validate title
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

//                 // Collect and validate age group
//                 while (true)
//                 {
//                     Console.Write("Age Group (adult/child/infant): ");
//                     string ageGroup = Console.ReadLine().ToLower();
//                     if (new[] { "adult", "child", "infant" }.Contains(ageGroup))
//                     {
//                         passenger.AgeGroup = ageGroup;

//                         // Validate date of birth for infants
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
//             }

//             return passengers;
//         }


//         public static void ValidatePassengerDetails(List<PassengerModel> passengers)
//         {
//             foreach (var passenger in passengers)
//             {
//                 if (string.IsNullOrWhiteSpace(passenger.FirstName) || string.IsNullOrWhiteSpace(passenger.LastName))
//                 {
//                     throw new ArgumentException("First Name and Last Name cannot be empty.");
//                 }

//                 if (passenger.AgeGroup == "infant" && passenger.DateOfBirth == null)
//                 {
//                     throw new ArgumentException("Infants must have a valid Date of Birth.");
//                 }
//             }
//         }
//     }
// }
