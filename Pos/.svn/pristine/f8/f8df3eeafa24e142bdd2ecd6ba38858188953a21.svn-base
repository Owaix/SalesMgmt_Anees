using System;
using System.Data;

namespace Com.LT.LabExpress.Reporting
{
    /// <summary>
    /// Business functions of FreePatients object
    /// </summary>
    public class FreePatients
    {
        #region ----- Properties -------

        public String crtBy { get; set; }

        public String labno { get; set; }

        public DateTime Date { get; set; }

        public String Investigation { get; set; }

        public String Patient { get; set; }

        public String Authorized_By { get; set; }

        public Decimal Charges { get; set; }

        public Decimal Disc { get; set; }

        public Int32 patientReportID { get; set; }

        public Int32 patientID { get; set; }

        public Int32 docID { get; set; }

        public Int32 reportID { get; set; }

        public String Stat { get; set; }

        public Decimal discountRate { get; set; }

        public DateTime dtStart { get; set; }

        public DateTime dtEnd { get; set; }

    #endregion

    #region ----- Construct --------

    /// <summary>
    /// Default constructor that takes no arguments and initialize all fields with default value
    /// </summary>
    /// <returns>FreePatients object</returns>
    public FreePatients()
        {
            this.crtBy = "";

            this.labno = "";

            this.Date = DateTime.MinValue;

            this.Investigation = "";

            this.Patient = "";

            this.Authorized_By = "";

            this.Stat = "";

            this.discountRate = 0M;

            this.Charges = 0;

            this.Disc = 0;

            this.patientReportID = 0;

            this.patientID = 0;

            this.docID = 0;

            this.reportID = 0;

        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="crtBy">Int32 crtBy of FreePatients</param>

        /// <param name="labno">String labno of FreePatients</param>

        /// <param name="Date">DateTime Date of FreePatients</param>

        /// <param name="Investigation">String Investigation of FreePatients</param>

        /// <param name="Patient">String Patient of FreePatients</param>

        /// <param name="Authorized_By">String Authorized_By of FreePatients</param>

        /// <param name="Charges">Decimal Charges of FreePatients</param>

        /// <param name="Disc">Decimal Disc of FreePatients</param>

        /// <param name="patientReportID">Int32 patientReportID of FreePatients</param>

        /// <param name="patientID">Int32 patientID of FreePatients</param>

        /// <param name="docID">Int32 docID of FreePatients</param>

        /// <param name="reportID">Int32 reportID of FreePatients</param>

        /// <returns>FreePatients object</returns>
        public FreePatients(String crtBy, String labno,Decimal discountRate ,String Stat,DateTime Date, String Investigation, String Patient, String Authorized_By, Decimal Charges, Decimal Disc, Int32 patientReportID, Int32 patientID, Int32 docID, Int32 reportID,DateTime dtStart, DateTime dtEnd)
        {
            this.crtBy = crtBy;

            this.labno = labno;

            this.Date = Date;

            this.dtStart = dtStart;

            this.dtEnd = dtEnd;

            this.Investigation = Investigation;

            this.Patient = Patient;

            this.Authorized_By = Authorized_By;

            this.Charges = Charges;

            this.Stat = Stat;

            this.discountRate = discountRate;

            this.Disc = Disc;

            this.patientReportID = patientReportID;

            this.patientID = patientID;

            this.docID = docID;

            this.reportID = reportID;

        }

        /// <summary>
        /// Overloaded contructor that takes one row from FreePatients table 
        /// and convert it into FreePatients object
        /// </summary>
        /// <param name="FreePatientsDataRow">DataRow one row from FreePatients table</param>
        /// <returns>FreePatients object</returns>
        public FreePatients(DataRow FreePatientsDataRow)
        {
            try
            {
                if (FreePatientsDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(FreePatientsDataRow["crtBy"].ToString()))
                {this.crtBy = (String)FreePatientsDataRow["crtBy"];}
                else { this.crtBy = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("labno") && !String.IsNullOrEmpty(FreePatientsDataRow["labno"].ToString()))
                {this.labno = (String)FreePatientsDataRow["labno"];}
                else { this.labno = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Date") && !String.IsNullOrEmpty(FreePatientsDataRow["Date"].ToString()))
                {this.Date = (DateTime)FreePatientsDataRow["Date"];}
                else { this.Date = DateTime.MinValue; }

                if (FreePatientsDataRow.Table.Columns.Contains("Investigation") && !String.IsNullOrEmpty(FreePatientsDataRow["Investigation"].ToString()))
                {this.Investigation = (String)FreePatientsDataRow["Investigation"];}
                else { this.Investigation = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Patient") && !String.IsNullOrEmpty(FreePatientsDataRow["Patient"].ToString()))
                {this.Patient = (String)FreePatientsDataRow["Patient"];}
                else { this.Patient = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Stat") && !String.IsNullOrEmpty(FreePatientsDataRow["Stat"].ToString()))
                { this.Stat = (String)FreePatientsDataRow["Stat"]; }
                else { this.Stat = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("discountRate") && !String.IsNullOrEmpty(FreePatientsDataRow["discountRate"].ToString()))
                { this.Disc = (Decimal)FreePatientsDataRow["discountRate"]; }
                else { this.discountRate = 0; }


                if (FreePatientsDataRow.Table.Columns.Contains("Authorized_By") && !String.IsNullOrEmpty(FreePatientsDataRow["Authorized_By"].ToString()))
                {this.Authorized_By = (String)FreePatientsDataRow["Authorized_By"];}
                else { this.Authorized_By = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Charges") && !String.IsNullOrEmpty(FreePatientsDataRow["Charges"].ToString()))
                {this.Charges = (Decimal)FreePatientsDataRow["Charges"];}
                else { this.Charges = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("Disc") && !String.IsNullOrEmpty(FreePatientsDataRow["Disc"].ToString()))
                {this.Disc = (Decimal)FreePatientsDataRow["Disc"];}
                else { this.Disc = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("patientReportID") && !String.IsNullOrEmpty(FreePatientsDataRow["patientReportID"].ToString()))
                {this.patientReportID = (Int32)FreePatientsDataRow["patientReportID"];}
                else { this.patientReportID = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("patientID") && !String.IsNullOrEmpty(FreePatientsDataRow["patientID"].ToString()))
                {this.patientID = (Int32)FreePatientsDataRow["patientID"];}
                else { this.patientID = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("docID") && !String.IsNullOrEmpty(FreePatientsDataRow["docID"].ToString()))
                {this.docID = (Int32)FreePatientsDataRow["docID"];}
                else { this.docID = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("reportID") && !String.IsNullOrEmpty(FreePatientsDataRow["reportID"].ToString()))
                {this.reportID = (Int32)FreePatientsDataRow["reportID"];}
                else { this.reportID = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("dtStart") && !String.IsNullOrEmpty(FreePatientsDataRow["dtStart"].ToString()))
                { this.dtStart = (DateTime)FreePatientsDataRow["dtStart"]; }
                else { this.dtStart = DateTime.MinValue; }

                if (FreePatientsDataRow.Table.Columns.Contains("dtEnd") && !String.IsNullOrEmpty(FreePatientsDataRow["dtEnd"].ToString()))
                { this.dtEnd = (DateTime)FreePatientsDataRow["dtEnd"]; }
                else { this.dtEnd = DateTime.MinValue; }



            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}