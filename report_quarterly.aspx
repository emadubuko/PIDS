<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="report_quarterly.aspx.cs" Inherits="_ReportQuarterly" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-refresh"></i> Quarterly Reports</h1>
        </div>

        <div class="row well well-sm">
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Year</small>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Quarter</small>
                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-8 col-sm-12 form-group-sm">
                <small>Select a Report</small>
                <div class="input-group">
                    <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control"></asp:DropDownList>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbPreview" runat="server" CssClass="btn-sm btn-primary" OnClick="lbPreview_Click">
                            <i class="icon-reload"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <div class="panel panel-default">
                    <br />
                    <rsweb:ReportViewer ID="rptViewer" runat="server" ProcessingMode="Local" Width="100%">
                    </rsweb:ReportViewer>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

