<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="reference_table.aspx.cs" Inherits="_ReferenceTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading">
            <h1><i class="icon-info"></i> Reference Tables</h1>
        </div>
        <br />
        <div class="row">
            <div class="col-md-9 col-sm-12 form-group-sm">
                <small>Select Reference Type</small>
                <div class="input-group">
                    <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control"></asp:DropDownList>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbOpen" runat="server" CssClass="btn-sm btn-primary" OnClick="lbOpen_Click">
                            <i class="fa fa-folder-open"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <br />
                <asp:linkbutton id="lbAddNew" runat="server" cssclass="btn btn-sm btn-info" OnClick="lbAddNew_Click">
                    <i class="icon-docs"></i> Add New Reference
                </asp:linkbutton>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <div class="panel panel-default repeater">
                    <asp:GridView ID="gvListRefTable" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" 
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnRowDeleting="gvList_OnRowDeleting" 
                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%">
                        <Columns>
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
                        <label>Reference Details </label>
                    </h3>
                </div>

                <asp:Panel ID="meta_data_ref" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-3 col-sm-12 form-group-sm">
                                <small>Reference ID </small>
                                <asp:textbox id="reference_id" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-3 col-sm-12 form-group-sm">
                                <small>Acronym</small>
                                <asp:textbox id="reference_acronym" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-6 col-sm-12 form-group-sm">
                                <small>Reference Name</small>
                                <asp:textbox id="reference_name" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <asp:HiddenField ID="meta_data_type" runat="server" />
                                <asp:HiddenField ID="meta_data_id" runat="server" />
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

