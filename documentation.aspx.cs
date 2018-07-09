using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PIDSLibrary;

public partial class _Documentation : System.Web.UI.Page
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
                objTrans.formatPanel(nxp_doc);
                objTrans.formatPanel(nxp);
                //objTrans.formatPanel(qry_nxp_parcel);

                setReadOnlyField();
                objTrans.PopulateLists(ref ddlYear, "GET_YEAR");
                objTrans.PopulateLists(ref ddlMonth, "GET_MONTH");
                objTrans.PopulateLists(ref nxp_doc);

                startNewNXP(sender, e);

                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                lbSearch_Click(sender, e);

            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    private void setReadOnlyField()
    {
        try
        {
            nxp_fob_value.Attributes.Add("readonly", "readonly");
            fob_value_figure.Attributes.Add("readonly", "readonly");
            fob_value_word.Attributes.Add("readonly", "readonly");
            txtTransID.Attributes.Add("readonly", "readonly");

            exporter_nxp_unit_price_dollar.Attributes.Add("readonly", "readonly");
            exporter_fob_value_dollar.Attributes.Add("readonly", "readonly");
            pia_unit_price.Attributes.Add("readonly", "readonly");
            pia_exchange_rate.Attributes.Add("readonly", "readonly");
            pia_exchange_date.Attributes.Add("readonly", "readonly");
            pia_fob_value_dollar.Attributes.Add("readonly", "readonly");
            pia_fob_value_naira.Attributes.Add("readonly", "readonly");
            pia_fob_value_word_dollar.Attributes.Add("readonly", "readonly");
            pia_fob_value_word_naira.Attributes.Add("readonly", "readonly");
            pia_fee_naira.Attributes.Add("readonly", "readonly");
            exporter_fob_value_word_dollar.Attributes.Add("readonly", "readonly");
            exporter_fob_value_word_naira.Attributes.Add("readonly", "readonly");
            ness_percent.Attributes.Add("readonly", "readonly");
            ness_fee_payable_dollar.Attributes.Add("readonly", "readonly");
            ness_fee_paybale_naira.Attributes.Add("readonly", "readonly");
            exporter_fob_value_ness_dollar.Attributes.Add("readonly", "readonly");
            ness_fee_over_under_pay.Attributes.Add("readonly", "readonly");
            ness_fee_paid_naira.Attributes.Add("readonly", "readonly");
            forex_proceed_dollar.Attributes.Add("readonly", "readonly");
        }
        catch (Exception ex)
        {

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
            /*
            string sWhereClause = " nxp_no LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%' ";
            */
            string sWhereClause = " (MONTH(bill_of_lading_date)=" + ddlMonth.SelectedValue + " AND " +
                "YEAR(bill_of_lading_date)=" + ddlYear.SelectedValue + ") AND " +
                "(transaction_id LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%') ";
            objTrans.getNonTemplateGrid(gvListDoc, sWhereClause);
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
    protected bool calcPIAandNESSValues()
    {
        try
        {
            if (objTrans.getSystemPrice(terminal_id.Value, DateTime.Parse(bill_of_lading_date.Text)) == false)
            {
                //pricing information does not exist.  disable save button bcos documentation cannot be done
                DisplayError(objTrans.ErrorMessage + "  Price Information for the Bill of Lading Date does not exist.");
                return false;
            }
            objTrans.NXP_Currency = currency_id.SelectedValue;
            pia_unit_price.Text = objTrans.Sys_Price.ToString();
            if (objTrans.getSystemExchangeRate(objTrans.NXP_Currency) == false)
            {
                //exchange rate information does not exist.  disable save button bcos documentation cannot be done.                    DisplayError(objTrans.ErrorMessage + "  Documentation process cannot continue.");
                //lbSave.Visible = false;
                //return;
            }
            pia_exchange_rate.Text = objTrans.Sys_ExchangeRate.ToString();
            pia_exchange_date.Text = objTrans.Sys_ExchangeDate.ToString();
            pia_fob_value_dollar.Text = (objTrans.Sys_Price * decimal.Parse(barrels_net_qty.Text)).ToString();
            pia_fob_value_naira.Text = ((objTrans.Sys_Price * decimal.Parse(barrels_net_qty.Text)) * objTrans.Sys_ExchangeRate).ToString();

            //compute and set NESS values
            ness_percent.Text = objTrans.getNESSFee().ToString();
            ness_fee_payable_dollar.Text = (decimal.Parse(pia_fob_value_dollar.Text) * decimal.Parse(ness_percent.Text)).ToString();

            //compute and set pia fee
            decimal dPiaPercent = objTrans.getPIAFee();
            pia_fee_naira.Text = (decimal.Parse(pia_fob_value_naira.Text) * dPiaPercent).ToString();

            if (Page.IsPostBack==true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "reCalcValues", "reCalcAllValues();", true);
            }
            return true;
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
            return false;
        }
    }
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string iRecID = "";
            string sTransID = "";
            string sParcelID = "";
            GridViewRow dgi;

            dgi = gvListDoc.SelectedRow;
            iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListDoc, "RecID")].Text;
            rec_id.Value = iRecID;
            if (objTrans.getViewByRecID(qry_nxp_parcel) == false)
            {
                //parcel does not exist
                DisplayError(objTrans.ErrorMessage);
                return;
            }

            sTransID = dgi.Cells[objTrans.getColumnIndexByName(gvListDoc, "TransID")].Text;
            sParcelID = dgi.Cells[objTrans.getColumnIndexByName(gvListDoc, "ParcelID")].Text;
            transaction_id.Value = sTransID;
            txtTransID.Text = sTransID;
            parcel_id.Value = sParcelID;
            if (objTrans.getPanel(nxp_doc) == false)
            {
                //documentation does not exist.  set stage for new documentation
                objTrans.clearPanel(nxp_doc);
                nxp_no_.Value = nxp_no.Text;
                rec_id_.Value = "";

                //compute and set PIA values
                if (objTrans.getNXP(nxp_no.Text)==false)
                {
                    //nxp information does not exist.  disable save button bcos documentation cannot be done on non-existent nxp
                    /*
                    DisplayError(objTrans.ErrorMessage + "  Documentation process cannot continue.");
                    lbSave.Visible = false;
                    return;
                    */
                    DisplayWarning("NXP information has not been entered for this parcel.  Exporter FOB Value and Unit Price cannot be determined. Deafult Currency used is " + currency_id.SelectedValue);
                    objTrans.NXP_Currency = currency_id.SelectedValue;
                }
                else
                {
                    //attempt retirving NXP in case this is the second parcel of a split parcel
                    getNXP(sender, e);  //load nxp form
                    lbCalculate.Visible = false;

                    exporter_fob_value_dollar.Text = objTrans.NXP_FOBValue.ToString();
                    exporter_nxp_unit_price_dollar.Text = objTrans.NXP_UnitPrice.ToString();
                }

                if (calcPIAandNESSValues()==false)
                {
                    DisplayError(objTrans.ErrorMessage + "  Documentation process cannot continue.");
                    //lbSave.Visible = false;
                    return;
                }
            }
            else
            {
                getNXP(sender, e);  //load nxp form
                calcPIAandNESSValues();
                lbCalculate.Visible = false;
            }
            pnlSearch.Visible = false;
            pnlInput.Visible = true;
        }
        catch (Exception ex)
        {
            pnlSearch.Visible = false;
            pnlInput.Visible = true;
            DisplayError(objTrans.ErrorMessage + "  " + ex.Message + "  Plz, check that bill of lading date, exchange rate and price date are properly setup in the system admin section.");
        }
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListDoc.PageIndex = e.NewPageIndex;
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
            dgi = gvListDoc.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListDoc.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListDoc.PageSize.ToString();
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
            //save documentation
            if (objTrans.savePanel(nxp_doc, false) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            if(objTrans.getPanel(nxp_doc)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                //return;
            }

            //save nxp
            /*
            transaction_id_.Value = parcel_id_.Text;        //re-visit this code.  assign transaction_id instead of parcel_id
            if (objTrans.savePanel(nxp, false) == false)
            {
                DisplayError(objTrans.ErrorMessage);
                return;
            }
            */
            nxp_no___.Value = txtNXPNo.Text;
            saveNXP();
            if (objTrans.getPanel(nxp)==false)
            {
                DisplayError(objTrans.ErrorMessage);
                //return;
            }

            //retrieve transaction details at the top of the screen
            if (objTrans.getViewByRecID(qry_nxp_parcel) == false)
            {
                //parcel does not exist
                //DisplayError(objTrans.ErrorMessage);
                //return;
            }

            DisplaySuccess("All information saved successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void lbCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            //compute and set PIA values
            if (objTrans.getNXP(nxp_no.Text) == false)
            {
                //nxp information does not exist.  disable save button bcos documentation cannot be done on non-existent nxp
                /*
                DisplayError(objTrans.ErrorMessage + "  Documentation process cannot continue.");
                lbSave.Visible = false;
                return;
                */
                DisplayError("NXP information has not been entered for this parcel.");
            }
            else
            {
                exporter_fob_value_dollar.Text = objTrans.NXP_FOBValue.ToString();
                exporter_nxp_unit_price_dollar.Text = objTrans.NXP_UnitPrice.ToString();
            }

            calcPIAandNESSValues();

        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }

    #region "NXP Routines"
    protected void startNewNXP(object sender, EventArgs e)
    {
        try
        {
            //objTrans.formatPanel(nxp);

            objTrans.PopulateLists(ref nxp);
            ddlPostBack_Click(destination_id, e);
            ddlPostBack_Click(crude_type_id, e);
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void getNXP(object sender, EventArgs e)
    {
        try
        {
            //objTrans.formatPanel(nxp);
            txtNXPNo.Text = nxp_no.Text;
            nxp_no___.Value = txtNXPNo.Text;
            if (objTrans.getPanel(nxp) == false)
            {
                DisplayError(objTrans.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void ddlPostBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (sender.Equals(destination_id))
            {
                objTrans.PopulateLists(ref point_of_exit_id, "GET_PofE", destination_id.SelectedValue);
            }
            if (sender.Equals(crude_type_id))
            {
                objTrans.PopulateLists(ref departure_port_id, "GET_TERMINAL", crude_type_id.SelectedValue);
            }
            if (sender.Equals(currency_id))
            {
                calcPIAandNESSValues();
            }
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void saveNXP()
    {
        try
        {
            transaction_id_.Value = transaction_id.Value;
            if (objTrans.updateNXPNumberAcrossBoard(txtNXPNo.Text, transaction_id_.Value) == false)
            {
                DisplayError("Unable to update NXP Number for Inspection findinds and Documentation.  Contact System Administrator.  " + objTrans.ErrorMessage);
                return;
            }
            nxp_fob_value.Text = (decimal.Parse(nxp_qty.Text) * decimal.Parse(nxp_unit_price.Text)).ToString();
            fob_value_figure.Text = (decimal.Parse(nxp_fob_value.Text) * decimal.Parse(exchange_rate.Text)).ToString();
            if (objTrans.savePanel(nxp, false) == false)
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

    #endregion

}