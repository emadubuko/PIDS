using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _ExchangeRate : Page
{
    TransactionModel objTrans = new TransactionModel();
    SysAdminModel objSys = new SysAdminModel();

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
            objSys.CloseConnection();
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
                objTrans.formatPanel(exchange_rate);

                loadGrid("");
            }
        }
        catch(Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        try
        {
            if(default_flag.SelectedValue=="1")
            {//reset all exchange rate to 0 before saving the new default rate
                if (objSys.resetAllExchangeRate(currency_acronym.Text) == false)
                {
                    DisplayError(objSys.ErrorMessage);
                }
            }
            if(objTrans.savePanel(exchange_rate, true)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGrid("");
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
            objTrans.clearPanel(exchange_rate);
            currency_acronym.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sWhereClause = " currency_acronym = '" + name_search.Text + "' ";
            loadGrid(sWhereClause);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void loadGrid(string sCondition)
    {
        try
        {
            objTrans.getNonTemplateGrid(gvListeRate, sCondition);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi = gvListeRate.SelectedRow;
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListeRate,"RecID")].Text;
            objTrans.getPanelByRecID(exchange_rate);
            currency_acronym.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');",true);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow dgi = gvListeRate.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListeRate, "RecID")].Text;
            if(objTrans.deletePanelByRecID(exchange_rate)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGrid("");
            DisplaySuccess("Record deleted successfully.");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            long iRecID = 0;
            string sCurrency = "";
            GridViewRow dgi = gvListeRate.Rows[e.NewEditIndex];
            sCurrency = dgi.Cells[objTrans.getColumnIndexByName(gvListeRate, "Currency")].Text;
            iRecID = long.Parse(dgi.Cells[objTrans.getColumnIndexByName(gvListeRate, "RecID")].Text);
            if(objSys.setActiveExchangeRate(sCurrency, iRecID) ==false)
            {
                DisplayError(objSys.ErrorMessage);
            }
            DisplaySuccess("Active/Default Exchnage Rate set successfully");
            e.Cancel = true;
            loadGrid("");
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
            gvListeRate.PageIndex = e.NewPageIndex;
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
            dgi = gvListeRate.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListeRate.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListeRate.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }



}