using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lib.Reporting.ReportModel
{
    public class Get_PatientInformation
    {
        #region -----Properties-----

        public int patientID { get; set; }

        [DisplayName("Name")]
        public string patientName { get; set; }

        [DisplayName("Age")]
        public string patAge { get; set; }

        [DisplayName("Type")]
        public string ageType { get; set; }

        [DisplayName("Gender")]
        public string gender { get; set; }

        [DisplayName("Comments")]
        public string cell { get; set; }

        [DisplayName("Bed")]
        public string bed { get; set; }

        [DisplayName("Ward")]
        public string ward { get; set; }

        public DateTime dob { get; set; }

        public DateTime crtDate { get; set; }

        public String crtBy { get; set; }

        public DateTime modDate { get; set; }

        public String modBy { get; set; }

        public Boolean active { get; set; }

        public int docID { get; set; }

        public String docName { get; set; }
        #endregion

        #region ----- Construct --------
        public Get_PatientInformation()
        {
            this.patientID = 0;

            this.patientName = "";

            this.patAge = "";

            this.gender = "";

            this.ageType = "";

            this.cell = "";

            this.ward = "";

            this.bed = "";

            this.crtBy = "";

            this.modBy = "";

            this.active = true;

            this.docID = 0;

            this.docName = "";
        }

        public Get_PatientInformation(int patientID, String patientName, String patAge, String ageType, String ward, 
        String bed, String gender, String cell,DateTime dob,String crtBy, DateTime crtDate,DateTime modDate, String modBy, Boolean active, int docID, String docName)
        {
            this.patientID = patientID;

            this.patientName = patientName;

            this.patAge = patAge;

            this.ageType = ageType;

            this.cell = cell;

            this.ward = ward;

            this.bed = bed;

            this.gender = gender;

            this.docID = docID;

            this.docName = docName;
        }
        public Get_PatientInformation(DataRow Get_PatientInformationDataRow)
        {
            try
            {
                if (Get_PatientInformationDataRow.Table.Columns.Contains("patientName") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["patientName"].ToString()))
                { this.patientName = (String)Get_PatientInformationDataRow["patientName"]; }
                else { this.patientName = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("gender") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["gender"].ToString()))
                { this.gender = (String)Get_PatientInformationDataRow["gender"]; }
                else { this.gender = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("bed") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["bed"].ToString()))
                { this.bed = (String)Get_PatientInformationDataRow["bed"]; }
                else { this.bed = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("ward") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["ward"].ToString()))
                { this.ward = (String)Get_PatientInformationDataRow["ward"]; }
                else { this.ward = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("patAge") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["patAge"].ToString()))
                { this.patAge = (String)Get_PatientInformationDataRow["patAge"]; }
                else { this.patAge = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("ageType") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["ageType"].ToString()))
                { this.ageType = (String)Get_PatientInformationDataRow["ageType"]; }
                else { this.ageType = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("cell") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["cell"].ToString()))
                { this.cell = (String)Get_PatientInformationDataRow["cell"]; }
                else { this.cell = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("patientID") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["patientID"].ToString()))
                { this.patientID = (Int32)Get_PatientInformationDataRow["patientID"]; }
                else { this.patientID = 0; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("crtDate") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["crtDate"].ToString()))
                { this.crtDate = (DateTime)Get_PatientInformationDataRow["crtDate"]; }
                else { this.crtDate = DateTime.MinValue; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["crtBy"].ToString()))
                { this.crtBy = (String)Get_PatientInformationDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("modDate") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["modDate"].ToString()))
                { this.modDate = (DateTime)Get_PatientInformationDataRow["modDate"]; }
                else { this.modDate = DateTime.MinValue; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("modBy") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["modBy"].ToString()))
                { this.modBy = (String)Get_PatientInformationDataRow["modBy"]; }
                else { this.modBy = ""; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("dob") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["dob"].ToString()))
                { this.dob = (DateTime)Get_PatientInformationDataRow["dob"]; }
                else { this.dob = DateTime.MinValue; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("active") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["active"].ToString()))
                { this.active = (Boolean)Get_PatientInformationDataRow["active"]; }
                else { /*this.active = false;*/ }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("docID") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["docID"].ToString()))
                { this.docID = (Int32)Get_PatientInformationDataRow["docID"]; }
                else { this.docID = 0; }

                if (Get_PatientInformationDataRow.Table.Columns.Contains("docName") && !String.IsNullOrEmpty(Get_PatientInformationDataRow["docName"].ToString()))
                { this.docName = (String)Get_PatientInformationDataRow["docName"]; }
                else { this.docName = ""; }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
