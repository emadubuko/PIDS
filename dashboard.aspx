<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading">
            <h1><i class="fa fa-dashboard"></i> My Dashboard</h1>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12">
            <ul class="statistics">
                <li>
                    <i class="icon-doc"></i>
                    <div class="number"><asp:Label ID="lblNXPTotal" runat="server"></asp:Label></div>
                    <div class="text-info">NXP done today</div>
                </li>
                <li>
                    <i class="icon-note"></i>
                    <div class="number">500</div>
                    <div class="text-info">Inspection(s) done today</div>
                </li>
                <li>
                    <i class="icon-docs"></i>
                    <div class="number">300</div>
                    <div class="title">Pending Documentation</div>
                </li>
                <li>
                    <i class="icon-graph"></i>
                    <div class="number">100</div>
                    <div class="title">Pending CCI</div>
                </li>
            </ul>
        </div>
        <div class="col-md-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title">
                        <i class="icon-info"></i>
                        <small>Welcome, </small> <span class="text-capitalize">Test User</span>
                    </h1>
                    <div class="panel-actions">
                    </div>
                </div>
                <div class="panel-body">
                    ...
                    <!--
                    <p class="text-info"><em>here you can...</em></p>
                    <ol>
                        <li><p><strong>Update your</strong> personal information</p></li>
                        <li><p>Check your <strong>Savings</strong>  Balance</p></li>
                        <li><p><strong>View your</strong> Loan balances</p></li>
                        <li><p><strong>Apply for</strong> a new Loan</p></li>
                        <li><p>Request for <strong>Withdrawal of</strong> Membership</p></li>
                    </ol>
                    -->
                </div>
            </div>
        </div>
        <div class="col-md-7 col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title"><i class="icon-bar-chart"></i> Chart</h1>
                    <div class="panel-actions">

                    </div>
                </div>
                <div class="panel-body">
                    ...
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">

</asp:Content>

