using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FormWebApp.Webforms
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);

                connection.Open();
                string query = "select count(*) from UserDB where UserName='"+TextBoxName.Text+"'";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                int temp = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
                if (temp == 1)
                {
                    Response.Write("User already Exist");
                }
                connection.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);

                connection.Open();
                string query = "insert into UserDB (UserName,Email,Password,Country) values (@UserName, @Email, @Password, @Country)";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@UserName", TextBoxName.Text);
                sqlCommand.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                sqlCommand.Parameters.AddWithValue("@Password", TextBoxPass.Text);
                sqlCommand.Parameters.AddWithValue("@Country", DropDownListCountry.SelectedItem.ToString());

                sqlCommand.ExecuteNonQuery();
                Response.Redirect("Manager.aspx");
                Response.Write("Registration Successful");
                connection.Close();
                //Response.Write("Registration is Successful");
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
            }
        }
    }
}