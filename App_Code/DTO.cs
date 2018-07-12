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
        else if(_db == "odbc")
        {
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["odbcConnectionString"].ConnectionString;
        }
    }

    public void PopulateReportField(ref DropDownList objList)
    {
        SqlCommand _command = new SqlCommand("select SPECIFIC_NAME 'Name', REPLACE(REPLACE(SPECIFIC_NAME, '_',' '),'sp ', ' ') 'f_name' from information_schema.routines where routine_type = 'PROCEDURE'");
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
            html += "<td>" + dt.Columns[i].ColumnName + "</td>";
        html += "</tr></thead>";

        //add rows
        html += "<tbody>";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count; j++)
                html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
            html += "</tr>";
        }
        html += "</tbody>  </table>";
        return html;
    }

    public DataTable RetrieveAsDataTable(string _commandText, Dictionary<string,string> param)
    {
        SqlCommand _command = new SqlCommand();
        _command.CommandText = _commandText;
        _command.CommandType = CommandType.StoredProcedure;
        foreach(var p in param)
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