<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="inspection_register.aspx.cs" Inherits="_InspectionRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel-primary">
        <div class="panel-heading row">
            <h1><i class="icon-book-open"></i> Inspection Register</h1>
        </div>

        <div class="row well well-sm">
            <div class="panel-heading">
                <h2>Find </h2>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <small>Year</small>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12 form-group-sm">
                <small>Month</small>
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPostback_SelectedIndexChanged">
                </asp:DropDownList>
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
                    <asp:GridView ID="gvListInspect" runat="server" AllowPaging="True" CssClass="myGridClass" AutoGenerateColumns="true" ShowFooter="true" 
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged" EmptyDataText="There is no record for the specified criteria" 
                        GridLines="None" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" Width="100%" OnPageIndexChanging="gvList_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelect" runat="server" CssClass="btn-sm btn-primary" 
                                            CausesValidation="False" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-edit"></i>
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

