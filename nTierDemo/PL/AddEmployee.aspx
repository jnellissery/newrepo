<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEmployee.aspx.cs" Inherits="AddEmployee" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
        <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.validate/1.5.5/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize validation on the entire ASP.NET form
            $("#form1").validate({
                onsubmit: false
            });

            $("#btnYES").click(function (evt) {
                var isValid = $("#form1").valid();
                if (!isValid)
                    evt.preventDefault();
            });
        });
     
       
  </script>
    <style type="text/css">
        
label {
  clear: both;
  float: left;
  line-height: 24px;
  margin-top: 10px;
  padding-right: 10px;
  text-align: right;
  vertical-align: middle;
  width: 125px;
}

label.error {
  clear: none;
  color: Red;
  float: left;
  padding-left: 10px;
  white-space: nowrap;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
   <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>

    <div>
        <strong>Add a New Employee<br />
        </strong>
        <br />
        <table style="width: 320px">
            <tr>
                <td>
                    LastName</td>
                <td>
                    <asp:TextBox ID="txtLName" runat="server" CssClass="required" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    FirstName</td>
                <td>
                    <asp:TextBox ID="txtFName" runat="server" CssClass="required"  ></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Title</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Address</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="required" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    City</td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" Height="16px" Width="127px"  CssClass="required"  >
                    </asp:DropDownList>
                    <ajax:CascadingDropDown ID="CascadingDropDown1" runat="server" Category="Country"  TargetControlID="ddlCountry" LoadingText="Loading Countries..." PromptText="Select Country" ServiceMethod="BindCountrydropdown" ServicePath="DropdownWebService.asmx"  SelectedValue='<%# Bind("Country") %>'>
                    </ajax:CascadingDropDown>
                </td>
            </tr>
            <tr>
                <td>
                    State</td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" Height="17px" Width="122px">
                    </asp:DropDownList>
                    <ajax:CascadingDropDown ID="StateCascading" runat="server" Category="State" TargetControlID="ddlState" ParentControlID="ddlCountry" LoadingText="Loading States..." PromptText="Select State" ServiceMethod="BindStatedropdown" ServicePath="DropdownWebService.asmx"  SelectedValue='<%# Bind("State")%>'>
                    </ajax:CascadingDropDown>
                </td>
            </tr>
            <tr>
                <td>
                    PostalCode</td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Country</td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Extension</td>
                <td>
                    <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox></td>
            </tr>
             <tr>
                <td>
                    DOB</td>
                <td>
                    <asp:TextBox ID="txtDOB" runat="server"  CssClass="required"  ></asp:TextBox></td>
                   <Ajax:CalendarExtender ID="CalendarExtender99" ClientIDMode="AutoID" Format="dd/MM/yyyy" TargetControlID="txtDOB" runat="server" >
                        </Ajax:CalendarExtender>
            </tr>
            <tr>
                <td>
                    Photo</td>
                <td>
                      <ajax:AsyncFileUpload ID="ImageUploadToDB" OnClientUploadComplete="uploadComplete" runat="server"
                                Width="300px" UploaderStyle="Modern" UploadingBackColor="#CCFFFF"  OnUploadedComplete="AsyncFileUploadLogo_UploadedComplete"  />
                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server" />
                     
                    <asp:Image ID="img" runat="server"  Width="50px" Height="50px" />
                    </td>

            </tr>
        </table>
       
        Are you sure you want to delete this employee record: &nbsp; &nbsp;
        <asp:Button ID="btnYES" runat="server" OnClick="btnYES_Click" Text="Submit" />
        &nbsp; &nbsp;
        <asp:Button ID="btnNO" runat="server" OnClick="btnNO_Click" Text="Cancel" /></div>
    </form>
</body>
</html>
<script type="text/javascript">
    function uploadComplete(sender, args) {

        try {
            var fileExtension = args.get_fileName();
            var gif = fileExtension.indexOf('.gif');
            var png = fileExtension.indexOf('.png');
            var jpg = fileExtension.indexOf('.jpg');
            var jpeg = fileExtension.indexOf('.jpeg');
            if (gif > 0 || png > 0 || jpg > 0 || jpeg > 0)
            {
                $get("<%=lblMsg.ClientID%>").innerHTML = "";
                
                   return;
               }
               else {
                   $get("<%=lblMsg.ClientID%>").innerHTML = "Valid File Extension allowed are {.gif,.png,jpg,.jpeg}";
                   return;
               }



           }
           catch (e) {
               alert(e.message);
           }
       }
</script>