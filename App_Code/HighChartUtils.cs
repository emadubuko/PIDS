using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HighChartUtils
/// </summary>
public class HighChartUtils
{
    public dynamic Generate_Simple_Array(DataTable dt, string chartTitle)
    {
        List<dynamic> data_array = new List<dynamic>();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            data_array.Add(new
            {
                name = dt.Rows[i][0].ToString(),
                y = dt.Rows[i][1],
            });
        }
        return new
        {
            data_array,
            title = string.Format(chartTitle),
        };
    }
    public dynamic Generate_Table_Of_Difference(DataTable dt, string chartTitle)
    {
        List<dynamic> series_data = new List<dynamic>();

        //get first series set
        string name_1 = dt.Columns[1].ColumnName;
        series_data.Add(new
        {
            name = name_1,
            stack = "current",
            data = getColumnValues(dt, name_1, dt.Columns[1].DataType.Name.ToString().ToLower())
        });
        //second series set
        string name_2 = dt.Columns[2].ColumnName;
        series_data.Add(new
        {
            name = name_2,
            stack = "previous",
            data = getColumnValues(dt, name_2, dt.Columns[2].DataType.Name.ToString().ToLower())
        });

        return new
        {
            series_data,
            xaxisCategory = getColumnValues(dt, dt.Columns[0].ColumnName, dt.Columns[0].DataType.Name.ToString().ToLower()),
            yAxistitle = "",
            title = string.Format(chartTitle, name_1.ToUpper(), name_2.ToUpper()), //string.Format("CRUDE OIL EXPORTS DIFFERENCE BETWEEN {0} AND {1} BY TERMINAL –QTY IN NET BBLS", name_1, name_2)
        };
    }

    public dynamic getColumnValues(DataTable data, string columnname, string dataType)
    {
        if (dataType == "int32")
        {
            return data.AsEnumerable().Select(r => r.Field<int>(columnname)).ToList();
        }
        else if (dataType == "Int64")
        {
            return data.AsEnumerable().Select(r => r.Field<Int64>(columnname)).ToList();
        }
        else if (dataType == "decimal")
        {
            return data.AsEnumerable().Select(r => r.Field<decimal>(columnname)).ToList();
        }
        else if (dataType == "string")
        {
            return data.AsEnumerable().Select(r => r.Field<string>(columnname)).ToList();
        }
        return null;
    }
}