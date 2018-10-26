using System;
using System.Collections.Generic;

namespace DiscountСalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вы хотите добавить новый продукт? 1 - да, 2 - нет");

            int.TryParse(Console.ReadLine(), out var answer);

            if (answer != 1)
                return;

            CreateProduct();

            Console.ReadLine();
        }

        private static int PercentCalculate(List<int> Param)
        {
            return Param[0] * Param[1] / 100;
        }

        private static int RubCalculate(List<int> Param)
        {
            return Param[0] > Param[2] ? Param[1] : 0;
        }

        private static int CardCalculate(List<int> Param)
        {
            return Param[0];
        }

        private static void CreateProduct()
        {
            var product = new Product();

            bool loop = true;

            int discountPrice = 0;

            int discountValue = 0;

            int discountresult = 0;

            Console.WriteLine("Введите название продукта");

            product.Name = Console.ReadLine();

            Console.WriteLine("Введите стоимость продукта");

            int.TryParse(Console.ReadLine(), out var price);

            while (price <= 0)
            {
                Console.WriteLine("Стоимость продукта не была введена или она была введена с ошибкой. Пожалуйста, введите стоимость продукта ещё раз");

                int.TryParse(Console.ReadLine(), out price);
            }

            product.Price = price;

            Console.WriteLine("Выбирете тип скидки: 1-подарочная карта; 2-процентная скидка; 3-сумма от стоимости; 0-выход;");

            var discount = new Discount();

            List<int> Param = new List<int> { 0, 0, 0, 0 };

            do
            { 
                int.TryParse(Console.ReadLine(), out var discounttype);
                switch (discounttype)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("Введите значение скидки на товар в рублях");
                        int.TryParse(Console.ReadLine(), out discountValue);
                        while (discountValue <= 0)
                        {
                            Console.WriteLine("Скидка должна быть положительным целым числом");
                            int.TryParse(Console.ReadLine(), out discountValue);
                        }
                        discountValue = discountValue > price ? price : discountValue;
                        Param[0] = discountValue;

                        Console.WriteLine("Введите дату начала действия скидки (дд.мм.гггг)");

                        DateTime startSellDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out startSellDate))
                        {
                            Console.WriteLine("неверный формат даты");
                        }

                        if (startSellDate != DateTime.MinValue)
                        {
                            discount.StartSellDate = startSellDate;
                        }

                        Console.WriteLine("Введите дату окончания действия скидки (дд.мм.гггг)");

                        DateTime endSellDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out endSellDate) || endSellDate < startSellDate)
                        {
                            Console.WriteLine("неверный формат даты или она меньше даты начала скидок");
                        }

                        if (endSellDate != DateTime.MinValue)
                        {
                            discount.EndSellDate = endSellDate;
                        }

                        if (DateTime.UtcNow >= discount.StartSellDate && DateTime.UtcNow <= discount.EndSellDate)
                        {
                            discountresult = discount.Calculate(Param, CardCalculate);
                            discount.SetSellInformation($"На товар действует скидка по карте в размере {discountresult} c {discount.StartSellDate} по {discount.EndSellDate}");
                        }
                        else
                        {
                            discount.SetSellInformation($"Отсутствует скидка на товар, так как карта действует c {discount.StartSellDate} по {discount.EndSellDate}");
                        }

                        loop = !loop;

                        break;
                    case 2:

                        Console.WriteLine("Введите значение скидки на товар (в % от общей стоимости)");

                        int.TryParse(Console.ReadLine(), out discountValue);

                        while (discountValue > 100 || discountValue <= 0)
                        {
                            Console.WriteLine("Значение скидки не может быть больше 100 и меньше 0");

                            int.TryParse(Console.ReadLine(), out discountValue);
                            
                        }

                        Param[0] = price;
                        Param[1] = discountValue;

                        discountresult = discount.Calculate(Param, PercentCalculate);

                        discount.SetSellInformation($"На товар действует {discountValue}% скидка({discountresult} рублей)");

                        loop = !loop;

                        break;
                    case 3:
                        Console.WriteLine("Введите сумму при которой выдается скидка");
                        int.TryParse(Console.ReadLine(), out discountPrice);
                        while (discountPrice < 0)
                        {
                            Console.WriteLine("Сумма должна быть положительным целым числом");
                            int.TryParse(Console.ReadLine(), out discountPrice);
                        }
                            
                        Console.WriteLine("Введите значение скидки на товар в рублях");
                        int.TryParse(Console.ReadLine(), out discountValue);
                        while (discountValue <= 0)
                        {
                            Console.WriteLine("Скидка должна быть положительным целым числом");
                            int.TryParse(Console.ReadLine(), out discountValue);
                        }
                        discountValue = discountValue > price ? price : discountValue;

                        Param[0] = price;
                        Param[1] = discountValue;
                        Param[2] = discountPrice;

                        discountresult = discount.Calculate(Param, RubCalculate);

                        if (discountPrice < price) { discount.SetSellInformation($"На товар действует скидка в размере {discountresult}, так как его стоимость выше {discountPrice}"); } else
                        { discount.SetSellInformation($"На товар не действует скидка, так как его цена не превышает {discountPrice}"); }

                        loop = !loop;

                        break;

                    default:
                        Console.WriteLine("Выбирете тип скидки: 1-подарочная карта; 2-процентная скидка; 3-сумма от стоимости; 0-выход; ");
                        break;
                }
            }
            while (loop);

            discount.DiscountValue = discountValue;
           
            Console.WriteLine($"Вы успешно добавили новый продукт: {product.Name}, стоимость - {product.Price - discountresult}  {discount.GetSellInformation()}");
        }
    }
}
