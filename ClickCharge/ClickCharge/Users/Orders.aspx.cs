using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Orders : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRechargeType();
            ddlRechargeType.SelectedValue = "2";
            fillNetworks();
            txtMobileNumber.Text = Convert.ToString(Session["MobileNumber"]);
            ddlNetwork.SelectedValue = Convert.ToString(Session["NetworksID"]);
        }
    }
    private void fillRechargeType()
    {
        string selectSQL;
        selectSQL = "SELECT RechargeTypeID,RechargeType From RechargeType";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "RechargeType");
        }
        catch (Exception err)
        {
        }
        finally
        {
            con.Close();
        }
        if (ds.Tables.Count > 0)
        {
            ddlRechargeType.DataSource = ds;
            ddlRechargeType.DataTextField = "RechargeType";
            ddlRechargeType.DataValueField = "RechargeTypeID";
            ddlRechargeType.DataBind();
            ddlRechargeType.Items.Insert(0, new ListItem("", ""));
        }
    }

    private void fillNetworks()
    {
        string selectSQL;
        selectSQL = "SELECT * From Networks where RechargeTypeID='" + ddlRechargeType.SelectedValue + "' ";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Networks");
        }
        catch (Exception err)
        {
        }
        finally
        {
            con.Close();
        }
        if (ds.Tables.Count > 0)
        {
            ddlNetwork.DataSource = ds;
            ddlNetwork.DataTextField = "NetworkName";
            ddlNetwork.DataValueField = "NetworksID";
            ddlNetwork.DataBind();

        }
        ddlNetwork.Items.Insert(0, new ListItem("", ""));
    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchContacts(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ContactNumber, ContactName from Contacts where " +
                "ContactNumber like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> contacts = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["ContactNumber"].ToString(), sdr["ContactNumber"].ToString());
                        contacts.Add(item);
                    }
                }
                conn.Close();
                return contacts;
            }
        }
    }
    protected void ddlRechargeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillNetworks();
        if (ddlRechargeType.SelectedValue == "2" || ddlRechargeType.SelectedValue == "3")
        {
            pnlMobile.Visible = true;
            pnlNetwork.Visible = true;
            ReqddlNetwork.Enabled = true;
            ReqtxtMobileNumber.Enabled = true;
        }
        else if (ddlRechargeType.SelectedValue == "5")
        {
            pnlMobile.Visible = true;
            pnlNetwork.Visible = false;
            ReqddlNetwork.Enabled = false;
            ReqtxtMobileNumber.Enabled = true;
        }
        else
        {
            pnlMobile.Visible = false;
            pnlNetwork.Visible = false;
            ReqddlNetwork.Enabled = false;
            ReqtxtMobileNumber.Enabled = false;
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnlRecharge.Visible = false;
        getCardDetails();
        getWalletAmount();
        chkWallet_CheckedChanged(sender, e);
        pnlPayment.Visible = true;
        lblSucess.Text = "";
    }
    private void getWalletAmount()
    {
        SqlDataReader dr;
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "";

        sql = "select SUM(p.Amount) Amount from (select (SUM(isnull(Amount,0))-isnull(DeductedAmount,0)) as Amount from Orders where RechargeNo=" + Session["MobileNumber"] + " and RechargeTypeID in (4,5) group by DeductedAmount) as p";

        SqlCommand cmd = new SqlCommand(sql, con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            if (dr["Amount"].ToString() != "" && dr["Amount"].ToString() != "0" && ddlRechargeType.SelectedValue != "4")
            {
                chkWallet.Text = "Do you want to deduct from wallet money(" + dr["Amount"].ToString() + ")?";
                hdnwalletamount.Value = dr["Amount"].ToString();
                chkWallet.Visible = true;
                chkWallet.Checked = true;
            }
            else
            {
                chkWallet.Text = "";
                hdnwalletamount.Value = "";
                chkWallet.Visible = false;
                chkWallet.Checked = false;
            }
        }
        else
        {
            chkWallet.Text = "";
            hdnwalletamount.Value = "";
            chkWallet.Visible = false;
            chkWallet.Checked = false;
        }
        dr.Close();
        con.Close();
    }
    private void getCardDetails()
    {
        SqlDataReader dr;
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "";

        sql = "select * from CardDetails where UserID=" + Session["UserID"] + "";

        SqlCommand cmd = new SqlCommand(sql, con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            txtCardNumber.Text = dr["CardNumber"].ToString();
            txtExpiry.Text = dr["ExpiryDate"].ToString();
            txtCVV.Text = dr["CVV"].ToString();
            chkSavePayment.Checked = true;
            hdnPayment.Value = "Y";
        }
        else
        {
            txtCardNumber.Text = "";
            txtExpiry.Text = "";
            txtCVV.Text = "";
            chkSavePayment.Checked = false;
            hdnPayment.Value = "N";
        }
        dr.Close();
        con.Close();
    }
    protected void chkWallet_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWallet.Checked == true)
        {
            if (hdnwalletamount.Value != "")
            {
                if (Convert.ToDecimal(txtAmount.Text) <= Convert.ToDecimal(hdnwalletamount.Value))
                {
                    txtCardNumber.Enabled = false;
                    ReqtxtCardNumber.Enabled = false;
                    txtExpiry.Enabled = false;
                    ReqtxtExpiry.Enabled = false;
                    txtCVV.Enabled = false;
                    ReqtxtCVV.Enabled = false;
                }
                else
                {
                    txtCardNumber.Enabled = true;
                    ReqtxtCardNumber.Enabled = true;
                    txtExpiry.Enabled = true;
                    ReqtxtExpiry.Enabled = true;
                    txtCVV.Enabled = true;
                    ReqtxtCVV.Enabled = true;
                }
            }
            else
            {
                txtCardNumber.Enabled = true;
                ReqtxtCardNumber.Enabled = true;
                txtExpiry.Enabled = true;
                ReqtxtExpiry.Enabled = true;
                txtCVV.Enabled = true;
                ReqtxtCVV.Enabled = true;
            }
        }
        else
        {
            txtCardNumber.Enabled = true;
            ReqtxtCardNumber.Enabled = true;
            txtExpiry.Enabled = true;
            ReqtxtExpiry.Enabled = true;
            txtCVV.Enabled = true;
            ReqtxtCVV.Enabled = true;
        }
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        pnlRecharge.Visible = true;
        pnlPayment.Visible = false;
    }
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        string insertSQL = "";
        if (Page.IsValid)
        {

            insertSQL = "INSERT INTO Orders (RechargeTypeID,NetworksID,RechargeNo,Amount,UserID,Date) VALUES (";

            insertSQL += "'" + ddlRechargeType.SelectedValue + "'";
            if (ddlNetwork.SelectedValue != "")
            {
                insertSQL += ",'" + ddlNetwork.SelectedValue + "'";
            }
            else
            {
                insertSQL += ",NULL";
            }
            string MobileNo = "";
            if (ddlRechargeType.SelectedValue == "4")
            {
                MobileNo = Session["MobileNumber"].ToString();
            }
            else
            {
                MobileNo = txtMobileNumber.Text;
            }
            insertSQL += ",'" + MobileNo + "'";
            insertSQL += ",'" + txtAmount.Text + "'";
            insertSQL += ",'" + Session["UserID"] + "',GETDATE())";


            if (chkSavePayment.Checked == true)
            {
                if (hdnPayment.Value == "Y")
                {
                    insertSQL += " update CardDetails set ";
                    insertSQL += " CardNumber='" + txtCardNumber.Text + "' ";
                    insertSQL += ",ExpiryDate='" + txtExpiry.Text + "' ";
                    insertSQL += ",CVV='" + txtCVV.Text + "' ";
                    insertSQL += "  where UserID='" + Session["UserID"] + "'";
                }
                else
                {
                    insertSQL += " insert into CardDetails(CardNumber,ExpiryDate,CVV,UserID) values(";
                    insertSQL += "'" + txtCardNumber.Text + "' ";
                    insertSQL += ",'" + txtExpiry.Text + "' ";
                    insertSQL += ",'" + txtCVV.Text + "' ";
                    insertSQL += ",'" + Session["UserID"] + "')";
                }
            }

            decimal amount = 0;
            if (hdnwalletamount.Value != "" && ddlRechargeType.SelectedValue != "4" && chkWallet.Checked == true)
            {
                if (Convert.ToDecimal(hdnwalletamount.Value) > Convert.ToDecimal(txtAmount.Text))
                {
                    amount = Convert.ToDecimal(txtAmount.Text);
                    decimal balamount = Convert.ToDecimal(hdnwalletamount.Value) - Convert.ToDecimal(txtAmount.Text);
                }
                insertSQL += " Update Orders set DeductedAmount=ISNULL((SELECT TOP 1 ISNULL(O.DeductedAmount,0) FROM Orders O WHERE O.RechargeNo='" + Session["MobileNumber"].ToString() + "' AND O.RechargeTypeID in (4,5) order by O.DeductedAmount DESC),0)+" + amount + " WHERE RechargeNo='" + Session["MobileNumber"].ToString() + "' AND RechargeTypeID  in (4,5)";
            }


            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            int added = 0;
            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
                lblSucess.Text = "Successfully Saved.";
                lblErrorMsg.Text = "";

                if (hdnwalletamount.Value != "" && ddlRechargeType.SelectedValue != "4" && chkWallet.Checked == true)
                {
                    if (Convert.ToDecimal(hdnwalletamount.Value) > Convert.ToDecimal(txtAmount.Text))
                    {
                        decimal balamount = Convert.ToDecimal(hdnwalletamount.Value) - Convert.ToDecimal(txtAmount.Text);
                        if (balamount <= 20 && Convert.ToDecimal(hdnwalletamount.Value) >= 20)
                        {
                            MailMessage mail = new MailMessage();
                            mail.To.Add(Convert.ToString(Session["EmailID"]));
                            mail.From = new MailAddress("clickcharge20@gmail.com");// Enter senders emailid
                            mail.Subject = "Wallet balance";
                            string Body = "Hi, <br/><br/>Your wallet balance is below $20. Please recharge as early as possible to use wallet amount in future.<br/><br/>Regards,<br/>Click Charge";
                            mail.Body = Body;
                            mail.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.EnableSsl = true;
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential
                            ("clickcharge20@gmail.com", "click2017");// Enter senders emailid and password  
                            smtp.Send(mail);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                lblSucess.Text = "";
                lblErrorMsg.Text = "Error in Saving record. ";
                lblErrorMsg.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
            if (added > 0)
            {
                Clear();
            }

        }
    }
    private void Clear()
    {
        pnlRecharge.Visible = true;
        pnlMobile.Visible = true;
        pnlNetwork.Visible = true;
        pnlPayment.Visible = false;
        hdnPayment.Value = "";
        hdnwalletamount.Value = "";
        txtAmount.Text = "";
        txtCardNumber.Text = "";
        txtCVV.Text = "";
        txtExpiry.Text = "";
        txtMobileNumber.Text = "";
        ddlNetwork.ClearSelection();
        ddlRechargeType.ClearSelection();
        chkWallet.Text = "";
        chkWallet.Checked = false;
    }
    protected void txtMobileNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(connectionString);
            string sql = "";
            sql = "select NetworksID from Contacts where ContactNumber=" + txtMobileNumber.Text + "";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ddlNetwork.SelectedValue = dr["NetworksID"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}