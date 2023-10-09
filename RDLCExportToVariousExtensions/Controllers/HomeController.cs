using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RDLCExportToVariousExtensions.Models;

namespace RDLCExportToVariousExtensions.Controllers
{
    public class HomeController : Controller
    {
        RamyaEntities db = new RamyaEntities();
        public ActionResult Index()
        {
            
            return View(db.FamilyDetails.ToList());
        }
        public ActionResult Reports(string ReportType) {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/FamilyReport.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "FamilyDataSet";
            reportDataSource.Value = db.FamilyDetails.ToList();
            localreport.DataSources.Add(reportDataSource);
            ReportParameter title = new ReportParameter("TDesign", "***MARAMI INFOTECH***");
            localreport.SetParameters(title);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
                
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            var deviceInfo = @"<DeviceInfo>
                    <EmbedFonts>None</EmbedFonts>
                   </DeviceInfo>";

            renderedByte = localreport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=Family_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }
        
    }
}