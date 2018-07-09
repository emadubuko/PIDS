using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _Pricing : Page
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
                objTrans.formatPanel(price_brent);
                objTrans.formatPanel(price_diff);

                objTrans.PopulateLists(ref ddlYearBrent, "GET_YEAR");
                objTrans.PopulateLists(ref ddlMonthBrent, "GET_MONTH");
                ddlMonthBrent.SelectedValue = DateTime.Now.Month.ToString();
                ddlYearBrent.SelectedValue = DateTime.Now.Year.ToString();
                loadGridBrent();

                objTrans.PopulateLists(ref price_diff);
                objTrans.PopulateLists(ref ddlYearDiff, "GET_YEAR");
                objTrans.PopulateLists(ref ddlMonthDiff, "GET_MONTH");
                objTrans.PopulateLists(ref diff_year, "GET_YEAR");
                objTrans.PopulateLists(ref diff_month, "GET_MONTH");
                ddlMonthDiff.SelectedValue = DateTime.Now.Month.ToString();
                ddlYearDiff.SelectedValue = DateTime.Now.Year.ToString();
                loadGridDiff();

                objTrans.PopulateLists(ref ddlYearPrice, "GET_YEAR");
                objTrans.PopulateLists(ref ddlMonthPrice, "GET_MONTH");
                ddlMonthPrice.SelectedValue = DateTime.Now.Month.ToString();
                ddlYearPrice.SelectedValue = DateTime.Now.Year.ToString();

            }
        }
        catch(Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void loadGridBrent()
    {
        try
        {
            string sWhereClause = " MONTH(brent_date) = " + ddlMonthBrent.SelectedValue + " AND YEAR(brent_date) = " + ddlYearBrent.SelectedValue;
            objTrans.getNonTemplateGrid(gvPriceBrent, sWhereClause);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void loadGridDiff()
    {
        try
        {
            string sWhereClause = " diff_month = " + ddlMonthDiff.SelectedValue + " AND diff_year = " + ddlYearDiff.SelectedValue;
            objTrans.getNonTemplateGrid(gvPriceDiff, sWhereClause);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveBrent_Click(object sender, EventArgs e)
    {
        try
        {
            if (objTrans.savePanel(price_brent, true) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGridBrent();
            DisplaySuccess("Record saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbUploadBrent_Click(object sender, EventArgs e)
    {
        try
        {
            if(fupBrent.HasFile==false)
            {
                DisplayError("Please, select the file to upload.");
                return;
            }

            string sYearMonthDay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            string ConStr = "";
            string ext = Path.GetExtension(fupBrent.FileName).ToLower();
            string path = Server.MapPath("~/upload/" + sYearMonthDay + fupBrent.FileName);
            fupBrent.SaveAs(path);
            hfdFilename.Value = path;
            if (ext.Trim() == ".xls")
            {
                //connection string for that file which extantion is .xls  
                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                //connection string for that file which extantion is .xlsx  
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            string query = "SELECT * FROM [Sheet1$]";
            OleDbConnection conn = new OleDbConnection(ConStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gvBrentUpload.DataSource = ds.Tables[0];
            gvBrentUpload.DataBind();
            conn.Close();
            //File.Delete(path);
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-brent-upload').modal('show');", true);
            //DisplaySuccess("Record saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbUploadBrentSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dDate;
            decimal dPrice;
            for(int i=0; i<=gvBrentUpload.Rows.Count-1; i++)
            {
                dDate = DateTime.Parse(gvBrentUpload.Rows[i].Cells[0].Text);
                dPrice = Decimal.Round(decimal.Parse(gvBrentUpload.Rows[i].Cells[1].Text), 4);
                if(objTrans.uploadDatedBrent(dDate,dPrice)==false)
                {
                    DisplayError(objTrans.ErrorMessage);
                    return;
                }
            }
            DisplaySuccess("Record uploaded successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveDiff_Click(object sender, EventArgs e)
    {
        try
        {
            if (objTrans.savePanel(price_diff, true) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGridDiff();
            DisplaySuccess("Record saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            if(sender.Equals(lbAddNewBrent))
            {
                objTrans.clearPanel(price_brent);
                rec_id.Value = "";
                brent_date.Enabled = true;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-brent').modal('show');", true);
            }
            if (sender.Equals(lbAddNewDiff))
            {
                objTrans.clearPanel(price_diff);
                rec_id_.Value = "";
                diff_month.Enabled = true;
                diff_year.Enabled = true;
                terminal_id.Enabled = true;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-diff').modal('show');", true);
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbGetPrice_Click(object sender, EventArgs e)
    {
        try
        {
            //DisplaySuccess("Prices retrieved successfully");
            DataSet ds = objTrans.getPrices(int.Parse(ddlMonthPrice.SelectedValue), int.Parse(ddlYearPrice.SelectedValue));
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.ReportPath = Server.MapPath("report\\price.rdlc");
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsPrice", ds.Tables[0]));
            rptViewer.DocumentMapCollapsed = true;

            rptViewer.AsyncRendering = true;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.FullPage;
            rptViewer.LocalReport.EnableExternalImages = true;

            //ReportParameter param = new ReportParameter("parmTitle", objRep.ReportTile);
            //rptViewer.LocalReport.SetParameters(param);

            rptViewer.LocalReport.Refresh();
            rptViewer.Visible = true;

        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(sender.Equals(ddlYearBrent))
            {
                loadGridBrent();
            }
            if (sender.Equals(ddlYearDiff))
            {
                loadGridDiff();
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (sender.Equals(ddlMonthBrent))
            {
                loadGridBrent();
            }
            if (sender.Equals(ddlMonthDiff))
            {
                loadGridDiff();
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (sender.Equals(gvPriceBrent))
            {
                GridViewRow dgi = gvPriceBrent.SelectedRow;
                rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvPriceBrent, "RecID")].Text;
                objTrans.getPanelByRecID(price_brent);
                brent_date.Enabled = false;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-brent').modal('show');", true);
            }
            if (sender.Equals(gvPriceDiff))
            {
                GridViewRow dgi = gvPriceDiff.SelectedRow;
                rec_id_.Value = dgi.Cells[objTrans.getColumnIndexByName(gvPriceDiff, "RecID")].Text;
                objTrans.getPanelByRecID(price_diff);
                diff_month.Enabled = false;
                diff_year.Enabled = false;
                terminal_id.Enabled = false;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-diff').modal('show');", true);
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void gvList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (sender.Equals(gvPriceBrent))
            {
                GridViewRow dgi = gvPriceBrent.Rows[e.RowIndex];
                rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvPriceBrent, "RecID")].Text;
                if (objTrans.deletePanelByRecID(price_brent) == false)
                {
                    DisplayError(objTrans.ErrorMessage);
                    return;
                }
                loadGridBrent();
                DisplaySuccess("Record deleted successfully.");
            }
            if (sender.Equals(gvPriceDiff))
            {
                GridViewRow dgi = gvPriceDiff.Rows[e.RowIndex];
                rec_id_.Value = dgi.Cells[objTrans.getColumnIndexByName(gvPriceDiff, "RecID")].Text;
                if (objTrans.deletePanelByRecID(price_diff) == false)
                {
                    DisplayError(objTrans.ErrorMessage);
                    return;
                }
                loadGridDiff();
                DisplaySuccess("Record deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (sender.Equals(gvPriceBrent))
            {
                gvPriceBrent.PageIndex = e.NewPageIndex;
                loadGridBrent();
            }
            if (sender.Equals(gvPriceDiff))
            {
                gvPriceDiff.PageIndex = e.NewPageIndex;
                loadGridDiff();
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPagingBrent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvPriceBrent.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPagingBrent");
            gvPriceBrent.PageSize = int.Parse(_ddl.SelectedValue);
            loadGridBrent();
            _ddl.SelectedValue = gvPriceBrent.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPagingDiff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvPriceDiff.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPagingDiff");
            gvPriceDiff.PageSize = int.Parse(_ddl.SelectedValue);
            loadGridDiff();
            _ddl.SelectedValue = gvPriceDiff.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }



}