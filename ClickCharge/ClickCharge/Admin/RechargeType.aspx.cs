using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RechargeType : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRechargeType();
        }
    }
    private void fillRechargeType()
    {
        string selectSQL;
        selectSQL = "SELECT * From RechargeType";
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
            gvSource.DataSource = ds;
            gvSource.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string insertSQL = "";
        if (Page.IsValid)
        {
            if (hdnRechargeTypeID.Value == "")
            {
                insertSQL = "INSERT INTO RechargeType (RechargeType,CreatedBy) VALUES (";
                insertSQL += "'" + txtRechargeType.Text + "'";
                insertSQL += ",'" + Session["UserID"] + "')";
            }
            else
            {
                insertSQL = "Update RechargeType set ";
                insertSQL += "RechargeType='" + txtRechargeType.Text + "'";
                insertSQL += " where RechargeTypeId='" + hdnRechargeTypeID.Value + "'";
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
                fillRechargeType();
                Clear();
            }

        }
    }
    protected void vldCode_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (hdnRechargeTypeID.Value == "")
        {
            string selectSQL;
            string RechargeTypeName = "";
            selectSQL = "SELECT RechargeType FROM RechargeType ";
            selectSQL += "WHERE RechargeType='" + txtRechargeType.Text + "'";
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
                    RechargeTypeName = reader["RechargeType"].ToString();
                    reader.Close();
                }
                if (txtRechargeType.Text != RechargeTypeName && RechargeTypeName == "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                }
            }
            catch (Exception err)
            {
                e.IsValid = false;
                lblErrorMsg.Text = "";
                lblErrorMsg.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            e.IsValid = true;
        }
    }
    protected void gvSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRechargeType")
        {
            int RechargeTypeID;
            RechargeTypeID = Convert.ToInt32(e.CommandArgument);
            string deleteSQL = "DELETE FROM RechargeType";
            deleteSQL += " WHERE RechargeTypeID=" + RechargeTypeID;
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
                fillRechargeType();
            }
        }
        if (e.CommandName == "Modify")
        {
            hdnRechargeTypeID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT * FROM RechargeType ";
            selectSQL += "WHERE RechargeTypeID='" + hdnRechargeTypeID.Value + "'";
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
                    txtRechargeType.Text = reader["RechargeType"].ToString();
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
        txtRechargeType.Text = "";
        hdnRechargeTypeID.Value = "";
    }
}