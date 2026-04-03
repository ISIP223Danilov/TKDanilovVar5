using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZpVar5Danlov;

namespace ZpVar5Danlov.UnitTesting
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        private readonly ZpVar5Danlov.Tests _logic = new ZpVar5Danlov.Tests();

        // ПРОВЕРКА СТАВОК 

        [TestMethod]
        public void GetRate_Assistant_Returns150() => Assert.AreEqual(150, _logic.GetRate("ассистент"));

        [TestMethod]
        public void GetRate_Docent_Returns250() => Assert.AreEqual(250, _logic.GetRate("доцент"));

        [TestMethod]
        public void GetRate_Professor_Returns350() => Assert.AreEqual(350, _logic.GetRate("профессор"));

        [TestMethod]
        public void GetRate_DifferentCase_ReturnsCorrectValue()
        {
            
            Assert.AreEqual(350, _logic.GetRate("ПРОФЕССОР"));
            Assert.AreEqual(150, _logic.GetRate("Ассистент"));
        }

        [TestMethod]
        public void GetRate_EmptyOrInvalid_ReturnsZero()
        {
            Assert.AreEqual(0, _logic.GetRate(""));
            Assert.AreEqual(0, _logic.GetRate("декан"));
        }

        // РАСЧЕТЫ 

        [TestMethod]
        public void Calculate_NoTax_StandardValue()
        {
            
            var (total, tax) = _logic.Calculate(10, 150, false);
            Assert.AreEqual(1500, total);
            Assert.AreEqual(0, tax);
        }

        [TestMethod]
        public void Calculate_WithTax_StandardValue()
        {
            
            var (total, tax) = _logic.Calculate(20, 350, true);
            Assert.AreEqual(7000, total);
            Assert.AreEqual(910, tax);
        }

        [TestMethod]
        public void Calculate_FractionalHours_ReturnsCorrectValue()
        {
            
            var (total, tax) = _logic.Calculate(1.5, 200, false);
            Assert.AreEqual(300, total);
        }

        [TestMethod]
        public void Calculate_ZeroHours_ReturnsZero()
        {
            var (total, tax) = _logic.Calculate(0, 350, true);
            Assert.AreEqual(0, total);
            Assert.AreEqual(0, tax);
        }

        [TestMethod]
        public void Calculate_VerySmallHours_ReturnsCorrectValue()
        {
            
            var (total, tax) = _logic.Calculate(0.1, 150, false);
            Assert.AreEqual(15, total);
        }

        // ОШИБКИ 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeHours_ThrowsException()
        {
            
            _logic.Calculate(-1, 150, false);
        }

        [TestMethod]
        public void Calculate_NegativeRate_ReturnsNegativeTotal()
        {
            
            var (total, tax) = _logic.Calculate(10, -100, false);
            Assert.AreEqual(-1000, total);
        }

        // ГРАНИЧНЫЕ И СПЕЦИАЛЬНЫЕ СЛУЧАИ 
        [TestMethod]
        public void Calculate_LargeValues_ReturnsCorrectTotal()
        {

            var (total, tax) = _logic.Calculate(1000, 1000, true);
            Assert.AreEqual(1000000, total);
            Assert.AreEqual(130000, tax);

        }
        [TestMethod]
        public void Calculate_TaxPrecision_ReturnsCorrectKopeks()
        {
            
            var (total, tax) = _logic.Calculate(123.45, 150, true);
            Assert.AreEqual(18517.5, total);
            Assert.AreEqual(2407.275, tax, 0.0001); 
        }

        [TestMethod]
        public void Calculate_ZeroRate_ReturnsZero()
        {
            
            var (total, tax) = _logic.Calculate(100, 0, true);
            Assert.AreEqual(0, total);
            Assert.AreEqual(0, tax);
        }

        [TestMethod]
        public void Calculate_OneHour_ReturnsRateValue()
        {
           
            var (total, tax) = _logic.Calculate(1, 250, false);
            Assert.AreEqual(250, total);
        }

        [TestMethod]
        public void Calculate_MaximumTax_Check()
        {
            
            var (total, tax) = _logic.Calculate(10, 50000, true);
            Assert.AreEqual(500000, total);
            Assert.AreEqual(65000, tax);
        }
    }
}
