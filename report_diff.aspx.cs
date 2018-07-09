using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _ReportDiff : Page
{
    TransactionModel objTrans = new TransactionModel();

    private void DisplaySuccess(String sMessage)
    {
        try
        {
            (this.Master as _PidsMaster).DisplayMessage(sMessage, _PidsMaster.MsgType.Success);
        }
        catch (Exception ex)
        {
            Session["msg"] = ex.Message.ToString();
            Response.Redirect("~/en");
        }
    }
    private void DisplayError(String sMessage)
    {
        try
        {
            (this.Master as _PidsMaster).DisplayMessage(sMessage, _PidsMaster.MsgType.Error);
        }
        catch (Exception ex)
        {
            Session["msg"] = ex.Message.ToString();
            Response.Redirect("~/en");
        }
    }
    private void DisplayWarning(String sMessage)
    {
        try
        {
            (this.Master as _PidsMaster).DisplayMessage(sMessage, _PidsMaster.MsgType.Warning);
        }
        catch (Exception ex)
        {
            Session["msg"] = ex.Message.ToString();
            Response.Redirect("~/en");
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        try
        {
            objTrans.CloseConnection();
        }
        catch (Exception ex)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Page.IsPostBack==false)
            {
                objTrans.PopulateLists(ref ddlMonthlyYear1, "GET_YEAR");
                objTrans.PopulateLists(ref ddlMonthlyYear2, "GET_YEAR");
                objTrans.PopulateLists(ref ddlQuarterlyYear1, "GET_YEAR");
                objTrans.PopulateLists(ref ddlQuarterlyYear2, "GET_YEAR");
                objTrans.PopulateLists(ref ddlYearlyYear1, "GET_YEAR");
                objTrans.PopulateLists(ref ddlYearlyYear2, "GET_YEAR");

                objTrans.PopulateLists(ref ddlMonthlyMonth1, "GET_MONTH");
                objTrans.PopulateLists(ref ddlMonthlyMonth2, "GET_MONTH");

                objTrans.PopulateLists(ref ddlQuarterlyQuarter1, "GET_QUARTER");
                objTrans.PopulateLists(ref ddlQuarterlyQuarter2, "GET_QUARTER");

                objTrans.PopulateLists(ref ddlMonthlyReport, "GET_REPORT_DIFFERENTIALS");
                objTrans.PopulateLists(ref ddlQuarterlyReport, "GET_REPORT_DIFFERENTIALS");
                objTrans.PopulateLists(ref ddlYearlyReport, "GET_REPORT_DIFFERENTIALS");
            }
        }
        catch(Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbPreview_Click(object sender, EventArgs e)
    {
        /*
        try
        {
            objTrans.ReportID = int.Parse(ddlReport.SelectedValue);
            if(objTrans.getReport()==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            DataSet ds = objTrans.getMonthlyReportData(int.Parse(ddlMonth1.SelectedValue), int.Parse(ddlYear1.SelectedValue));
            if(ds == null)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            rptViewer.ProcessingMode = ProcessingMode.Local;
            string sFilename = objTrans.FileName;
            rptViewer.LocalReport.ReportPath = Server.MapPath(sFilename);
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource(objTrans.DatasetName, ds.Tables[0]));
            rptViewer.DocumentMapCollapsed = true;

            rptViewer.AsyncRendering = true;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.FullPage;
            rptViewer.LocalReport.EnableExternalImages = true;

            string sTitle = objTrans.ReportTile + " (" + getMonthName(int.Parse(ddlMonth1.SelectedValue)) + " " + ddlYear1.SelectedValue + ")";
            ReportParameter param = new ReportParameter("parmTitle", sTitle);
            rptViewer.LocalReport.SetParameters(param);
            rptViewer.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
        */
    }
    private string getMonthName(int iMonth)
    {
        try
        {
            string sMonth = "";
            switch(iMonth)
            {
                case 1:
                    sMonth = "January";
                    break;
                case 2:
                    sMonth = "February";
                    break;
                case 3:
                    sMonth = "March";
                    break;
                case 4:
                    sMonth = "April";
                    break;
                case 5:
                    sMonth = "May";
                    break;
                case 6:
                    sMonth = "June";
                    break;
                case 7:
                    sMonth = "July";
                    break;
                case 8:
                    sMonth = "August";
                    break;
                case 9:
                    sMonth = "September";
                    break;
                case 10:
                    sMonth = "October";
                    break;
                case 11:
                    sMonth = "November";
                    break;
                case 12:
                    sMonth = "December";
                    break;
            }
            return sMonth;
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
            return null;
        }
    }
    private string getQuarterName(int iQuarter)
    {
        try
        {
            string sQuarter = "";
            switch (iQuarter)
            {
                case 1:
                    sQuarter = "First Quarter";
                    break;
                case 2:
                    sQuarter = "Second Quarter";
                    break;
                case 3:
                    sQuarter = "Third Quarter";
                    break;
                case 4:
                    sQuarter = "Fourth Quarter";
                    break;
            }
            return sQuarter;
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
            return null;
        }
    }
    private DateTime getStartDate(int iQuarter, string sYear)
    {
        try
        {
            DateTime dStartDate = DateTime.Now.Date;
            switch (iQuarter)
            {
                case 1:
                    dStartDate = DateTime.Parse("1/1/" + sYear);
                    break;
                case 2:
                    dStartDate = DateTime.Parse("4/1/" + sYear);
                    break;
                case 3:
                    dStartDate = DateTime.Parse("7/1/" + sYear);
                    break;
                case 4:
                    dStartDate = DateTime.Parse("10/1/" + sYear);
                    break;
            }
            return dStartDate;
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
            return DateTime.Now.Date;
        }
    }
    private DateTime getEndDate(int iQuarter, string sYear)
    {
        try
        {
            DateTime dEndDate = DateTime.Now.Date;
            switch (iQuarter)
            {
                case 1:
                    dEndDate = DateTime.Parse("3/31/" + sYear);
                    break;
                case 2:
                    dEndDate = DateTime.Parse("6/30/" + sYear);
                    break;
                case 3:
                    dEndDate = DateTime.Parse("9/30/" + sYear);
                    break;
                case 4:
                    dEndDate = DateTime.Parse("12/31/" + sYear);
                    break;
            }
            return dEndDate;
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
            return DateTime.Now.Date;
        }
    }


}