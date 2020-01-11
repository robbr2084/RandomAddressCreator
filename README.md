# RandomAddressCreator
C# Class to create random addresses, phone numbers, emails, and company names

Includes data files containing first names, last names, streets, city, state, county, zip code, and company names 

Public Methods include:
   string ToTitleCase(string str)

   string RandomFirstName(int boyProbability = 50)

   string RandomBoy()

   string RandomGirl()

   string RandomLastName()

   string RandomFullName(int boyProbability = 50)

   string RandomStreet()

   string RandomAddress1()

   string RandomAddress2(int probability = 10)

   string RandomCompany()

   CityStateZipCounty RandomLocation()

   CityStateZipCounty RandomLocationByState(string stateAbbreviation)

   string RandomPhone()

   string RandomEmail()

   string RandomEmail(string firstName, string lastName)

   string RandomEmail(string firstName, string lastName, string domainName)

   string RandomEmail(string domainName)

   PersonalNameAddress RandomPersonalAddress()

   CompanyNameAddress RandomCompanyAddress()
 