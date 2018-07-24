using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Offers : System.Web.UI.Page
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
        catch (Exception err)
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
        }
    }
    private void fillOffers()
    {
        string selectSQL;
        selectSQL = "SELECT O.Offer,O.Photo From Offers O where O.CategoryID='" + ddlCategory.SelectedValue + "' and convert(date, ExpiryDate,103) >=convert(date, getdate(),103)  ";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Offers");
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
            dtlst.DataSource = ds;
            dtlst.DataBind();
        }
    }


    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillOffers();
    }

}