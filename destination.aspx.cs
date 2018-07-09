using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _Destination : Page
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
                objTrans.PopulateLists(ref ddlCountry, "GET_COUNTRY");
                loadGrid(true, false);
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        try
        {
            if(sender.Equals(lbSaveDest))
            {
                objTrans.savePanel(country_dest, true);
                loadGrid(true, false);
            }
            if(sender.Equals(lbSavePofE))
            {
                objTrans.savePanel(country_dest_pofe, true);
                loadGrid(false, true);
            }
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
            if(sender.Equals(lbAddNewDest))
            {//button clicked is new destination
                if (ddlCountry.Items.Count <= 0)
                {
                    DisplayError("You have to create COUNTRY before DESTINATION.");
                    return;
                }
                objTrans.clearPanel(country_dest);
                country_acronym.Value = ddlCountry.SelectedValue;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
            }

            if (sender.Equals(lbAddNewPofE))
            {//button clicked is new point of exit
                if(ddlDestination.Items.Count<=0)
                {
                    DisplayError("You have to create/select DESTINATION before POINT of EXIT.");
                    return;
                }
                objTrans.clearPanel(country_dest_pofe);
                country_acronym_.Value = ddlCountry.SelectedValue;
                destination_acronym.Value = ddlDestination.SelectedValue;
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup2').modal('show');", true);
            }
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
            if(sender.Equals(lbOpenDest))
            {
                loadGrid(true, true);
            }
            if(sender.Equals(lbOpenPofE))
            {
                loadGrid(false, true);
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void loadGrid(bool bLoadDest, bool bLoadPofE)
    {
        try
        {
            if(bLoadDest==true)
            {
                ddlDestination.Items.Clear();
                string sWhereClause = " country_acronym='" + ddlCountry.SelectedValue + "' ";
                objTrans.getNonTemplateGrid(gvListDestination, sWhereClause);
                if(gvListDestination.Rows.Count>0)
                {
                    GridViewRow dgi = gvListDestination.Rows[0];
                    rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListDestination, "RecID")].Text;
                    objTrans.getPanelByRecID(country_dest);
                    objTrans.PopulateLists(ref ddlDestination, "GET_DESTINATION_BY_COUNTRY", ddlCountry.SelectedValue);
                    bLoadPofE = true;
                }
            }
            if(bLoadPofE==true)
            {
                string sWhereClause = " destination_acronym='" + ddlDestination.SelectedValue + "' ";
                objTrans.getNonTemplateGrid(gvListPofE, sWhereClause);
            }
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
            if(sender.Equals(gvListDestination))
            {
                GridViewRow dgi = gvListDestination.SelectedRow;
                rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListDestination, "RecID")].Text;
                objTrans.getPanelByRecID(country_dest);
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
            }
            if(sender.Equals(gvListPofE))
            {
                GridViewRow dgi = gvListPofE.SelectedRow;
                rec_id_.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListPofE, "RecID")].Text;
                objTrans.getPanelByRecID(country_dest_pofe);
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup2').modal('show');", true);
            }
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
            if(sender.Equals(gvListDestination))
            {
                GridViewRow dgi = gvListDestination.Rows[e.RowIndex];
                rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListDestination, "RecID")].Text;
                objTrans.deletePanelByRecID(country_dest);
                loadGrid(true, true);
                DisplaySuccess("Record deleted successfully.");
            }
            if (sender.Equals(gvListPofE))
            {
                GridViewRow dgi = gvListPofE.Rows[e.RowIndex];
                rec_id_.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListPofE, "RecID")].Text;
                objTrans.deletePanelByRecID(country_dest_pofe);
                loadGrid(false, true);
                DisplaySuccess("Record deleted successfully.");
            }
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
            if (sender.Equals(gvListDestination))
            {
                GridViewRow dgi = gvListDestination.Rows[e.NewEditIndex];
                rec_id.Value = dgi.Cells[objTrans.getColumnIndexByName(gvListDestination, "RecID")].Text;
                objTrans.getPanelByRecID(country_dest);
                //objTrans.PopulateLists(ref ddlDestination, "GET_DESTINATION_BY_COUNTRY", meta_data_id.Value);
                ddlDestination.SelectedValue = entity_acronym.Text;
                loadGrid(false, true);
                e.Cancel = true;
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
            if (sender.Equals(gvListDestination))
            {
                gvListDestination.PageIndex = e.NewPageIndex;
                loadGrid(true, true);
            }
            if (sender.Equals(gvListPofE))
            {
                gvListPofE.PageIndex = e.NewPageIndex;
                loadGrid(false, true);
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPagingDest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvListDestination.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPagingDest");
            gvListDestination.PageSize = int.Parse(_ddl.SelectedValue);
            loadGrid(true, true);
            _ddl.SelectedValue = gvListDestination.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPagingPofE_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow dgi;
            dgi = gvListPofE.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPagingPofE");
            gvListPofE.PageSize = int.Parse(_ddl.SelectedValue);
            loadGrid(false, true);
            _ddl.SelectedValue = gvListPofE.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }


}