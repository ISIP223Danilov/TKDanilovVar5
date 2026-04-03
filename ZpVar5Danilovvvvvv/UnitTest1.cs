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
            // Проверка, что "ПРОФЕССОР" (в верхнем регистре) тоже работает
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
            // 10 часов * 150 = 1500, налог 0
            var (total, tax) = _logic.Calculate(10, 150, false);
            Assert.AreEqual(1500, total);
            Assert.AreEqual(0, tax);
        }

        [TestMethod]
        public void Calculate_WithTax_StandardValue()
        {
            // 20 часов * 350 = 7000. Налог 13% от 7000 = 910
            var (total, tax) = _logic.Calculate(20, 350, true);
            Assert.AreEqual(7000, total);
            Assert.AreEqual(910, tax);
        }

        [TestMethod]
        public void Calculate_FractionalHours_ReturnsCorrectValue()
        {
            // 1.5 часа * 200 = 300
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
            // 0.1 часа * 150 = 15
            var (total, tax) = _logic.Calculate(0.1, 150, false);
            Assert.AreEqual(15, total);
        }

        // ОШИБКИ 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeHours_ThrowsException()
        {
            // Отрицательные часы — это бизнес-ошибка
            _logic.Calculate(-1, 150, false);
        }

        [TestMethod]
        public void Calculate_NegativeRate_ReturnsNegativeTotal()
        {
            // Проверка математики: если ставка вдруг отрицательная (хотя это странно)
            var (total, tax) = _logic.Calculate(10, -100, false);
            Assert.AreEqual(-1000, total);
        }
    }
}
