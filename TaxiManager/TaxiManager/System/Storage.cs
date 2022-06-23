using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace TaxiManager
{
    public class Storage
    {
        public void SaveDrivers(ObservableCollection<Driver> drivers)
        {
            string json = JsonConvert.SerializeObject(drivers);
            File.WriteAllText("drivers.json", json);
        }

        public ObservableCollection<Driver> LoadDrivers()
        {
            if(File.Exists("drivers.json") == true)
            {
                string json = File.ReadAllText("drivers.json");
                return JsonConvert.DeserializeObject<ObservableCollection<Driver>>(json);
            }
            else
            {
                return new ObservableCollection<Driver>();
            }
        }
    }
}
