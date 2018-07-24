<%@ Page Title="" Language="C#" MasterPageFile="~/Users/UserMaster.master" AutoEventWireup="true" CodeFile="Issues.aspx.cs" Inherits="Users_Issues" Theme="ClickCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="view_plans">
        <h4 class="view_title">Issue</h4>
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
                        <h5>Issue</h5>
                        <asp:TextBox ID="txtIssue" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqtxtIssue" runat="server" ErrorMessage="*" ControlToValidate="txtIssue" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnCancel_Click" CausesValidation="false" />
                        <br /><br />
                        <asp:GridView ID="gvSource" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Data Found"
                            Width="700px" CellPadding="4" GridLines="None" ForeColor="#333333">
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="14pt" ForeColor="Navy" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueID" runat="server" Text='<%#Eval("IssueID") %>' >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Name" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssue" runat="server" Text='<%#Eval("Issue") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Answer" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnswer" runat="server" Text='<%#Eval("Answer") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Answer By" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnswer" runat="server" Text='<%#Eval("AnswerBy") %>'>
                                        </asp:Label>
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

