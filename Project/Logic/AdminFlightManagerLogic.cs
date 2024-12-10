using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using DataModels;

namespace DataAccess
{

    public static class AdminFlightManagerLogic
    {

        public static List<FlightModel> GetAllFlights()
        {
            return FlightsAccess.ReadAll();
        }
        public static bool SaveChangesLogic(FlightModel flight)
        {
            var flights = FlightsAccess.ReadAll();
            var flightToUpdate = flights.FirstOrDefault(f => f.Id == flight.Id);
            if (flightToUpdate != null)
            {
                flightToUpdate.Airline = flight.Airline;
                flightToUpdate.TicketPrice = flight.TicketPrice;
                flightToUpdate.Gate = flight.Gate;
                flightToUpdate.DepartureAirport = flight.DepartureAirport;
                flightToUpdate.ArrivalDestination = flight.ArrivalDestination;
                flightToUpdate.IsCancelled = flight.IsCancelled;
                flightToUpdate.DepartureDate = flight.DepartureDate;
                flightToUpdate.FlightTime = flight.FlightTime;
                FlightsAccess.WriteAll(flights);
                return true;
            }
            return false;
        }


        public static FlightModel SearchFlightLogic(int id)
        {
            var Flight = FlightsAccess.ReadAll();
            return Flight.FirstOrDefault(flight => flight.Id == id);
        }

        public static bool TicketPriceLogic(double newTicketPrice)
        {
            if (newTicketPrice >= 0)
            {
                return true;
            }
            return false;

        }

        public static bool GateLogic(string newGate)
        {
            if (newGate.Length >= 2 && newGate.Length <= 3 && "ABCDEF".Contains(char.ToUpper(newGate[0])) &&
            int.TryParse(newGate.Substring(1), out int number) &&
            number >= 1 && number <= 30)
            {
                return true;
            }
            return false;
        }

        public static bool Date(string input)
        {
            string[] dateParts = input.Split('-');
            if (dateParts.Length == 3)
            {
                string yearStr = dateParts[2];
                string monthStr = dateParts[1].PadLeft(2, '0');
                string dayStr = dateParts[0].PadLeft(2, '0');
                input = $"{dayStr}-{monthStr}-{yearStr}";

                if (yearStr.Length == 4 && monthStr.Length == 2 && dayStr.Length == 2)
                {
                    int year = int.Parse(yearStr);
                    int month = int.Parse(monthStr);
                    int day = int.Parse(dayStr);

                    if ((month == 4 || month == 6 || month == 9 || month == 11) && day >= 1 && day <= 30 && year >= 2024)
                    {
                        return true;
                    }
                    else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day >= 1 && day <= 31 && year >= 2024)
                    {
                        return true;
                    }
                    else if (month == 2 && day >= 1 && day <= 28 && year >= 2024)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static void RefreshFlightData()
        {

            List<FlightModel> flightList = FlightsAccess.ReadAll();
        }


    }
}