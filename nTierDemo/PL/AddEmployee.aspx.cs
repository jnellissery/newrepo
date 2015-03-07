using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DAL;
using BLL;

public partial class AddEmployee : System.Web.UI.Page
{
    // thsi is commited on 03/07/2015
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnYES_Click(object sender, EventArgs e)
    {
        Employee emp = new Employee();
      
        
        emp.LastName = txtLName.Text;
        emp.FirstName = txtFName.Text;
        emp.Address = txtAddress.Text;
        emp.City = ddlCountry.SelectedValue;
        emp.Country = ddlCountry.SelectedValue;
        emp.State = ddlState.SelectedValue;
        emp.PostalCode = txtCode.Text;
        emp.Extension = txtExtension.Text;
        emp.Title = txtTitle.Text;
        emp.DOB = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", null);
        string ImageName = string.Empty;
        byte[] Image = null;
        if (ImageUploadToDB.PostedFile != null && ImageUploadToDB.PostedFile.FileName != "")
        {
            ImageName = Path.GetFileName(ImageUploadToDB.FileName);
            Image = new byte[ImageUploadToDB.PostedFile.ContentLength];
            HttpPostedFile UploadedImage = ImageUploadToDB.PostedFile;
            UploadedImage.InputStream.Read(Image, 0, (int)ImageUploadToDB.PostedFile.ContentLength);
        }
        emp.Photo = Image;
   
        EmployeeHandler empHandler = new EmployeeHandler();


        if (empHandler.AddNewEmployee(emp) == true)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnNO_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
       
         
    }
    protected void AsyncFileUploadLogo_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        string ImageName = string.Empty;
        byte[] Image = null;
        if (ImageUploadToDB.PostedFile != null && ImageUploadToDB.PostedFile.FileName != "")
        {
            ImageName = Path.GetFileName(ImageUploadToDB.FileName);
            Image = new byte[ImageUploadToDB.PostedFile.ContentLength];
            HttpPostedFile UploadedImage = ImageUploadToDB.PostedFile;
            UploadedImage.InputStream.Read(Image, 0, (int)ImageUploadToDB.PostedFile.ContentLength);
        }
        Response.Buffer = true;
        Response.Clear();

        img.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(Image);
      
    }
}
