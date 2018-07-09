<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="cci_manager.aspx.cs" Inherits="_CCI_Manager" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-badge"></i> CCI Manager</h1>
        </div>
    </div>
    <asp:Panel ID="pnlView" runat="server">
        <div class="row well well-sm">
            <div class="panel-heading">
                <h2>Find Parcel</h2>
            </div>
            <div class="col-md-1 col-sm-12 form-group-sm">
                <small>Year</small>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Month</small>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <small>Search</small>
                <div class="input-group">
                    <asp:textbox id="name_search" runat="server" cssclass="form-control" placeholder="NXP No or Consignee or Consignor"></asp:textbox>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbSearch" runat="server" CssClass="btn-sm btn-primary" OnClick="lbSearch_Click">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>No of Parcels</small>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>CCI Issued</small>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Not Issued</small>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <div class="panel panel-default">
                    <asp:GridView ID="gvListCCI" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" EmptyDataText="There is no record for the specified criteria" 
                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                    <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn btn-sm btn-primary" 
                                        CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-print"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPaging" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <asp:LinkButton ID="lbGenCCI" runat="server" CssClass="btn btn-sm btn-primary pull-right" CausesValidation="False" 
                    OnClick="lbGenCCI_Click" Visible="false">
                    <i class="fa fa-check-square"></i> Generate CCI
                </asp:LinkButton>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlReport" runat="server" Visible="false">
        <div class="row panel-body">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <br />
                <rsweb:ReportViewer ID="rptViewer" runat="server" ProcessingMode="Local" Width="100%">
                </rsweb:ReportViewer>
            </div>
        </div>
    </asp:Panel>


    <div class="modal fade" id="modal-cci" tabindex="-1" role="dialog" aria-labelledby="modal-cci" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Clean Certificate of Inspection</label>
                    </h3>
                </div>

                <asp:Panel ID="nxp_parcel" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>CCI will be generated for the under-listed Parcels</small>
                                <label>NXP No, NESS Receipt No, Consignor</label>
                                <asp:ListBox ID="lstCCI" runat="server" CssClass="form-control" Height="200px"></asp:ListBox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>CCI Issue Date</small>
                                <asp:TextBox ID="cci_issue_date" runat="server" CssClass="form-control date"></asp:TextBox>
                                <br />
                                <label>Click Yes to generate CCI now</label>
                                <br /><br />
                            </div>
                            <div class="col-md-6 col-sm-12 form-group">
                                <asp:linkbutton id="lbCancel" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                    <i class="fa fa-undo"></i> No
                                </asp:linkbutton>
                            </div>
                            <div class="col-md-6 col-sm-12 form-group">
                                <asp:linkbutton id="lbGenCCIYes" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbGenCCIYes_Click">
                                    <i class="fa fa-check"></i> Yes
                                </asp:linkbutton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">

</asp:Content>

