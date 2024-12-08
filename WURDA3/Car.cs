using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WURDA3
{
    internal class Car
    {
        public Car(string line) {
            string[] temp = line.Split(';');
            rendszam = temp[0];
            forgalmiErvenyesseg = DateTime.Parse(temp[1]);
            gyarto = temp[2];
            allapot = temp[3];
            klima = temp[4];
            if (temp[5].Trim().ToLower() != "manuális" && temp[5].Trim().ToLower() != "automata")
                throw new Exception("Hibás váltó típus: " + temp[5]);
            automata = true;
            if (temp[5].Trim().ToLower() != "manuális")
                automata = false;
            ar = int.Parse(temp[6]);
            evjarat = int.Parse(temp[7]);
        }
        public string rendszam { get; set; }
        public DateTime forgalmiErvenyesseg { get; set; }
        public string gyarto { get; set; }
        public string allapot { get; set; }
        public string klima { get; set; }
        public bool automata { get; set; }
        public int ar { get; set; }
        public int evjarat { get; set; }
        public override string ToString()
        {
            return $"{rendszam} - {forgalmiErvenyesseg.ToString("yyyy MMMM dd")} - {gyarto} - {allapot} - {klima} - {automata} - {ar} - {evjarat}";
        }
    }
}
