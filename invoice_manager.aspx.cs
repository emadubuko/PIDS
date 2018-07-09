using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PIDSLibrary;

public partial class _Invoice_Manager : Page
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
                lbSearch_Click(sender, e);
            }
        }
        catch(Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbSearch_Click(object sender, EventArgs e)
    {
        try
        {
            loadGrid();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void loadGrid()
    {
        try
        {
            //int sLastDay = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue));
            string sDate = ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue;
            DateTime dDate = DateTime.Parse(sDate);
            string sWhereClause = " ((MONTH(bill_of_lading_date)=" + ddlMonth.SelectedValue + " AND " +
                "YEAR(bill_of_lading_date)=" + ddlYear.SelectedValue + ")) AND " +
                "(cci_no LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%') ";
            objTrans.getNonTemplateGrid(gvListInv, sWhereClause);

            sWhereClause = " (bill_of_lading_date <= '" + dDate.ToString() + "') AND " +
                "(cci_no LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%') AND invoice_no IS NULL ";
            objTrans.getNonTemplateGrid(gvListInvSupp, sWhereClause);

            setStatus();
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void ddlPostback_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadGrid();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if(sender.Equals(gvListInv))
            {
                gvListInv.PageIndex = e.NewPageIndex;
            }
            if(sender.Equals(gvListInvSupp))
            {
                gvListInvSupp.PageIndex = e.NewPageIndex;
            }
            lbSearch_Click(sender, e);
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvListInv.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListInv.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListInv.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPagingSupp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvListInvSupp.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPagingSupp");
            gvListInvSupp.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListInvSupp.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void setStatus()
    {
        try
        {
            GridViewRow dgi;
            string sInvNo = "";
            int iTotalRows = gvListInv.Rows.Count;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                //GridViewRow gdv = gvListInv.Rows[i];                
                dgi = gvListInv.Rows[i];
                sInvNo = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "Invoice No")].Text.Trim();
                sInvNo = sInvNo.Replace("&nbsp;", "");
                if (sInvNo == string.Empty)
                {
                    //parcel due for invoice generation
                    gvListInv.Rows[i].ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    gvListInv.Rows[i].ForeColor = System.Drawing.Color.LightGreen;
                    CheckBox _chk = (CheckBox)dgi.FindControl("chkSelect");
                    _chk.Visible = false;
                }
            }

            sInvNo = "";
            iTotalRows = gvListInvSupp.Rows.Count;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                //GridViewRow gdv = gvListInvSupp.Rows[i];
                dgi = gvListInvSupp.Rows[i];
                sInvNo = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "Invoice No")].Text.Trim();
                sInvNo = sInvNo.Replace("&nbsp;", "");

                if (sInvNo == string.Empty)
                {
                    //supplementary parcel due for invoice generation
                    gvListInvSupp.Rows[i].ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    gvListInvSupp.Rows[i].ForeColor = System.Drawing.Color.LightGreen;
                    CheckBox _chkSupp = (CheckBox)dgi.FindControl("chkSelectSupp");
                    _chkSupp.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbGenInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            string sCCINo = "";
            string sTransID = "";
            string sConsignor = "";
            string sConsignee = "";
            string sNetQty = "";
            string sListText = "";
            GridViewRow dgi;
            int iTotalRows = 0;

            //add items to main list
            iTotalRows = gvListInv.Rows.Count;
            lstInv.Items.Clear();
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                dgi = gvListInv.Rows[i];
                CheckBox _chk = (CheckBox)dgi.FindControl("chkSelect");

                if (_chk.Visible == true && _chk.Checked == true)
                {
                    //iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "RecID")].Text;
                    sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "CCI No")].Text.Trim();
                    sTransID = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "TransID")].Text.Trim();
                    sConsignor = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "Consignor")].Text.Trim();
                    sConsignee = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "Consignee")].Text.Trim();
                    sNetQty = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "Net Barrels")].Text.Trim();
                    sListText = sTransID + ", " + sConsignee + ", " + sConsignor + ", " + sNetQty;
                    ListItem _lst = new ListItem(sListText, sCCINo);
                    lstInv.Items.Add(_lst);
                }
            }

            //add items to supplementary list
            iTotalRows = gvListInvSupp.Rows.Count;
            lstInvSupp.Items.Clear();
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                dgi = gvListInvSupp.Rows[i];
                CheckBox _chk = (CheckBox)dgi.FindControl("chkSelectSupp");

                if (_chk.Visible == true && _chk.Checked == true)
                {
                    //iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "RecID")].Text;
                    sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "CCI No")].Text.Trim();
                    sTransID = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "TransID")].Text.Trim();
                    sConsignor = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "Consignor")].Text.Trim();
                    sConsignee = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "Consignee")].Text.Trim();
                    sNetQty = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "Net Barrels")].Text.Trim();
                    sListText = sTransID + ", " + sConsignee + ", " + sConsignor + ", " + sNetQty;
                    ListItem _lstSupp = new ListItem(sListText, sCCINo);
                    lstInvSupp.Items.Add(_lstSupp);
                }
            }

            if (lstInv.Items.Count <= 0 && lstInvSupp.Items.Count <= 0)
            {
                //no parcel selected
                DisplayError("You need to select at least one completely documented parcel to generate invoice Number");
                return;
            }
            inv_issue_date.Text = DateTime.Now.ToShortDateString();
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-invoice').modal('show');", true);

        }
        catch (Exception ex)
        {
            DisplayError("You need to select at least one completely documented parcel to generate Invoice Number. " + ex.Message);
        }
    }
    protected void lbGenInvoiceYes_Click(object sender, EventArgs e)
    {
        try
        {
            string sInvNo = "";
            int iMonth = 0;
            int iYear = 0;
            string sCCINo = string.Empty;
            string sDate = string.Empty;
            DateTime dDate = DateTime.MinValue;
            GridViewRow dgi;
            int iTotalRows;

            //main list
            iTotalRows = gvListInv.Rows.Count;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                dgi = gvListInv.Rows[i];
                CheckBox _chk = (CheckBox)dgi.FindControl("chkSelect");
                if (_chk.Checked == true)
                {
                    sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "CCI No")].Text.Trim();
                    sDate = dgi.Cells[objTrans.getColumnIndexByName(gvListInv, "B/L Date")].Text.Trim();
                    if (DateTime.TryParse(sDate, out dDate) == false)
                    {
                        DisplayError("Invalid Bill of Lading Date for CCI Number " + sCCINo);
                        return;
                    }
                    iMonth = dDate.Month;
                    iYear = dDate.Year;

                    sInvNo = objTrans.getNewInvoiceNo(iYear, iMonth);
                    if(sInvNo == string.Empty)
                    {
                        DisplayError("Unable to generate Invoice for CCI Number " + sCCINo);
                        return;
                    }
                    if(objTrans.updateInvoiceNo(sCCINo,sInvNo,dDate)==false)
                    {
                        DisplayError("Unable to update Invoice Number for CCI Number " + sCCINo);
                        return;
                    }
                }
            }

            //supplementary list
            iTotalRows = gvListInvSupp.Rows.Count;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                dgi = gvListInvSupp.Rows[i];
                CheckBox _chk = (CheckBox)dgi.FindControl("chkSelectSupp");
                if (_chk.Checked == true)
                {
                    sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "CCI No")].Text.Trim();
                    sDate = dgi.Cells[objTrans.getColumnIndexByName(gvListInvSupp, "B/L Date")].Text.Trim();
                    if (DateTime.TryParse(sDate, out dDate) == false)
                    {
                        DisplayError("Invalid Bill of Lading Date for supplementary CCI Number " + sCCINo);
                        return;
                    }
                    iMonth = dDate.Month;
                    iYear = dDate.Year;

                    sInvNo = objTrans.getNewInvoiceNoSuplementary(iYear, iMonth);
                    if (sInvNo == string.Empty)
                    {
                        DisplayError("Unable to generate Supplementary Invoice for CCI Number " + sCCINo);
                        return;
                    }
                    if (objTrans.updateInvoiceNo(sCCINo, sInvNo, dDate) == false)
                    {
                        DisplayError("Unable to update Supplementary Invoice Number for CCI Number " + sCCINo);
                        return;
                    }
                }
            }

            lbSearch_Click(sender, e);
            DisplaySuccess("Invoice generated successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbPrintInvoice_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objTrans.getInvScheduleReport(ddlMonth.SelectedValue, ddlYear.SelectedValue);
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.ReportPath = Server.MapPath("report\\invoice_schedule.rdlc");
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsInvSchedule", ds.Tables[0]));
            rptViewer.DocumentMapCollapsed = true;

            rptViewer.AsyncRendering = true;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.FullPage;
            rptViewer.LocalReport.EnableExternalImages = true;

            rptViewer.LocalReport.Refresh();
            pnlView.Visible = false;
            pnlReport.Visible = true;


        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }

}