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
                //string AdminConnString = "Data Source = dataserver.wingis.local; Initial Catalog = admin; Persist Security Info = True; User ID = arcims; Password = gis4web";
                using (SqlConnection connection = new SqlConnection(AdminConnString))
                {
                    connection.Open();
                    string sql = "Select * From web.Agencies";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgencyList agency = new AgencyList();
                                agency.AgencyID = reader.GetInt32(0);
                                agency.AName = reader.GetString(1);
                                agency.ACity = reader.GetString(2);
                                agency.AState = reader.GetString(3);
                                agency.AZip = reader.GetString(4);

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
        public int AgencyID { get; set; }
        public string AName { get; set; }
        public string AStreet { get; set; }
        public string ACity { get; set; }
        public string AState { get; set; }
        public string AZip { get; set; }
    }
}
