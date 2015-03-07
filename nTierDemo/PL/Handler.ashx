<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";

        if (context.Request.QueryString["ImgId"] != null)
        {
            int imgId = 0;
            imgId = Convert.ToInt16(context.Request.QueryString["imgId"]);
           //System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(GetImageFromDB(imgId), false);
        //  System.Drawing.Image imgFromGB = System.Drawing.Image.FromStream(memoryStream);
            //imgFromGB.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    private byte[] GetImageFromDB(Byte[] image)
    {

        return image;
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}