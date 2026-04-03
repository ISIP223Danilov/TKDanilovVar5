using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpVar5Danlov
{
    public class Tests
    {
        // Метод для получения ставки
        public double GetRate(string position)
        {
            switch (position.ToLower())
            {
                case "ассистент": return 150;
                case "доцент": return 250;
                case "профессор": return 350;
                default: return 0;
            }
        }


        // Основной метод расчета
        public (double total, double tax) Calculate(double hours, double rate, bool applyTax)
        {
            if (hours < 0) throw new ArgumentException("Часы не могут быть отрицательными");

            double total = hours * rate;
            double tax = applyTax ? total * 0.13 : 0;
            return (total, tax);
        }
    }
}
