using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BlazorApp.Data {

    [XmlRoot("HomesList")]
    public class HomesList {
        [XmlElement("Homes")]
        public List<Homes> Homes { get; set; }
    }

    public class Homes {
        [XmlElement("ID")]
        public int ID { get; set; }
        [XmlElement("Address")]
        public string Address { get; set; }
        [XmlElement("Beds")]
        public int Beds { get; set; }
        [XmlElement("Baths")]
        public int Baths { get; set; }
        [XmlElement("HouseSize")]
        public double HouseSize { get; set; }
        [XmlElement("LotSize")]
        public double LotSize { get; set; }
        [XmlElement("Price")]
        public decimal Price { get; set; }
        [XmlElement("Features")]
        public string Features { get; set; }
        [XmlElement("YearBuilt")]
        public int YearBuilt { get; set; }
        [XmlElement("Status")]
        public int Status { get; set; }
        [XmlElement("Photo")]
        public byte[] Photo { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class HomesProvider {
        private IWebHostEnvironment _env;
        public HomesProvider(IWebHostEnvironment env) {
            _env = env;
        }
        public List<Homes> GetDataFormXml() {           
            XmlSerializer formatter = new XmlSerializer(typeof(HomesList));
            using (FileStream fs = new FileStream("Homes.xml", FileMode.Open)) {
                HomesList homesList = (HomesList)formatter.Deserialize(fs);
                foreach (Homes home in homesList.Homes) {
                    home.PhotoUrl = home.ID.ToString() + ".jpg";
                    File.WriteAllBytes(_env.WebRootPath + "/Images/" + home.PhotoUrl, home.Photo);
                }
                return homesList.Homes;
            }
        }
    }

}
