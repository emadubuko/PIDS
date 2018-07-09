<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="template.aspx.cs" Inherits="template" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel-primary">
        <!-- SAMPLE INPUT TAG -->
        <div class="row">
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Sample TextBox</label>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:TextBox ID="surname" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Sample TextBox</label>
            </div>
            <div class="col-md-4 col-sm-12 form-group-sm">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Sample TextBox</label>
            </div>
            <div class="col-md-4 col-sm-12 form-group-sm">
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Sample Dropdown</small>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <!-- SAMPLE BUTTON TAG -->
        <div class="row">
            <div class="col-md-12 form-group-sm">
                <br />
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary">
                    <i class="fa fa-save"></i> Save
                </asp:LinkButton>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger">
                    <i class="fa fa-eraser"></i> Delete
                </asp:LinkButton>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-warning">
                    <i class="fa fa-undo"></i> Cancel
                </asp:LinkButton>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-default"  data-toggle="modal" data-target="#modal-popup">
                    <i class="icon-docs"></i> Popup
                </asp:LinkButton>
            </div>
        </div>
        <br />

        <div class="row">
            <!-- SAMPLE SEARCH TAG -->
            <div class="col-md-4 col-sm-12 form-group">
                <small>Sample grouping of textbox and link button for search</small>
                <div class="input-group">
                    <asp:Textbox ID="search_value" CssClass="form-control" runat="server"></asp:Textbox>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbSearch" runat="server" CssClass="btn-sm btn-primary">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>

            <div class="col-md-4 col-sm-12 form-group">
                <small>Sample grouping of textbox and link button for search</small>
                <div class="input-group">
                    <asp:Textbox ID="Textbox4" CssClass="form-control" runat="server"></asp:Textbox>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn-sm btn-primary">
                            <i class="fa fa-search"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
            <div class="col-md-4 col-sm-12 form-group">
            </div>
            <div class="col-md-4 col-sm-12 form-group">
            </div>

            <br />
            <!-- SAMPLE GRID TAG -->
            <div class="col-md-12">
                <asp:GridView ID="gvList" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="False" 
                    GridLines="None" Width="100%" RowStyle-Font-Size="X-Small" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="RecID">
                            <ItemTemplate>
                                <asp:Label ID="lblRecID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.rec_id") %>' Width="100%"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbOpen" runat="server" CssClass="btn-sm btn-primary">
                                    <i class="fa fa-edit"></i> Open
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <br />
            <!-- SAMPLE TABS TAG -->
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title visible-lg visible-md">
                            <i class="icon-info"></i> Title (e.g. Personal Information)
                            <ul class="nav nav-tabs visible-lg visible-md">
                                <li class="active"><a href="#tab1" data-toggle="tab">Personal Data</a></li>
                                <li><a href="#tab2" data-toggle="tab">Next of Kin</a></li>
                                <li><a href="#tab3" data-toggle="tab">Other Accounts</a></li>
                            </ul>
                        </div>
                        <ul class="nav navbar-nav navbar-actions visible-xs visible-sm">
				            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-bars"></i><span> Title (e.g. Personal Information)</span></a>
	        		            <ul class="dropdown-menu">
                                    <li class="active"><a href="#tab1" data-toggle="tab">Personal Data</a></li>
                                    <li><a href="#tab2" data-toggle="tab">Next of Kin</a></li>
                                    <li><a href="#tab3" data-toggle="tab">Other Accounts</a></li>
	        		            </ul>
				            </li>
			            </ul>
                    </div>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab1">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small title="Bank Verification Number">tab1 content</small>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small title="Bank Verification Number">tab1 content</small>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small title="Bank Verification Number">tab1 content</small>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small title="Bank Verification Number">tab1 content</small>
                                    </div>
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small title="Bank Verification Number">tab1 content</small>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab2">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small>tab2 content</small>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab3">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 form-group-sm">
                                        <small>tab3 content</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <div class="text-right"> 
                            <asp:LinkButton ID="lbSave" runat="server" CssClass="btn btn-primary btn-sm">
                                <i class="fa fa-save"></i> Save Changes
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-popup" tabindex="-1" role="dialog" aria-labelledby="modal-popup" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title"><label>Sample Popup</label></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-12 form-group">
                            <label>Sample TextBox</label>
                        </div>
                        <div class="col-md-9 col-sm-12 form-group">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-primary pull-left">
                                <i class="fa fa-save"></i> Save
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-warning pull-right">
                                <i class="fa fa-undo"></i> Cancel
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

