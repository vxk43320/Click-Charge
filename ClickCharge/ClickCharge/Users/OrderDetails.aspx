<%@ Page Title="" Language="C#" MasterPageFile="~/Users/UserMaster.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="Users_OrderDetails" Theme="ClickCharge"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">My Orders</h4>
        <div class="clearfix"></div>
        <table align="Center">
            <tr>
                <td align="Center">
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblSucess" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="Center">
                    <span style="font-weight:bold">Your Wallet balance amount is:</span><asp:Label ID="lblWalletBalance" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="edit-for-sett">
                        <h5>Recharge Type</h5>
                           <asp:DropDownList ID="ddlOptions" runat="server" Width="505px" AutoPostBack="true" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                               <asp:ListItem Value="1" Text="Only Last 30 days"></asp:ListItem>
                               <asp:ListItem Value="2" Text="All Transactions"></asp:ListItem>
                           </asp:DropDownList><br />
                        <asp:DropDownList ID="ddlRechargeType" runat="server" Width="505px" AutoPostBack="True" OnSelectedIndexChanged="ddlRechargeType_SelectedIndexChanged"></asp:DropDownList>
                        
                        
                        <br /><br />
                        <asp:GridView ID="gvSource" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Data Found"
                            Width="561px" CellPadding="4" GridLines="None" ForeColor="#333333" OnRowDataBound="gvSource_RowDataBound">
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="14pt" ForeColor="Navy" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%#Eval("OrderID") %>' Visible="false">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recharge No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRechargeNo" runat="server" Text='<%#Eval("RechargeNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Network">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetwork" runat="server" Text='<%#Eval("NetworkName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:BoundField DataField="Date" HeaderText="Date" />                               
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

