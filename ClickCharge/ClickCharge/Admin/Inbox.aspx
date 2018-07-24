<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Admin_Inbox" Theme="ClickCharge"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">Inbox</h4>
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
                        <asp:Panel ID="pnlissue" runat="server" Visible="false" >
                             <h5>Issue</h5>
                        <asp:TextBox ID="txtIssue" runat="server" Enabled="false"></asp:TextBox>                        
                        <br /><br />
                        <h5>Answer/Fix</h5>
                        <asp:HiddenField ID="hdnIssueID" runat="server" />
                        <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqtxtAnswer" runat="server" ErrorMessage="*" ControlToValidate="txtAnswer" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br /><br />
                        
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnCancel_Click" CausesValidation="false" />
                        <br /><br />
                        </asp:Panel>
                        <asp:GridView ID="gvSource" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Data Found"
                            OnRowCommand="gvSource_RowCommand" Width="561px" CellPadding="4" GridLines="None" ForeColor="#333333" OnRowDataBound="gvSource_RowDataBound">
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="14pt" ForeColor="Navy" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueID" runat="server" Text='<%#Eval("IssueID") %>' Visible="false">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recharge Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssue" runat="server" Text='<%#Eval("Issue") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Answer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnswer" runat="server" Text='<%#Eval("Answer") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnModify" runat="server" CommandArgument='<%#Eval("IssueID") %>'
                                            CommandName="Answer" Text="Answer" CausesValidation="false" Width="100px" />
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

