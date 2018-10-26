using System;
using System.Collections.Generic;

namespace DiscountСalculator
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Discount : DiscountMessages
    {
        public int DiscountValue { get; set; }
        public DateTime? StartSellDate { get; set; }
        public DateTime? EndSellDate { get; set; }
        public int Calculate(List<int> Param, Func<List<int>, int> func)
        {
            return func(Param);
        }
    }

    public class DiscountMessages
    {
        string SellMessage = "В настоящий момент на данный товар нет скидок.";

        public void SetSellInformation(string Message)
        {
            SellMessage = Message;
        }
        public string GetSellInformation()
        {
            return SellMessage;
        }
    }
}
