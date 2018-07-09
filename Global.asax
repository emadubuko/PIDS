<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    static void RegisterRoutes(RouteCollection routes)
    {
        //first param is unique, second param is the expected url, thrid param is the actual file/page

        //general root menu
        routes.MapPageRoute("rtHome", "en", "~/default.aspx");
        routes.MapPageRoute("rtTimeOut", "error", "~/default.aspx");
        routes.MapPageRoute("rtDash", "dashboard", "~/dashboard.aspx");

        //system admin
        routes.MapPageRoute("rtSysAdmin", "system-admin", "~/system_admin.aspx");
        routes.MapPageRoute("rtRerence", "basic-settings", "~/reference.aspx");
        routes.MapPageRoute("rtRefTable", "reference-table", "~/reference_table.aspx");
        routes.MapPageRoute("rtAdminBank", "admin-bank", "~/bank.aspx");
        routes.MapPageRoute("rtAdminCountry", "admin-country", "~/country.aspx");
        routes.MapPageRoute("rtAdminDest", "admin-destination", "~/destination.aspx");
        routes.MapPageRoute("rtAdminExchRate", "admin-exchange-rate", "~/exchange_rate.aspx");
        routes.MapPageRoute("rtAdminConsignee", "admin-consignee", "~/consignee.aspx");
        routes.MapPageRoute("rtAdminConsignor", "admin-consignor", "~/consignor.aspx");
        routes.MapPageRoute("rtAdminInspector", "admin-inspector", "~/inspector.aspx");
        routes.MapPageRoute("rtAdminNESS", "admin-ness", "~/ness_admin.aspx");
        routes.MapPageRoute("rtAdminPIA", "admin-pia", "~/pia_admin.aspx");
        routes.MapPageRoute("rtAdminTerminal", "admin-terminal", "~/terminal.aspx");
        routes.MapPageRoute("rtAdminVessel", "admin-vessel", "~/vessel.aspx");
        routes.MapPageRoute("rtAdminPrice", "admin-price", "~/pricing.aspx");

        //others
        routes.MapPageRoute("rtNXP", "nxp", "~/nxp.aspx");
        routes.MapPageRoute("rtNXPReg", "nxp-register", "~/nxp_register.aspx");
        routes.MapPageRoute("rtInspFind", "inspection-finding", "~/inspection_finding.aspx");
        routes.MapPageRoute("rtInspReg", "inspection-register", "~/inspection_register.aspx");
        routes.MapPageRoute("rtDoc", "documentation", "~/documentation.aspx");
        routes.MapPageRoute("rtCCIMan", "cci-manager", "~/cci_manager.aspx");
        routes.MapPageRoute("rtReport", "reports", "~/reports.aspx");
        routes.MapPageRoute("rtInvMan", "invoice-manager", "~/invoice_manager.aspx");
        routes.MapPageRoute("rtRepMonthly", "monthly-reports", "~/report_monthly.aspx");
        routes.MapPageRoute("rtRepQuarterly", "quarterly-reports", "~/report_quarterly.aspx");
        routes.MapPageRoute("rtRepYearly", "yearly-reports", "~/report_yearly.aspx");
        routes.MapPageRoute("rtRepDiff", "differential-reports", "~/report_diff.aspx");
        
    }

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
        //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
    }
       
</script>
