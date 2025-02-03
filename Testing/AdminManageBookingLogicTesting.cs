

using DataAccess;


namespace Testing
{
    [TestClass]
    public class TestAdminManageBookingLogic
    {
        [TestInitialize]
        public void Setup()
        {

            AdminManageBookingLogic.allBookedFlights = new Dictionary<string, List<BookedFlightsModel>>
            {
                { "Flight1", new List<BookedFlightsModel>
                    {
                        new BookedFlightsModel
                        {
                            BookedSeats = new List<string> { "12A", "5C", "1D" }
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void SeatLogic_ValidAndAvailableSeat_ReturnsTrue()
        {
            bool result = AdminManageBookingLogic.NewSeatLogic("15B", AdminManageBookingLogic.allBookedFlights["Flight1"][0]);
            Assert.IsTrue(result, "Expected valid and available seat to return true.");
        }

        [TestMethod]
        public void SeatLogic_AlreadyBookedSeat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.NewSeatLogic("12A", AdminManageBookingLogic.allBookedFlights["Flight1"][0]);
            Assert.IsFalse(result, "Expected already booked seat to return false.");
        }

        [TestMethod]
        public void SeatLogic_InvalidSeatFormat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.NewSeatLogic("40Z", AdminManageBookingLogic.allBookedFlights["Flight1"][0]);
            Assert.IsFalse(result, "Expected invalid seat format to return false.");
        }

        [TestMethod]
        public void SeatLogic_LowerCaseSeat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.NewSeatLogic("5c", AdminManageBookingLogic.allBookedFlights["Flight1"][0]);
            Assert.IsFalse(result, "Expected case-insensitive check for already booked seat to return false.");
        }

        [TestMethod]
        public void SeatLogic_OutOfRangeRow_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.NewSeatLogic("31A", AdminManageBookingLogic.allBookedFlights["Flight1"][0]);
            Assert.IsFalse(result, "Expected out-of-range row number to return false.");
        }
    }
}
