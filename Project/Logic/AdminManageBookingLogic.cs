using DataModels;
using System.Collections.Generic;
using System.Text.Json;
namespace DataAccess
{

    public enum BaggageValidationResult
    {
        Valid20Kg,
        Valid25Kg,
        Overweight,
        InvalidWeight
    }


    public static class AdminManageBookingLogic
    {
        public static List<FlightModel> allFlights = DataAccessClass.ReadList<FlightModel>("DataSources/flights.json");
        public static Dictionary<string, List<BookedFlightsModel>> allBookedFlights = BookedFlightsAccess.LoadAll();

        public static bool NewSeatLogic(string newSeat, BookedFlightsModel selectedBooking)
        {
            if (!(newSeat.Length >= 2 && newSeat.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newSeat[^1])) &&
            int.TryParse(newSeat.Substring(0, newSeat.Length - 1), out int number) &&
            number >= 1 && number <= 30))
            {
                return false;
            }
            foreach (var seat in allBookedFlights)
            {
                foreach (var book in seat.Value)
                {
                    if (book.BookedSeats.Any(seat => string.Equals(seat, newSeat, StringComparison.OrdinalIgnoreCase)))
                    {

                        return false;

                    }
                }
            }

            string letterPart = newSeat.Substring(1).ToUpper();
            string numberPart = newSeat.Substring(0, 1);
            string seatStr = $"{numberPart}{letterPart}";
            selectedBooking.BookedSeats.Add(seatStr);
            NotificationLogic.newSeats.Add(seatStr);

            return true;
        }

        public static bool ChangeSeatLogic(string newSeat, BookedFlightsModel selectedBooking, int seatIndex)
        {
            if (NewSeatLogic(newSeat, selectedBooking))
            {
                string letterPart = newSeat.Substring(1).ToUpper();
                string numberPart = newSeat.Substring(0, 1);
                newSeat = $"{numberPart}{letterPart}";
                var oldSeat = selectedBooking.BookedSeats[seatIndex - 1];
                selectedBooking.BookedSeats[seatIndex - 1] = newSeat;

                NotificationLogic.seatChanges.Add(oldSeat);
                NotificationLogic.seatChanges.Add(newSeat);

                return true;
            }

            return false;

        }

        public static (bool, int) CheckValidSelection(string number, BookedFlightsModel selectedBooking)
        {
            return (int.TryParse(number, out int seatIndex) && seatIndex > 0 && seatIndex <= selectedBooking.BookedSeats.Count, seatIndex);
        }

        public static (bool, int) CheckValidSelectionPet(string number, BookedFlightsModel selectedBooking)
        {
            return (int.TryParse(number, out int PetIndex) && PetIndex > 0 && PetIndex <= selectedBooking.Pets.Count, PetIndex);
        }

        public static bool IsPetTypeValid(string petType)
        {
            return petType == "dog" || petType == "cat" || petType == "bunny" || petType == "bird";
        }

        public static void CreateNewPet(string petType, string petName, BookedFlightsModel selectedBooking, FlightModel flight)
        {
            var newPet = new PetModel(petType, petName) { Fee = 50.0 };
            selectedBooking.Pets.Add(newPet);
            NotificationLogic.newPets.Add(newPet);
            flight.TotalPets++;
        }

        public static void ChangePetLogic(BookedFlightsModel selectedBooking, string petName, string newPetType, int PetIndex)
        {
            var oldPet = selectedBooking.Pets[PetIndex - 1];
            selectedBooking.Pets.RemoveAt(PetIndex - 1);// -1 to match the index

            // Add the new pet
            PetModel newPet = new PetModel(newPetType, petName);
            selectedBooking.Pets.Insert(PetIndex - 1, newPet);
            NotificationLogic.petChanges.Add(oldPet);
            NotificationLogic.petChanges.Add(newPet);
        }

        public static int CarryOnIsValid(double weightBaggage, string initials, string baggageType, BookedFlightsModel selectedBooking)
        {
            if (weightBaggage > 0 && weightBaggage <= 20)
            {

                var newBaggage = new BaggageModel(initials, baggageType, weightBaggage) { Fee = 50 };
                selectedBooking.BaggageInfo.Add(newBaggage);
                NotificationLogic.newBaggageAdded.Add(newBaggage);

                return 50;
            }

            else if (weightBaggage > 20 && weightBaggage <= 25)
            {
                var newBaggage = new BaggageModel(initials, baggageType, weightBaggage) { Fee = 0 };
                selectedBooking.BaggageInfo.Add(newBaggage);
                NotificationLogic.newBaggageAdded.Add(newBaggage);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public static BaggageValidationResult CheckedIsValid(double weightBaggage, string initials, string baggageType, BookedFlightsModel selectedBooking)
        {
            double feeBaggage = 0;

            if (weightBaggage == 20)
            {
                var newBaggage = new BaggageModel(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                selectedBooking.BaggageInfo.Add(newBaggage);
                NotificationLogic.newBaggageAdded.Add(newBaggage);
                return BaggageValidationResult.Valid20Kg;
            }
            else if (weightBaggage == 25)
            {
                var newBaggage = new BaggageModel(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                selectedBooking.BaggageInfo.Add(newBaggage);
                NotificationLogic.newBaggageAdded.Add(newBaggage);
                return BaggageValidationResult.Valid25Kg;
            }
            else if (weightBaggage > 25)
            {
                feeBaggage = 50;
                var newBaggage = new BaggageModel(initials, baggageType, weightBaggage) { Fee = feeBaggage };
                selectedBooking.BaggageInfo.Add(newBaggage);
                NotificationLogic.newBaggageAdded.Add(newBaggage);
                return BaggageValidationResult.Overweight;
            }
            else
            {
                return BaggageValidationResult.InvalidWeight;
            }
        }


        public static void SaveBookingData(string email, List<BookedFlightsModel> booking)
        {
            BookedFlightsAccess.Save(email, booking);
        }
    }
}