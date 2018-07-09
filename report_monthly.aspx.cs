using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _ReportMonthly : Page
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
                objTrans.PopulateLists(ref ddlMonth, "GET_MONTH");
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                objTrans.PopulateLists(ref ddlReport, "GET_REPORT_MONTHLY");
            }

            //if (!Page.IsPostBack)
            //{
            //    BindEnumToListControls(typeof(SeriesChartType), ddlCharType);
            //}

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

            DataSet ds = objTrans.getMonthlyReportData(int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue));
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

            string sTitle = objTrans.ReportTile + " (" + getMonthName(int.Parse(ddlMonth.SelectedValue)) + " " + ddlYear.SelectedValue + ")";
            ReportParameter param = new ReportParameter("parmTitle", sTitle);
            rptViewer.LocalReport.SetParameters(param);
            rptViewer.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
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
    //public void BindEnumToListControls(Type enumType, ListControl listcontrol)
    //{
    //    string[] names = Enum.GetNames(enumType);
    //    listcontrol.DataSource = names.Select((key, value) =>
    //    new { key, value }).ToDictionary(x => x.key, x => x.value + 1);
    //    listcontrol.DataTextField = "Key";
    //    listcontrol.DataValueField = "Value";
    //    listcontrol.DataBind();
    //}
    //protected void ddlCharType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddlCharType.SelectedItem.Text);
    //}


}