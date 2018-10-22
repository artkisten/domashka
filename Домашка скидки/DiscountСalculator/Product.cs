using System;

namespace DiscountСalculator
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Discount : IDiscount
    {
        public int DiscountValue { get; set; }
        public DateTime? StartSellDate { get; set; }
        public DateTime? EndSellDate { get; set; }
        private int Price;
        private bool Type;
        private int DiscountPrice;

        public Discount(int Price, bool Type, int DiscountPrice)
        {
            this.Price = Price;
            this.Type = Type;
            this.DiscountPrice = DiscountPrice;
        }

        public int CalculateDiscount()
        {
            if (Type)
            {
                return DiscountValue != 0 &&
                    StartSellDate.HasValue &&
                    EndSellDate.HasValue &&
                    StartSellDate <= DateTime.UtcNow &&
                    EndSellDate >= DateTime.UtcNow
                    ? (Price * DiscountValue / 100)
                    : 0;
            }
            return (DiscountPrice < Price) ? DiscountValue : 0;
        }

        public string GetSellInformation()
        {
            string a = "р.";
            if (Type) { a = "%"; }
            return DiscountValue != 0 && DiscountPrice < Price && StartSellDate.HasValue && EndSellDate.HasValue
                ? $"На данный товар действует скидка {DiscountValue}{a} в период с {StartSellDate.Value.ToShortDateString()} по {EndSellDate.Value.ToShortDateString()}. " +
                    $"Сумма с учётом скидки - {Price - CalculateDiscount()}р."
                : "В настоящий момент на данный товар нет скидок.";
        }
    }
}
