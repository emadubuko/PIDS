<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="pricing.aspx.cs" Inherits="_Pricing" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="fa fa-credit-card"></i> Prices</h1>
        </div>

        <div class="row well">
            <div class="col-md-12 col-sm-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title visible-lg visible-md">
                            <ul class="nav nav-tabs visible-lg visible-md">
                                <li class="active"><a href="#tab1" data-toggle="tab">Dated Brent Prices</a></li>
                                <li><a href="#tab2" data-toggle="tab">Price Differentials</a></li>
                                <li><a href="#tab3" data-toggle="tab">Crude Price Analysis</a></li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab1"> 
                                <div class="row">
                                    <div class="col-md-6 col-sm-12 form-group-sm">
                                        <label>--Input--</label>
                                        <div class="row">
                                            <div class="col-md-5 col-sm-12 form-group-sm">
                                                <small>Year</small>
                                                <asp:DropDownList ID="ddlYearBrent" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-5 col-sm-12 form-group-sm">
                                                <small>Month</small>
                                                <asp:DropDownList ID="ddlMonthBrent" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <br />
                                                <asp:linkbutton id="lbAddNewBrent" runat="server" cssclass="btn btn-sm btn-primary pull-right" OnClick="lbAddNew_Click">
                                                    <i class="icon-docs"></i> Add New
                                                </asp:linkbutton>
                                            </div>
                                            <div class="col-md-12 col-sm-12 form-group-sm">
                                                <div class="panel panel-default">
                                                    <asp:GridView ID="gvPriceBrent" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                                                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnRowDeleting="gvList_OnRowDeleting" OnPageIndexChanging="gvList_PageIndexChanging"
                                                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn-sm btn-primary" 
                                                                            CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                                                        <i class="fa fa-edit"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlPagingBrent" runat="server" AutoPostBack="true" 
                                                                        CssClass="form-control" OnSelectedIndexChanged="ddlPagingBrent_SelectedIndexChanged">
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-sm btn-danger" 
                                                                            CausesValidation="False" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'>
                                                                        <i class="fa fa-eraser"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-12 form-group-sm">
                                    </div>
                                    <div class="col-md-5 col-sm-12 form-group-sm">
                                        <label>--Upload--</label>
                                        <div class="row">
                                            <div class="col-md-8 col-sm-12 form-group-sm">
                                                <small>Select file to upload</small>
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fupBrent" CssClass="form-control" runat="server" />
                                                    <span class="input-group-addon input-group-addon-select">
                                                        <asp:LinkButton ID="lbUploadBrent" runat="server" CssClass="btn-sm btn-primary" OnClick="lbUploadBrent_Click">
                                                            <i class="fa fa-upload"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="upload/brent_price.xlsx"><small>Download sample MS Excel file</small></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tab2"> 
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        &nbsp
                                    </div>
                                    <div class="col-md-3 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlYearDiff" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 form-group-sm">
                                        <small>Month</small>
                                        <asp:DropDownList ID="ddlMonthDiff" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <br />
                                        <asp:linkbutton id="lbAddNewDiff" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbAddNew_Click">
                                            <i class="icon-docs"></i> Add New
                                        </asp:linkbutton>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        &nbsp
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <div class="panel panel-default">
                                            <asp:GridView ID="gvPriceDiff" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                                                OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnRowDeleting="gvList_OnRowDeleting" OnPageIndexChanging="gvList_PageIndexChanging" 
                                                GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn-sm btn-primary" 
                                                                    CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                                                <i class="fa fa-edit"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlPagingDiff" runat="server" AutoPostBack="true" 
                                                                CssClass="form-control" OnSelectedIndexChanged="ddlPagingDiff_SelectedIndexChanged">
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-sm btn-danger" 
                                                                    CausesValidation="False" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'>
                                                                <i class="fa fa-eraser"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab3"> 
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        &nbsp
                                    </div>
                                    <div class="col-md-3 col-sm-12 form-group-sm">
                                        <small>Year</small>
                                        <asp:DropDownList ID="ddlYearPrice" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-12 form-group-sm">
                                        <small>Month</small>
                                        <asp:DropDownList ID="ddlMonthPrice" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        <br />
                                        <asp:linkbutton id="lbGetPrice" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbGetPrice_Click">
                                            <i class="icon-arrow-right"></i> Get Crude Prices
                                        </asp:linkbutton>
                                    </div>
                                    <div class="col-md-2 col-sm-12 form-group-sm">
                                        &nbsp
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <rsweb:ReportViewer ID="rptViewer" runat="server" ProcessingMode="Local" Width="400px">
                                        </rsweb:ReportViewer>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-brent" tabindex="-1" role="dialog" aria-labelledby="modal-brent" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>
                            Dated Brent Price
                        </label>
                    </h3>
                </div>

                <asp:Panel ID="price_brent" runat="server">
                    <asp:HiddenField ID="rec_id" runat="server" />
                    <div class="modal-body well">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Date</small>
                                <asp:textbox id="brent_date" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Brent Price</small>
                                <asp:textbox id="brent_price" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <br />
                                <asp:linkbutton id="lbSaveBrent" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSaveBrent_Click">
                                    <i class="fa fa-save"></i> Save
                                </asp:linkbutton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-brent-upload" tabindex="-1" role="dialog" aria-labelledby="modal-brent-upload" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>
                            Upload Dated Brent Price
                        </label>
                    </h3>
                </div>
                <div class="modal-body well">
                    <div class="row">
                        <asp:HiddenField ID="hfdFilename" runat="server" />
                        <small>By clicking Save to Database, any previous Brent price for the date being uploaded will be overwritten</small>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <asp:GridView ID="gvBrentUpload" runat="server" AllowPaging="False" CssClass="myGridClass" AutoGenerateColumns="true" 
                                GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <br />
                            <asp:linkbutton id="lbUploadBrentSave" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbUploadBrentSave_Click">
                                <i class="fa fa-save"></i> Save to Database
                            </asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-diff" tabindex="-1" role="dialog" aria-labelledby="modal-diff" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>
                            Price Differentials
                        </label>
                    </h3>
                </div>

                <asp:Panel ID="price_diff" runat="server">
                    <asp:HiddenField ID="rec_id_" runat="server" />
                    <div class="modal-body">
                        <div class="row well">
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Year</small>
                                <asp:DropDownList ID="diff_year" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Month</small>
                                <asp:DropDownList ID="diff_month" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Terminal</small>
                                <asp:DropDownList ID="terminal_id" runat="server" cssclass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Diffenretial Value</small>
                                <asp:textbox id="diff_price" runat="server" cssclass="form-control" Text="0"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <br />
                                <asp:linkbutton id="lbSaveDiff" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSaveDiff_Click">
                                    <i class="fa fa-save"></i> Save
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

