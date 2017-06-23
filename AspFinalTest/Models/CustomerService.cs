using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspFinalTest.Models
{
    public class CustomerService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        public List<Customer> GetCustomer(Customer selectitem)
        {
            DataTable dt = new DataTable();

            string sql = @"Select CustomerID,CompanyName,ContactName,ContactTitle
                            from Sales.Customers
                            where (CustomerID LIKE @CustomerID OR @CustomerID = '')
                            AND (CompanyName LIKE '%'+@CompanyName+'%' OR @CompanyName = '') AND (ContactName LIKE '%'+@ContactName+'%' OR @ContactName = '') AND (ContactTitle LIKE '%'+@ContactTitle+'%' OR @ContactTitle = '')";
            
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", selectitem.CustomerID == null ? string.Empty : selectitem.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", selectitem.CompanyName == null ? string.Empty : "%" + selectitem.CompanyName + "%"));
                cmd.Parameters.Add(new SqlParameter("@ContactName", selectitem.ContactName == null ? string.Empty : "%" + selectitem.CompanyName + "%"));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", selectitem.ContactTitle == null ? string.Empty : selectitem.ContactTitle));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
                  
            return this.MapCustomerDataToList(dt);
        }

        private List<Customer> MapCustomerDataToList(DataTable customerData)
        {
            List<Customer> result = new List<Customer>();
            foreach (DataRow row in customerData.Rows)
            {
                result.Add(new Customer()
                {
                    CustomerID = row["CustomerID"].ToString(),
                    CompanyName = row["CompanyName"].ToString(),
                    ContactName = row["ContactName"].ToString(),
                    ContactTitle= row["ContactTitle"].ToString(),
                });
            }
            return result;
        }

        
    }
}