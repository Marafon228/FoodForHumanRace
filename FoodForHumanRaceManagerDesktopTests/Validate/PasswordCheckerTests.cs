using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodForHumanRaceManagerDesktop.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodForHumanRaceManagerDesktop.Validate.Tests
{
    [TestClass()]
    public class PasswordCheckerTests
    {
        [TestMethod()]
        public void Check_8Symvols_ReturnsTrue()
        {
            //Arrange.
            string password = "ASqw12$$";
            bool expected = true;

            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Check_4Symvols_ReturnsFalse()
        {
            //Arrange.
            string password = "Aq1$";
            

            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.IsFalse(actual);

        }

        [TestMethod()]
        public void Check_30Symvols_ReturnsFalse()
        {
            //Arrange.
            string password = "ASDqwe123$ASDqwe123$ASDqwe123$";
            bool expected = false;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected,actual);

        }

        [TestMethod()]
        public void Check_PasswordWithDigits_returnsTrue()
        {
            //Arrange.
            string password = "ASDqwe1$";
            bool expected = true;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void Check_PasswordWithoutDigits_returnsFalse()
        {
            //Arrange.
            string password = "ASDqweASD$";
            bool expected = false;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Check_PasswordWithSpecSymvols_returnsTrue()
        {
            //Arrange.
            string password = "Aqwe123$";
            bool expected = true;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void Check_PasswordWithoutSpecSymvols_returnsFalse()
        {
            //Arrange.
            string password = "ASDqwe123$";
            bool expected = true;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Check_PasswordWithCapsSymvols_returnsTrue()
        {
            //Arrange.
            string password = "Aqwe123$";
            bool expected = true;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Check_PasswordWithoutCapsSymvols_returnsFalse()
        {
            //Arrange.
            string password = "asdqwe123$";
            bool expected = false;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Check_PasswordWithLowerSymvols_returnsTrue()
        {
            //Arrange.
            string password = "ASDq123$";
            bool expected = true;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void Check_PasswordWithoutLowerSymvols_returnsFalse()
        {
            //Arrange.
            string password = "ASDQWE123$";
            bool expected = false;



            //Act.
            bool actual = PasswordChecker.ValidatePassword(password);

            //Assert.
            Assert.AreEqual(expected, actual);

        }

    }


}