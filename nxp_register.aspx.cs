using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _NXPRegister : Page
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
            string sWhereClause = " (MONTH(transaction_date)=" + ddlMonth.SelectedValue + " AND " + 
                "YEAR(transaction_date)=" + ddlYear.SelectedValue + ") AND " + 
                "(nxp_no LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%') ";
            objTrans.getNonTemplateGrid(gvListNXP, sWhereClause);
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
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string iRecID = "";
            GridViewRow dgi = gvListNXP.SelectedRow;
            iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListNXP, "RecID")].Text;
            Session["nxp_rec_id"] = iRecID;
            Response.Redirect("~/nxp", true);
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
            gvListNXP.PageIndex = e.NewPageIndex;
            lbSearch_Click(sender, e);
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }


}