<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="_Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="fa fa-print"></i> Reports</h1>
        </div>

        <div class="row well">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <rsweb:ReportViewer ID="rptViewer" runat="server" ProcessingMode="Local" Width="400px">
                </rsweb:ReportViewer>
            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">

</asp:Content>

