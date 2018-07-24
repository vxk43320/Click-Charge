using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Category : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCategory();
        }
    }
    private void fillCategory()
    {
        string selectSQL;
        selectSQL = "SELECT * From Category";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Category");
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
            if (hdnCategoryID.Value == "")
            {
                insertSQL = "INSERT INTO Category (CategoryName,CreatedBy) VALUES (";
                insertSQL += "'" + txtCategory.Text + "'";
                insertSQL += ",'" + Session["UserID"] + "')";
            }
            else
            {
                insertSQL = "Update Category set ";
                insertSQL += "CategoryName='" + txtCategory.Text + "'";
                insertSQL += " where CategoryId='" + hdnCategoryID.Value + "'";
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
                fillCategory();
                Clear();
            }

        }
    }
    protected void vldCode_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (hdnCategoryID.Value == "")
        {
            string selectSQL;
            string CategoryName = "";
            selectSQL = "SELECT CategoryName FROM Category ";
            selectSQL += "WHERE CategoryName='" + txtCategory.Text + "'";
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
                    CategoryName = reader["CategoryName"].ToString();
                    reader.Close();
                }
                if (txtCategory.Text != CategoryName && CategoryName == "")
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
        if (e.CommandName == "DeleteCategory")
        {
            int CategoryID;
            CategoryID = Convert.ToInt32(e.CommandArgument);
            string deleteSQL = "DELETE FROM Category";
            deleteSQL += " WHERE CategoryID=" + CategoryID;
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
                fillCategory();
            }
        }
        if (e.CommandName == "Modify")
        {
            hdnCategoryID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT * FROM Category ";
            selectSQL += "WHERE CategoryID='" + hdnCategoryID.Value + "'";
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
                    txtCategory.Text = reader["CategoryName"].ToString();
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
        txtCategory.Text = "";
        hdnCategoryID.Value = "";
    }
}