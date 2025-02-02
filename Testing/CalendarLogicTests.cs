using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CalendarLogicTests
{
    [TestClass]
    public class CalendarLogicTests
    {
        [TestMethod]
        public void Test_GetCalendarHeader_HappyPath()
        {

            int month = 1; 
            int year = 2025;

            string result = CalendarLogic.GetCalendarHeader(month, year);

            Assert.AreEqual("2025               January\nSun Mon Tue Wed Thu Fri Sat", result);
        }

        [TestMethod]
        public void Test_FebruaryLeapYear()
        {

            int month = 2; 
            int year = 2024; 
            string departureAirport = "";
            string destination = "";


            var (calendarLines, startingDayOfWeek) = CalendarLogic.GenerateCalendarLines(month, year, departureAirport, destination);


            int totalDays = 0;
            foreach (string line in calendarLines)
            {
                totalDays += line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Count(day => int.TryParse(day, out _)); 
            }


            Assert.AreEqual(29, totalDays, "The calendar should display 29 days for February in a leap year.");
        }


        [TestMethod]
        public void Test_InvalidMonth()
        {

            int month = 13; 
            int year = 2025;
            string departureAirport = "";
            string destination = "";

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                CalendarLogic.GenerateCalendarLines(month, year, departureAirport, destination));
        }




    }
}
