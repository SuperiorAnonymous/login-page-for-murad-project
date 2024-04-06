using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace login_page_for_murad_project.Models
{
    public class DbConext
    {
      
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);

        public bool Add(UsersModels um)
        {
            string str = "insert into users values('" + um.Email + "','" + um.Password + "','" + um.Name + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int a = cmd.ExecuteNonQuery();
            if(a >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(UsersModels um)
        {
            string str = "update users set email='"+um.Email+"',password='"+um.Password+"',name='"+um.Name + "' where id='" + um.Id + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<UsersModels> GetUsers() 
        {
            List<UsersModels> lst = new List<UsersModels>();

            SqlCommand cmd = new SqlCommand("select * from users ", con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new UsersModels
                {
                    Id = Convert.ToInt32(dr[0]),
                    Email = Convert.ToString(dr[1]),
                    Password = Convert.ToString(dr[2]),
                    Name = Convert.ToString(dr[3])
                });
            }
            return lst;
        }
        public List<UsersModels> GetById(string email)
        {
            List<UsersModels> lst = new List<UsersModels>();

            SqlCommand cmd = new SqlCommand("select * from users where email='" + email + "'", con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new UsersModels
                {
                    Id = Convert.ToInt32(dr[0]),
                    Email = Convert.ToString(dr[1]),
                    Password = Convert.ToString(dr[2]),
                    Name = Convert.ToString(dr[3])
                });
            }
            return lst;
        }
    }
}