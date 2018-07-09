using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _ReportQuarterly : Page
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
                objTrans.PopulateLists(ref ddlYear, "GET_YEAR");
                objTrans.PopulateLists(ref ddlQuarter, "GET_QUARTER");
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                objTrans.PopulateLists(ref ddlReport, "GET_REPORT_QUARTERLY");
            }
        }
        catch(Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbPreview_Click(object sender, EventArgs e)
    {
        try
        {
            objTrans.ReportID = int.Parse(ddlReport.SelectedValue);
            if(objTrans.getReport()==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            DataSet ds = objTrans.getQuarterlyReportData( 
                getStartDate(int.Parse(ddlQuarter.SelectedValue), ddlYear.SelectedValue), 
                getEndDate(int.Parse(ddlQuarter.SelectedValue), ddlYear.SelectedValue));
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

            string sTitle = objTrans.ReportTile + " (" + getQuarterName(int.Parse(ddlQuarter.SelectedValue)) + " " + ddlYear.SelectedValue + ")";
            ReportParameter param = new ReportParameter("parmTitle", sTitle);
            rptViewer.LocalReport.SetParameters(param);
            rptViewer.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private string getQuarterName(int iQuarter)
    {
        try
        {
            string sQuarter = "";
            switch(iQuarter)
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