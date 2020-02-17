using System;
using System.Collections.Generic;
using System.Text;

namespace facade
{
    class Pump
    {
        public readonly string pumpSID;
        private float refuelSpeed; // liters per second
        public bool vacant = true;

        public void Refuel(Cistern cistern, float ammount)
        {
            var fuelAmmount = cistern.Refuel(ammount);
            if ((fuelAmmount == 0) || (!vacant))
            {
                Console.WriteLine($"WARNING. Cannot refuel at pump {pumpSID}, {ammount} liters of \"{cistern.fuelType}\".");
                return;
            }
            vacant = false;
            Console.WriteLine($"Refueling at pump {pumpSID}, {ammount} liters of \"{cistern.fuelType}\".");
            System.Threading.Tasks.Task.Delay(Convert.ToInt32(ammount * 1000 / refuelSpeed)).ContinueWith(_ =>
            {
                Console.WriteLine($"Refueling at pump {pumpSID} finished.");
                vacant = true;
            });
            return; // redundant
        }

        public Pump(string sid, float rs)
        {
            pumpSID = sid;
            refuelSpeed = rs;
        }
    }

    class Cistern
    {
        public readonly string fuelType;
        public float fuelLeft;

        public float Refuel(float amount)
        {
            if (fuelLeft - amount <= 0)
                return 0.0f;
            fuelLeft -= amount;
            return fuelLeft;
        }

        public Cistern(string type, float cap)
        {
            fuelType = type;
            fuelLeft = cap;
        }
    }

    class Station
    {
        private List<Cistern> cisterns;
        private List<Pump> pumps;

        public void Refuel(string sid, string type, float ammount)
        {
            foreach (var c in cisterns)
            {
                if (type.Equals(c.fuelType))
                {
                    foreach (var p in pumps)
                    {
                        if (sid.Equals(p.pumpSID))
                        {
                            p.Refuel(c, ammount);
                            break;
                        }
                    }
                    return;
                }
            }
            // on error
            Console.WriteLine($"WARNING. Unknown refueling error, pump \"{sid}\", {ammount} liters of \"{type}\".");
        }

        public string Report()
        {
            string s = "            **** REPORT ****\n";
            foreach (var c in cisterns)
                s += $"\tCistern for \"{c.fuelType}\", {c.fuelLeft} liters left.\n";
            foreach (var p in pumps)
                s += $"\tPump \"{p.pumpSID}\", vacant: {p.vacant}\n";
            return s;
        }

        public Station(List<KeyValuePair<string, float>> c,
                       List<KeyValuePair<string, float>> p)
        {
            cisterns = new List<Cistern>();
            pumps = new List<Pump>();
            foreach (var val in c) cisterns.Add(new Cistern(val.Key, val.Value));
            foreach (var val in p) pumps.Add(new Pump(val.Key, val.Value));
        }
    }
}
