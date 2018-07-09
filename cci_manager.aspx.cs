using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using _Foundation;
using PIDSLibrary;

public partial class _CCI_Manager : Page
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
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                lbSearch_Click(sender, e);

                //objTrans.formatPanel(nxp_parcel);
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
            string sWhereClause = " (MONTH(bill_of_lading_date)=" + ddlMonth.SelectedValue + " AND " +
                "YEAR(bill_of_lading_date)=" + ddlYear.SelectedValue + ") AND " +
                "(nxp_no LIKE '%" + name_search.Text + "%' OR consignee_name LIKE '%" + name_search.Text +
                "%' OR consignor_name LIKE '%" + name_search.Text + "%') ";
            objTrans.getNonTemplateGrid(gvListCCI, sWhereClause);

            setStatus();
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
            //string iRecID = "";
            string sCCINo = "";
            GridViewRow dgi;

            dgi = gvListCCI.SelectedRow;
            //iRecID = dgi.Cells[objGrid.getColumnIndexByName(gvListCCI, "RecID")].Text;
            sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "CCI No")].Text;

            DataSet ds = objTrans.getCCIReport(sCCINo);
            //DataSet dsAddendum = objTrans.getCCIReport(sCCINo);
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.ReportPath = Server.MapPath("report\\cci.rdlc");
            //rptViewer.LocalReport.
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsCCI", ds.Tables[0]));
            rptViewer.DocumentMapCollapsed = true;

            rptViewer.AsyncRendering = true;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.FullPage;
            rptViewer.LocalReport.EnableExternalImages = true;

            //ReportParameter param = new ReportParameter("parmTitle", objRep.ReportTile);
            //rptViewer.LocalReport.SetParameters(param);
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
            rptViewer.LocalReport.Refresh();
            pnlView.Visible = false;
            pnlReport.Visible = true;


        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            string sCCINo = "";
            GridViewRow dgi;

            dgi = gvListCCI.SelectedRow;
            sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "CCI No")].Text;

            DataSet dsAddendum = objTrans.getCCIReport(sCCINo);
            e.DataSources.Add(new ReportDataSource("dsCCIAddendum", dsAddendum.Tables[0]));

        }
        catch(Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListCCI.PageIndex = e.NewPageIndex;
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
            dgi = gvListCCI.FooterRow;
            DropDownList _ddl = (DropDownList)dgi.FindControl("ddlPaging");
            gvListCCI.PageSize = int.Parse(_ddl.SelectedValue);
            lbSearch_Click(sender, e);
            _ddl.SelectedValue = gvListCCI.PageSize.ToString();
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    private void setStatus()
    {
        try
        {
            GridViewRow dgi;
            string sCCINo = "";
            string sNXPNo = "";
            string sNESSRecNo = "";
            int iTotalRows = gvListCCI.Rows.Count;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                GridViewRow gdv = gvListCCI.Rows[i];
                CheckBox _chk = (CheckBox)gdv.FindControl("chkSelect");
                LinkButton _lb = (LinkButton)gdv.FindControl("lbSelect");
                
                dgi = gvListCCI.Rows[i];
                sCCINo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "CCI No")].Text.Trim();
                sNXPNo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "NXP No")].Text.Trim();
                sNESSRecNo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "NESS Receipt No")].Text.Trim();

                sCCINo = sCCINo.Replace("&nbsp;", "");
                sNXPNo = sNXPNo.Replace("&nbsp;", "");
                sNESSRecNo = sNESSRecNo.Replace("&nbsp;", "");

                if (sCCINo == string.Empty)
                {
                    if (sNXPNo == string.Empty || sNESSRecNo == string.Empty)
                    {
                        //parcel not due for CCI generation
                        _chk.Visible = false;
                        _lb.Visible = false;
                    }

                    if (sNXPNo != string.Empty && sNESSRecNo != string.Empty)
                    {
                        //parcel due for CCI generation
                        gvListCCI.Rows[i].ForeColor = System.Drawing.Color.Blue;
                        _chk.Visible = true;
                        _lb.Visible = false;
                    }
                    //gvListCCI.Rows[i].ForeColor = System.Drawing.Color.Red;
                    lbGenCCI.Visible = true;
                }
                else
                {
                    gvListCCI.Rows[i].ForeColor = System.Drawing.Color.LightGreen;
                    _chk.Visible = false;
                    _lb.Visible = true;
                }
                //_chk.Visible = true;
                //_lb.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DisplayError(ex.Message);
        }
    }
    protected void lbGenCCI_Click(object sender, EventArgs e)
    {
        try
        {
            string iRecID = "";
            string sNXPNo = "";
            string sNESSRecNo = "";
            string sConsignor = "";
            string sListText = "";
            GridViewRow dgi;
            int iTotalRows = gvListCCI.Rows.Count;

            lstCCI.Items.Clear();
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                GridViewRow gdv = gvListCCI.Rows[i];
                CheckBox _chk = (CheckBox)gdv.FindControl("chkSelect");

                if(_chk.Checked==true)
                {
                    dgi = gvListCCI.Rows[i];
                    iRecID = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "RecID")].Text;
                    sNXPNo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "NXP No")].Text.Trim();
                    sNESSRecNo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "NESS Receipt No")].Text.Trim();
                    sConsignor = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "Consignor")].Text.Trim();
                    sListText = sNXPNo + ", " + sNESSRecNo + ", " + sConsignor;
                    ListItem _lst = new ListItem(sListText, iRecID);
                    lstCCI.Items.Add(_lst);
                }
            }

            if(lstCCI.Items.Count<=0)
            {
                //no parcel selected
                DisplayError("You need to select at least one completely documented parcel to generate CCI");
                return;
            }
            cci_issue_date.Text = DateTime.Now.ToShortDateString();
            ClientScript.RegisterStartupScript(this.GetType(), "openPopup", "$('#modal-cci').modal('show');", true);
        }
        catch (Exception ex)
        {
            DisplayError("You need to select at least one completely documented parcel to generate CCI. " + ex.Message);
        }
    }
    protected void lbGenCCIYes_Click(object sender, EventArgs e)
    {
        try
        {
            string sTransID = "";
            string sParcelNo = "";
            int iMonth = 0;
            int iYear = 0;
            string sCCINo = string.Empty;
            DateTime dDate = DateTime.MinValue;
            GridViewRow dgi;
            int iTotalRows = gvListCCI.Rows.Count;

            if(DateTime.TryParse(cci_issue_date.Text, out dDate)==false)
            {
                DisplayError("Invalid CCI Issue Date");
                return;
            }

            iMonth = dDate.Month;
            iYear = dDate.Year;
            for (int i = 0; i <= iTotalRows - 1; i++)
            {
                GridViewRow gdv = gvListCCI.Rows[i];
                CheckBox _chk = (CheckBox)gdv.FindControl("chkSelect");
                if (_chk.Checked == true)
                {
                    dgi = gvListCCI.Rows[i];
                    sTransID = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "TransID")].Text.Trim();
                    sParcelNo = dgi.Cells[objTrans.getColumnIndexByName(gvListCCI, "Parcel No")].Text.Trim();
                    sTransID = sTransID.Replace("&nbsp;", "");
                    sParcelNo = sParcelNo.Replace("&nbsp;", "");
                    sCCINo = objTrans.getNewCCI(iMonth, iYear);
                    if(sCCINo==string.Empty)
                    {
                        DisplayError("Unable to generate CCI for parcel number " + sTransID);
                        return;
                    }
                    if(objTrans.saveCCINo(sTransID,sParcelNo,sCCINo,dDate)==false)
                    {
                        DisplayError("Unable to save CCI No to parcel record " + sTransID);
                        return;
                    }
                }
            }
            lbSearch_Click(sender, e);
            DisplaySuccess("CCI generated successfully");
        }
        catch (Exception ex)
        {
            DisplayError(objTrans.ErrorMessage + ex.Message);
        }
    }


}