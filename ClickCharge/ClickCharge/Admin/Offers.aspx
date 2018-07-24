<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Offers.aspx.cs" Inherits="Admin_Offers" Theme="ClickCharge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                        <asp:HiddenField ID="hdnOfferID" runat="server" />
                        <asp:DropDownList ID="ddlCategory" runat="server" Width="505px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="ReqddlCategory" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br /><br />
                        <h5>Offer</h5>
                        <asp:TextBox ID="txtOffer" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqtxtOffer" runat="server" ErrorMessage="*" ControlToValidate="txtOffer" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br /><br />
                        <h5>Image</h5>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        
                        <br /><br />
                        <h5>Expiry Date</h5>
                        <asp:TextBox ID="txtExpiryDate" runat="server" OnTextChanged="txtExpiryDate_TextChanged"></asp:TextBox>  
                    <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtExpiryDate" Format="MM/dd/yyyy HH:mm:ss"> </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="ReqtxtExpiryDate" runat="server" ErrorMessage="*" ControlToValidate="txtExpiryDate" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="lblOfferID" runat="server" Text='<%#Eval("OfferID") %>' Visible="false">
                                             
                                        </asp:Label>
                                        <asp:Label ID="lblCategoryID" runat="server" Text='<%#Eval("CategoryID") %>' Visible="false">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Offer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffer" runat="server" Text='<%#Eval("Offer") %>'>
                                        </asp:Label
                                            >
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRechargeType" runat="server" Text='<%#Eval("CategoryName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl ='<%# "~/Photos/"+ Eval("Photo") %>' height="50px" Width="50px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expiry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiryDate" runat="server" Text='<%#Eval("ExpiryDate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify">
                                    <ItemTemplate>
                                        <asp:Button ID="btnModify" runat="server" CommandArgument='<%#Eval("OfferID") %>'
                                            CommandName="Modify" Text="Modify" CausesValidation="false" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteOffer" CommandArgument='<%#Eval("OfferID") %>'
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

