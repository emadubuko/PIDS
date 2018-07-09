using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _PIAAdmin : Page
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
                objTrans.formatPanel(pia_admin);
                pia_value.Attributes.Add("readonly", "readonly");
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
            if (default_flag.SelectedValue == "1")
            {//reset all NESS rate to 0 before saving the new default rate
                if (objSys.resetAllPIARate() == false)
                {
                    DisplayError(objSys.ErrorMessage);
                }
            }
            pia_value.Text = (decimal.Parse(pia_figure.Text) / 100).ToString();
            objTrans.savePanel(pia_admin, true);
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
            objTrans.clearPanel(pia_admin);
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
            string sWhereClause = " pia_date LIKE '%" + name_search.Text + "%' ";
            objTrans.getNonTemplateGrid(gvListPIAAdmin, sWhereClause);
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
            string sWhereClause = "";
            objTrans.getNonTemplateGrid(gvListPIAAdmin, sWhereClause);
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
            GridViewRow dgi = gvListPIAAdmin.SelectedRow;
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListPIAAdmin, "RecID")].Text;
            objTrans.getPanelByRecID(pia_admin);
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
            GridViewRow dgi = gvListPIAAdmin.Rows[e.RowIndex];
            rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListPIAAdmin, "RecID")].Text;
            objTrans.deletePanelByRecID(pia_admin);
            loadGrid();
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
            GridViewRow dgi = gvListPIAAdmin.Rows[e.NewEditIndex];
            iRecID = long.Parse(dgi.Cells[objTrans.getColumnIndexByName(gvListPIAAdmin, "RecID")].Text);
            if(objSys.setActivePIARate(iRecID)==false)
            {
                DisplayError(objSys.ErrorMessage);
            }
            DisplaySuccess("Active/Default Rate set successfully");
            e.Cancel = true;
            loadGrid();
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }



}