using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PIDSLibrary;

public partial class _ReportMonthly : Page
{
    TransactionModel objTrans = new TransactionModel();

    public bool tableLoaded = false;
    public bool loadChart = false;
    public string functionToLoad = "";

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
            if (Page.IsPostBack == false)
            {
                //objTrans.PopulateLists(ref ddlYear, "GET_YEAR");
                //objTrans.PopulateLists(ref ddlMonth, "GET_MONTH");
                //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                //objTrans.PopulateLists(ref ddlReport, "GET_REPORT_MONTHLY");
                new DTO().PopulateReportField(ref ddlReport);
            }

        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbPreview_Click(object sender, EventArgs e)
    {
        try
        {
            string reportCmd = ddlReport.SelectedValue;

            DTO dTO = new DTO();
            string dateRange = daterange_hidden.Value;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@startDate", dateRange.Split('-')[0]);
            param.Add("@endDate", dateRange.Split('-')[1]);
            var data = dTO.RetrieveAsDataTable(reportCmd, param);

            var htmlTable = dTO.ConvertDataTableToHTML(data);
            placeholder1.Controls.Add(new Literal { Text = htmlTable });
            tableLoaded = true;

            var chartData = GenerateChartData(data);
            if (chartData != null)
            {
                loadChart = true;
                //functionToLoad = "build_side_by_side_column_chart";
                //chart_data_hidden.Value = JsonConvert.SerializeObject(chartData);
            }

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
            switch (iMonth)
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

    private dynamic GenerateChartData(DataTable data)
    {
        HighChartUtils hg = new HighChartUtils();
        dynamic chartdata;
        loadChart = false;
        switch (ddlReport.SelectedValue)
        {
            case "sp_CRUDE_OIL_EXPORTS_DIFFERENCE_WITH_PREVIOUS_MONTH_BY_TERMINAL_QTY_IN_NET_BBLS":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS DIFFERENCE BETWEEN {0} AND {1} BY TERMINAL –QTY IN NET BBLS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_VOLUME_OF_CRUDE_OIL_DIFFERENCE_INSPECTED_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH":
                chartdata = hg.Generate_Table_Of_Difference(data, "VOLUME OF CRUDE OIL DIFFERENCE INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_VOLUME_OF_CRUDE_OIL_DIFFERENCE_INSPECTED_BETWEEN_CURRENT_MONTH_AND_MONTH_IN_PREVIOUS_YEAR":
                chartdata = hg.Generate_Table_Of_Difference(data, "VOLUME OF CRUDE OIL DIFFERENCE INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_TOTAL_NUMBER_OF_VESSELS_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_YEAR":
                chartdata = hg.Generate_Table_Of_Difference(data, "TOTAL NUMBER OF VESSELS INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_TOTAL_NUMBER_OF_VESSELS_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH":
                chartdata = hg.Generate_Table_Of_Difference(data, "TOTAL NUMBER OF VESSELS INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH_BY_DESTINATION_QTY_IN_NET_BARREL":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} AND {1} BY Destination – QTY IN NET BARRELS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_NXP_ISSUING_BANKS_QTY_IN_NETBARRELS":
                chartdata = hg.Generate_Simple_Array(data, "CRUDE OIL EXPORTS INSPECTED BY NXP ISSUING BANKS– QUANTITY IN NET BARRELS");
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_FOB_VALUES_OF_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINAL_IN_USD":
                chartdata = hg.Generate_Table_Of_Difference(data, "FOB VALUES OF CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} and {1} BY TERMINAL (IN USD)");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            default:
                return null;

        }
    }


}