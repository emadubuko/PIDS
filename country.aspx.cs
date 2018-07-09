using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _Country : Page
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
            if (Page.IsPostBack == false)
            {
                objTrans.formatPanel(country);
                //loadGrid("");
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (objTrans.savePanel(country, true) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            DisplaySuccess("Record saved successfully");
            loadGrid("");
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
            objTrans.clearPanel(country);
            entity_acronym.Enabled = true;
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
            loadGrid(name_search.Text);
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void loadGrid(string sCondition)
    {
        try
        {
            string sWhereClause = " entity_name LIKE '%" + sCondition + "%' ";
            objTrans.getNonTemplateGrid(gvListCountry, sWhereClause);
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
            GridViewRow dgi = gvListCountry.SelectedRow;
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListCountry, "RecID")].Text;
            objTrans.getPanelByRecID(country);
            entity_acronym.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
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
            GridViewRow dgi = gvListCountry.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListCountry, "RecID")].Text;
            if(objTrans.deletePanelByRecID(country)==false)
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
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListCountry.PageIndex = e.NewPageIndex;
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
            dgi = gvListCountry.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListCountry.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListCountry.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }

}