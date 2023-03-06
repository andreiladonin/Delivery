using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public class OptionProgram
    {
        private static int idCouriers = 0;
        public static void OptionCourier()
        {
            while (true)
            {
                Console.WriteLine("Рздел с опции для курьеров.");
                Console.WriteLine("\t (n) - Новый курьер");
                Console.WriteLine("\t (l) - Лист курьеров");
                Console.WriteLine("\t (o) - Список заказов у курьера");
                Console.WriteLine("\t (d) - Удалить курьера");
                Console.WriteLine("\t (s) - Загрузить курьеров");
                Console.WriteLine("\t (q) - Назад");
                string answer = Console.ReadLine();
                if (answer[0] == 'n')
                {
                    Console.WriteLine("Настройка опции курьера");

                    Console.WriteLine("Введите максимальную грузоподЪемность курьера");
                    double weight = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Позиция X:");
                    double pointX = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Позиция Y:");
                    double pointY = Double.Parse(Console.ReadLine());

                    if (weight > 0 && weight <= FootCourier.MAX_WEIGHT)
                    {
                        ++idCouriers;
                        var courier = new FootCourier(weight, "Unkow " + idCouriers, new Point(pointX, pointY));
                        Company.Couriers.Add(courier);
                        Console.WriteLine("Создан пеший курьер");
                    }
                    else if (weight > FootCourier.MAX_WEIGHT)
                    {
                        ++idCouriers;
                        var courier = new TransportCourier(weight, "Unkow " + idCouriers, new Point(pointX, pointY));
                        Company.Couriers.Add(courier);

                        Console.WriteLine("Создан курьер на транспорте");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка создание курьеров");
                    }

                }
                else if (answer[0] == 'l')
                {
                    foreach (var courier in Company.Couriers)
                    {

                        Console.Write(new String('-', 35) + "Список курьеров");
                        Console.Write(new String('-', 35) + '\n');
                        Console.WriteLine(courier);
                    }
                }
                else if (answer[0] == 'd')
                {

                    Console.WriteLine("Введите ID курерьера: ");
                    string strId = Console.ReadLine();
                    if (Int32.TryParse(strId, out int result))
                    {
                        var courier = Company.Couriers.First(x => x.Id == result);
                        if (courier != null)
                        {
                            var orders = courier.GetOrders();
                            Company.Orders.AddRange(orders);
                            Company.Couriers.Remove(courier);
                            Console.WriteLine("Курьер удален");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Надо перераспределить заказы");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Курьер не найден");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка введеного Id");
                    }
                } else if (answer[0] == 'o')
                {

                    Console.WriteLine("Введите ID курерьера: ");
                    string strId = Console.ReadLine();
                    if (Int32.TryParse(strId, out int result))
                    {
                        var courier = Company.Couriers.First(x => x.Id == result);
                        if (courier != null)
                        {
                            courier.PrintOrders();

                        }
                        else
                        {
                            Console.WriteLine("Курьер не найден");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка введеного Id");
                    }
                } 
                else if (answer[0] == 's')
                {
                    Console.WriteLine("Введите имя файла....");

                    string? fileName = Console.ReadLine();

                    if (fileName != null && File.Exists(fileName))
                    {
                        string data = File.ReadAllText(fileName);

                        JArray lstCourier = JArray.Parse(data);

                        foreach (JObject courierJson in lstCourier)
                        {
                            double weight = Convert.ToDouble(courierJson["carryingCapacity"]);

                            if (weight > 0 && weight <= FootCourier.MAX_WEIGHT)
                            {
                               
                                var courier = new FootCourier(Convert.ToDouble(courierJson["carryingCapacity"]), courierJson["name"].ToString(), new Point(Convert.ToDouble(courierJson["location"]["X"]), Convert.ToDouble(courierJson["location"]["Y"])));
                                Company.Couriers.Add(courier);
                                Console.WriteLine("Создан пеший курьер");
                            }
                            else if (weight > FootCourier.MAX_WEIGHT)
                            {
                                
                                var courier = new TransportCourier(Convert.ToDouble(courierJson["carryingCapacity"]), courierJson["name"].ToString(), new Point(Convert.ToDouble(courierJson["location"]["X"]), Convert.ToDouble(courierJson["location"]["Y"])));
                                Company.Couriers.Add(courier);

                                Console.WriteLine("Создан курьер на транспорте");
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Курьеры успешно загружены");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Проверьте имя файла, либо его существование");
                    }
                }
                else if (answer[0] == 'q')
                {
                    break;
                }
            }

        }

        public static void AddOrder()
        {
            Console.WriteLine("Введите вес заказа ");

            double weight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("ИЗ Позиция X:");
            double fromPointX = Double.Parse(Console.ReadLine());
            Console.WriteLine("ИЗ Позиция Y:");
            double fromPointY = Double.Parse(Console.ReadLine());
            Console.WriteLine("В Позиция X:");
            double toPointX = Double.Parse(Console.ReadLine());
            Console.WriteLine("В Позиция Y:");
            double toPointY = Double.Parse(Console.ReadLine());


            Order order = new(weight, new Point(fromPointX, fromPointY), new Point(toPointX, toPointY));

            Console.WriteLine("Заказ создан");
            Console.WriteLine("\t " + order);
            Company.Orders.Add(order);
        }

        internal static void DeleteOrder()
        {
            Console.WriteLine("Введите ID заказа");

            string answer = Console.ReadLine();
            if (int.TryParse(answer, out int id))
            {
                Courier courier = null;
                foreach (var item  in Company.Couriers)
                {
                    var order = item.GetOrders()
                        .Where(x => x.Id == id).FirstOrDefault();
                    if (order != null) courier = item;

                }

                courier.GetOrders().RemoveAll(x => x.Id == id);
                Console.WriteLine($"Заказ удален у Курьера {courier.Name}");
            }
        }
    }
}
