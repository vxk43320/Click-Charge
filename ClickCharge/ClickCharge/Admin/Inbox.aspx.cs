using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Inbox : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillIssue();
        }
    }
    private void fillIssue()
    {
        string selectSQL;
        selectSQL = "SELECT * From Issues";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Issue");
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
            if (hdnIssueID.Value == "")
            {
            }
            else
            {
                insertSQL = "Update Issues set ";
                insertSQL += "Answer='" + txtAnswer.Text + "'";
                insertSQL += ",AnswerBy='" + Session["UserID"] + "'";
                insertSQL += " where IssueId='" + hdnIssueID.Value + "'";
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
                fillIssue();
                Clear();
            }

        }
    }
    
    protected void gvSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Answer")
        {
            hdnIssueID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT * FROM Issues ";
            selectSQL += "WHERE IssueID='" + hdnIssueID.Value + "'";
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
                    txtIssue.Text = reader["Issue"].ToString();
                    reader.Close();
                    pnlissue.Visible = true;
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
        txtIssue.Text = "";
        txtAnswer.Text = "";
        hdnIssueID.Value = "";
        pnlissue.Visible = false;
    }
    protected void gvSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAnswer = (Label)e.Row.FindControl("lblAnswer");
            Button btnModify = (Button)e.Row.FindControl("btnModify");
            if (lblAnswer.Text != "")
            {
                btnModify.Visible = false;
            }
            else
            {
                btnModify.Visible = true;
            }
        }
    }
}