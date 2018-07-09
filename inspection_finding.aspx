  <%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="inspection_finding.aspx.cs" Inherits="_InspectionFinding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        function checkdate(input){
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval=false
            if (!validformat.test(input.value))
                alert("Invalid Date Format. Please correct and submit again.")
            else{ //Detailed check for valid date ranges
                var monthfield=input.value.split("/")[0]
                var dayfield=input.value.split("/")[1]
                var yearfield=input.value.split("/")[2]
                var dayobj = new Date(yearfield, monthfield-1, dayfield)
                if ((dayobj.getMonth()+1!=monthfield)||(dayobj.getDate()!=dayfield)||(dayobj.getFullYear()!=yearfield))
                alert("Invalid Day, Month, or Year range detected. Please correct and submit again.")
                else
                returnval=true
            }
            if (returnval==false) input.select()
            return returnval
        }
    </script>

    <script type="text/javascript">
        function shipmentChange() {
            var textBofLDate = document.getElementById('<%= bill_of_lading_date.ClientID %>').value;
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var validformat1 = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var validformat2 = /^\d{2}\-\d{2}\-\d{4}$/ //Basic check for format validity
            var validformat3 = /^\d{4}\/\d{2}\/\d{2}$/ //Basic check for format validity
            var validformat4 = /^\d{4}\-\d{2}\-\d{2}$/ //Basic check for format validity

            if (!validformat1.test(textBofLDate))
            {
                if (!validformat2.test(textBofLDate))
                {
                    if (!validformat3.test(textBofLDate))
                    {
                        if (!validformat4.test(textBofLDate))
                        {
                            alert("You must enter a valid Bill of Lading Date before attempting this operation.");
                            return;
                        }
                    }
                }
            }

            var varShipType = document.getElementById('<%= shipment_type_id.ClientID %>').value;
            if (varShipType === 'SINGLE') {
                document.getElementById('divParcelNo').style.display = "none";
                document.getElementById('<%= no_of_parcel.ClientID %>').value = "1";
            }
            else {
                document.getElementById('divParcelNo').style.display = "block";
            }
        }
        function parcelNoChange() {
            var textBofLDate = document.getElementById('<%= bill_of_lading_date.ClientID %>').value;
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var validformat1 = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var validformat2 = /^\d{2}\-\d{2}\-\d{4}$/ //Basic check for format validity
            var validformat3 = /^\d{4}\/\d{2}\/\d{2}$/ //Basic check for format validity
            var validformat4 = /^\d{4}\-\d{2}\-\d{2}$/ //Basic check for format validity

            if (!validformat1.test(textBofLDate)) {
                if (!validformat2.test(textBofLDate)) {
                    if (!validformat3.test(textBofLDate)) {
                        if (!validformat4.test(textBofLDate)) {
                            alert("You must enter a valid Bill of Lading Date before attempting this operation.");
                            return;
                        }
                    }
                }
            }

            $('#modal-split').modal('show');
        }
        $(document).ready(function () {
            var varShipType = document.getElementById('<%= shipment_type_id.ClientID %>').value;
            if (varShipType === 'SINGLE') {
                document.getElementById('divParcelNo').style.display = "none";
            }
            else {
                document.getElementById('divParcelNo').style.display = "block";
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-note"></i> Inspection Findings </h1>
        </div>
             

        <!--NXP INFORMATION-->
        <asp:Panel ID="nxp_inspection" runat="server">
            <div class="row well well-sm">
                <asp:HiddenField ID="rec_id" runat="server" />
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>NXP Number</small><br />
                    <label><asp:Label ID="nxp_no" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Transaction Number</small><br />
                    <label><asp:Label ID="transaction_id__" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Bill of Lading Date</small><br />
                    <label><asp:Label ID="bill_of_lading_date_" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Net Qty (Barrels)</small><br />
                    <label><asp:TextBox ID="barrels_net_qty__" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Consignor</small><br />
                    <label><asp:Label ID="consignor_name" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Consignee</small><br />
                    <label><asp:Label ID="consignee_name" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Terminal</small><br />
                    <label><asp:Label ID="departure_port_desc" runat="server"></asp:Label></label>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Vessel</small><br />
                    <label><asp:Label ID="vessel_name" runat="server"></asp:Label></label>
                </div>
            </div>
        </asp:Panel>

        <div class="row">
            <div class="col-md-6 col-sm-12">
                <asp:Panel ID="nxp_vessel" runat="server">
                    <asp:HiddenField ID="transaction_id___" runat="server" />
                    <asp:HiddenField ID="nxp_no_" runat="server" />
                    <asp:HiddenField ID="rec_id_" runat="server" />
                    <div class="row well panel-primary">
                        <div class="panel-info">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 form-group-sm">
                                    <h3><strong>Basic Vessel Details</strong></h3>
                                </div>
                                <div class="col-md-6 col-sm-12 form-group-sm">
                                    <div class="input-group">
                                        <small>Click button to SAVE Vessel</small>
                                        <span class="input-group-addon input-group-addon-select">
                                            <asp:linkbutton id="lbSaveVessel" runat="server" cssclass="btn btn-sm btn-primary pull-right" OnClick="lbSaveVessel_Click">
                                                <i class="fa fa-save fa-2x"></i> 
                                            </asp:linkbutton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 form-group-sm">
                                    <hr />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Terminal Inspector</small>
                            <asp:DropDownList ID="terminal_inspector_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Ship Inspector</small>
                            <asp:DropDownList ID="ship_inspector_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Shipment/Loading Reference No</small>
                            <asp:TextBox ID="reference_no" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Bill of Lading (Dated)</small>
                            <asp:TextBox ID="bill_of_lading_date" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Shipment Type</small>
                            <asp:DropDownList ID="shipment_type_id" runat="server" CssClass="form-control" onchange="shipmentChange();"></asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm" id="divParcelNo">
                            <small>No of Parcels</small>
                            <asp:DropDownList ID="no_of_parcel" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Load Completed (HH.MM/DD.MMM.YYYY)</small>
                            <asp:TextBox ID="load_completed_date" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Terminal</small>
                            <asp:DropDownList ID="terminal_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Vessel</small>
                            <asp:DropDownList ID="vessel_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <br />
                            <asp:linkbutton id="lbVesselMore" runat="server" cssclass="btn btn-sm btn-default pull-right" data-toggle="modal" data-target="#modal-vessel">
                                <i>More vessel details</i> 
                            </asp:linkbutton>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-md-1 col-sm-12 form-group-sm">
            </div>
            <div class="col-md-5 col-sm-12 form-group-sm">
                <asp:Panel ID="nxp_bofl" runat="server">
                    <asp:HiddenField ID="rec_id__" runat="server" />
                    <asp:HiddenField ID="nxp_no__" runat="server" />
                    <div class="row well panel-primary">
                        <!--
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <asp:linkbutton id="lbGenTransID" runat="server" cssclass="btn btn-sm btn-primary pull-right">
                                <i class="fa fa-arrow-circle-o-right"></i> Generate Transaction Number
                            </asp:linkbutton>
                        </div>
                        -->
                        <div class="panel-info">
                            <div class="row">
                                <div class="col-md-10 col-sm-12 form-group-sm">
                                    <h3><strong>Bill of Lading Figures (Grand Total)</strong></h3>
                                </div>
                                <div class="col-md-2 col-sm-12 form-group-sm">
                                    <asp:linkbutton id="lbSaveBofL" runat="server" cssclass="btn btn-sm btn-primary pull-right" OnClick="lbSaveBofL_Click">
                                        <i class="fa fa-save fa-2x"></i>
                                    </asp:linkbutton>
                                </div>
                                <div class="col-md-12 col-sm-12 form-group-sm">
                                    <hr />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>Transaction No:</small>
                        </div>
                        <div class="col-md-8 col-sm-12 form-group-sm">
                            <label>
                                <asp:TextBox ID="transaction_id" runat="server" CssClass="form-control" ReadOnly="true" Width="100%"></asp:TextBox>
                            </label>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            *
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <label>Gross Qty</label>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <label>Net Qty</label>
                        </div>

                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>Barrels</small>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="barrels_gross_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="barrels_net_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>Long Tons</small>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="long_tons_gross_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="long_tons_net_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>Metric Tons</small>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="metric_tons_gross_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="metric_tons_net_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>API</small>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="api_gross_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="api_net_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>SG</small>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="sg_gross_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <asp:TextBox ID="sg_net_qty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4 col-sm-12 form-group-sm">
                            <small>BSW(%)</small>
                        </div>
                        <div class="col-md-8 col-sm-12 form-group-sm">
                            <asp:TextBox ID="bsw_percent" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <h3>Parcel Details</h3>
                <asp:GridView ID="gvListParcel" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" 
                    OnSelectedIndexChanged="gvList_SelectedIndexChanged" 
                    GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" AlternatingRowStyle-CssClass="myAltRowClass">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSelect" runat="server"  
                                        CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                    <i class="fa fa-edit fa-2x"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-12 col-sm-12 form-group-sm">
                <asp:linkbutton id="lbSaveAll" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSaveAll_Click">
                    <i class="fa fa-save"></i> Save all Changes and Create Parcel
                </asp:linkbutton>
            </div>
        </div>

    </div>




    <div class="modal fade" id="modal-popup" tabindex="-1" role="dialog" aria-labelledby="modal-popup" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

            <asp:Panel ID="nxp_parcel" runat="server">
                <asp:HiddenField ID="rec_id___" runat="server" />
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label>Parcel <asp:Label ID="parcel_no" runat="server" Text="1"></asp:Label> </label>
                    </h3>
                </div>
                <div class="modal-body">
                    <div class="row well">
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            *
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Transaction No</small><br />
                            <label><asp:Label ID="transaction_id_" runat="server"></asp:Label></label>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Parcel ID</small><br />
                            <label><asp:Label ID="parcel_id" runat="server"></asp:Label></label>
                        </div>

                        <div class="col-md-2 col-sm-12 form-group-sm">
                            
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <label>Gross Qty</label>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <label>Net Qty</label>
                        </div>

                        <div class="col-md-2 col-sm-12 form-group-sm">
                            <small>Barrels</small>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="barrels_gross_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="barrels_net_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            <small>Long Tons</small>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="long_tons_gross_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="long_tons_net_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            <small>Metric Tons</small>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="metric_tons_gross_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <asp:TextBox ID="metric_tons_net_qty_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Load Completed</small>
                            <asp:TextBox ID="load_completed" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Loading Reference</small>
                            <asp:TextBox ID="reference_no_" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Consignor</small>
                            <asp:DropDownList ID="consignor_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Consignee</small>
                            <asp:DropDownList ID="consignee_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Country</small>
                            <asp:DropDownList ID="country_id" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="country_id_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-5 col-sm-12 form-group-sm">
                            <small>Destination</small>
                            <asp:DropDownList ID="destination_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12 form-group-sm">
                            
                        </div>
                        <div class="col-md-10 col-sm-12 form-group-sm">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 form-group">
                                    <br />
                                    <asp:linkbutton id="lbCancelParcel" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                        <i class="fa fa-undo"></i> Cancel
                                    </asp:linkbutton>
                                </div>
                                <div class="col-md-6 col-sm-12 form-group">
                                    <br />
                                    <asp:linkbutton id="lbSaveParcel" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSaveParcel_Click">
                                        <i class="fa fa-save"></i> Save 
                                    </asp:linkbutton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-split" tabindex="-1" role="dialog" aria-labelledby="modal-split" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label class="text-center">Parcel Information</label>
                    </h3>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <small class="text-center">
                                This action will delete previous parcel information. <br />Do you want to continue?
                            </small>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbCancelSplit" runat="server" cssclass="btn btn-warning pull-left" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-undo"></i> No
                            </asp:linkbutton>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group">
                            <asp:linkbutton id="lbSaveSplit" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSaveSplit_Click">
                                <i class="fa fa-check"></i> Yes
                            </asp:linkbutton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-vessel" tabindex="-1" role="dialog" aria-labelledby="modal-vessel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title">
                        <label class="text-center">Additional Vessel Details</label>
                    </h3>
                </div>

                <asp:Panel ID="nxp_vessel_others" runat="server">
                <div class="modal-body">
                    <div class="row well">
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Dead Weight</small>
                            <asp:TextBox ID="dead_weight" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Volume(BBLS)</small>
                            <asp:TextBox ID="vessel_volume" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>LOA</small>
                            <asp:TextBox ID="loa" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Draught</small>
                            <asp:TextBox ID="draught" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Built</small>
                            <asp:TextBox ID="built" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Owner</small>
                            <asp:TextBox ID="owner" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Flag</small>
                            <asp:TextBox ID="flag" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Previous Name</small>
                            <asp:TextBox ID="previous_name" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Shipment Agent</small>
                            <asp:TextBox ID="shipment_agent" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>VEF</small>
                            <asp:TextBox ID="vef" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Free Water</small>
                            <asp:TextBox ID="free_water" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Ship Figure</small>
                            <asp:TextBox ID="ship_figure" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Nominated Quantity</small>
                            <asp:TextBox ID="nominated_quantity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>LAYCAN</small>
                            <asp:TextBox ID="laycan" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>EOSP</small>
                            <asp:TextBox ID="eosp" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>NORT</small>
                            <asp:TextBox ID="nort" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>PILOT ONBOARD</small>
                            <asp:TextBox ID="pilot_onboard" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>BERTHED</small>
                            <asp:TextBox ID="berthed" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Commenced Loading</small>
                            <asp:TextBox ID="commenced_loading" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>LET GO</small>
                            <asp:TextBox ID="let_go" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Sailed</small>
                            <asp:TextBox ID="sailed" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12 form-group-sm">
                            <small>Demurrage Status</small>
                            <asp:DropDownList ID="demurrage_status_id" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Distruption during loading (Remarks)</small>
                            <asp:TextBox ID="disruption_remarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Discrepancy in Quantity (Remarks)</small>
                            <asp:TextBox ID="discrepancy_remarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12 form-group-sm">
                            <small>Special Incident (Remarks)</small>
                            <asp:TextBox ID="special_incident_remarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12 col-sm-12 form-group-sm">
                            <br />
                            <asp:linkbutton id="lbSaveVesselOthers" runat="server" cssclass="btn btn-primary pull-right" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-check"></i> Ok
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

