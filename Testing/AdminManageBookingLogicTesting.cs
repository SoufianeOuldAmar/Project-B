
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
            bool result = AdminManageBookingLogic.SeatLogic("15B");
            Assert.IsTrue(result, "Expected valid and available seat to return true.");
        }

        [TestMethod]
        public void SeatLogic_AlreadyBookedSeat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.SeatLogic("12A");
            Assert.IsFalse(result, "Expected already booked seat to return false.");
        }

        [TestMethod]
        public void SeatLogic_InvalidSeatFormat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.SeatLogic("40Z");
            Assert.IsFalse(result, "Expected invalid seat format to return false.");
        }

        [TestMethod]
        public void SeatLogic_LowerCaseSeat_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.SeatLogic("5c");
            Assert.IsFalse(result, "Expected case-insensitive check for already booked seat to return false.");
        }

        [TestMethod]
        public void SeatLogic_OutOfRangeRow_ReturnsFalse()
        {
            bool result = AdminManageBookingLogic.SeatLogic("31A");
            Assert.IsFalse(result, "Expected out-of-range row number to return false.");
        }
    }
}
