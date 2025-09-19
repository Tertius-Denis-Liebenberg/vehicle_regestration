using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace vehicle_regestration.Pages
{
    public partial class Contractor : System.Web.UI.Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["SizananiDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContractors();
            }
        }

        private void BindContractors()
        {
            ddlContractors.Items.Clear();
            ddlContractors.Items.Add(new ListItem("-- Select Contractor --", ""));

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_CONTRACTORS_READ", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ddlContractors.Items.Add(new ListItem(
                        reader["CONTRACTOR_NAME"].ToString(),
                        reader["ENTRY_ID"].ToString()
                    ));
                }
            }
        }

        protected void ddlContractors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlContractors.SelectedValue))
            {
                BindVehicles(Convert.ToInt32(ddlContractors.SelectedValue));
            }
        }

        private void BindVehicles(int contractorId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_CONTRACTOR_VEHICLE_READ", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CONTRACTOR_ID", contractorId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            gvVehicles.DataSource = dt;
            gvVehicles.DataBind();
        }

        protected void btnSaveContractor_Click(object sender, EventArgs e)
        {
            string contractorName = txtContractorName.Value.Trim();
            string contractorPhone = txtContractorNumber.Value.Trim();
            string contractorEmail = txtContractorEmail.Value.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_CONTRACTOR_INSERT", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", contractorName);
                cmd.Parameters.AddWithValue("@PHONE_NUMBER", contractorPhone);
                cmd.Parameters.AddWithValue("@EMAIL", string.IsNullOrEmpty(contractorEmail) ? (object)DBNull.Value : contractorEmail);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            txtContractorName.Value = "";
            txtContractorNumber.Value = "";
            txtContractorEmail.Value = "";

            BindContractors();
        }
    }
}
