<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Networks.aspx.cs" Inherits="Admin_Networks" Theme="ClickCharge"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">Networks</h4>
        <div class="clearfix"></div>
        <table align="Center">
            <tr>
                <td align="Center">
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblSucess" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="edit-for-sett">
                        <h5>Recharge Type</h5>
                        <asp:HiddenField ID="hdnNetworksID" runat="server" />
                        <asp:DropDownList ID="ddlRechargeType" runat="server" Width="505px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="ReqddlRechargeType" runat="server" ErrorMessage="*" ControlToValidate="ddlRechargeType" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br /><br />
                        <h5>Network</h5>
                        <asp:TextBox ID="txtNetwork" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqtxtNetwork" runat="server" ErrorMessage="*" ControlToValidate="txtNetwork" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnCancel_Click" CausesValidation="false" />
                        <br /><br />
                        <asp:GridView ID="gvSource" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Data Found"
                            OnRowCommand="gvSource_RowCommand" Width="561px" CellPadding="4" GridLines="None" ForeColor="#333333">
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="14pt" ForeColor="Navy" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetworksID" runat="server" Text='<%#Eval("NetworksID") %>' Visible="false">
                                             
                                        </asp:Label>
                                        <asp:Label ID="lblRechargeTypeID" runat="server" Text='<%#Eval("RechargeTypeID") %>' Visible="false">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recharge Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRechargeType" runat="server" Text='<%#Eval("RechargeType") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Network">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetwork" runat="server" Text='<%#Eval("NetworkName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify">
                                    <ItemTemplate>
                                        <asp:Button ID="btnModify" runat="server" CommandArgument='<%#Eval("NetworksID") %>'
                                            CommandName="Modify" Text="Modify" CausesValidation="false" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteNetworks" CommandArgument='<%#Eval("NetworksID") %>'
                                            Text="Delete" CausesValidation="false" Width="100px"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

