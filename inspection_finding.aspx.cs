using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _InspectionFinding : System.Web.UI.Page
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
                objTrans.formatPanel(nxp_vessel, nxp_vessel_others);
                objTrans.formatPanel(nxp_bofl);
                objTrans.formatPanel(nxp_parcel);
                objTrans.formatPanel(nxp_inspection);

                try
                {   //check if inspection register is the caller
                    string iRecID = Session["inspection_rec_id"].ToString();
                    if(iRecID.Length > 0) loadInspection(iRecID);
                    Session["inspection_rec_id"] = string.Empty;
                    Session["inspection_bofl_date"] = bill_of_lading_date.Text;
                    return;
                }
                catch (Exception ex) { }

                objTrans.PopulateLists(ref nxp_vessel);
                objTrans.PopulateLists(ref nxp_vessel_others);
                objTrans.PopulateLists(ref nxp_parcel);
                objTrans.PopulateLists(ref destination_id, "GET_DESTINATION_BY_COUNTRY", country_id.SelectedValue);

                //store present bill of lading date
                Session["inspection_bofl_date"] = bill_of_lading_date.Text;
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void country_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objTrans.PopulateLists(ref destination_id, "GET_DESTINATION_BY_COUNTRY", country_id.SelectedValue);
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void loadGridParcel()
    {
        try
        {
            string sWhereClause = " transaction_id = '" + transaction_id.Text + "'";
            objTrans.getNonTemplateGrid(gvListParcel, sWhereClause);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void createTransactionID()
    {
        try
        {
            //create transaction ID for bill of lading
            DateTime dTransDate;
            try
            {
                //trap error for the first running bcos bofldateprev will throw null error
                DateTime sBofLDatePrev = DateTime.Parse(Session["inspection_bofl_date"].ToString()).Date;
                DateTime sBofLDateNew = DateTime.Parse(bill_of_lading_date.Text.ToString()).Date;
                if (sBofLDatePrev != sBofLDateNew)
                {
                    dTransDate = DateTime.Parse(bill_of_lading_date.Text);
                    transaction_id.Text = objTrans.getNewTransactionID(dTransDate.Month, dTransDate.Year, nxp_no.Text, terminal_id.SelectedValue);
                    transaction_id___.Value = transaction_id.Text;
                    if(gvListParcel.Rows.Count>0)
                    {
                        //bill of lading date is being changed.  change parcel id and transaction id
                        changeParcelID();
                    }
                    lbSaveAll.Visible = true;
                    lbSaveBofL.Visible = true;
                    return;
                }
            }
            catch (Exception ex) { }

            if(transaction_id___.Value.Trim() == string.Empty)
            {
                dTransDate = DateTime.Parse(bill_of_lading_date.Text);
                transaction_id.Text = objTrans.getNewTransactionID(dTransDate.Month, dTransDate.Year, nxp_no.Text, terminal_id.SelectedValue);
                transaction_id___.Value = transaction_id.Text;
                lbSaveAll.Visible = true;
                lbSaveBofL.Visible = true;
                Session["inspection_bofl_date"] = dTransDate.ToString();
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveVessel_Click(object sender,EventArgs e)
    {
        try
        {
            createTransactionID();

            if (objTrans.savePanel(nxp_vessel, nxp_vessel_others, false) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            if (rec_id_.Value==null || rec_id_.Value=="")
            {
                //this is the first time of saving vessel details.  
                //retrieve back the saved values so that savePanel will use UPDATE nstead of INSERT on subsequent clicks
                if (objTrans.getPanel(nxp_vessel) == false)
                {
                    DisplayError("Unable to retrieve saved Vessel Details. " + objTrans.ErrorMessage);
                    return;
                }
            }
            DisplaySuccess("Vessel details saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveBofL_Click(object sender,EventArgs e)
    {
        try
        {
            createTransactionID();

            if (objTrans.savePanel(nxp_bofl, false) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            if (rec_id__.Value == string.Empty || rec_id__.Value == "")
            {
                //this is the first time of saving bill of lading.  
                //retrieve back the saved values so that savePanel will use UPDATE nstead of INSERT on subsequent clicks
                if (objTrans.getPanel(nxp_bofl) == false)
                {
                    DisplayError("Unable to retrieve Bill of Lading Details. " + objTrans.ErrorMessage);
                    return;
                }
            }

            //lbSaveSplit_Click(sender, e);

            DisplaySuccess("Bill of Lading saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            int iNoOfParcel = int.Parse(no_of_parcel.SelectedValue);

            lbSaveBofL_Click(sender, e);        //save bill of lading information
            lbSaveVessel_Click(sender, e);      //save vessel information

            if ((shipment_type_id.SelectedValue == "SINGLE") && (iNoOfParcel == 1))
            {
                //create parcel details using bill of lading if shipment type is SINGLE
                if (iNoOfParcel != gvListParcel.Rows.Count)
                {
                    objTrans.createParcelUsingBofL(nxp_no.Text, transaction_id.Text, consignee_id.SelectedValue, consignor_id.SelectedValue);
                    loadGridParcel();
                }
            }
            else
            {
                //create split parcels
                lbSaveSplit_Click(sender, e);
            }
            if(objTrans.sendMailTransaction(transaction_id.Text)==false)
            {
                DisplaySuccess("All information saved successfully but unable to send mail notification.  Contact site Administrator.");
                return;
            }
            DisplaySuccess("All information saved successfully");

        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbSaveParcel_Click(object sender, EventArgs e)
    {
        try
        {
            if(barrels_gross_qty.Text.Trim()==string.Empty || barrels_net_qty.Text.Trim()==string.Empty)
            {
                DisplayError("You have to enter the Bill of Lading information.");
                return;
            }
            if(objTrans.isParcelQtyWithinBofL(transaction_id.Text, parcel_id.Text, decimal.Parse(barrels_gross_qty.Text), 
                decimal.Parse(barrels_net_qty.Text), decimal.Parse(barrels_gross_qty_.Text), 
                decimal.Parse(barrels_net_qty_.Text))==false)
            {
                DisplayError(objTrans.ErrorMessage);
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
                return;
            }
            if(objTrans.savePanel(nxp_parcel, true)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            loadGridParcel();
            DisplaySuccess("Parcel saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + " " + objTrans.ErrorMessage + " " + ex.Message);
        }
    }
    protected void lbSaveSplit_Click(object sender, EventArgs e)
    {
        try
        {
            string sParcelID = "";
            int i = 0;
            int iNoOfParcel = int.Parse(no_of_parcel.SelectedValue);

            if(iNoOfParcel != gvListParcel.Rows.Count && shipment_type_id.SelectedValue.ToUpper() != "SINGLE")
            //create parcel list if number of parcels selected is different from the total parcels already created
            {
                if (objTrans.deleteParcels(transaction_id.Text) == false)
                {
                    //DisplayError(objTrans.ErrorMessage);
                    //return;
                }
                for (i = 1; i <= iNoOfParcel; i++)
                {
                    sParcelID = objTrans.getNewParcelID(transaction_id.Text, i);
                    if (objTrans.createParcel(nxp_no.Text, transaction_id.Text, sParcelID, i) == false)
                    {
                        DisplayError(objTrans.ErrorMessage);
                        return;
                    }
                }
                loadGridParcel();
            }

        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void changeParcelID()
    {
        try
        {
            GridViewRow dgi;
            string sParcelID = "";
            string sParcelNo = "";
            string sTransID = transaction_id.Text;
            int iNoOfParcel = gvListParcel.Rows.Count - 1;

            for (int i = 0; i <= iNoOfParcel; i++)
            {
                dgi = gvListParcel.Rows[i];
                sParcelNo = dgi.Cells[objTrans.getColumnIndexByName(gvListParcel, "Parcel")].Text;
                sParcelID = dgi.Cells[objTrans.getColumnIndexByName(gvListParcel, "Parcel ID")].Text;
                if (objTrans.updateParcelID(sParcelID, sTransID, sParcelNo) == false)
                {
                    DisplayError(objTrans.ErrorMessage);
                    return;
                }
            }
            //loadGridParcel();
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void loadInspection(string iRecID)
    {
        try
        {
            //VESSEL DETAILS

            //retrieve NXP INFO section
            rec_id.Value = iRecID;
            if(objTrans.getViewByRecID(nxp_inspection) ==false)
            {
                //NXP does not exist
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            //retrieve vessel details
            transaction_id___.Value = transaction_id__.Text;
            if(objTrans.getPanel(nxp_vessel, nxp_vessel_others)==false)
            {
                //VESSEL details does not exit.  Clear vessel & bill of lading inputs and set the stage for new vessel creation
                objTrans.clearPanel(nxp_vessel);
                nxp_no_.Value = nxp_no.Text;
                transaction_id___.Value = transaction_id__.Text;
                rec_id_.Value = "";

                //hide every save button except SAVE VESSEL because we need bill of lading date to generate transaction_id
                //for single and split parcels
                lbSaveAll.Visible = false;
                lbSaveBofL.Visible = false;
                lbSaveVessel.Visible = true;
            }

            //retrieve bill of lading details
            transaction_id.Text = transaction_id__.Text;
            if (objTrans.getPanel(nxp_bofl)==false)
            {
                //BILL of LADING details does not exist. Clear bill of lading info and set stage for new bill of lading details
                objTrans.clearPanel(nxp_bofl);
                nxp_no__.Value = nxp_no.Text;
                transaction_id.Text = transaction_id__.Text;
                rec_id__.Value = "";
                //DateTime dTransDate = DateTime.Parse(bill_of_lading_date.Text);
                //transaction_id.Text = objTrans.getNewTransactionID(dTransDate.Month, dTransDate.Year, nxp_no.Text);
            }

            //load parcel grid
            loadGridParcel();

        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_SelectedIndexChanged(object sender, EventArgs a)
    {
        try
        {
            GridViewRow dgi;
            string iRecID;
            //LOAD PARCEL DETAILS
            if (sender.Equals(gvListParcel))
            {
                dgi = gvListParcel.SelectedRow;
                iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListParcel, "RecID")].Text;
                rec_id___.Value = iRecID;
                objTrans.getPanelByRecID(nxp_parcel);
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-popup').modal('show');", true);
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }


}