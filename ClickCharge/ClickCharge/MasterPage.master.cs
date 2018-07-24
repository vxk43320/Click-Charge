using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True");
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillNetworks();
        }
    }

    private void fillNetworks()
    {
        string selectSQL;
        selectSQL = "SELECT * From Networks where RechargeTypeID=2 ";        
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
        ddlNetwork.Items.Insert(0, new ListItem("Network", ""));
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string sql = "";
        if (txtUsername.Text == "admin")
        {
            sql = "select AdminID as UserId,'' as MobileNumber,'' as NetworksID,'' as EmailID from Admin where username='" + txtUsername.Text + "' and password='" + txtPassword.Text + "'";
        }
        else
        {
            sql = "select UserId,MobileNumber,NetworksID,EmailID from Users where MobileNumber='" + txtUsername.Text + "' and password='" + txtPassword.Text + "'";
        }
        SqlCommand cmd = new SqlCommand(sql, con);
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            Session["UserID"] = dr["UserId"].ToString();
            Session["MobileNumber"] = dr["MobileNumber"].ToString();
            Session["NetworksID"] = dr["NetworksID"].ToString();
            Session["EmailID"] = dr["EmailID"].ToString();
        }
        dr.Close();
        con.Close();
        if (Session["UserID"] != null)
        {
            if (txtUsername.Text == "admin")
            {
                Response.Redirect("~/Admin/Inbox.aspx");
            }
            else
            {
                Response.Redirect("~/Users/Orders.aspx");
            }            
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string s = "insert into Users values('" + txtName.Text + "','" + txtMobileno.Text + "'," + Convert.ToInt32(ddlNetwork.SelectedValue) + ",'" + txtEmailid.Text + "','" + txtregPassword.Text + "')";
        SqlCommand cmd = new SqlCommand(s, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblMsg.Text = "";
            lblMsg.Text = "Error in Registration. ";
            lblMsg.Text += err.Message;
        }
        finally
        {
            con.Close();
        }
        lblMsg.Text = "Successfully Registered";
        lblMsg.Visible = true;

        Clear();
    }
    private void Clear()
    {
        txtName.Text = "";
        txtregPassword.Text = "";
        txtMobileno.Text = "";
        txtEmailid.Text = "";
    }
}
