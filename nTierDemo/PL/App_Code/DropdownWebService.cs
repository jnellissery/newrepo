using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.Specialized;
using AjaxControlToolkit;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Linq;


/// <summary>
/// Summary description for DropdownWebService
/// </summary>

using System.Xml.Linq;[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]

public class DropdownWebService : System.Web.Services.WebService
{

    public AuthSoapHd1 spAuthenticationHeader;
    [WebMethod, SoapHeader("spAuthenticationHeader")]
    public CascadingDropDownNameValue[] BindCountrydropdown(string knownCategoryValues, string category)
    {
        //SqlConnection concountry = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        //concountry.Open();
        //SqlCommand cmdcountry = new SqlCommand("select * from Country", concountry);
        //SqlDataAdapter dacountry = new SqlDataAdapter(cmdcountry);
        //cmdcountry.ExecuteNonQuery();
        //DataSet dscountry = new DataSet();
        //dacountry.Fill(dscountry);
        //concountry.Close();


       // XDocument doc = new XDocument(Server.MapPath("Country.xml"));

        
        XDocument doc = new XDocument(XDocument.Load(Server.MapPath("Country.xml")));

        var result = from t in doc.Descendants("countrys").Elements("country")
                                     select new Country
                                     {
                                         CountryName = t.Attribute("CountryName").Value,
                                         CountryID = t.Attribute("CountryID").Value
                                     };
        List<CascadingDropDownNameValue> countrydetails = new List<CascadingDropDownNameValue>();
        foreach (var obj1 in result)
        {


           string CountryID = obj1.CountryID;
            string CountryName = obj1.CountryName.ToString();
            countrydetails.Add(new CascadingDropDownNameValue(CountryName, CountryID));
        }
      //  DropDownListCountry.DataTextField = "Name";
      //  DropDownListCountry.DataValueField = "Name";
      //  DropDownListCountry.DataBind();
      //  DropDownListCountry.Items.Insert(0, new ListItem(" Select ", "0"));

        
       // foreach (XElement mainLoop in  obj)
        {
            //string CountryID = dtrow["CountryID"].ToString();
           // string CountryName = dtrow["CountryName"].ToString();
          //  countrydetails.Add(new CascadingDropDownNameValue(CountryName, CountryID));
        }
        return countrydetails.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] BindStatedropdown(string knownCategoryValues, string category)
    {
        int CountryID;
        StringDictionary countrydetails = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        CountryID = Convert.ToInt32(countrydetails["Country"]);
       /* SqlConnection constate = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        constate.Open();
        SqlCommand cmdstate = new SqlCommand("select * from State where CountryID=@CountryID", constate);
        cmdstate.Parameters.AddWithValue("@CountryID", CountryID);
        cmdstate.ExecuteNonQuery();
        SqlDataAdapter dastate = new SqlDataAdapter(cmdstate);
        DataSet dsstate = new DataSet();
        dastate.Fill(dsstate);
        constate.Close();*/
        XDocument doc = new XDocument(XDocument.Load(Server.MapPath("Country.xml")));
        var result1 = ((from t in doc.Descendants("countrys").Elements("country") where t.Attribute("CountryID").Value == CountryID.ToString() select t).ToList()); 
                      
         var result = from t in result1.Elements("state")  select new State
                    {
                         StateName = t.Attribute("StateName").Value,
                         StateID = t.Attribute("StateID").Value
                     }; 
        List<CascadingDropDownNameValue> statedetails = new List<CascadingDropDownNameValue>();
       foreach (var obj1 in result)
        {
            string StateID = obj1.StateID;
            string StateName = obj1.StateName.ToString();
            statedetails.Add(new CascadingDropDownNameValue(StateName, StateID));
        }
        return statedetails.ToArray();
    }
    [WebMethod]
    public CascadingDropDownNameValue[] BindRegiondropdown(string knownCategoryValues, string category)
    {
        int stateID;
        StringDictionary statedetails = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        stateID = Convert.ToInt32(statedetails["State"]);
        SqlConnection conregion = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        conregion.Open();
        SqlCommand cmdregion = new SqlCommand("Select * from Region where StateID=@StateID", conregion);
        cmdregion.Parameters.AddWithValue("@StateID", stateID);
        cmdregion.ExecuteNonQuery();
        SqlDataAdapter daregion = new SqlDataAdapter(cmdregion);
        DataSet dsregion = new DataSet();
        daregion.Fill(dsregion);
        conregion.Close();
        List<CascadingDropDownNameValue> regiondetails = new List<CascadingDropDownNameValue>();
        foreach (DataRow dtregionrow in dsregion.Tables[0].Rows)
        {
            string regionID = dtregionrow["RegionID"].ToString();
            string regionname = dtregionrow["RegionName"].ToString();
            regiondetails.Add(new CascadingDropDownNameValue(regionname, regionID));

        }
        return regiondetails.ToArray();
    }
}
public class Country
{
    public string CountryName { get; set; }
    public string CountryID { get; set; }
}

public class State
{
    public string StateName { get; set; }
    public string StateID { get; set; }
}
public class AuthSoapHd1 : SoapHeader
{
    public string strUserName;
    public string strPassword;
}