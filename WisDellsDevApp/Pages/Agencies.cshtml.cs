using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


// I hate to tell you this, but this is very clearly a .NET 5 application because there is NameSpacing, Classing and voids here.
// .NET 6 Apps have this all "Assumed" and there is no explicit classing, namespacing, etc
// 1 JULY 2022 Attempt was made to cast doubles
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
                    string sql = "Select * From DropOffLog";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgencyList agency = new AgencyList();
                                agency.RecNo = (int)reader.GetInt32(0);
                                agency.DeliveryType = (string)reader.GetString(1);
                                agency.DeliveredWhen = (DateTime)reader.GetDateTime(2);


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
        public string? DeliveryType { get; set; }
        public DateTime DeliveredWhen { get; set; } 
        public double PaymentAmount { get; set; }
    }
}
