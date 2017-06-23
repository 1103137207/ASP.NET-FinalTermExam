using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspFinalTest.Models
{
    public class CodeService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        public List<Code> GetTitle()
        {
            DataTable result = new DataTable();
            string sql = @"select CodeId,CodeVal from dbo.CodeTable where CodeType='Title' ";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapGetTitle(result);
        }

        private List<Code> MapGetTitle(DataTable orderData)
        {
            List<Code> result = new List<Code>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Code()
                {
                    CodeId = row["CodeId"].ToString(),
                    CodeVal = row["CodeVal"].ToString()
                });
            }
            return result;

        }
    }
}