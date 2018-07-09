using System;
using System.Web.UI;
using PIDSLibrary;

public partial class _NXP : Page
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
            if (Page.IsPostBack == false)
            {
                objTrans.formatPanel(nxp);
                setReadOnlyField();
                try
                {   //check if nxp register is the caller
                    string iRecID = Session["nxp_rec_id"].ToString();
                    if (iRecID.Length > 0)
                    {
                        rec_id.Value = iRecID;
                        if (objTrans.getPanelByRecID(nxp) == false)
                        {
                            DisplayError(objTrans.ErrorMessage);
                        }
                        Session["nxp_rec_id"] = string.Empty;
                    }
                    return;
                }
                catch(Exception ex) { }

                objTrans.PopulateLists(ref nxp);
                ddlPostBack_Click(destination_id, e);
                ddlPostBack_Click(crude_type_id, e);
                //transaction_date.Text = DateTime.Now.ToString("yyyy-mm-dd");
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void setReadOnlyField()
    {
        try
        {
            nxp_fob_value.Attributes.Add("readonly", "readonly");
            fob_value_figure.Attributes.Add("readonly", "readonly");
            fob_value_word.Attributes.Add("readonly", "readonly");
        }
        catch(Exception ex)
        {

        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        try
        {
            if(transaction_id.Text.Trim()!=string.Empty && nxp_no.Text.Trim()!=string.Empty)
            {
                if(objTrans.updateNXPNumberAcrossBoard(nxp_no.Text, transaction_id.Text)==false)
                {
                    DisplayError("Unable to update NXP Number for Inspection findinds and Documentation. NXP will not be saved.  Contact System Administrator.  " + objTrans.ErrorMessage);
                    return;
                }
            }
            nxp_fob_value.Text = (decimal.Parse(nxp_qty.Text) * decimal.Parse(nxp_unit_price.Text)).ToString();
            fob_value_figure.Text = (decimal.Parse(nxp_fob_value.Text) * decimal.Parse(exchange_rate.Text)).ToString();
            if(objTrans.savePanel(nxp, true)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            DisplaySuccess("Saving successful");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void ddlPostBack_Click(object sender,EventArgs e)
    {
        try
        {
            if(sender.Equals(destination_id))
            {
                objTrans.PopulateLists(ref point_of_exit_id, "GET_PofE", destination_id.SelectedValue);
            }
            if (sender.Equals(crude_type_id))
            {
                objTrans.PopulateLists(ref departure_port_id, "GET_TERMINAL", crude_type_id.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }


}