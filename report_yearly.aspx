<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="report_yearly.aspx.cs" Inherits="_ReportYearly" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-refresh"></i> Yearly Reports</h1>
        </div>

        <div class="row well well-sm">
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Year</small>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <small>Select a Report</small>
                <div class="input-group">
                    <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control"></asp:DropDownList>
                    <span class="input-group-addon input-group-addon-select">
                        <asp:LinkButton ID="lbPreview" runat="server" CssClass="btn-sm btn-primary" OnClick="lbPreview_Click">
                            <i class="icon-reload"></i>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
        </div>

          <div class="row well well-sm">
            <div class="col-md-12 col-sm-12 form-group-sm">
                <br />
                <div class="table-responsive" style="overflow-x: hidden;">
                    <table id="main-table" class="table table-striped table-hover" style="font-size: 12px; width: 100%">
                        <thead>
                            <tr>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="row">
            <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
        </div>
    </div>

    <asp:HiddenField ID="chart_data_hidden" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
    <script src="js/moment.min.js"></script>
    <script src="js/daterangepicker.min.js"></script>
    <link href="js/daterangepicker.css" rel="stylesheet" />

    <script src="plugins/datatables/Buttons-1.0.3/js/buttons.print.min.js"></script>
    <script src="plugins/datatables/Buttons-1.0.3/js/dataTables.buttons.min.js"></script>
    <script src="plugins/datatables/DataTables-1.10.9/js/jszip.min.js"></script>
    <script src="plugins/datatables/DataTables-1.10.9/js/pdfmake.min.js"></script>
    <script src="plugins/datatables/DataTables-1.10.9/js/vfs_fonts.js"></script>
    <script src="plugins/datatables/Buttons-1.0.3/js/buttons.html5.min.js"></script>
    <script src="plugins/datatables/Buttons-1.0.3/js/buttons.colVis.min.js"></script>

    <%--highchart--%>
    <script src="plugins/highchart/highcharts-new.js"></script>
    <script src="plugins/highchart/drilldown.js"></script>
    <script src="plugins/highchart/exporting.js"></script>
    <script src="plugins/highchart/export-data.js"></script>
    <script src="plugins/highchart/offline-exporting.js"></script>
    <script src="js/highchart-utilities.js"></script>
    <%--<script src="js/highchart-utilities.js?v=<% =DateTime.Now %>"></script>--%>

    <script>

        //var previous_date_selection = $("#ContentPlaceHolder1_daterange_hidden").val();
        //$("#daterange").change(function () {
        //    $("#ContentPlaceHolder1_daterange_hidden").val($("#daterange").val());
        //}); 

      
        var load = '<% =tableLoaded %>';
        if (load === 'True') {
            var tbl = $("#main-table").DataTable();
            try {
                tbl.destroy();
                $("#main-table").empty();
            } catch{

            }
        }

        if ('<%  =loadChart%>' == 'True') {

            var functionName = '<% =functionToLoad %>';
            var my_data = $("#ContentPlaceHolder1_chart_data_hidden").val();


            switch (functionName) {
                case 'dual_axis_bar_chart':
                    build_bar_chart_dual_axis();
                    break;
                case 'column_chart':
                    build_Column_chart();
                    break;
                case 'drill_down_chart':
                    build_drilldown_bar_chart();
                    break;
                case 'pos_neg_chart':
                    Build_Pos_Neg_Chart();
                    break;
                case 'build_side_by_side_column_chart':
                    build_side_by_side_column_chart(JSON.parse(my_data));
                    break;
                case 'stacked_bar_with_percent':
                    build_stacked_bar_with_percent();
                    break;
                case 'donut':
                    BuildDonut()
                    break;
                case 'Build_Pie_Chart':
                    Build_Pie_Chart(JSON.parse(my_data));
                    break;
                default:
                    break;
            }

        }
         
    </script>
</asp:Content>

