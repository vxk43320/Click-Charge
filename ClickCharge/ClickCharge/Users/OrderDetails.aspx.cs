using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_OrderDetails : System.Web.UI.Page
{
    String connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\ClickCharge.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            fillRechargeType();
            fillMyOrders();
            getWalletAmount();
        }
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

                lblWalletBalance.Text = dr["Amount"].ToString();
            }
            else
            {
                lblWalletBalance.Text = "0";
            }
        }
        else
        {
            lblWalletBalance.Text = "0";
        }
        dr.Close();
        con.Close();
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
        }
    }

    private void fillMyOrders()
    {
        string selectSQL;
        selectSQL = "SELECT O.OrderID,N.NetworkName,O.RechargeNo,O.Amount,(CONVERT(CHAR(10), O.Date, 103) + ' ' +FORMAT(O.Date, 'hh:mm:ss tt')) as Date From Orders O left outer join Networks N on O.NetworksID=N.NetworksID and O.RechargeTypeID=N.RechargeTypeID where O.UserID=" + Session["UserID"] + " and O.RechargeTypeID='" + ddlRechargeType.SelectedValue + "'";
        if (ddlOptions.SelectedValue =="1")
        {
            selectSQL = selectSQL + " and O.Date  > Dateadd(day,-30,getdate())";
            //selectSQL = " SELECT O.Date <Dateadd(day,-30,getdate())";

        }
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "MyOrders");
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
    protected void ddlRechargeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMyOrders();
    }
    protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMyOrders();
    }
    protected void gvSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlRechargeType.SelectedValue =="4" || ddlRechargeType.SelectedValue =="5")
            {
                e.Row.Cells[2].Visible = false;
            }
            else
            {
                e.Row.Cells[2].Visible = true ;
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlRechargeType.SelectedValue == "4" || ddlRechargeType.SelectedValue == "5")
            {
                e.Row.Cells[2].Visible = false;
            }
            else
            {
                e.Row.Cells[2].Visible = true;
            }
        }
    }
}