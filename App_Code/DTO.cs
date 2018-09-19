using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DTO
/// </summary>
public class DTO
{
    IDbConnection _connection;

    public DTO()
    {
        _connection = new SqlConnection();
        string _db = ConfigurationManager.ConnectionStrings["installedDB"].ConnectionString;
        if (_db == "mssql")
        {
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["mssqlConnectionString"].ConnectionString;
        }
        else if (_db == "odbc")
        {
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["odbcConnectionString"].ConnectionString;
        }
    }

    /*
    private dynamic GenerateChartData(DataTable data, string report, out string functionToLoad )
    {
        HighChartUtils hg = new HighChartUtils();
        dynamic chartdata;
       bool loadChart = false;
        switch (report)
        {
            case "sp_EXPORTS_COVERED":
                chartdata = hg.Generate_Simple_Array(data, "EXPORTS COVERED, Terminal BY NET BARREL", 0, 2);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINAL_QTY_IN_NET_BARREL":
                chartdata = hg.Generate_Simple_Array(data, "CRUDE OIL EXPORTS INSPECTED BY TERMINAL QTY IN NET BARREL");
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata); 
            case "sp_CRUDE_OIL_EXPORTS_DIFFERENCE_WITH_PREVIOUS_MONTH_BY_TERMINAL_QTY_IN_NET_BBLS":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS DIFFERENCE BETWEEN {0} AND {1} BY TERMINAL –QTY IN NET BBLS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_FOB_VALUES_OF_CRUDE_OIL_EXPORTS_INSPECTED_BY_DESTINATIONS_IN_USD":
                chartdata = hg.Generate_Table_Of_Difference(data, "FOB VALUES OF CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} AND {1} BY DESTINATIONS IN USD");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
             
            case "sp_CRUDE_OIL_EXPORTS_DIFFERENCE_WITH_PREVIOUS_MONTH_IN_YEAR_BEFORE_BY_TERMINAL_QTY_IN_NET_BBLS":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS DIFFERENCE BETWEEN {0} AND {1} BY TERMINAL –QTY IN NET BBLS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_VOLUME_OF_CRUDE_OIL_DIFFERENCE_INSPECTED_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH":
                chartdata = hg.Generate_Table_Of_Difference(data, "VOLUME OF CRUDE OIL DIFFERENCE INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_VOLUME_OF_CRUDE_OIL_DIFFERENCE_INSPECTED_BETWEEN_CURRENT_MONTH_AND_MONTH_IN_PREVIOUS_YEAR":
                chartdata = hg.Generate_Table_Of_Difference(data, "VOLUME OF CRUDE OIL DIFFERENCE INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_TOTAL_NUMBER_OF_VESSELS_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_YEAR":
                chartdata = hg.Generate_Table_Of_Difference(data, "TOTAL NUMBER OF VESSELS INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_TOTAL_NUMBER_OF_VESSELS_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH":
                chartdata = hg.Generate_Table_Of_Difference(data, "TOTAL NUMBER OF VESSELS INSPECTED BETWEEN {0} AND {1} BY TERMINAL");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_DIFFERENCE_BETWEEN_CURRENT_MONTH_AND_PREVIOUS_MONTH_BY_DESTINATION_QTY_IN_NET_BARREL":
                chartdata = hg.Generate_Table_Of_Difference(data, "CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} AND {1} BY Destination – QTY IN NET BARRELS");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_NXP_ISSUING_BANKS_QTY_IN_NETBARRELS":
                chartdata = hg.Generate_Simple_Array(data, "CRUDE OIL EXPORTS INSPECTED BY NXP ISSUING BANKS– QUANTITY IN NET BARRELS");
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_FOB_VALUES_OF_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINAL_IN_USD":
                chartdata = hg.Generate_Table_Of_Difference(data, "FOB VALUES OF CRUDE OIL EXPORTS INSPECTED DIFFERENCE BETWEEN {0} and {1} BY TERMINAL (IN USD)");
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                 
            case "sp_TERMINAL_QUANTITY_ANALYSIS":
                chartdata = hg.Generate_Table_Of_Difference(data, "TERMINAL QUANTITY ANALYSIS", 1, 2, 3);
                loadChart = true;
                functionToLoad = "build_side_by_side_column_chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_EXPORTERS_QUANTITY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_VESSELS_QTY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_DESTINATIONS_QTY_IN_NET_BARRELS":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TRADERS_QUANTITY_IN_NET_BARRELS":
                var _2nd_last_column = data.Columns.Count - 2;
                chartdata = hg.Generate_Simple_Array(data, report + " - " + ddlMonth.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem, 0, _2nd_last_column); //"CRUDE OIL EXPORTS INSPECTED " + ddlMonth.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem + " BY EXPORTERS QUANTITY IN NET BARRELS", 0, _2nd_last_column);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata);
                
            case "sp_TOP_5_DESTINATIONS_BY_QUANTITY_IN_NET_BARRELS":
            case "sp_TOP_5_DESTINATIONS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
            case "sp_TOP_5_EXPORTERS_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_VESSELS_ATTENDED_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_VESSELS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
            case "sp_TOP_5_EXPORTERS_CRUDE_OIL_LIFTED_BY_FOB_VALUE_IN_USD":
            case "sp_TOP_5_TRADERS_CRUDE_OIL_LIFTED_BY_FOB_VALUE_IN_USD":
            case "sp_TOP_5_TRADERS_BY_QTY_IN_NET_BARRELS":
            case "sp_TOP_5_NXP_ISSUING_BANKS_CRUDE_OIL_LIFTED_BY_FOB_VALUES_IN_USD":
                chartdata = hg.Generate_Simple_Array(data, ddlReport.SelectedItem.ToString() + " - " + ddlMonth.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata);
               ;
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TERMINALS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_EXPORTERS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_VESSELS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_TRADERS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_PRODUCTS_FOB_VALUES_IN_USD":
            case "sp_CRUDE_OIL_EXPORTS_INSPECTED_BY_NXP_ISSUING_BANKS_FOB_VALUES_IN_USD":
                chartdata = hg.Generate_Simple_Array(data, ddlMonth.SelectedItem.ToString().ToUpper() + ", " + ddlYear.SelectedItem + " - " + ddlReport.SelectedItem.ToString(), 0, 2);
                loadChart = true;
                functionToLoad = "Build_Pie_Chart";
                return JsonConvert.SerializeObject(chartdata);
                
            default:
                return null;

        }
    }

    */
    public void PopulateReportField(ref DropDownList objList, string report_type)
    {
        SqlCommand _command = new SqlCommand(); //("Select SPECIFIC_NAME 'Name', REPLACE(REPLACE(SPECIFIC_NAME, '_',' '),'sp ', ' ') 'f_name' from information_schema.routines where routine_type = 'PROCEDURE' order by SPECIFIC_NAME");
        _command.CommandText = "sp_GET_ALL_REPORT";
        _command.CommandType = CommandType.StoredProcedure;
        _command.Parameters.Add("@reportType", SqlDbType.NVarChar).Value = report_type;

        var data = RetrieveAsDataTable(_command);
        objList.DataSource = data.DefaultView;
        objList.DataTextField = "f_name";
        objList.DataValueField = "Name";
        objList.DataBind();
    }

