<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="report_diff.aspx.cs" Inherits="_ReportDiff" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-refresh"></i> Differential Reports</h1>
        </div>

        <div class="row well well-sm">
            <div class="col-md-12 col-sm-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title visible-lg visible-md">
                            <ul class="nav nav-tabs visible-lg visible-md">
                                <li class="active"><a href="#tab1" data-toggle="tab">Monthly Differential Report</a></li>
                                <li><a href="#tab2" data-toggle="tab">Quarterly Differential Report</a></li>
                                <li><a href="#tab3" data-toggle="tab">Yearly Differential Report</a></li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab1"> 
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        <label class="text-center">1st Category</label>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        <label class="text-center">2nd Category</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlMonthlyYear1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Month</small>
                                        <asp:DropDownList ID="ddlMonthlyMonth1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlMonthlyYear2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Month</small>
                                        <asp:DropDownList ID="ddlMonthlyMonth2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <br />
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlMonthlyReport" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <span class="input-group-addon input-group-addon-select">
                                                <asp:LinkButton ID="lbMonthlyPreview" runat="server" CssClass="btn-sm btn-primary" OnClick="lbPreview_Click">
                                                    <i class="icon-reload"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab2"> 
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        <label class="text-center">1st Category</label>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        <label class="text-center">2nd Category</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlQuarterlyYear1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Quarter</small>
                                        <asp:DropDownList ID="ddlQuarterlyQuarter1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group-sm">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlQuarterlyYear2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>Quarter</small>
                                        <asp:DropDownList ID="ddlQuarterlyQuarter2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <br />
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlQuarterlyReport" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <span class="input-group-addon input-group-addon-select">
                                                <asp:LinkButton ID="lbQuarterlyPreview" runat="server" CssClass="btn-sm btn-primary" OnClick="lbPreview_Click">
                                                    <i class="icon-reload"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab3"> 
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>1st Year</small>
                                        <asp:DropDownList ID="ddlYearlyYear1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <small>2nd Year</small>
                                        <asp:DropDownList ID="ddlYearlyYear2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-8 col-sm-8 form-group-sm">
                                        <br />
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlYearlyReport" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <span class="input-group-addon input-group-addon-select">
                                                <asp:LinkButton ID="lbYearlyPreview" runat="server" CssClass="btn-sm btn-primary" OnClick="lbPreview_Click">
                                                    <i class="icon-reload"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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

