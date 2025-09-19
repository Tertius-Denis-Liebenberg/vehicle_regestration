using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vehicle_regestration.Pages
{
    public partial class frmVehicles : System.Web.UI.Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["SizananiDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContractors();
                BindVehicles();
            }
        }

        private void BindContractors()
        {
            ddlContractor.Items.Clear();
            ddlContractor.Items.Add(new ListItem("-- Select Contractor --", ""));

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_CONTRACTORS_READ", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ddlContractor.Items.Add(new ListItem(reader["CONTRACTOR_NAME"].ToString(),
                        reader["ENTRY_ID"].ToString()));
                }
            }
        }

        private void BindVehicles()
        {
            ClearPopup();

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_VEHICLES_READ", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            gvVehicles.DataSource = dt;
            gvVehicles.DataBind();
        }

        private void ClearPopup()
        {
            txtType.Value = string.Empty;
            txtRegNo.Value = string.Empty;
            txtModel.Value = string.Empty;
            txtWeight.Value = string.Empty;
            ddlContractor.SelectedIndex = 0;
        }

        protected void btnSaveVehicle_Click(object sender, EventArgs e)
        {
            int contractorId = string.IsNullOrEmpty(ddlContractor.SelectedValue) ? 0 : Convert.ToInt32(ddlContractor.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(hfVehicleID.Value))
                {
                    cmd.CommandText = "USP_VEHICLE_INSERT";
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandText = "USP_VEHICLE_UPDATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ENTRY_ID", Convert.ToInt32(hfVehicleID.Value));
                }

                cmd.Parameters.AddWithValue("@TYPE", txtType.Value.Trim());
                cmd.Parameters.AddWithValue("@REGESTRATION_NUMBER", txtRegNo.Value.Trim());
                cmd.Parameters.AddWithValue("@MODEL", txtModel.Value.Trim());
                cmd.Parameters.AddWithValue("@WEIGHT", Convert.ToDecimal(txtWeight.Value));
                cmd.Parameters.AddWithValue("@CONTRACTOR_ID", contractorId > 0 ? (object)contractorId : DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindVehicles();
        }

        protected void gvVehicles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int vehicleID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteVehicle")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("USP_VEHICLE_DELETE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ENTRY_ID", vehicleID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindVehicles();
            }
            else if (e.CommandName == "EditVehicle")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("USP_VEHICLES_READ", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VEHICLE_ID", vehicleID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        hfVehicleID.Value = vehicleID.ToString();
                        txtType.Value = reader["TYPE"].ToString();
                        txtRegNo.Value = reader["REGESTRATION_NUMBER"].ToString();
                        txtModel.Value = reader["MODEL"].ToString();
                        txtWeight.Value = reader["WEIGHT"].ToString();
                        ddlContractor.SelectedValue = reader["CONTRACTOR_ID"].ToString();
                    }
                }

                // Open modal via ScriptManager
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal",
                    "var myModal = new bootstrap.Modal(document.getElementById('vehicleModal')); myModal.show();", true);
            }
        }
    }
}
