using System;

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

        private static void CreateProduct()
        {
            var product = new Product();

            bool loop = true;

            bool percentDiscount = false;

            int discountPrice = 0;

            int discountValue = 0;

            Console.WriteLine("Введите название продукта");

            product.Name = Console.ReadLine();

            Console.WriteLine("Введите стоимость продукта");

            int.TryParse(Console.ReadLine(), out var price);

            while (price == 0)
            {
                Console.WriteLine("Стоимость продукта не была введена или она была введена с ошибкой. Пожалуйста, введите стоимость продукта ещё раз");

                int.TryParse(Console.ReadLine(), out price);
            }

            product.Price = price;

            Console.WriteLine("Выбирете тип скидки: 1-подарочная карта; 2-процентная скидка; 3-сумма от стоимости; 4-другое");

            do
            { 
                int.TryParse(Console.ReadLine(), out var discounttype);
                switch (discounttype)
                {
                    case 1:
                        Console.WriteLine("Введите значение скидки на товар в рублях");

                        int.TryParse(Console.ReadLine(), out discountValue);

                        loop = !loop;

                        break;
                    case 2:
                        percentDiscount = true;

                        Console.WriteLine("Введите значение скидки на товар (в % от общей стоимости)");

                        int.TryParse(Console.ReadLine(), out discountValue);

                        while (discountValue > 100)
                        {
                            Console.WriteLine("Значение скидки не может быть больше 100");

                            int.TryParse(Console.ReadLine(), out discountValue);
                            
                        }

                        loop = !loop;

                        break;
                    case 3:
                        Console.WriteLine("Введите сумму при которой выдается скидка");

                        int.TryParse(Console.ReadLine(), out discountPrice);

                        Console.WriteLine("Введите значение скидки на товар в рублях");

                        int.TryParse(Console.ReadLine(), out discountValue);

                        loop = !loop;

                        break;

                    case 4:
                        Console.WriteLine("Выберите тип скидки 1-в рублях 2-в процентах");

                        int.TryParse(Console.ReadLine(), out int typevalue);

                        percentDiscount = typevalue == 2 ? true : false;

                        Console.WriteLine("Введите значение скидки на товар");

                        int.TryParse(Console.ReadLine(), out discountValue);

                        while (discountValue > 100 && typevalue == 2)
                        {
                            Console.WriteLine("Значение скидки не может быть больше 100");

                            int.TryParse(Console.ReadLine(), out discountValue);

                        }

                        Console.WriteLine("Введите сумму при которой выдается скидка (0 если не требуется)");

                        int.TryParse(Console.ReadLine(), out discountPrice);

                        loop = (typevalue == 2 || typevalue == 1) ? !loop : loop;                        
                        break;

                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }
            while (loop);

            var discount = new Discount(product.Price, percentDiscount, discountPrice);

            discount.DiscountValue = discountValue;

            Console.WriteLine("Введите дату начала действия скидки");

            DateTime.TryParse(Console.ReadLine(), out var startSellDate);

            if (startSellDate != DateTime.MinValue)
            {
                discount.StartSellDate = startSellDate;
            }

            Console.WriteLine("Введите дату окончания действия скидки");

            DateTime.TryParse(Console.ReadLine(), out var endSellDate);

            if (endSellDate != DateTime.MinValue)
            {
                discount.EndSellDate = endSellDate;
            }

            Console.WriteLine($"Вы успешно добавили новый продукт: {product.Name}, стоимость - {product.Price}р. {discount.GetSellInformation()}");
        }
    }
}
