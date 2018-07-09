<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="exchange_rate.aspx.cs" Inherits="_ExchangeRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading">
            <h1><i class="fa fa-share-alt-square"></i> Exchange Rate</h1>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 col-sm-12 form-group-sm text-right">
                <label>Date Search</label>
            </div>
            <div class="col-md-6 col-sm-12 form-group-sm">
                <div class="input-group">
                    <asp:textbox id="name_search" runat="server" cssclass="form-control"></asp:textbox>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbSearch" runat="server" CssClass="btn-sm btn-primary" OnClick="lbSearch_Click">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <asp:linkbutton id="lbAddNew" runat="server" cssclass="btn btn-sm btn-info" OnClick="lbAddNew_Click">
                    <i class="icon-docs"></i> Add New
                </asp:linkbutton>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <div class="panel panel-default repeater">
                    <asp:GridView ID="gvListeRate" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnRowDeleting="gvList_OnRowDeleting" OnRowEditing="gvList_OnRowEditing" 
                        OnPageIndexChanging="gvList_PageIndexChanging"
                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn-sm btn-info" 
                                            CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-arrow-right"></i>
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
                            <asp:TemplateField HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn-sm btn-primary" 
                                            CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-edit"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
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

    <div class="modal fade" id="modal-popup" tabindex="-1" role="dialog" aria-labelledby="modal-popup" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Exchange Rate </label>
                    </h3>
                </div>

                <asp:Panel ID="exchange_rate" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 form-group-sm">
                                <small>Date</small>
                                <asp:textbox id="rated_date" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-6 col-sm-12 form-group-sm">
                                <small>Exchange Rate</small>
                                <asp:textbox id="rated_value" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-6 col-sm-12 form-group-sm">
                                <small>Currency</small>
                                <asp:DropDownList ID="currency_acronym" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 col-sm-12 form-group-sm">
                                <small>Default/Active</small>
                                <asp:DropDownList ID="default_flag" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <asp:HiddenField ID="rec_id" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbCancel" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-undo"></i> Cancel
                            </asp:linkbutton>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbSave" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
                                <i class="fa fa-save"></i> Save
                            </asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

