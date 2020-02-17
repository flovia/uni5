using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace facade
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cisterns = new List<KeyValuePair<string, float>>();
            cisterns.Add(new KeyValuePair<string, float>("DT", 174.78f));
            cisterns.Add(new KeyValuePair<string, float>("95", 130.04f));
            cisterns.Add(new KeyValuePair<string, float>("98", 0.0f));
            var pumps = new List<KeyValuePair<string, float>>();
            pumps.Add(new KeyValuePair<string, float>("Main 1", 14.0f));
            pumps.Add(new KeyValuePair<string, float>("Main 2", 12.6f));
            pumps.Add(new KeyValuePair<string, float>("Reserve", 9.3f));

            var st = new Station(cisterns, pumps);

            st.Refuel("Main 1", "DT", 90f);
            st.Refuel("Main 2", "95", 12.5f);
            await Task.Delay(500);
            st.Refuel("Reserve", "DT", 8.3f);
            await Task.Delay(2500);
            st.Refuel("Main 1", "95", 38f);
            st.Refuel("Main 2", "DT", 73f);
            st.Refuel("Main 2", "98", 5f);
            await Task.Delay(3500);
            st.Refuel("Main", "DT", 35f);
            st.Refuel("Main 1", "95", 13f);
            await Task.Delay(1000);
            st.Refuel("Main 2", "98", 5f);
            st.Refuel("Reserve", "95", 13f);
            st.Refuel("Main 1", "95", 10f);
            await Task.Delay(5000);
            st.Refuel("Main", "DT", 35f);
            st.Refuel("Main 1", "95", 13f);
            await Task.Delay(1500);
            st.Refuel("Main 2", "98", 5f);
            st.Refuel("Main 2", "DT", 56f);

            await Task.Delay(3000);
            st.Refuel("Main 2", "95", 8f);
            Console.WriteLine(st.Report());
            await Task.Delay(1500);
        }
    }
}
