using System;

public partial class _PidsMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisplayMessage("", MsgType.Success);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    public enum MsgType
    {
        Error = 0,
        Success = 1,
        Warning = 2
    }
    public void DisplayMessage(String sMessage, MsgType type)
    {
        try
        {
            if (sMessage.Trim() == "") {
                pnlAlert.Visible = false;
            }
            else
            {
                lblMsg.Text = sMessage;
                if (type == MsgType.Success)
                {
                    pnlAlert.CssClass = "alert alert-success";
                    spIcon.InnerHtml = "<i class='fa fa-check-circle-o'></i>";
                }
                else if (type == MsgType.Warning)
                {
                    pnlAlert.CssClass = "alert alert-warning";
                    spIcon.InnerHtml = "<i class='fa fa-exclamation-triangle'></i>";
                }
                else
                {
                    pnlAlert.CssClass = "alert alert-danger";
                    spIcon.InnerHtml = "<i class='fa fa-exclamation-circle'></i>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "displayMsg", "alert('" + sMessage + "');", true);
                }
                pnlAlert.Visible = true;
                //ClientScript.RegisterStartupScript(this.GetType(), "displayMsg", "alert('" + sMessage + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            pnlAlert.Visible = true;
        }
    }

    protected void _Logout_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/en");
        }
        catch (Exception ex)
        {
            String sMessage = ex.Message;
            Response.Redirect("~/en?" + sMessage);
        }
        
    }

    protected void lnkSavePassword_Click(object sender, EventArgs e)
    {
        String strMsg = "";
        try
        {
        }
        catch (Exception ex)
        {
            DisplayMessage(strMsg + ex.Message, MsgType.Error);
        }
    }
}
