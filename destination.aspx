<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="destination.aspx.cs" Inherits="_Destination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel-primary"> 
        <div class="panel-heading">
            <h1><i class="fa fa-anchor"></i> Destination & Point of Exit</h1>
        </div>
        <br />
        <div class="row">
            <div class="col-md-9 col-sm-12 form-group-sm">
                <small>Select Country</small>
                <div class="input-group">
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbOpenDest" runat="server" CssClass="btn-sm btn-primary" OnClick="lbOpen_Click">
                            <i class="fa fa-folder-open"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <br />
                <asp:linkbutton id="lbAddNewDest" runat="server" cssclass="btn btn-sm btn-info" OnClick="lbAddNew_Click">
                    <i class="icon-docs"></i> Add New Destination
                </asp:linkbutton>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <label>List of Destination</label>
                <div class="panel panel-default repeater">
                    <asp:GridView ID="gvListDestination" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnRowDeleting="gvList_OnRowDeleting" OnRowEditing="gvList_OnRowEditing" 
                        OnPageIndexChanging="gvList_PageIndexChanging"
                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn-sm btn-info" 
                                            CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-folder-open"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPagingDest" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" OnSelectedIndexChanged="ddlPagingDest_SelectedIndexChanged">
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
        <div class="row">
            <div class="col-md-9 col-sm-12 form-group-sm">
                <small>Select Destination</small>
                <div class="input-group">
                    <asp:DropDownList ID="ddlDestination" runat="server" CssClass="form-control"></asp:DropDownList>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbOpenPofE" runat="server" CssClass="btn-sm btn-primary" OnClick="lbOpen_Click">
                            <i class="fa fa-folder-open"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <br />
                <asp:linkbutton id="lbAddNewPofE" runat="server" cssclass="btn btn-sm btn-info" OnClick="lbAddNew_Click">
                    <i class="icon-docs"></i> Add New Point of Exit
                </asp:linkbutton>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <label>Points of Exit</label>
                <div class="panel panel-default repeater">
                    <asp:GridView ID="gvListPofE" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
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
                                    <asp:DropDownList ID="ddlPagingPofE" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" OnSelectedIndexChanged="ddlPagingPofE_SelectedIndexChanged">
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

    <div class="modal fade" id="modal-popup" tabindex="-1" role="dialog" aria-labelledby="modal-popup" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Destination Details </label>
                    </h3>
                </div>

                <asp:Panel ID="country_dest" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4 col-sm-12 form-group-sm">
                                <small>Acronym</small>
                                <asp:textbox id="entity_acronym" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-8 col-sm-12 form-group-sm">
                                <small>Name</small>
                                <asp:textbox id="entity_name" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <asp:HiddenField ID="country_acronym" runat="server" />
                                <asp:HiddenField ID="rec_id" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbCancelDest" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-undo"></i> Cancel
                            </asp:linkbutton>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbSaveDest" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
                                <i class="fa fa-save"></i> Save
                            </asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-popup2" tabindex="-1" role="dialog" aria-labelledby="modal-popup2" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Point of Exit </label>
                    </h3>
                </div>

                <asp:Panel ID="country_dest_pofe" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4 col-sm-12 form-group-sm">
                                <small>Acronym</small>
                                <asp:textbox id="entity_acronym_" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-8 col-sm-12 form-group-sm">
                                <small>Name</small>
                                <asp:textbox id="entity_name_" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <asp:HiddenField ID="country_acronym_" runat="server" />
                                <asp:HiddenField ID="destination_acronym" runat="server" />
                                <asp:HiddenField ID="rec_id_" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbCancelPofE" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-undo"></i> Cancel
                            </asp:linkbutton>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbSavePofE" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
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

