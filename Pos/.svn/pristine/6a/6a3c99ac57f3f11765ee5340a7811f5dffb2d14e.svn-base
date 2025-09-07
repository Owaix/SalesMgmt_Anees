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
            CompaniesList.Add(new CompaniesModel { CompanyID = 1011, CompanyTitle = "IceSmoke", CompanyAddress = "Shop#A-29,Sahara Arcade,main boulevard,Bahria enclave, Islamabad", CompanyPhone = "0317-0000623" });
        }

        public CompaniesModel GetCompanyID(int CompanyID)
        {
            return CompaniesList.FirstOrDefault(x => x.CompanyID == CompanyID);
        }
    }
}