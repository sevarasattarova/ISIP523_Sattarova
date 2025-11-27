using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        break;
                    case "2":
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
    }
}
