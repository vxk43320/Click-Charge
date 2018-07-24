<%@ Page Title="" Language="C#" MasterPageFile="~/Users/UserMaster.master" AutoEventWireup="true" CodeFile="Offers.aspx.cs" Inherits="Users_Offers" Theme="ClickCharge"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">Offers</h4>
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
                        <h5>Category</h5>
                        <asp:DropDownList ID="ddlCategory" runat="server" Width="505px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                        
                        <br /><br />
                        <asp:DataList ID="dtlst" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Both" RepeatDirection="Horizontal" RepeatColumns="3">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <ItemStyle BackColor="White" ForeColor="#003399" />
                            <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl ='<%# "~/Photos/"+ Eval("Photo") %>' height="100px" Width="150px" /><br />
                                <asp:Label ID="lblOffer" runat="server" Text='<%# Eval("Offer") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

