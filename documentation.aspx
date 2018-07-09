  <%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="documentation.aspx.cs" Inherits="_Documentation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/number-to-words.js"></script>
    <script type="text/javascript">
        function calcNESSValues() {
            //NESS calculation
            var textNessFeePercent = document.getElementById('<%= ness_percent.ClientID %>').value.split(',').join('');
            var textPiaFobValue = document.getElementById('<%= pia_fob_value_dollar.ClientID %>').value.split(',').join('');
            var varNessFeePayUSD = textPiaFobValue * textNessFeePercent;
            document.getElementById('<%= ness_fee_payable_dollar.ClientID %>').value = varNessFeePayUSD;

            var textPiaExchRate = document.getElementById('<%= pia_exchange_rate.ClientID %>').value.split(',').join('');;
            var varNessFeePayNGN = varNessFeePayUSD * textPiaExchRate;
            document.getElementById('<%= ness_fee_paybale_naira.ClientID %>').value = varNessFeePayNGN;

            var textNessFeePaidDollar = document.getElementById('<%= ness_fee_paid_dollar.ClientID %>').value.split(',').join('');
            var textExpExchRate = document.getElementById('<%= exporter_exch_rate.ClientID %>').value.split(',').join('');
            var textNessFeePaidNGN = textNessFeePaidDollar * textExpExchRate;
            document.getElementById('<%= ness_fee_paid_naira.ClientID %>').value = textNessFeePaidNGN;
            document.getElementById('<%= ness_fee_over_under_pay.ClientID %>').value = varNessFeePayNGN - textNessFeePaidNGN;

            var textNessExpUnitPrice = document.getElementById('<%= exporter_unit_price_ness.ClientID %>').value.split(',').join('');
            var textNetBarrels = document.getElementById('<%= barrels_net_qty.ClientID %>').value.split(',').join('');
            var textNessExpFOBValue = textNessExpUnitPrice * textNetBarrels;
            document.getElementById('<%= exporter_fob_value_ness_dollar.ClientID %>').value = textNessExpFOBValue;
            document.getElementById('<%= forex_proceed_dollar.ClientID %>').value = textNessExpFOBValue;
        }

        function calcPIAValues() {
            //PIA calculation
            var textNetBarrels = document.getElementById('<%= barrels_net_qty.ClientID %>').value.split(',').join('');
            var textPiaUnitPrice = document.getElementById('<%= pia_unit_price.ClientID %>').value.split(',').join('');
            var textPiaExchRate = document.getElementById('<%= pia_exchange_rate.ClientID %>').value.split(',').join('');

            var varPiaFobValueDollar = textNetBarrels * textPiaUnitPrice;
            document.getElementById('<%= pia_fob_value_dollar.ClientID %>').value = varPiaFobValueDollar;
            document.getElementById('<%= pia_fob_value_naira.ClientID %>').value = textPiaExchRate * varPiaFobValueDollar;
        }

        function calcFOBValue() {
            //NXP calculation
            var textQty = document.getElementById('<%= nxp_qty.ClientID %>').value.split(',').join('');
            var textUnitPrice = document.getElementById('<%= nxp_unit_price.ClientID %>').value.split(',').join('');
            var textExchRate = document.getElementById('<%= exchange_rate.ClientID %>').value.split(',').join('');

            document.getElementById('<%= nxp_fob_value.ClientID %>').value = textQty * textUnitPrice;
            document.getElementById('<%= fob_value_figure.ClientID %>').value = (textQty * textUnitPrice) * textExchRate;
        }

        function transferNXPValues() {
            //transfer NXP values to Documentation
            var textNXPUnitPrice = document.getElementById('<%= nxp_unit_price.ClientID %>').value;//.split(',').join('');
            var textNXPFobValue = document.getElementById('<%= nxp_fob_value.ClientID %>').value;//.split(',').join('');

            document.getElementById('<%= exporter_nxp_unit_price_dollar.ClientID %>').value = textNXPUnitPrice;
            document.getElementById('<%= exporter_fob_value_dollar.ClientID %>').value = textNXPFobValue;
        }

        function reCalcAllValues()
        {
            calcFOBValue();
            transferNXPValues();
            calcPIAValues();
            calcNESSValues();

            var nxpFobValueNaira = document.getElementById('<%= fob_value_figure.ClientID %>').value.split(',').join('');
            if (nxpFobValueNaira > 0) document.getElementById('<%= fob_value_word.ClientID %>').value = translate(nxpFobValueNaira);

            var piaFobValueDollar = document.getElementById('<%= pia_fob_value_dollar.ClientID %>').value.split(',').join('');
            if (piaFobValueDollar > 0) document.getElementById('<%= pia_fob_value_word_dollar.ClientID %>').value = translate(piaFobValueDollar);

            var piaFobValueNaira = document.getElementById('<%= pia_fob_value_naira.ClientID %>').value.split(',').join('');
            if (piaFobValueNaira > 0) document.getElementById('<%= pia_fob_value_word_naira.ClientID %>').value = translate(piaFobValueNaira);

            var expFobValueDollar = document.getElementById('<%= exporter_fob_value_dollar.ClientID %>').value.split(',').join('');
            if (expFobValueDollar > 0) document.getElementById('<%= exporter_fob_value_word_dollar.ClientID %>').value = translate(expFobValueDollar);
            var expExchRate = document.getElementById('<%= exporter_exch_rate.ClientID %>').value.split(',').join('');
            var expFobValueNaira = expFobValueDollar * expExchRate;
            if (expFobValueNaira > 0) document.getElementById('<%= exporter_fob_value_word_naira.ClientID %>').value = translate(expFobValueNaira);
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
            <h1><i class="icon-docs"></i> Documentation</h1>
        </div>

        <!-- SEARCH AREA -->
        <asp:Panel ID="pnlSearch" runat="server" Visible="true">
            <div class="row well well-sm">
                <div class="panel-heading">
                    <h2>Find Parcel</h2>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <small>Year</small>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-3 col-sm-12 form-group-sm">
                    <small>Month</small>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-7 col-sm-12 form-group-sm">
                    <small>Search</small>
                    <div class="input-group">
                        <asp:textbox id="name_search" runat="server" cssclass="form-control" placeholder="Transaction No or Consignee or Consignor"></asp:textbox>
                        <span class="input-group-addon input-group-addon-select">
                            <asp:LinkButton ID="lbSearch" runat="server" CssClass="btn-sm btn-primary" OnClick="lbSearch_Click">
                                <i class="fa fa-search"></i>
                            </asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 form-group-sm">
                    <div class="panel panel-default">
                        <asp:GridView ID="gvListDoc" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" 
                            OnSelectedIndexChanged="gvList_SelectedIndexChanged" EmptyDataText="There is no record for the specified criteria" 
                            OnPageIndexChanging="gvList_PageIndexChanging" ShowFooter="true"
                            GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn-sm btn-primary" 
                                                CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                            <i class="fa fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPaging" runat="server" AutoPostBack="true" 
                                            CssClass="form-control" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </asp:Panel>


        <!-- EDIT AREA -->
        <asp:Panel ID="pnlInput" runat="server" Visible="false">
            <asp:Panel ID="qry_nxp_parcel" runat="server">
                <div class="row well well-sm">
                    <asp:HiddenField ID="rec_id" runat="server" />
                    <div class="col-md-3 col-sm-12 form-group-sm">
                        <small>NXP Number</small><br />
                        <label><asp:Label ID="nxp_no" runat="server"></asp:Label></label>
                    </div>
                    <div class="col-md-3 col-sm-12 form-group-sm">
                        <small>Transaction Number</small><br />
                        <label><asp:Label ID="parcel_id_" runat="server"></asp:Label></label>
                        <!--<label><asp:Label ID="transaction_id__" runat="server"></asp:Label></label>-->
                    </div>
                    <div class="col-md-3 col-sm-12 form-group-sm">
                        <small>Bill of Lading Date</small><br />
                        <label><asp:Label ID="bill_of_lading_date" runat="server"></asp:Label></label>
                    </div>
                    <div class="col-md-3 col-sm-12 form-group-sm">
                        <small>Net Qty (Barrels)</small><br />
                        <label><asp:TextBox ID="barrels_net_qty" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></label>
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
                        <label><asp:Label ID="terminal_name" runat="server"></asp:Label></label>
                        <asp:HiddenField ID="terminal_id" runat="server" />
                    </div>
                    <div class="col-md-3 col-sm-12 form-group-sm">
                        <small>Vessel</small><br />
                        <label><asp:Label ID="vessel_name" runat="server"></asp:Label></label>
                        <asp:HiddenField ID="vessel_id" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <div class="row well">
                <div class="col-md-12 col-sm-12 form-group-sm">
                    <div class="panel panel-default">
                        <asp:Panel ID="nxp_doc" runat="server">
                            <asp:HiddenField ID="nxp_no_" runat="server" />
                            <asp:HiddenField ID="rec_id_" runat="server" />
                            <asp:HiddenField ID="transaction_id" runat="server" />
                            <asp:HiddenField ID="parcel_id" runat="server" />

                            <div class="panel-heading">
                                <div class="panel-title visible-lg visible-md">
                                    <asp:LinkButton ID="lbCalculate" runat="server" CssClass="btn btn-sm btn-danger" OnClick="lbCalculate_Click" Visible="false">
                                        <i class=" icon-reload"></i> Re-calculate PIA & NESS values
                                    </asp:LinkButton>
                                    <ul class="nav nav-tabs visible-lg visible-md visible-sm">
                                        <li class="active"><a href="#tab1" data-toggle="tab">NXP Details</a></li>
                                        <li><a href="#tab2" data-toggle="tab">PIA Values</a></li>
                                        <li><a href="#tab3" data-toggle="tab">NESS Administration</a></li>
                                        <li><a href="#tab4" data-toggle="tab">RFI Details</a></li>
                                        <li><a href="#tab5" data-toggle="tab"><b><i class="icon-note"></i> Inspection Findings</b></a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="tab-content">
                                    <div class="tab-pane" id="tab2"> 
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Measurement Units</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:DropDownList ID="measurement_unit_id" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Basis of Sale</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:DropDownList ID="basis_of_sale" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Unit Price on NXP ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_nxp_unit_price_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Method of Payment</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:DropDownList ID="method_of_payment" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter FOB Invoice Value on NXP ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID ="exporter_fob_value_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small> Exporter Freight Charges</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_freight_charges" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Total Invoice Value (Figure)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="total_invoice_value_figure" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Insurance Charges</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_insurance_charges" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA Unit Price</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_unit_price" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA Exchange Rate</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_exchange_rate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA Exchange Date</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_exchange_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA Fee (₦)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_fee_naira" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA FOB Value of Goods ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_fob_value_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>PIA Total FOB Value of Goods in (₦)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="pia_fob_value_naira" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Total FOB Value of Goods in Words ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="pia_fob_value_word_dollar" runat="server" CssClass="form-control"
                                                        TextMode="MultiLine" Height="100px" Rows="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Total FOB Value of Goods in Words (₦)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="pia_fob_value_word_naira" runat="server" CssClass="form-control"
                                                        TextMode="MultiLine" Height="100px" Rows="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter FOB Value of Goods in Words ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="exporter_fob_value_word_dollar" runat="server" CssClass="form-control"
                                                        TextMode="MultiLine" Height="100px" Rows="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter FOB Value of Goods in Words (₦)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="exporter_fob_value_word_naira" runat="server" CssClass="form-control"
                                                        TextMode="MultiLine" Height="100px" Rows="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab3">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Invoice No</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_invoice_no" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Invoice Date</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_invoice_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Receipt Number</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_receipt_no" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Receipt Date</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_receipt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS(%)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_percent" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Name of NESS Bank</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:DropDownList ID="ness_bank_id" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Fee Payable($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_fee_payable_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Fee Paid($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_fee_paid_dollar" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Fee Payable(₦)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_fee_paybale_naira" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Fee Paid(₦) </small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_fee_paid_naira" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Actual Unit Price (NESS)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_unit_price_ness" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Exchange Rate (NESS)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_exch_rate" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter Exchange Date (NESS)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_exch_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Forex Proceeds Due to Transaction($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="forex_proceed_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Exporter FOB Value (NESS) $</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="exporter_fob_value_ness_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>Balance Paid ($)</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="balance_paid_dollar" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <small>NESS Fee Overpayment/Underpayment</small>
                                            </div>
                                            <div class="col-md-3 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="ness_fee_over_under_pay" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-sm-12 form-group-sm">
                                                <asp:CheckBox ID="use_exporter_values" runat="server" CssClass="form-control" Text="&nbsp; Display Exporter FOB Value on CCI and Invoice" />
                                            </div>
                                            
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab4">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>RFI Number</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_no" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>RFI Receipt Date</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_receipt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>Invoice Details</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_invoice_details" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>Invoice Number</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_invoice_no" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>Invoice Date</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_invoice_date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-12 form-group-sm">
                                                <small>Comments</small>
                                            </div>
                                            <div class="col-md-4 col-sm-12 form-group-sm">
                                                <asp:TextBox ID="rfi_comment" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="tab1">
                                        <asp:Panel ID="nxp" runat="server">
                                            <asp:HiddenField ID="rec_id__" runat="server" />
                                            <asp:HiddenField ID="transaction_id_" runat="server" />
                                            <asp:HiddenField ID="nxp_no___" runat="server" />

                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 form-group-sm">
                                                    <h2>Product Details</h2>
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
                                                    <asp:TextBox ID="txtNXPNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-sm-12 form-group-sm">
                                                    <small>Transaction Number</small>
                                                </div>
                                                <div class="col-md-4 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="txtTransID" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                    <asp:DropDownList ID="measurement_unit_id_" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 col-sm-12 form-group-sm">
                                                    <small>Quantity on NXP</small>
                                                </div>
                                                <div class="col-md-4 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="nxp_qty" runat="server" CssClass="form-control number" onchange="reCalcAllValues();"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-sm-12 form-group-sm">
                                                    <small>Unit Price on NXP</small>
                                                </div>
                                                <div class="col-md-4 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="nxp_unit_price" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
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
                                                    <asp:DropDownList ID="currency_id" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostBack_Click"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 col-sm-12 form-group-sm">
                                                    <small>Exchange Rate on NXP</small>
                                                </div>
                                                <div class="col-md-4 col-sm-12 form-group-sm">
                                                    <asp:TextBox ID="exchange_rate" runat="server" CssClass="form-control" onchange="reCalcAllValues();"></asp:TextBox>
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

                                                <div class="col-md-12 col-sm-12 form-group-sm">
                                                    <hr />
                                                </div>

                                                <div class="col-md-12 col-sm-12 form-group-sm">
                                                    <h2>Consignor & Consignee Details</h2>
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

                                                <div class="col-md-12 col-sm-12 form-group-sm">
                                                    <hr />
                                                </div>

                                                <div class="col-md-12 col-sm-12 form-group-sm">
                                                    <h2>Shipment Details</h2>
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
                                    </div>
                                    <div class="tab-pane active" id="tab5"> 
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-md-12 col-sm-12 form-group-sm">
                    <asp:linkbutton id="lbSave" runat="server" cssclass="btn btn-primary pull-right" OnClick="lbSave_Click">
                        <i class="fa fa-save"></i> Save all Changes
                    </asp:linkbutton>
                </div>
            </div>
        </asp:Panel>

    </div>
        

<!--
    <div class="col-md-2 col-sm-12 form-group-sm">
        <small>CCI Number</small>
    </div>
    <div class="col-md-4 col-sm-12 form-group-sm">
        <asp:TextBox ID="cci_no" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    </div>
    <div class="col-md-2 col-sm-12 form-group-sm">
        <small>CCI Issue Date</small>
    </div>
    <div class="col-md-4 col-sm-12 form-group-sm">
        <asp:TextBox ID="cci_issue_date" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
    </div>
    <div class="col-md-2 col-sm-12 form-group-sm">
        <small>CCI Control Sheet No</small>
    </div>
    <div class="col-md-4 col-sm-12 form-group-sm">
        <asp:TextBox ID="cci_control_sheet_no" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="col-md-2 col-sm-12 form-group-sm">
        <small>CCI Dispatch Date</small>
    </div>
    <div class="col-md-4 col-sm-12 form-group-sm">
        <asp:TextBox ID="cci_dispatch_date" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="col-md-2 col-sm-12 form-group-sm">
        <small>Approval Status</small>
    </div>
    <div class="col-md-4 col-sm-12 form-group-sm">
        <asp:DropDownList ID="cci_approval_status" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
-->        
   </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

