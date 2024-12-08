using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WURDA3
{
    internal class CarManager
    {
        List<Car> cars;
        public CarManager(List<Car> cars)
        {
            this.cars = cars;
        }
        public void LejartForgalmi()
        {
            List<Car> cars = this.cars.Where(car => car.forgalmiErvenyesseg < DateTime.UtcNow).ToList();
            foreach (Car item in cars)
            {
                Console.WriteLine($"{item.rendszam} - {item.gyarto} ({item.forgalmiErvenyesseg.ToString("yyyy MMMM dd")})");
            }
        }
        public bool F3_searchTrueFalse(List<Car> listForNoReason, string gyarto, string valto)
        {
            if (valto.Trim().ToLower() != "manuális" && valto.Trim().ToLower() != "automata")
                throw new Exception("Hibás váltó típus: " + valto);
            bool automata = true;
            if (valto.Trim().ToLower() != "manuális")
                automata = false;
            int found = listForNoReason.Where(car => car.gyarto == gyarto && car.automata == automata).Count();
            if (found > 0)
                return true;
            return false;
        }
        public void Search(string gyarto, string valto)
        {
            bool automata = true;
            if (valto.Trim().ToLower() != "manuális")
                automata = false;
            List<Car> cars = this.cars.Where(car => car.gyarto == gyarto && car.automata == automata).ToList();
            foreach (Car item in cars)
            {
                Console.WriteLine($"{item.rendszam} - váltó: {valto} - {item.gyarto} - {item.ar}");
            }
        }
        public void F5_gyarto(List<Car> listForNoReason, List<Car> anotherList, string gyarto)
        {
            //why?
            anotherList.Clear();
            anotherList = listForNoReason.Where(car => car.gyarto == gyarto).ToList();
            //why not return?
            //a list is not even value type, so I don't need to use the ref keyword
            foreach (Car item in anotherList)
            {
                string valto = "manuális";
                if (item.automata)
                    valto = "automata";
                Console.WriteLine($"{item.rendszam} - {item.allapot}, {valto} váltó, évjárat : {item.evjarat}");
            }
            double avgPrice = anotherList.Average(car => car.ar);
            Console.WriteLine($"Átlag ár: {avgPrice}");
        }
        public void DeleteOneCar(string renszam) {
            int count = cars.Where(car => car.rendszam == renszam).Count();
            if (count < 1)
                throw new Exception("Nem található autó a megadott rendszámmal: " + renszam);
            if (count > 1)
                throw new Exception("Több autó található a megadott rendszámmal: " + renszam);
            cars.Remove(cars.Where(car => car.rendszam == renszam).First());
            Console.WriteLine($"Sikeresen törlte a járművet {renszam} rendszámmal");
        }
        public int F8_amortizacio(Car oneCar) {
            int price = 0;
            int klimeFelar = 0;
            switch (oneCar.klima.ToLower().Trim()) {
                case "nincs":
                    klimeFelar = 0;
                    break;
                case "manuális":
                    klimeFelar = 120000;
                    break;
                case "digitális":
                    klimeFelar = 245000;
                    break;
                case "digitális-többzónás":
                    klimeFelar = 320000;
                    break;
            }
            double amortizacio = 0;
            switch (oneCar.allapot.ToLower().Trim())
            {
                case "újszerű":
                    amortizacio = 0.11;
                    break;
                case "megkímélt":
                    amortizacio = 0.20;
                    break;
                case "sérült":
                    amortizacio = 0.35;
                    break;
                case "hibás":
                    amortizacio = 0.42;
                    break;
            }
            //no month/day is taken into consideration!
            int eletkorEvekben = DateTime.UtcNow.Year - oneCar.evjarat;
            //the calculation is wrong!
            price = (int)Math.Round(oneCar.ar * Math.Pow(amortizacio, eletkorEvekben)) + klimeFelar;
            return price;
        }
        public void PlusOne() {
            Dictionary<string, Car> pricePerManufacturer = new Dictionary<string, Car>();
            foreach (Car item in cars)
            {
                if (pricePerManufacturer.ContainsKey(item.gyarto))
                {
                    if (item.ar > pricePerManufacturer[item.gyarto].ar)
                    {
                        pricePerManufacturer[item.gyarto].ar = item.ar;
                    }
                }
                else
                {
                    pricePerManufacturer.Add(item.gyarto, item);
                }
            }
            foreach (KeyValuePair<string, Car> item in pricePerManufacturer)
            {
                Console.WriteLine($"{item.Key} - {item.Value.ar}");
            }
        }
    }
}
