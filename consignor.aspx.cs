using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _Consignor : Page
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
                //loadGrid("");
                objTrans.formatPanel(consignor);
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
            if (objTrans.savePanel(consignor, true) == false)
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
            objTrans.clearPanel(consignor);
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
            string sWhereClause = " entity_name LIKE '%" + name_search.Text + "%' ";
            loadGrid(sWhereClause);
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
            objTrans.getNonTemplateGrid(gvListConsignor, sCondition);
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
            GridViewRow dgi = gvListConsignor.SelectedRow;
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListConsignor, "RecID")].Text;
            objTrans.getPanelByRecID(consignor);
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
            GridViewRow dgi = gvListConsignor.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListConsignor, "RecID")].Text;
            if(objTrans.deletePanelByRecID(consignor)==false)
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
            gvListConsignor.PageIndex = e.NewPageIndex;
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
            dgi = gvListConsignor.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListConsignor.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListConsignor.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }

}