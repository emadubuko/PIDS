  <%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="nxp.aspx.cs" Inherits="_NXP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/number-to-words.js"></script>
    <script type="text/javascript">
        function calcFOBValue() {
            var textQty = document.getElementById('<%= nxp_qty.ClientID %>').value.split(',').join('');
            var textUnitPrice = document.getElementById('<%= nxp_unit_price.ClientID %>').value.split(',').join('');
            var textExchRate = document.getElementById('<%= exchange_rate.ClientID %>').value.split(',').join('');

            document.getElementById('<%= nxp_fob_value.ClientID %>').value = textQty * textUnitPrice;
            var textFobFigure = (textQty * textUnitPrice) * textExchRate;
            document.getElementById('<%= fob_value_figure.ClientID %>').value = textFobFigure;

            if (textFobFigure > 0)
            {
                document.getElementById('<%= fob_value_word.ClientID %>').value = translate(textFobFigure);
            }
        }

        var num2words = new NumberToWords();
        function translate(input) {
         if (isNaN(input)) {
          return;
         };
         num2words.setMode("international");
         var intl = num2words.numberToWords(input);
         intl = intl.replace("Zero", "");
         return intl;  
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-doc"></i> NXP Form</h1>
        </div>
    
        <asp:Panel ID="nxp" runat="server">
            <asp:HiddenField ID="rec_id" runat="server" />
            <div class="row well">
                <div class="panel-heading">
                    <h2>Product Details</h2><hr />
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Transaction Number</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="transaction_id" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Transaction Date</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="transaction_date" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>NXP Number</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="nxp_no" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Product</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="product_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Measurement Unit</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="measurement_unit_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Quantity on NXP</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="nxp_qty" runat="server" CssClass="form-control number" onchange="calcFOBValue();"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Unit Price on NXP</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="nxp_unit_price" runat="server" CssClass="form-control" onchange="calcFOBValue();"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>FOB Value on NXP</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="nxp_fob_value" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Currency</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="currency_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Exchange Rate on NXP</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="exchange_rate" runat="server" CssClass="form-control" onchange="calcFOBValue();"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Other Charges</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="other_charges" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Freight</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="freight" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>FOB Value (Naira Figure)</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="fob_value_figure" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>FOB Value (Naira Words)</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="fob_value_word" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Height="50px"></asp:TextBox>
                </div>
            </div>
            <div class="row well">
                <div class="panel-heading">
                    <h2>Consignor & Consignee Details</h2><hr />
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Consignor</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="consignor_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Consignee</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="consignee_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Consignor RC No.</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="consignor_rc_no" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>HS Code</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="hs_code" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Consignor NEPC No.</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="consignor_nepc_reg_no" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Packaging</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="packaging_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Crude Type</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="crude_type_id" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostBack_Click"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Bank</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="bank_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row well">
                <div class="panel-heading">
                    <h2>Shipment Details</h2><hr />
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Shipment Date</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:TextBox ID="shipment_date" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Shipment Type</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="shipment_type_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Terminal/Departure Port</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="departure_port_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Destination</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="destination_id" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostBack_Click"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Point of Exit</small>
                </div>
                <div class="col-md-4 col-sm-12 form-group-sm">
                    <asp:DropDownList ID="point_of_exit_id" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

        </asp:Panel>

        <div class="row">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <asp:linkbutton id="lbSave" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
                    <i class="fa fa-save"></i> Save Form 
                </asp:linkbutton>
            </div>
        </div>

    </div>
   </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

