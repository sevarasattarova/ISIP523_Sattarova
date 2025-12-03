using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace framework_sattarova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chois;
            while (true)
            {
                Console.WriteLine("-----МЕНЮ МАГАЗИНА----");
                Console.WriteLine("1. Просмотр товаров");
                Console.WriteLine("2. Регистрация пользователя");
                Console.WriteLine("3. Вход в аккаунт");
                Console.WriteLine("4. Вывод всех пвз");
                chois = Console.ReadLine();

                switch (chois)
                {
                    case "1":
                        Print();
                        break;
                    case "2":
                        Registracion();
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Ошибка");
                        break;
                }
            }
        }
        static void Print()
        {
            List<products> products = Core.Context.products.ToList();

            Console.WriteLine("Наши товары");

            foreach (products product in products)
            {
                Console.WriteLine($"{product.ID}, {product.name}, {product.price}, {product.kolvo}");
            }

            Console.WriteLine("-----------------------------------------");


        }
        static void Registracion()
        {
            Console.WriteLine("Введите  Login");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            Console.WriteLine("Введите пароль для проверки");
            string passwordVerification = Console.ReadLine();

            if (login == " " || password == " " || passwordVerification == " ")
            {
                Console.WriteLine("Все поля должны быть заполнены");
                return;
            }

            if (password != passwordVerification)
            {
                Console.WriteLine("Пароли должны быть одинаковыми");
                return;
            }

            bool LoginProverka = Core.Context.person.Any(u => u.name == login);
            if (LoginProverka)
            {
                Console.WriteLine("Пользователь с таким логином уже есть");
                return;
            }
            person newUser = new person
            {
                name = login,
                password = password,

            };

            Core.Context.person.Add(newUser);
            Core.Context.SaveChanges();

            Console.WriteLine("Вы успешно зарегестрировались");
        }
        static void Autification()
        {
            Console.WriteLine("Введите логин");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            if (login == " " || password == " ")
            {
                Console.WriteLine("Значение не должно быть пустым");
                return;
            }
            person user = Core.Context.person.FirstOrDefault(u => u.name == login);

            if (user == null)
            {
                Console.WriteLine("Нет такого пользователя");
                return;
            }
            if (user.password != password)
            {
                Console.WriteLine("Неправильный пароль");
                return;
            }
            Console.WriteLine("Авторизация прошла успешно");

            UserMenu(user);
        }
        static void OutputAllPVZ()
        {
            List<pvs> pvz = Core.Context.pvs.ToList();

            Console.WriteLine("Наши ПВЗ");

            foreach (pvs pvzs in pvz)
            {
                Console.WriteLine($"{pvzs.ID}, {pvzs.name}, {pvzs.addres}");
            }
            Console.WriteLine("----------------------------------------");
        }
        static void UserMenu(person user)
        {
            Console.WriteLine($"Личный кабинет{user.name}");
            while (true)
            {
                Console.WriteLine("1. Посмотреть товары");
                Console.WriteLine("2. Добавить товар");
                Console.WriteLine("3. Посмотреть свою корзину");
                Console.WriteLine("4. Заказать товар из корзины");
                Console.WriteLine("5. История заказов");
                Console.WriteLine("6. Выйти из акка");

                string chois = Console.ReadLine();



                switch (chois)
                {
                    case "1":
                        Print();
                        break;
                    case "2":
                        AddProduct(user);
                        break;
                    case "3":
                        LookBasket(user);
                        break;
                    case "4":
                        BueBasket(user);
                        break;
                    case "5":
                        break;
                    case "6":
                        Console.WriteLine("Выйти из аккаунта");
                        return;
                    default:
                        Console.WriteLine("Нет такого попробуй снова");
                        break;

                }

            }
        }
        static void AddProduct(person user)
        {
            Print();
            Console.WriteLine("Введите ID товара для добавления");
            int productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите кол-во:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            products product = Core.Context.products.FirstOrDefault(p => p.ID == productID);
            if (product == null)
            {
                Console.WriteLine("Товар с таким ID не найден");
                return;
            }
            if (quantity > product.kolvo)
            {
                Console.WriteLine("Недостаточно товаров на складе");
                return;
            }
            if (quantity <= 0)
            {
                Console.WriteLine("Товара должно быть >0");
                return;
            }

            corzina existingBasket = Core.Context.corzina
                .FirstOrDefault(b => b.userID == user.ID && b.productID == product.ID);


            corzina newBasket = new corzina
            {
                userID = user.ID,
                productID = product.ID,
            };
            Core.Context.corzina.Add(newBasket);
            Console.WriteLine("Мы добавили товар");
            Core.Context.SaveChanges();
        }
        static void LookBasket(person user)
        {
            List<corzina> baskets = Core.Context.corzina
                .Where(b => b.userID == user.ID)
                .ToList();

            Console.WriteLine("Ваша корзина");
            if (!baskets.Any())
            {
                Console.WriteLine("Корзина пуста");
                return;
            }

            decimal totalPrice = 0;
            int itemNumber = 1;

            foreach (corzina basket in baskets)
            {
                Console.WriteLine($"{basket.products.name}");
                Console.WriteLine($"Цена:{basket.products.price}");
                Console.WriteLine($"Кол-во:{basket.kolvo}");
                Console.WriteLine($"Суммa:{basket.products.price * basket.kolvo}");

                totalPrice += basket.products.price * basket.kolvo;
                itemNumber++;
            }
            Console.WriteLine($"Общая сумма:{totalPrice}");
            Console.WriteLine("--------------------------------------------------------------------");
        }
        static void BueBasket(person user)
        {
            Console.WriteLine("Оформление заказа");
            LookBasket(user);
            while (true)
            {
                Console.WriteLine("1. купить всю корзину");
                Console.WriteLine("2. купить товар из корзины");
                Console.WriteLine("3. Вернутся назад");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        break;
                    case "2":
                        
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Ошибка");
                        break;
                }
            }
        }
    }
}


   

      

