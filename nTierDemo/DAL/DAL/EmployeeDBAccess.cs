using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Web;
namespace DAL
{
    public class EmployeeDBAccess
    {
        public bool AddNewEmployee(Employee employee)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@Title", employee.Title),
                new SqlParameter("@Address", employee.Address),
                new SqlParameter("@City", employee.City),
                new SqlParameter("@State", employee.State),
                new SqlParameter("@PostalCode", employee.PostalCode),
                new SqlParameter("@Country", employee.Country),
                new SqlParameter("@Extension", employee.Extension),
                new SqlParameter("@DOB",employee.DOB),
                 new SqlParameter("@Photo",employee.Photo)
            };

            return SqlDBHelper.ExecuteNonQuery("AddNewEmployee", CommandType.StoredProcedure, parameters);
        }

        public bool UpdateEmployee(Employee employee)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employee.EmployeeID),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@Title", employee.Title),
                new SqlParameter("@Address", employee.Address),
                new SqlParameter("@City", employee.City),
                new SqlParameter("@State", employee.State),
                new SqlParameter("@PostalCode", employee.PostalCode),
                new SqlParameter("@Country", employee.Country),
                new SqlParameter("@Extension", employee.Extension),
                new SqlParameter("@DOB",employee.DOB),
                new SqlParameter("@Photo",employee.Photo)
            };

            return SqlDBHelper.ExecuteNonQuery("UpdateEmployee", CommandType.StoredProcedure, parameters);
        }

        public bool DeleteEmployee(int empID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@empId", empID)
            };

            return SqlDBHelper.ExecuteNonQuery("DeleteEmployee", CommandType.StoredProcedure, parameters);
        }

        public Employee GetEmployeeDetails(int empID)
        {
            Employee employee = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@empId", empID)
            };
            //Lets get the list of all employees in a datataable
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("GetEmployeeDetails", CommandType.StoredProcedure, parameters))
            {
                //check if any record exist or not
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    //Lets go ahead and create the list of employees
                    employee = new Employee();

                    //Now lets populate the employee details into the list of employees                                           
                    employee.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                    employee.LastName = row["LastName"].ToString();
                    employee.FirstName = row["FirstName"].ToString();
                    employee.Title = row["Title"].ToString();
                    employee.Address = row["Address"].ToString();
                    employee.City = row["City"].ToString();
                    employee.State = row["Country"].ToString();
                    employee.PostalCode = row["PostalCode"].ToString();
                    employee.Country = row["Country"].ToString();
                    employee.Extension = row["Extension"].ToString();
                    employee.DOB = Convert.ToDateTime(row["DOB"]);
                    if (!DBNull.Value.Equals(row["Photo"])) 
                    employee.Photo= (Byte[]) row["Photo"];
                    employee.Row_num = Convert.ToInt32(row["Row_num"]);
                    
                }
            }

            return employee;
        }

        public List<Employee> GetEmployeeList(string status = "", string selectedrow="")
        {
            List<Employee> listEmployees = null;

            //Lets get the list of all employees in a datataable
            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("GetEmployeeList", CommandType.StoredProcedure))
            {
                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    //Lets go ahead and create the list of employees
                    listEmployees = new List<Employee>();

                    //Now lets populate the employee details into the list of employees
                    XDocument doc = new XDocument(XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("Country.xml")));
                //    var result1 = ((from t in doc.Descendants("countrys").Elements("country") where t.Attribute("CountryID").Value == CountryID.ToString() select t).ToList()); 

                    foreach (DataRow row in table.Rows)
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                        employee.LastName = row["LastName"].ToString();
                        employee.FirstName = row["FirstName"].ToString();
                        employee.Title = row["Title"].ToString();
                        employee.Address = row["Address"].ToString();
                        employee.City = row["City"].ToString();
                      
                        employee.PostalCode = row["PostalCode"].ToString();
                        var main = ((from t in doc.Descendants("countrys").Elements("country") where t.Attribute("CountryID").Value == row["Country"].ToString() select t)).ToList();
                        if (status == "edit" && table.Rows.IndexOf(row) ==  Convert.ToInt32( selectedrow))
                        {
                            employee.Country = row["Country"].ToString();//result1[0].CountryName.ToString();
                            employee.State = row["State"].ToString(); //result[0].StateName.ToString();

                           
                        }
                        else
                        {
                            var result1 = ((from t in doc.Descendants("countrys").Elements("country") where t.Attribute("CountryID").Value == row["Country"].ToString() select new { CountryName = t.Attribute("CountryName").Value }).ToList());
                            employee.Country = result1[0].CountryName.ToString();
                            var result = (from t in main.Elements("state")
                                          where t.Attribute("StateID").Value == row["State"].ToString()
                                          select new
                                          {
                                              StateName = t.Attribute("StateName").Value

                                          }).ToList();
                            employee.State = result[0].StateName.ToString();
                        }
                    
                        employee.Extension = row["Extension"].ToString();
                        employee.DOB = Convert.ToDateTime( row["DOB"]);
                        if (!DBNull.Value.Equals(row["Photo"])) 
                        employee.Photo = (Byte[])row["Photo"];
                        employee.Row_num =  Convert.ToInt32(row["Row_num"]);
                        listEmployees.Add(employee);
                    } 
                }
            }

            return listEmployees;
        }        
    }
}
