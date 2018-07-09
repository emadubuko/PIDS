using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _Reference : Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Page.IsPostBack==false)
            {
                lblTitle.Text = Session["meta_data_title"].ToString();
                lblTitlePopup.Text = lblTitle.Text;
                hfdMetaDataType.Value = Session["meta_data_type"].ToString();
                meta_data_type.Value = hfdMetaDataType.Value;
                lbAddNew.Text = "<i class=\"icon-docs\"></i> New " + Session["meta_data_title"].ToString();
                loadGrid();
            }
        }
        catch(Exception ex)
        {
            //DisplayError(ex.Message);
            Response.Redirect("~/en", true);
        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        try
        {
            if(objTrans.savePanel(meta_data_ref, true)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGrid();
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
            objTrans.clearPanel(meta_data_ref);
            meta_data_type.Value = hfdMetaDataType.Value;
            reference_id.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void loadGrid()
    {
        try
        {
            string sWhereClause = " meta_data_type='" + hfdMetaDataType.Value + "' ";
            objTrans.getNonTemplateGrid(gvListRefTable, sWhereClause);
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
            GridViewRow dgi = gvListRefTable.SelectedRow;
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListRefTable,"RecID")].Text;
            objTrans.getPanelByRecID(meta_data_ref);
            reference_id.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');",true);
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
            GridViewRow dgi = gvListRefTable.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListRefTable, "RecID")].Text;
            if(objTrans.deletePanelByRecID(meta_data_ref)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGrid();
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
            gvListRefTable.PageIndex = e.NewPageIndex;
            loadGrid();
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
            dgi = gvListRefTable.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListRefTable.PageSize = int.Parse(_ddl.SelectedValue);
            loadGrid();
            _ddl.SelectedValue = gvListRefTable.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }

}