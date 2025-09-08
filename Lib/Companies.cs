using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    public class CompaniesModel
    {
        public int CompanyID { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string Note { get; set; }
    }

    public class Companies
    {
        List<CompaniesModel> CompaniesList = null;
        public Companies()
        {
            CompaniesList = new List<CompaniesModel>();


            CompaniesList.Add(new CompaniesModel { CompanyID = 101, CompanyTitle = "IceSmoke", CompanyAddress = "Shop#A-29,Sahara Arcade,main boulevard,Bahria enclave, Islamabad", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1012, CompanyTitle = "DSADsl kdlsa", CompanyAddress = "Shop#A-29,Sahara Arcade,main boulevard,Bahria enclave, Islamabad", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1013, CompanyTitle = "   Elegance Pharmacy               Medical Store", CompanyAddress = "65B Block-A Latifabad no 6 ,hyderabad ContactNo :03180303078 , 03111995559", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1014, CompanyTitle = "   Elegance Pharmacy               Medical Store", CompanyAddress = "65B Block-A Latifabad no 6 ,hyderabad ContactNo :03180303078 , 03111995559", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1015, CompanyTitle = "Noman Traders By NMH", CompanyAddress = "65B Block-A Latifabad no 6 ,hyderabad ContactNo :03180303078 , 03111995559", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1016, CompanyTitle = "Fruitanic", CompanyAddress = "65B Block-A Latifabad no 6 ,hyderabad ContactNo :03180303078 , 03111995559", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1017, CompanyTitle = "Pharma Company", CompanyAddress = "65B Block-A Latifabad no 6 ,hyderabad ContactNo :03180303078 , 03111995559", CompanyPhone = "0317-0000623" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1018, CompanyTitle = "Brohi Balochistan Restuarent ", CompanyAddress = "Alhamd Center Opp Board Office unit no 6 Latifabad ", CompanyPhone = "" });
            //  CompaniesList.Add(new CompaniesModel { CompanyID = 1019, CompanyTitle = "Riksha Biryani ", CompanyAddress = "Near Mujahid Hotel Jama Masjid Road Unit No 8 (Hyd) ", CompanyPhone = "03127660027 03352716657" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1020, CompanyTitle = "Riksha Biryani ", CompanyAddress = "Jama Masjid Road near Meri chai ka hotel Unit No 8 (Hyd) ", CompanyPhone = "03127660027 03352716657" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1021, CompanyTitle = "Riksha Biryani ", CompanyAddress = "Gari khata chock Qilla ki chahari Shelter Plaza Wali shop ", CompanyPhone = "03127660027 03352716657" });

            CompaniesList.Add(new CompaniesModel { CompanyID = 1022, CompanyTitle = "ZR ELECTRONICS", CompanyAddress = "Shop # 42,43 Ground Floor, Digital center,Rahat Cinema Road,Hyd ", CompanyPhone = "0222-784362 0311-2426987 0300-9379180 0333-2637260" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1023, CompanyTitle = "Mon-o-salwa", CompanyAddress = "Shop # 13,3 chicken market latifabad # 7 Hyd ", CompanyPhone = "0313-3106722" });

            //Start
            CompaniesList.Add(new CompaniesModel { CompanyID = 1027, CompanyTitle = "IMRAN AUTOS", CompanyAddress = "Station Road Gari khata hyderabad", CompanyPhone = "03193028155 03332887655" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1028, CompanyTitle = "DASTAGIR PAINT HOUSE", CompanyAddress = "Shop No 1 Qazi Zaheer Height Near Muhammadi Masjid Unit No 10 Latifabad,Hyderabad", CompanyPhone = "03163878829" });
            CompaniesList.Add(new CompaniesModel { CompanyID = 1029, CompanyTitle = "MATT INTERNATIONAL", CompanyAddress = "BANGLOW NO A/2/C BLOCK E UNIT NO 6 lATIFABAD", CompanyPhone = "03130017000" });

            CompaniesList.Add(new CompaniesModel { CompanyID = 1019, CompanyTitle = "Raksha Biryani ", CompanyAddress = "shop No 22, opp: Nagori Milk shop Unit no 8 Latifabad", CompanyPhone = "03127660027 03352716657" });

            //CompaniesList.Add(new CompaniesModel { CompanyID = 1024, CompanyTitle = "SHAH NOOR TRADERS", CompanyAddress = "commericial Area Near shah Latif Dairy Unit # 7 Latifabad Hyderabad ", CompanyPhone = "0314-4031426 0346-2718764" });
            //CompaniesList.Add(new CompaniesModel { CompanyID = 1025, CompanyTitle = "SHAH NOOR TRADERS", CompanyAddress = "commericial Area Near shah Latif Dairy Unit # 7 Latifabad Hyderabad ", CompanyPhone = "0314-4031426 0346-2718764" });
            //CompaniesList.Add(new CompaniesModel { CompanyID = 1026, CompanyTitle = "SHAH NOOR TRADERS", CompanyAddress = "commericial Area Near shah Latif Dairy Unit # 7 Latifabad Hyderabad ", CompanyPhone = "0314-4031426 0346-2718764" });

            CompaniesList.Add(new CompaniesModel
            {
                CompanyID = 1024,
                CompanyTitle = "New Dawn Electronics",
                CompanyAddress = "Foujdari Road, Rambo Center Street, Hyderabad",
                CompanyPhone = "0321-3772088",
                Note= "Deals in Solar & Electronics"
            });

        }

        public CompaniesModel GetCompanyID(int CompanyID)
        {
            return CompaniesList.FirstOrDefault(x => x.CompanyID == CompanyID);
        }
    }
}