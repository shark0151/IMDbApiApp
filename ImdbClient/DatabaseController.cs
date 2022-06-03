using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ImdbClient
{
    public class DatabaseController
    {
        string urlLink = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        //Student
    }
}
