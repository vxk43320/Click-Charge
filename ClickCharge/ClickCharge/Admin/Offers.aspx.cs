using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Offers : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCategory();
            fillOffers();
        }
    }
    private void fillCategory()
    {
        string selectSQL;
        selectSQL = "SELECT CategoryID,CategoryName From Category";
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
            ddlCategory.DataSource = ds;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("", ""));
        }
    }
    private void fillOffers()
    {
        string selectSQL;
        selectSQL = "SELECT O.OfferID,O.CategoryID,O.Offer,O.Photo,C.CategoryName,case when ExpiryDate is not null then convert(varchar(10),O.ExpiryDate,103) else '' end as ExpiryDate From Offers O inner join Category C on O.CategoryID=C.CategoryID ";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Offers");
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
            if (hdnOfferID.Value == "")
            {
                if (FileUpload1.HasFile)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
                    if (!AllowedFileExtensions.Contains
         (FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'))))
                    {
                        lblSucess.Text = "";
                        lblErrorMsg.Text = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                    }
                    else if (FileUpload1.FileContent.Length > MaxContentLength)
                    {
                        lblSucess.Text = "";
                        lblErrorMsg.Text = "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                    }
                    else
                    {
                        var fileName = Path.GetFileName(FileUpload1.FileName);
                        var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                        FileUpload1.SaveAs(path);
                        insertSQL = "INSERT INTO Offers (CategoryID,Offer,Photo,CreatedBy,ExpiryDate) VALUES (";
                        insertSQL += "'" + ddlCategory.SelectedValue + "'";
                        insertSQL += ",'" + txtOffer.Text + "'";
                        insertSQL += ",'" + fileName + "'";
                        insertSQL += ",'" + Session["UserID"] + "','" + Convert.ToDateTime(txtExpiryDate.Text) + "')";
                    }
                }
                else
                {
                    lblSucess.Text = "";
                    lblErrorMsg.Text = "Please Upload Your file";
                }

            }
            else
            {
                string fileName = "";
                if (FileUpload1.HasFile)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png"};
                    if (!AllowedFileExtensions.Contains
         (FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'))))
                    {
                        lblSucess.Text = "";
                        lblErrorMsg.Text = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                    }
                    else if (FileUpload1.FileContent.Length > MaxContentLength)
                    {
                        lblSucess.Text = "";
                        lblErrorMsg.Text = "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                    }
                    else
                    {
                        fileName = Path.GetFileName(FileUpload1.FileName);
                        var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                        FileUpload1.SaveAs(path);
                    }
                }
                insertSQL = "Update Offers set ";
                insertSQL += "CategoryID='" + ddlCategory.SelectedValue + "'";
                insertSQL += ",Offer='" + txtOffer.Text + "'";
                insertSQL += ",ExpiryDate='" + Convert.ToDateTime(txtExpiryDate.Text) + "'";
                if (fileName!="")
                {
                    insertSQL += ",Photo='" + fileName  + "'";
                }
                insertSQL += " where OfferId='" + hdnOfferID.Value + "'";
            }
            if (insertSQL != "")
            {
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
                    fillOffers();
                    Clear();
                }
            }

        }
    }

    protected void gvSource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteOffers")
        {
            int OffersID;
            OffersID = Convert.ToInt32(e.CommandArgument);
            string deleteSQL = "DELETE FROM Offers";
            deleteSQL += " WHERE OfferID=" + OffersID;
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
                fillOffers();
            }
        }
        if (e.CommandName == "Modify")
        {
            hdnOfferID.Value = e.CommandArgument.ToString();
            string selectSQL;
            selectSQL = "SELECT CategoryID,Offer, case when ExpiryDate is not null then convert(varchar(10),ExpiryDate,103) else '' end as ExpiryDate FROM Offers ";
            selectSQL += "WHERE OfferID='" + hdnOfferID.Value + "'";
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
                    ddlCategory.SelectedValue = reader["CategoryID"].ToString();
                    txtOffer.Text = reader["Offer"].ToString();
                    txtExpiryDate.Text = reader["ExpiryDate"].ToString();
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
        txtOffer.Text = "";
        ddlCategory.ClearSelection();
        hdnOfferID.Value = "";
        txtExpiryDate.Text = "";
    }


    protected void txtExpiryDate_TextChanged(object sender, EventArgs e)
    {

    }
}