using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace vehicle_regestration.Pages
{
    public partial class frmSummary : System.Web.UI.Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["SizananiDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSummary();
            }
        }

        private void BindSummary()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("USP_CONTRACTOR_VEHICLE_COUNT_READ", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            gvSummary.DataSource = dt;
            gvSummary.DataBind();
        }
    }
}
