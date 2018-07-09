<%@ Page Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="system_admin.aspx.cs" Inherits="_SysAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/dashboard.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel-primary"> 
        <div class="panel-heading row">
            <h1><i class="icon-settings"></i> System Admin</h1>
        </div>


        <div class="row well">
            <div class="panel-heading">
                <h3>Basic Settings<hr /></h3>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbCrudeType" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="icon-chemistry fa-5x"></i>
                    <div>
                        <small>Crude Type</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbMeasurementUnit" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-adjust fa-5x"></i>
                    <div>
                        <small>Measurement Unit</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbCurrency" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-money fa-5x"></i>
                    <div>
                        <small>Currency</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbPackaging" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-paperclip fa-5x"></i>
                    <div>
                        <small>Packaging</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbProductType" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="icon-diamond fa-5x"></i>
                    <div>
                        <small>Product Type</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbShipmentType" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-briefcase fa-5x"></i>
                    <div>
                        <small>Shipment Type</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbParcelNumber" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-cubes fa-5x"></i>
                    <div>
                        <small>Number of Parcels</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbDemurrageStatus" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-anchor fa-5x"></i>
                    <div>
                        <small>Demurrage Status</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbBasisOfSale" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-bell-o fa-5x"></i>
                    <div>
                        <small>Basis of Sale</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbMethodOfPayment" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-credit-card fa-5x"></i>
                    <div>
                        <small>Method of Payment</small>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:LinkButton ID="lbApprovalStatus" runat="server" class="user-tab center text-center" OnClick="lbBasicSettings_Click">
                    <i class="fa fa-check-square fa-5x"></i>
                    <div>
                        <small>Approval Status</small>
                    </div>
                </asp:LinkButton>
            </div>

        </div>




        <div class="row well">
            <div class="panel-heading">
                <h3>Advance Settings<hr /></h3>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/admin-bank" runat="server" class="user-tab center text-center">
                    <i class="fa fa-home fa-5x"></i>
                    <div>
                        <small>Banks</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink2" NavigateUrl="~/admin-country" runat="server" class="user-tab center text-center">
                    <i class="fa fa-flag fa-5x"></i>
                    <div>
                        <small>Country</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink3" NavigateUrl="~/admin-destination" runat="server" class="user-tab center text-center">
                    <i class="fa fa-anchor fa-5x"></i>
                    <div>
                        <small>Destination & Point of Exit</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink4" NavigateUrl="~/admin-consignee" runat="server" class="user-tab center text-center">
                    <i class="fa fa-user fa-5x"></i>
                    <div>
                        <small>Consignee</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink5" NavigateUrl="~/admin-consignor" runat="server" class="user-tab center text-center">
                    <i class="fa fa-users fa-5x"></i>
                    <div>
                        <small>Consignor</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink6" NavigateUrl="~/admin-inspector" runat="server" class="user-tab center text-center">
                    <i class="fa fa-user-md fa-5x"></i>
                    <div>
                        <small>Inspectors</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink7" NavigateUrl="~/admin-terminal" runat="server" class="user-tab center text-center">
                    <i class="fa fa-ge fa-5x"></i>
                    <div>
                        <small>Terminals</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink8" NavigateUrl="~/admin-vessel" runat="server" class="user-tab center text-center">
                    <i class="fa fa-life-bouy fa-5x"></i>
                    <div>
                        <small>Vessels</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink9" NavigateUrl="~/admin-exchange-rate" runat="server" class="user-tab center text-center">
                    <i class="fa fa-share-alt-square fa-5x"></i>
                    <div>
                        <small>Exchange Rate</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink10" NavigateUrl="~/admin-price" runat="server" class="user-tab center text-center">
                    <i class="fa fa-credit-card fa-5x"></i>
                    <div>
                        <small>Prices</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink11" NavigateUrl="~/admin-ness" runat="server" class="user-tab center text-center">
                    <i class="fa fa-bar-chart-o fa-5x"></i>
                    <div>
                        <small>NESS Admin</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink13" NavigateUrl="~/admin-pia" runat="server" class="user-tab center text-center">
                    <i class="fa fa-briefcase fa-5x"></i>
                    <div>
                        <small>PIA Admin</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink18" NavigateUrl="#" runat="server" class="user-tab center text-center" data-toggle="modal" data-target="#modal-number">
                    <i class="icon-list fa-5x"></i>
                    <div>
                        <small>Transaction Numbering</small>
                    </div>
                </asp:HyperLink>
            </div>
            <div class="col-md-2 col-sm-12">
                <asp:HyperLink ID="HyperLink12" NavigateUrl="#" runat="server" class="user-tab center text-center" data-toggle="modal" data-target="#modal-db">
                    <i class="fa fa-eraser fa-5x"></i>
                    <div>
                        <small>Delete Transactions</small>
                    </div>
                </asp:HyperLink>
            </div>
        </div>

    </div>

    <div class="modal fade" id="modal-number" tabindex="-1" role="dialog" aria-labelledby="modal-number" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Transaction Settings</label>
                    </h3>
                </div>
                <asp:Panel ID="trans_number" runat="server">
                    <asp:HiddenField ID="rec_id" Value="1" runat="server" />
                    <div class="modal-body well">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>General Transaction</small>
                                <asp:textbox id="trans_no" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>CCI Number</small>
                                <asp:textbox id="cci_no" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>CCI Control Sheet</small>
                                <asp:textbox id="cci_cs_no" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Inspection Admin Email 1</small>
                                <asp:textbox id="admin_email1" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Inspection Admin Email 2</small>
                                <asp:textbox id="admin_email2" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                            <div class="col-md-12 col-sm-12 form-group-sm">
                                <small>Inspection Admin Email 3</small>
                                <asp:textbox id="admin_email3" runat="server" cssclass="form-control"></asp:textbox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 form-group">
                            <asp:linkbutton id="lbSave" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
                                <i class="fa fa-save"></i> Update
                            </asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-db" tabindex="-1" role="dialog" aria-labelledby="modal-db" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Delete Transactions</label>
                    </h3>
                </div>
                <div class="modal-body well">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <asp:CheckBox ID="chkNXP" runat="server" cssclass="form-control" Text=" All NXPs" /><br />
                        </div>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <asp:CheckBox ID="chkInsp" runat="server" cssclass="form-control" Text=" All Inspection Findings" /><br />
                        </div>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <asp:CheckBox ID="chkDoc" runat="server" cssclass="form-control" Text=" All Documentation" /><br />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 form-group">
                            <asp:linkbutton id="lbDelete" runat="server" cssclass="btn btn-danger pull-right" OnClick="lbDelete_Click">
                                <i class="fa fa-eraser"></i> Delete Now
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

