using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Issues : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillIssues();
        }
    }
    private void fillIssues()
    {
        string selectSQL;
        selectSQL = "SELECT I.IssueID,I.Issue,I.Answer,A.UserName as AnswerBy From Issues I left outer join Admin A on I.AnswerBy=A.AdminID where I.UserID=" + Session["UserID"] + "";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Issues");
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

            insertSQL = "INSERT INTO Issues (Issue,UserID) VALUES (";
            insertSQL += "'" + txtIssue.Text + "'";
            insertSQL += ",'" + Session["UserID"] + "')";

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
                fillIssues();
                Clear();
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
    }
}