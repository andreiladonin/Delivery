using SamgtuProject;
using System.Collections.Immutable;
using System.Linq;

Console.WriteLine();
Console.WriteLine();

while (true)
{
    Console.WriteLine("Текущее время : " + DateTime.Now);
    Console.WriteLine("Опции программы \n\t (1) - добавить заказ " +
        "\n\t (2) - список заказов у компании" +
        " \n\t (3) - рапсределить заказы \n\t " +
        "(4) - удалить заказ у курьеа \n\t " +
        "(5) - Работа с курьерами \n\t " +
        "(0) - завершить работу ");

    string answer = Console.ReadLine();
    if (answer == null || answer[0] == '0')
    {
        break;
    } else if (answer[0] == '1')
    {
        if (Company.Couriers.Count == 0)
        {
            Console.WriteLine("Добавьте сначала курьеров");
            continue;
        }
        OptionProgram.AddOrder();
    } else if (answer[0] == '2') {

        if (Company.Orders.Count > 0)
        {
            foreach (var order in Company.Orders)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("Заказов нет");

        }
    } else if (answer[0] == '3')
    {
        if (Company.Orders.Count > 0)
        {
            Company.DistributeOrders();

            foreach (var courier in Company.Couriers)
            {
                courier.PrintOrders();
            }

            Console.WriteLine($"Общая прибыль компании {Company.SumTotal} рублей.");
        }
        else
            Console.WriteLine("Нет заказов");
    } else if (answer[0] == '4') {
        OptionProgram.DeleteOrder();
    }
    else if (answer[0] == '5')
    {
        OptionProgram.OptionCourier();
    }
    
}
