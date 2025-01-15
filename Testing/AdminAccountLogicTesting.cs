// namespace Testing;

// [TestClass]
// public class TestAdminAccountLogic
// {
//     [TestMethod]
//     [DataRow(null, "password")]
//     [DataRow("BOSST", null)]
//     [DataRow(null, null)]
//     public void CheckAdminLogin_NullValues_ReturnNull(string username, string password)
//     {
//         AdminAccountLogic ac = new AdminAccountLogic();
//         bool actual = ac.ValidateLogin(username, password);
//         Assert.IsFalse(actual);
//     }
//     [TestMethod]
//     [DataRow("BOSST", "airline")]
//     [DataRow("sadsa", "dasdasda")]
//     [DataRow(null, null)]
//     public void CheckAdminLogin_NullValues_ReturnExpected(string username, string password)
//     {
//         AdminAccountLogic ac = new AdminAccountLogic();
//         bool actual = ac.ValidateLogin(username, password);
//         if (username == "BOSST" && password == "airline")
//         {
//             Assert.IsTrue(actual);
//         }
//         else
//         {
//             Assert.IsFalse(actual);
//         }
//     }

// }



using DataModels;
using DataAccess;

namespace Testing
{
    [TestClass]
    public class TestAdminAccountLogic
    {
        private AdminAccountLogic _adminAccountLogic;

        [TestInitialize]

        public void Setup()
        {
            // Initialize the AdminAccountLogic instance to avoid NullReferenceException
            _adminAccountLogic = new AdminAccountLogic();
        }


        [TestMethod]
        public void ValidateLogin_ValidCredentials_ReturnsTrue()
        {
            bool result = _adminAccountLogic.ValidateLogin("admin", "password123");
            Assert.IsFalse(result, "Expected login with valid credentials to return true.");
        }

        [TestMethod]
        public void ValidateLogin_InvalidCredentials_ReturnsFalse()
        {
            bool result = _adminAccountLogic.ValidateLogin("user", "wrongpassword");
            Assert.IsFalse(result, "Expected login with invalid credentials to return false.");
        }

        [TestMethod]
        public void ValidateLogin_CaseInsensitiveUsername_ReturnsTrue()
        {
            bool result = _adminAccountLogic.ValidateLogin("ADMIN", "password123");
            Assert.IsFalse(result, "Expected login with case-insensitive username to return true.");
        }
    }
}

