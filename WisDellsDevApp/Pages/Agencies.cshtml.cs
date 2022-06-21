using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WisDellsDevApp.Pages
{
    public class AgenciesModel : PageModel
    {

        public readonly IConfiguration _configuration;
        public AgenciesModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<AgencyList> listAgencies = new List<AgencyList>();
        public void OnGet()
        {
            try
            {
                 string AdminConnString = _configuration.GetConnectionString("csAdmin");
                //string AdminConnString = "Data Source = DATASERVER\\SQL2017; Initial Catalog = Intranet; Persist Security Info = True; User ID = webuser; Password = info4web";
                //string AdminConnString = "Data Source = dataserver.wingis.local; Initial Catalog = admin; Persist Security Info = True; User ID = arcims; Password = gis4web";
                using (SqlConnection connection = new SqlConnection(AdminConnString))
                {
                    connection.Open();
                    string sql = "Select * From PickUpLog";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgencyList agency = new AgencyList();
                                agency.RecNo = reader.GetInt32(0);
                                agency.PickUpBy = reader.GetString(1);

                                listAgencies.Add(agency);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }

    public class AgencyList
    {
        public int RecNo { get; set; }
        public string PickUpBy { get; set; }

    }
}
