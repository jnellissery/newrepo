using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Login1_LoginError(object sender, EventArgs e)
    {
        if (ViewState["LoginErrors"] == null)
            ViewState["LoginErrors"] = 0;

        int ErrorCount = (int)ViewState["LoginErrors"] + 1;
        ViewState["LoginErrors"] = ErrorCount;

        if ((ErrorCount > 3) && (Login1.PasswordRecoveryUrl != string.Empty))
            Response.Redirect(Login1.PasswordRecoveryUrl);
    }
}