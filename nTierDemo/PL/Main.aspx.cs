using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
public partial class Main : System.Web.UI.Page
{
    EmployeeHandler empHandler = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;

        empHandler = new EmployeeHandler();
        if (IsPostBack == false)
        {
            BindData();
        }
    }

    private void BindData(string status = "", string selectedrow = "")
    {
        
            GridView1.DataSource = empHandler.GetEmployeeList(status, selectedrow);
            GridView1.DataBind();
       
        //List<Employee> SortedList=null;
        //if (ViewState["SortExpr"] != null)
        //{
        //    string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
        //    List<Employee> list = (List<Employee>)GridView1.DataSource;

        //    if (SortOrder[1] == "ASC")
        //    {
        //        SortedList = list.OrderBy(o => o.LastName).ToList();
        //    }
        //    else
        //    {
        //        SortedList = list.OrderByDescending(o => o.LastName).ToList();
        //    }
        //    GridView1.DataSource = SortedList;
        //    GridView1.DataBind();
        //}
        //else
        //{

        //}

       
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
      int  Row_num = (int)((System.Web.UI.WebControls.GridView)(sender)).DataKeys[e.NewEditIndex].Value;
      BindData("edit", Convert.ToString(Row_num-1));
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        
    {
       
        Label lblID = GridView1.Rows[e.RowIndex].FindControl("lblID") as Label;

        TextBox txtLastName = GridView1.Rows[e.RowIndex].FindControl("txtLastName") as TextBox;
        TextBox txtFirstName = GridView1.Rows[e.RowIndex].FindControl("txtFirstName") as TextBox;
        TextBox txtTitle = GridView1.Rows[e.RowIndex].FindControl("txtTitle") as TextBox;
        TextBox txtAddress = GridView1.Rows[e.RowIndex].FindControl("txtAddress") as TextBox;
        TextBox txtCity = GridView1.Rows[e.RowIndex].FindControl("txtCity") as TextBox;
        DropDownList ddlState = GridView1.Rows[e.RowIndex].FindControl("ddlState") as DropDownList;
        TextBox txtPostalCode = GridView1.Rows[e.RowIndex].FindControl("txtPostalCode") as TextBox;
        DropDownList ddlCounttry = GridView1.Rows[e.RowIndex].FindControl("ddlCountry") as DropDownList;
        TextBox txtExtension = GridView1.Rows[e.RowIndex].FindControl("txtExtension") as TextBox;
        TextBox txtDOB = GridView1.Rows[e.RowIndex].FindControl("txtDOB") as TextBox;

        if (lblID != null && txtLastName != null && txtFirstName != null && txtTitle != null &&
            txtAddress != null && txtCity != null && ddlState != null &&
            txtPostalCode != null && ddlCounttry != null && txtExtension != null)
        {
            Employee employee = new Employee();

            employee.EmployeeID = Convert.ToInt32(lblID.Text.Trim());
            employee.LastName = txtLastName.Text;
            employee.FirstName = txtFirstName.Text;
            employee.Title = txtTitle.Text;
            employee.Address = txtAddress.Text;
            employee.City = txtCity.Text;
            employee.State = ddlState.SelectedValue;
            employee.PostalCode = txtPostalCode.Text;
            employee.Country = ddlCounttry.SelectedValue;
            employee.Extension = txtExtension.Text;
            employee.DOB = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", null); //Convert.ToDateTime();

            //Let us now update the database
            if (empHandler.UpdateEmployee(employee) == true)
            {
                lblResult.Text = "Record Updated Successfully";
            }
            else
            {
                lblResult.Text = "Failed to Update record";
            }

            //end the editing and bind with updated records.
            GridView1.EditIndex = -1;
            BindData();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEmployee.aspx");
    }

    public localhost.DropdownWebService set_autho_webservice()
    {
        localhost.DropdownWebService objWebService = new localhost.DropdownWebService();
        localhost.AuthSoapHd1 objAuthSoapHeader = new localhost.AuthSoapHd1();

        string strUsrName = ConfigurationManager.AppSettings["UserName"];
        string strPassword = ConfigurationManager.AppSettings["Password"];

        objAuthSoapHeader.strUserName = strUsrName;
        objAuthSoapHeader.strPassword = strPassword;

        objWebService.AuthSoapHd1Value = objAuthSoapHeader;
        return objWebService;
        //string str = objWebService.HelloWorld();

        //DataSet dsData = objWebService.SensitiveData();
        //Response.Write(str);
        // GridView1.DataSource = dsData;
        //GridView1.DataBind();	
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string ImageID = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
            System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("rowimage");
            byte[] int1;

            int1 = ((DAL.Employee)(e.Row.DataItem)).Photo;
            if (int1 != null)
                UsrImages.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String((Byte[])((DAL.Employee)(e.Row.DataItem)).Photo);  //"DisplayImage.ashx?ImgId=" + ImageID;
        }
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
        Response.RedirectPermanent("DeleteEmployee.aspx?id=" + id,true);
    }
}