    public string ConvertDataTableToHTML(DataTable dt)
    {
        string html = "<table class='table table-striped table-bordered table-hover' style='font-size:12px; width: 100%'>";
        //add header row
        html += "<thead style='background-color: #337ab7;color: #fff;'><tr>";
        for (int i = 0; i < dt.Columns.Count; i++)
            if (dt.Columns[i].DataType.Name.ToString().ToLower() == "decimal")
            {
                html += "<td class=' sum'>" + dt.Columns[i].ColumnName + "</td>";
            }
            else if ("int32,int64".Contains(dt.Columns[i].DataType.Name.ToString().ToLower()))
            {
                html += "<td class=' sum'>" + dt.Columns[i].ColumnName + "</td>";
            }
            else
            {
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            }
        //html += "<td>" + dt.Columns[i].ColumnName + "</td>";
        html += "</tr></thead>";

        //add the footer
        html += "<tfoot><tr>";
        for (int i = 0; i < dt.Columns.Count; i++)
            html += "<th></th>";
        html += "</tr></tfoot>";

        //add rows
        html += "<tbody>";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count; j++)
                if (dt.Columns[j].DataType.Name.ToString().ToLower() == "decimal")
                {
                    html += "<td class=' sum'>" + string.Format("{0:N2}", dt.Rows[i][j]) + "</td>";
                }
                else if ("int32,int64".Contains(dt.Columns[j].DataType.Name.ToString().ToLower()))
                {
                    html += "<td class=' sum'>" + string.Format("{0:N0}", dt.Rows[i][j]) + "</td>";
                }
                else
                {
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                }
            html += "</tr>";
        }
        html += "</tbody></table>";
        return html;
    }

    public DataTable RetrieveAsDataTable(string _commandText, Dictionary<string, string> param)
    {
        SqlCommand _command = new SqlCommand();
        _command.CommandText = _commandText;
        _command.CommandType = CommandType.StoredProcedure;
        foreach (var p in param)
        {
            _command.Parameters.Add(p.Key, SqlDbType.Date).Value = p.Value.Trim();
        }

        return RetrieveAsDataTable(_command);
    }

    public DataTable RetrieveAsDataTable(IDbCommand _command)
    {
        DataTable ds = new DataTable();
        try
        {
            using (var conn = _connection)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                _command.Connection = conn;
                da.SelectCommand = (SqlCommand)_command;

                OpenConnection();

                da.Fill(ds);
                _command.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }


    protected void OpenConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }
        catch (Exception ex)
        {
            //this.ErrorMessage = this.ErrorMessage + "OpenConnection(): " + ex.Message;
        }
    }

    private void CloseConnection()
    {
        try
        {
            _connection.Close();
        }
        catch (Exception ex)
        {
            //this.ErrorMessage = this.ErrorMessage + "CloseConnection(): " + ex.Message;
        }
    }
}