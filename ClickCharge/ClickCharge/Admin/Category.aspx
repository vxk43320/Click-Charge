﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Admin_Category" Theme="ClickCharge"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">Category</h4>
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
                        <h5>Category Name</h5>
                        <asp:HiddenField ID="hdnCategoryID" runat="server" />
                        <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqtxtCategory" runat="server" ErrorMessage="*" ControlToValidate="txtCategory" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="vldCode" runat="server" ErrorMessage="Category already exists."
                            ValidateEmptyText="False" OnServerValidate="vldCode_ServerValidate" ControlToValidate="txtCategory" Display="Dynamic" />
                        
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
                                        <asp:Label ID="lblCategoryID" runat="server" Text='<%#Eval("CategoryID") %>' Visible="false">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CategoryName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Modify">
                                    <ItemTemplate>
                                        <asp:Button ID="btnModify" runat="server" CommandArgument='<%#Eval("CategoryID") %>'
                                            CommandName="Modify" Text="Modify" CausesValidation="false" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteCategory" CommandArgument='<%#Eval("CategoryID") %>'
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

