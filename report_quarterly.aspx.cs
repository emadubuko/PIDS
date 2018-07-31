using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;
using Newtonsoft.Json;

public partial class _ReportQuarterly : Page
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
        List<ListItem> quarters = new List<ListItem>();
        quarters.Add(new ListItem { Text = "Jan - Mar", Value = "Jan - Mar" });
        quarters.Add(new ListItem { Text = "Apr - Jun", Value = "Apr - Jun" });
        quarters.Add(new ListItem { Text = "Jul - Sep", Value = "Jul - Sep" });
        quarters.Add(new ListItem { Text = "Oct - Dec", Value = "Oct - Dec" });

        List<ListItem> years = new List<ListItem>();
        years.Add(new ListItem { Text = DateTime.Now.Year.ToString(), Value = DateTime.Now.Year.ToString(), Selected = true, });
        years.Add(new ListItem { Text = DateTime.Now.AddYears(-1).Year.ToString(), Value = DateTime.Now.AddYears(-1).Year.ToString() });
        years.Add(new ListItem { Text = DateTime.Now.AddYears(-2).Year.ToString(), Value = DateTime.Now.AddYears(-2).Year.ToString() });
        try
        {
            if(Page.IsPostBack==false)
            {
                ddlYear.DataTextField = "Text";
                ddlYear.DataValueField = "Value";
                ddlYear.DataSource = years;
                ddlYear.DataBind();

                ddlQuarter.DataTextField = "Text";
                ddlQuarter.DataValueField = "Value";
                ddlQuarter.DataSource = quarters;
                ddlQuarter.DataBind();                
                new DTO().PopulateReportField(ref ddlReport, "QUARTER");
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
            string reportCmd = ddlReport.SelectedValue;

            DTO dTO = new DTO();
            Dictionary<string, string> param = GetStartDate(ddlQuarter.SelectedValue, ddlYear.SelectedValue);
            var data = dTO.RetrieveAsDataTable(reportCmd, param);

            var htmlTable = dTO.ConvertDataTableToHTML(data);
            placeholder1.Controls.Add(new Literal { Text = htmlTable });
            tableLoaded = true;

             GenerateChartData(data);
            

        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private Dictionary<string, string> GetStartDate(string iQuarter, string sYear)
    {
        try
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            switch (iQuarter)
            {
                case "Jan - Mar":
                    param.Add("@startDate", DateTime.Parse("01/01/" + sYear).ToShortDateString());
                    param.Add("@endDate", DateTime.ParseExact("31/03/" + sYear,"dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    break;
                case "Apr - Jun":
                    param.Add("@startDate", DateTime.ParseExact("01/04/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    param.Add("@endDate", DateTime.ParseExact("30/06/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    break;
                case "Jul - Sep":
                    param.Add("@startDate", DateTime.ParseExact("01/07/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    param.Add("@endDate", DateTime.ParseExact("30/09/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    break;
                case "Oct - Dec":
                    param.Add("@startDate", DateTime.ParseExact("01/10/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    param.Add("@endDate", DateTime.ParseExact("31/12/" + sYear, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                    break;
            }
            return param;
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
            case "sp_EXPORTS_COVERED":
                chartdata = hg.Generate_Simple_Array(data, "EXPORTS COVERED, Terminal BY NET BARREL", 0, 2);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINAL_QTY_IN_NET_BARREL":
                chartdata = hg.Generate_Simple_Array(data, "CRUDE OIL EXPORTS INSPECTED BY TERMINAL QTY IN NET BARREL");
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_DIFFERENCE_WITH_PREVIOUS_MONTH_BY_TERMINAL_QTY_IN_NET_BBLS":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS DIFFERENCE BETWEEN {0} AND {1} BY TERMINAL –QTY IN NET BBLS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_FOB_VALUES_OF_CRUDE_OIL_EXPORTS_INSPECTED_BY_DESTINATIONS_IN_USD":
                chartdata = hg.Generate_Table_Of_Difference(data, "FOB VALUES OF CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} AND {1} BY DESTINATIONS IN USD");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_DIFFERENCE_WITH_PREVIOUS_MONTH_IN_YEAR_BEFORE_BY_TERMINAL_QTY_IN_NET_BBLS":
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
            case "sp_TERMINAL_QUANTITY_ANALYSIS":
                chartdata = hg.Generate_Table_Of_Difference(data, "TERMINAL QUANTITY ANALYSIS", 1, 2, 3);
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_EXPORTERS_QUANTITY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_VESSELS_QTY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_DESTINATIONS_QTY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TRADERS_QUANTITY_IN_NET_BARRELS":
                var _2nd_last_column = data.Columns.Count - 2;
                chartdata = hg.Generate_Simple_Array(data, ddlReport.SelectedItem.ToString() + " - " + ddlQuarter.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem, 0, _2nd_last_column); //"CRUDE OIL EXPORTS INSPECTED " + ddlMonth.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem + " BY EXPORTERS QUANTITY IN NET BARRELS", 0, _2nd_last_column);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_TOP_5_DESTINATIONS_BY_QUANTITY_IN_NET_BARRELS":
            case "sp_TOP_5_DESTINATIONS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
            case "sp_TOP_5_EXPORTERS_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_VESSELS_ATTENDED_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_VESSELS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
            case "sp_TOP_5_EXPORTERS_CRUDE_OIL_LIFTED_BY_FOB_VALUE_IN_USD":
            case "sp_TOP_5_TRADERS_CRUDE_OIL_LIFTED_BY_FOB_VALUE_IN_USD":
            case "sp_TOP_5_TRADERS_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_NXP_ISSUING_BANKS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
                chartdata = hg.Generate_Simple_Array(data, ddlReport.SelectedItem.ToString() + " - " + ddlQuarter.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINALS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_EXPORTERS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_VESSELS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TRADERS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_PRODUCTS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_NXP_ISSUING_BANKS_FOB_VALUES_IN_USD":
                chartdata = hg.Generate_Simple_Array(data, ddlQuarter.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem + " - " + ddlReport.SelectedItem.ToString(), 0, 2);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                chart_data_hidden.Value = JsonConvert.SerializeObject(chartdata);
                return chartdata;
            default:
                return null;

        }


        #region deprecated
        /*
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
        */
        #endregion
    }
}