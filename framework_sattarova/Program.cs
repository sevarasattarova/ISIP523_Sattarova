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
                        Console.WriteLine("Ошибка лол");
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

            if ( login == " " || password == " " || passwordVerification == " ")
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
    }
    }

