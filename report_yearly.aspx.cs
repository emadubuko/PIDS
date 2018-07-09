using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _ReportYearly : Page
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
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                objTrans.PopulateLists(ref ddlReport, "GET_REPORT_YEARLY");
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

            DataSet ds = objTrans.getYearlyReportData(int.Parse(ddlYear.SelectedValue));
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

            string sTitle = objTrans.ReportTile + " (" + ddlYear.SelectedValue + ")";
            ReportParameter param = new ReportParameter("parmTitle", sTitle);
            rptViewer.LocalReport.SetParameters(param);
            rptViewer.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }


}