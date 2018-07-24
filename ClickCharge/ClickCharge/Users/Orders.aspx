<%@ Page Title="" Language="C#" MasterPageFile="~/Users/UserMaster.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Users_Orders" Theme="ClickCharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hfCustomerId.ClientID %>").value = e.get_value();
        }
    </script>
    <style type="text/css">
        input[type="checkbox"]
        {
            width:15px;
            height:15px;
        }
        input[type="checkbox"] + label {
    color: #000;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="view_plans">
        <h4 class="view_title">Recharge</h4>
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
                        <asp:Panel ID="pnlRecharge" runat="server">
                            <h5>Recharge Type</h5>
                            <asp:DropDownList ID="ddlRechargeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRechargeType_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ReqddlRechargeType" runat="server" ErrorMessage="*" ControlToValidate="ddlRechargeType" Display="Dynamic" ValidationGroup="Recharge"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            <asp:Panel ID="pnlMobile" runat="server">
                                <h5>Recharge Number</h5>
                                <asp:TextBox ID="txtMobileNumber" runat="server" OnTextChanged="txtMobileNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ServiceMethod="SearchContacts" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtMobileNumber" ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hfCustomerId" runat="server" />
                                <asp:RequiredFieldValidator ID="ReqtxtMobileNumber" runat="server" ErrorMessage="*" ControlToValidate="txtMobileNumber" Display="Dynamic" ValidationGroup="Recharge"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="enter valid number" ControlToValidate="txtMobileNumber" ValidationExpression="\d{10}" Display="Dynamic" ValidationGroup="Recharge"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                            </asp:Panel>
                            <asp:Panel ID="pnlNetwork" runat="server">
                                <h5>Network</h5>
                                <asp:DropDownList ID="ddlNetwork" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqddlNetwork" runat="server" ErrorMessage="*" ControlToValidate="ddlNetwork" Display="Dynamic" ValidationGroup="Recharge"></asp:RequiredFieldValidator>
                                <br />
                                <br />
                            </asp:Panel>
                            <h5>Amount</h5>
                            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ErrorMessage="*" ControlToValidate="txtAmount" Display="Dynamic"></asp:RequiredFieldValidator>

                            <br />
                            <br />
                            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" ValidationGroup="Recharge" />
                        </asp:Panel>
                        <asp:Panel ID="pnlPayment" runat="server" Visible="false">
                            <asp:CheckBox ID="chkWallet" runat="server" Visible="false" ForeColor="Black" AutoPostBack="true" OnCheckedChanged="chkWallet_CheckedChanged" />
                            <asp:HiddenField ID="hdnwalletamount" runat="server" />
                            <br />
                            <br />
                            <h5>Card Number</h5>
                            <asp:TextBox ID="txtCardNumber" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReqtxtCardNumber" runat="server" ErrorMessage="*" ControlToValidate="txtCardNumber" Display="Dynamic" ValidationGroup="Paymnet"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegtxtCardNumber" runat="server" ErrorMessage="cannot exceed 16 no's" ControlToValidate="txtCardNumber" ValidationExpression="\d{16}" Display="Dynamic" ValidationGroup="Payment"></asp:RegularExpressionValidator>
                            <br />
                            <br />
                            <h5>Expiry Date</h5>
                            <asp:TextBox ID="txtExpiry" runat="server" placeholder="MMYY" Width="100px" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReqtxtExpiry" runat="server" ErrorMessage="*" ControlToValidate="txtExpiry" Display="Dynamic" ValidationGroup="Paymnet"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegtxtExpiry" runat="server" ErrorMessage="cannot exceed 4 no's" ControlToValidate="txtExpiry" ValidationExpression="\d{4}" Display="Dynamic" ValidationGroup="Payment"></asp:RegularExpressionValidator>
                            <br />
                            <br />
                            <h5>CVV</h5>
                            <asp:TextBox ID="txtCVV" runat="server" Width="100px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ReqtxtCVV" runat="server" ErrorMessage="*" ControlToValidate="txtCVV" Display="Dynamic" ValidationGroup="Paymnet"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegtxtCVV" runat="server" ErrorMessage="cannot exceed 3 no's" ControlToValidate="txtCVV" ValidationExpression="\d{3}" Display="Dynamic" ValidationGroup="Payment"></asp:RegularExpressionValidator>
                            <br />
                            <br />
                            <asp:CheckBox ID="chkSavePayment" runat="server" ForeColor="Black" Text="Do you want to save payment details with us?" Checked="true" />
                            <asp:HiddenField ID="hdnPayment" runat="server" />
                            <br />
                            <br />
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnFinish" runat="server" Text="Finish" OnClick="btnFinish_Click" />
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

