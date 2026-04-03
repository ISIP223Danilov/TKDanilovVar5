using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZpVar5Danlov; 

namespace ZpVar5Danlov.UnitTesting 
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        // --- ТЕСТЫ СТАВОК (GetRate) ---

        [TestMethod]
        public void GetRate_Assistant_Returns150()
        {
            var logic = new ZpVar5Danlov.Tests();
            Assert.AreEqual(150, logic.GetRate("ассистент"));
        }

        [TestMethod]
        public void GetRate_Docent_Returns250()
        {
            var logic = new ZpVar5Danlov.Tests();
            Assert.AreEqual(250, logic.GetRate("доцент"));
        }

        [TestMethod]
        public void GetRate_Professor_Returns350()
        {
            var logic = new ZpVar5Danlov.Tests();
            Assert.AreEqual(350, logic.GetRate("профессор"));
        }

        [TestMethod]
        public void GetRate_UnknownPosition_ReturnsZero()
        {
            var logic = new ZpVar5Danlov.Tests();
            Assert.AreEqual(0, logic.GetRate("ректор"));
        }

        // --- ТЕСТЫ РАСЧЕТОВ (Calculate) ---

        [TestMethod]
        public void Calculate_NoTax_SimpleValue()
        {
            var logic = new ZpVar5Danlov.Tests();
            // 10 часов по 150 руб = 1500, налог 0
            var result = logic.Calculate(10, 150, false);
            Assert.AreEqual(1500, result.total, "Общая сумма неверна");
            Assert.AreEqual(0, result.tax, "Налог должен быть 0");
        }

        [TestMethod]
        public void Calculate_WithTax_Professor()
        {
            var logic = new ZpVar5Danlov.Tests();
            // 20 часов * 350 = 7000. Налог 13% = 910
            var result = logic.Calculate(20, 350, true);
            Assert.AreEqual(7000, result.total);
            Assert.AreEqual(910, result.tax);
        }

        [TestMethod]
        public void Calculate_ZeroHours_ReturnsZeros()
        {
            var logic = new ZpVar5Danlov.Tests();
            var result = logic.Calculate(0, 350, true);
            Assert.AreEqual(0, result.total);
            Assert.AreEqual(0, result.tax);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeHours_ThrowsException()
        {
            var logic = new ZpVar5Danlov.Tests();
            // Отрицательные часы должны вызывать ошибку
            logic.Calculate(-5, 150, false);
        }
    }
}
