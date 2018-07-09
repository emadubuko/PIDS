using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _ReferenceTable : Page
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
                objTrans.PopulateLists(ref ddlReference, "GET_REFERENCE_TYPE");
                loadGrid();
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
            objTrans.savePanel(meta_data_ref, true);
            loadGrid();
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
            meta_data_type.Value = ddlReference.SelectedValue;
            meta_data_id.Value = ddlReference.SelectedValue;
            reference_id.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbOpen_Click(object sender, EventArgs e)
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
            string sWhereClause = " meta_data_type='" + ddlReference.SelectedValue + "' ";
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
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow dgi = gvListRefTable.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListRefTable, "RecID")].Text;
            objTrans.deletePanelByRecID(meta_data_ref);
            loadGrid();
            DisplaySuccess("Record deleted successfully.");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }

}