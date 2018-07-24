using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Contacts : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillContacts();
            fillNetworks();
        }
    }
    private void fillNetworks()
    {
        string selectSQL;
        selectSQL = "SELECT * From Networks where RechargeTypeID=2 ";
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
    private void fillContacts()
    {
        string selectSQL;
        selectSQL = "SELECT C.ContactsID,C.ContactNumber,C.ContactName,ISNULL(N.NetworkName,'') as NetworkName From Contacts C left outer join Networks N on C.NetworksID=N.NetworksID  where UserID=" + Session["UserID"] + "";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Contacts");
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
            gvSource.DataSource = ds;
            gvSource.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string insertSQL = "";
        if (Page.IsValid)
        {
            if (hdnContactsID.Value == "")
            {
                insertSQL = "INSERT INTO Contacts (ContactNumber,ContactName,NetworksID,UserID) VALUES (";
                insertSQL += "'" + txtContactNumber.Text  + "'";
                insertSQL += ",'" + txtContactName.Text + "'";
                insertSQL += ",'" + ddlNetwork.SelectedValue + "'";
                insertSQL += ",'" + Session["UserID"] + "')";
            }
            else
            {
                insertSQL = "Update Contacts set ";
                insertSQL += "ContactNumber='" + txtContactNumber.Text + "'";
                insertSQL += ",ContactName='" + txtContactName.Text + "'";
                insertSQL += ",NetworksID='" + ddlNetwork.SelectedValue + "'";
                insertSQL += " where ContactsID='" + hdnContactsID.Value + "'";
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
                fillContacts();
                Clear();
            }

        }
    }

    protected void gvSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteContacts")
        {
            int ContactsID;
            ContactsID = Convert.ToInt32(e.CommandArgument);
            string deleteSQL = "DELETE FROM Contacts";
            deleteSQL += " WHERE ContactsID=" + ContactsID;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(deleteSQL, con);
            int deleted = 0;
            try
            {
                con.Open();
                deleted = cmd.ExecuteNonQuery();
                lblSucess.Text = "Successfully Deleted.";
                lblErrorMsg.Text = "";
            }
            catch (Exception err)
            {
                lblSucess.Text = "";
                lblErrorMsg.Text = "Error deleting record. ";
                lblErrorMsg.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
            if (deleted > 0)
            {
                fillContacts();
            }
        }
        if (e.CommandName == "Modify")
        {
            hdnContactsID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT * FROM Contacts ";
            selectSQL += "WHERE ContactsID='" + hdnContactsID.Value + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtContactNumber.Text = reader["ContactNumber"].ToString();
                    txtContactName.Text = reader["ContactName"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(reader["NetworksID"])))
                    {
                        ddlNetwork.SelectedValue = Convert.ToString(reader["NetworksID"]);
                    }                    
                    reader.Close();
                }
            }
            catch (Exception err)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text += err.Message;
            }
            finally
            {
                con.Close();
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lblErrorMsg.Text = "";
        lblSucess.Text = "";
    }
    private void Clear()
    {
        txtContactNumber.Text = "";
        txtContactName.Text = "";
        hdnContactsID.Value = "";
        ddlNetwork.ClearSelection();
    }
  
}