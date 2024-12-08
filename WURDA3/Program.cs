using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WURDA3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager file = new FileManager("MP1_ZH2_autok.csv");
            //1. feladat
            List<Car> cars = file.ReadAll();
            //2. feladat
            CarManager manager = new CarManager(cars);
            manager.LejartForgalmi();
            //3.-4. feladat
            Console.WriteLine("4. feladat: Add meg az autó típusát");
            string gyarto = Console.ReadLine();
            Console.WriteLine("Add meg az autó váltóját (manuális/automata)");
            string valto = Console.ReadLine();
            try
            {
                if (!manager.F3_searchTrueFalse(cars, gyarto, valto))
                    Console.WriteLine("Sajnos nincs ilyen jármű");
                else
                {
                    manager.Search(gyarto, valto);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //5.-6. feladat
            string gyartoKereses = "Nissan";
            List<Car> gyartokAutoi = new List<Car>();
            manager.F5_gyarto(cars, gyartokAutoi, gyartoKereses);
            //7. feladat
            Console.WriteLine("Adja meg a kitörölni szánt autó rendszámát");
            string rendszam = Console.ReadLine().Trim().ToUpper();
            try
            {
                manager.DeleteOneCar(rendszam);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //8. feladat a managerben
            Console.WriteLine(manager.F8_amortizacio(cars.Last()));
            //+1
            manager.PlusOne();




            Console.ReadKey();
        }
       
    }
}
