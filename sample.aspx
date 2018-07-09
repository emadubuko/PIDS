<%@ Page Title="" Language="C#" MasterPageFile="~/pids.master" AutoEventWireup="true" CodeFile="sample.aspx.cs" Inherits="bank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <div class="panel-primary">
        <div class="row">
            <asp:Panel id="pnlView" runat="server" visible="true">
                 <div class="col-md-2 col-sm-12 form-group-sm">
                    <label>Name Search</label>
                </div>
                <div class="col-md-8 col-sm-12 form-group-sm">
                    <asp:TextBox ID="name_search" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12 form-group-sm">
                    <asp:LinkButton ID="link_name_search" runat="server" CssClass="btn btn-default"  data-toggle="modal" data-target="#modal-popup">
                        <i class="icon-docs"></i> Add Details
                    </asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel id="pnlEdit" runat="server" visible="false">
            </asp:Panel>
        </div>
    </div>

    <div class="modal fade" id="modal-popup" tabindex="-1" role="dialog" aria-labelledby="modal-popup" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="modal-title"><label>Bank Details </label></h3>
                </div>
                <div class="modal-body">
                    <div class="row">

            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Bank ID </label>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:TextBox ID="bank_id" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Bank Name</label>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:TextBox ID="bank_name" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Bank Acronym</label>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:TextBox ID="bank_acronym" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12 form-group-sm">
                <label>Bank Address</label>
            </div>
            <div class="col-md-10 col-sm-12 form-group-sm">
                <asp:TextBox ID="bank_aaddress" runat="server" CssClass="form-control"></asp:TextBox>
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

    <table  width="100%" border="0" align="center">

        <tr>
            <td>
                <small>H </small>
                <asp:DropDownList ID="Time_hour" runat="server" CssClass="DropDown" Width="44px">
                    <asp:ListItem Text="00" Value="0" Selected="True" />
                    <asp:ListItem Text="01" Value="1" />
                    <asp:ListItem Text="02" Value="2" />
                    <asp:ListItem Text="03" Value="3" />
                    <asp:ListItem Text="04" Value="4" />
                    <asp:ListItem Text="05" Value="5" />
                    <asp:ListItem Text="06" Value="6" />
                    <asp:ListItem Text="07" Value="7" />
                    <asp:ListItem Text="08" Value="8" />
                    <asp:ListItem Text="09" Value="9" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="11" Value="11" />
                    <asp:ListItem Text="12" Value="12" />
                    <asp:ListItem Text="13" Value="13" />
                    <asp:ListItem Text="14" Value="14" />
                    <asp:ListItem Text="15" Value="15" />
                    <asp:ListItem Text="16" Value="16" />
                    <asp:ListItem Text="17" Value="17" />
                    <asp:ListItem Text="18" Value="18" />
                    <asp:ListItem Text="19" Value="19" />
                    <asp:ListItem Text="20" Value="20" />
                    <asp:ListItem Text="21" Value="21" />
                    <asp:ListItem Text="22" Value="22" />
                    <asp:ListItem Text="23" Value="23" />
                  

                </asp:DropDownList>
                <small>M </small>
                <asp:DropDownList ID="time_minute" runat="server" CssClass="DropDown" Width="44px">
                    <asp:ListItem Text="00" Value="0" Selected="True" />
                    <asp:ListItem Text="01" Value="1" />
                    <asp:ListItem Text="02" Value="2" />
                    <asp:ListItem Text="03" Value="3" />
                    <asp:ListItem Text="04" Value="4" />
                    <asp:ListItem Text="05" Value="5" />
                    <asp:ListItem Text="06" Value="6" />
                    <asp:ListItem Text="07" Value="7" />
                    <asp:ListItem Text="08" Value="8" />
                    <asp:ListItem Text="09" Value="9" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="11" Value="11" />
                    <asp:ListItem Text="12" Value="12" />
                    <asp:ListItem Text="13" Value="13" />
                    <asp:ListItem Text="14" Value="14" />
                    <asp:ListItem Text="15" Value="15" />
                    <asp:ListItem Text="16" Value="16" />
                    <asp:ListItem Text="17" Value="17" />
                    <asp:ListItem Text="18" Value="18" />
                    <asp:ListItem Text="19" Value="19" />
                    <asp:ListItem Text="20" Value="20" />
                    <asp:ListItem Text="21" Value="21" />
                    <asp:ListItem Text="22" Value="22" />
                    <asp:ListItem Text="23" Value="23" />
                    <asp:ListItem Text="24" Value="24" />
                    <asp:ListItem Text="25" Value="25" />
                    <asp:ListItem Text="26" Value="26" />
                    <asp:ListItem Text="27" Value="27" />
                    <asp:ListItem Text="28" Value="28" />
                    <asp:ListItem Text="29" Value="29" />
                    <asp:ListItem Text="30" Value="30" />
                    <asp:ListItem Text="31" Value="31" />
                    <asp:ListItem Text="32" Value="32" />
                    <asp:ListItem Text="33" Value="33" />
                    <asp:ListItem Text="34" Value="34" />
                    <asp:ListItem Text="35" Value="35" />
                    <asp:ListItem Text="36" Value="36" />
                    <asp:ListItem Text="37" Value="37" />
                    <asp:ListItem Text="38" Value="38" />
                    <asp:ListItem Text="39" Value="39" />
                    <asp:ListItem Text="40" Value="40" />
                    <asp:ListItem Text="41" Value="41" />
                    <asp:ListItem Text="42" Value="42" />
                    <asp:ListItem Text="43" Value="43" />
                    <asp:ListItem Text="44" Value="44" />
                    <asp:ListItem Text="45" Value="45" />
                    <asp:ListItem Text="46" Value="46" />
                    <asp:ListItem Text="47" Value="47" />
                    <asp:ListItem Text="48" Value="48" />
                    <asp:ListItem Text="49" Value="49" />
                    <asp:ListItem Text="50" Value="50" />
                    <asp:ListItem Text="51" Value="51" />
                    <asp:ListItem Text="52" Value="52" />
                    <asp:ListItem Text="53" Value="53" />
                    <asp:ListItem Text="54" Value="54" />
                    <asp:ListItem Text="55" Value="55" />
                    <asp:ListItem Text="56" Value="56" />
                    <asp:ListItem Text="57" Value="57" />
                    <asp:ListItem Text="58" Value="58" />
                    <asp:ListItem Text="59" Value="59" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>

         </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

