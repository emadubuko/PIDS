using System;

public partial class _default : System.Web.UI.Page
{
    private void DisplayMessage(string sMessage)
    {
        lblMessage.Text = sMessage;
        //replace microsoft ajax script below with its equivalent jquery javascript
        //ScriptManager.RegisterStartupScript(lnkLogin, lnkLogin.GetType(), "alert", "displayMsg('" + sMessage + "');", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //check if exit is by error
            string sErrCode = Request.QueryString["code"].ToString();
            if(sErrCode=="0")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "displayMsg", "alert('Your session has timed out.  Please, login to continue.');", true);
            }
        }
        catch (Exception ex)
        {
            //LoginUser.FailureText = ex.Message;
        }
    }
    protected void lbLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/dashboard", true);
        }
        catch (Exception ex)
        {
            DisplayMessage(ex.Message);
        }
    }


}