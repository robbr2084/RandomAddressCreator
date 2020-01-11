using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
//using System.Web.Hosting;

namespace Immeo
{
    public class NameAddressCreator
    {
        public List<string> Boys;
        public List<string> Girls;
        public List<string> Families;
        public List<string> Streets;
        public  List<string> Headings;
        public List<string> StreetTypes;
        public List<string> Address2Types;
        public List<CityStateZipCounty> Cities;
        public List<string> Companies;
        public List<string> Letters;
        private string DataPath;
        public  Random Rnd;
        private TextInfo TextConverter = CultureInfo.CurrentCulture.TextInfo;

        public NameAddressCreator(string pathToDataFiles)
        {
            DataPath = pathToDataFiles;

            //DataPath = HostingEnvironment.MapPath(DataPath);

            Boys = File.ReadAllLines(Path.Combine(DataPath, "boys1000.csv")).ToList();
            Girls = File.ReadAllLines(Path.Combine(DataPath, "girls1000.csv")).ToList();
            Families = File.ReadAllLines(Path.Combine(DataPath, "family1000.csv")).ToList();
            Streets = File.ReadAllLines(Path.Combine(DataPath, "streetnames.csv")).ToList();
            Companies = File.ReadAllLines(Path.Combine(DataPath, "companies.csv")).ToList();

            Letters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StreetTypes = new List<string>() { "St", "Street", "Ave", "Avenue", "Pl", "Place", "Pkwy", "Parkway", "Ln", "Lane", "Dr", "Drive", "Way", "Circle", "Hill" };
            Address2Types = new List<string>() { "Ste", "Suite", "Bldg", "Building", "Box", "Route", "Ext" };
            Headings = new List<string>() { "N", "S", "E", "W", "North", "South", "East", "West" };

            List<string> lines = File.ReadAllLines(Path.Combine(DataPath, "citystatezipcounty.csv")).ToList();
            Cities = new List<CityStateZipCounty>();

            char[] seperators = new char[] { ',' };
            foreach (var line in lines)
            {
                string[] items = line.Split(seperators).ToArray();

                CityStateZipCounty location = new CityStateZipCounty()
                {

                    Zip = Convert.ToInt32(items[0]).ToString("00000"),
                    City = ToTitleCase(items[1]),
                    State = items[2],
                    County = ToTitleCase(items[3])
                };
                Cities.Add(location);
            }


            Rnd = new Random(DateTime.Now.Second * DateTime.Now.Minute * DateTime.Now.Millisecond);
        }

        public string ToTitleCase(string str)
        {
            str = str.ToLower();
            str = TextConverter.ToTitleCase(str);
            return str;
        }

        public string RandomFirstName(int boyProbability = 50)
        {
            if (Rnd.Next(100) > boyProbability)
            {
                return RandomBoy();
            }
            return RandomGirl();
        }
        
        public string RandomBoy()
        {
            return Boys[Rnd.Next(Boys.Count - 1)];
        }

        public string RandomGirl()
        {
            return Girls[Rnd.Next(Girls.Count - 1)];
        }

        public string RandomLastName()
        {
            return Families[Rnd.Next(Families.Count - 1)];
        }

        public string RandomFullName(int boyProbability = 50)
        {
            string fullName;
            if (Rnd.Next(100) > boyProbability)
            {
                fullName = RandomBoy();
            }
            else
            {
                fullName = RandomGirl();
            }
            fullName += " " + RandomLastName();
            return fullName;
        }

        public string RandomStreet()
        {
            return Streets[Rnd.Next(Streets.Count - 1)];
        }

        public string RandomAddress1()
        {
            string address1 = Rnd.Next(10000).ToString() + " " + Headings[Rnd.Next(Headings.Count - 1)] + " " + Streets[Rnd.Next(Streets.Count - 1)] + " " + StreetTypes[Rnd.Next(StreetTypes.Count - 1)];
            return address1;
        }

        public string RandomAddress2(int probability = 10)
        {
            if (Rnd.Next(100) > probability) return string.Empty;

            string address2 = Address2Types[Rnd.Next(Address2Types.Count - 1)] + " #" + Rnd.Next(1000).ToString();
            return address2;
        }

        public string RandomCompany()
        {
            return Companies[Rnd.Next(Companies.Count - 1)];
        }

        public CityStateZipCounty RandomLocation()
        {
            return Cities[Rnd.Next(Cities.Count - 1)];
        }

        public CityStateZipCounty RandomLocationByState(string stateAbbreviation)
        {
            stateAbbreviation = stateAbbreviation.ToUpper();
            List<CityStateZipCounty> byState = Cities.Where(x => x.State == stateAbbreviation).ToList();
            return byState[Rnd.Next(byState.Count - 1)];
        }

        public string RandomPhone()
        {
            string phone = (200 + Rnd.Next(799)).ToString("000") + "-" + (200 + Rnd.Next(799)).ToString("000") + "-" + Rnd.Next(9999).ToString("0000");
            return phone;
        }

        public string RandomEmail()
        {
            string email = Letters[Rnd.Next(Letters.Count - 1)] + RandomLastName();
            email += "@" + Companies[Rnd.Next(Companies.Count - 1)];
            email = email.Replace(" ", "-") + ".com";
            email = email.ToLower();
            return email;
        }

        public string RandomEmail(string firstName, string lastName)
        {
            string email = firstName.Substring(0, 1) + lastName;
            email += "@" + Companies[Rnd.Next(Companies.Count - 1)];
            email = email.Replace(" ", "-") + ".com";
            email = email.ToLower();
            return email;
        }
        public string RandomEmail(string firstName, string lastName, string domainName)
        {
            string email = firstName.Substring(0, 1) + lastName;
            email += "@" + domainName;
            email = email.Replace(" ", "-") + ".com";
            email = email.ToLower();
            return email;
        }

        public string RandomEmail(string domainName)
        {
            string email = domainName;
            email = email.Replace(" ", "-") + ".com";
            email = email.ToLower();
            return email;
        }

        public PersonalNameAddress RandomPersonalAddress()
        {
            CityStateZipCounty loc = RandomLocation();

            PersonalNameAddress address = new PersonalNameAddress()
            {
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                Address1 = RandomAddress1(),
                Address2 = RandomAddress2(),
                City = loc.City,
                State = loc.State,
                Zip = loc.Zip,
                County = loc.County,
                Phone = RandomPhone(),
            };
            address.FullName = address.FirstName + " " + address.LastName;
            address.Email = RandomEmail(address.FirstName, address.LastName);

            return address;
        }
        public CompanyNameAddress RandomCompanyAddress()
        {
            CityStateZipCounty loc = RandomLocation();

            CompanyNameAddress address = new CompanyNameAddress()
            {
                CompanyName = RandomCompany(),
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                Address1 = RandomAddress1(),
                Address2 = RandomAddress2(),
                City = loc.City,
                State = loc.State,
                Zip = loc.Zip,
                County = loc.County,
                Phone = RandomPhone(),
            };
            address.FullName = address.FirstName + " " + address.LastName;
            address.Email = RandomEmail(address.FirstName, address.LastName, address.CompanyName);

            return address;
        }

    }

    public class PersonalNameAddress
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class CompanyNameAddress
    {
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class CityStateZipCounty
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
    }


}