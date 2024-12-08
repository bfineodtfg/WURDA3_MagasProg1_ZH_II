using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WURDA3
{
    internal class FileManager
    {
        public string fileName { get; set; }
        public FileManager(string fileName) {
            this.fileName = fileName;
        }
        public List<Car> ReadAll()
        {
            List<Car> cars = new List<Car>();
            try
            {
                foreach (string item in File.ReadAllLines(fileName, Encoding.UTF8).Skip(1))
                {
                    cars.Add(new Car(item));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cars;
        }
        
    }
}
