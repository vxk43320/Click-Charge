using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Networks : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRechargeType();
            fillNetworks();
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
        catch (Exception )
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
        selectSQL = "SELECT N.NetworksID,N.RechargeTypeID,N.NetworkName,R.RechargeType From Networks N inner join RechargeType R on N.RechargeTypeID=R.RechargeTypeID ";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Networks");
        }
        catch (Exception )
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
            if (hdnNetworksID.Value == "")
            {
                insertSQL = "INSERT INTO Networks (RechargeTypeID,NetworkName,CreatedBy) VALUES (";
                insertSQL += "'" + ddlRechargeType.SelectedValue + "'";
                insertSQL += ",'" + txtNetwork.Text + "'";
                insertSQL += ",'" + Session["UserID"] + "')";
            }
            else
            {
                insertSQL = "Update Networks set ";
                insertSQL += "RechargeTypeID='" + ddlRechargeType.SelectedValue + "'";
                insertSQL += ",NetworkName='" + txtNetwork.Text + "'";
                insertSQL += " where NetworksId='" + hdnNetworksID.Value + "'";
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
                fillNetworks();
                Clear();
            }

        }
    }
    
    protected void gvSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteNetworks")
        {
            int NetworksID;
            NetworksID = Convert.ToInt32(e.CommandArgument);
            string deleteSQL = "DELETE FROM Networks";
            deleteSQL += " WHERE NetworksID=" + NetworksID;
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
                fillNetworks();
            }
        }
        if (e.CommandName == "Modify")
        {
            hdnNetworksID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT * FROM Networks ";
            selectSQL += "WHERE NetworksID='" + hdnNetworksID.Value + "'";
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
                    ddlRechargeType.SelectedValue = reader["RechargeTypeID"].ToString();
                    txtNetwork.Text = reader["NetworkName"].ToString();
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
        txtNetwork.Text = "";
        ddlRechargeType.ClearSelection();
        hdnNetworksID.Value = "";
    }
}