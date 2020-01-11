using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Immeo;
using System.Globalization;

namespace TestRandomAddresses
{
    class Program
    {
        static NameAddressCreator addresses = new NameAddressCreator("C:\\temp"); // PATH TO DATA FILES ... CHANGE THIS TO THE PATH WHERE THE DATA FILES RESIDE
		
        static void Main(string[] args)
        {

            for (int lp = 0; lp < 100; lp++)
            {
                Console.WriteLine("First Name = " + addresses.RandomFirstName());
                Console.WriteLine("Last Name = " + addresses.RandomLastName());
                Console.WriteLine("Full Name = " + addresses.RandomFullName());
                Console.WriteLine("Phone = " + addresses.RandomPhone());
                Console.WriteLine("Email (overload 1) = " + addresses.RandomEmail());
                string first = addresses.RandomFirstName();
                string last = addresses.RandomLastName();
                string company = addresses.RandomCompany();
                Console.WriteLine("Email (overload 2) = " + addresses.RandomEmail(first, last));
                Console.WriteLine("Email (overload 3) = " + addresses.RandomEmail(first, last, company));

            }


            for (int lp = 0; lp < 100; lp++)
            {
                PersonalNameAddress add = addresses.RandomPersonalAddress();
                Console.WriteLine(add.FullName + " | " + add.Address1 + " | " + add.Address2 + " | " + add.City + ", " + add.State + " " + add.Zip + " | " + add.Phone + " | " + add.Email);
            }

            for (int lp = 0; lp < 100; lp++)
            {
                CompanyNameAddress add = addresses.RandomCompanyAddress();
                Console.WriteLine(add.CompanyName + " | " + add.FullName + " | " + add.Address1 + " | " + add.Address2 + " | " + add.City + ", " + add.State + " " + add.Zip + " | " + add.Phone + " | " + add.Email);
            }
            Console.ReadKey();
        }
    }
}
