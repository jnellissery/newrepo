<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width:100%">
    <div >
     <asp:Login ID="Login1" runat="server" CssClass="LoginControl"
        CreateUserText="Register"
        CreateUserUrl="~/Register.aspx"
        HelpPageText="Additional Help" HelpPageUrl="~/Help.aspx" 
        InstructionText="Please enter your user name and password for login." 
         onloginerror="Login1_LoginError"  Width="100%" Height="650px"  Font-Names="font-family:Arial" DestinationPageUrl="~/Main.aspx" >
         <LayoutTemplate>
            
                         <table style="margin-left:30%;margin-top:-20%" >
                             <tr>
                                 <td  style="text-align:center;font-size:large;"   colspan="2">Please enter your user name and password for login.</br></br></td>
                                 
                             </tr>
                             
                             <tr>
                                 <td align="right">
                                     <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="UserName" runat="server" style="width:150px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="Password" runat="server" TextMode="Password" style="width:150px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </br></br>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="2" style="text-align:left; padding-left:27%">
                                     <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time."  />
                                     </br></br></br>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align:center" colspan="2" style="color:Red;">
                                     <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                 </td>
                             </tr>
                             
                             <tr>
                                 <td style="text-align:right" colspan="2">
                                     <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="2">
                                     <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/CreateUser.aspx">Register</asp:HyperLink>
                                     <br />
                                     <asp:HyperLink ID="HelpLink" runat="server" NavigateUrl="~/Help.aspx">Additional Help</asp:HyperLink>
                                 </td>
                             </tr>
                         </table>
                   
         </LayoutTemplate>
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
