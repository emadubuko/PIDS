using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _SysAdmin : System.Web.UI.Page
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
                objTrans.getPanelByRecID(trans_number);
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage);
        }
    }
    protected void lbBasicSettings_Click(object sender, EventArgs e)
    {
        try
        {
            if (sender.Equals(lbCrudeType))
            {
                Session["meta_data_title"] = "Crude Type";
                Session["meta_data_type"] = "CRUDE_TYPE";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbMeasurementUnit))
            {
                Session["meta_data_title"] = "Measurement Unit";
                Session["meta_data_type"] = "MEASUREMENT_UNIT";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbCurrency))
            {
                Session["meta_data_title"] = "Currency";
                Session["meta_data_type"] = "CURRENCY";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbPackaging))
            {
                Session["meta_data_title"] = "Packaging";
                Session["meta_data_type"] = "PACKAGING";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbProductType))
            {
                Session["meta_data_title"] = "Product Type";
                Session["meta_data_type"] = "PRODUCT_TYPE";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbShipmentType))
            {
                Session["meta_data_title"] = "Shipment Type";
                Session["meta_data_type"] = "SHIPMENT_TYPE";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbParcelNumber))
            {
                Session["meta_data_title"] = "Number of Parcels";
                Session["meta_data_type"] = "PARCEL_NUMBER";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbDemurrageStatus))
            {
                Session["meta_data_title"] = "Demurrage Status";
                Session["meta_data_type"] = "DEMURRAGE_STATUS";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbBasisOfSale))
            {
                Session["meta_data_title"] = "Basis of Sale";
                Session["meta_data_type"] = "BASIS_OF_SALE";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbMethodOfPayment))
            {
                Session["meta_data_title"] = "Method of Payment";
                Session["meta_data_type"] = "METHOD_OF_PAYMENT";
                Response.Redirect("~/basic-settings", true);
            }
            if (sender.Equals(lbApprovalStatus))
            {
                Session["meta_data_title"] = "Approval Status";
                Session["meta_data_type"] = "APPROVAL_STATUS";
                Response.Redirect("~/basic-settings", true);
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
            if(objTrans.savePanel(trans_number,false)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            DisplaySuccess("Record saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage);
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if(chkNXP.Checked==true)
            {
                objTrans.deleteNXPs();
            }
            if (chkInsp.Checked == true)
            {
                objTrans.deleteInspection();
            }
            if (chkDoc.Checked == true)
            {
                objTrans.deleteDocumentation();
            }
            DisplaySuccess("Record deleted successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage);
        }
    }




}