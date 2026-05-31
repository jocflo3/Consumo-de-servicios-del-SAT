using Microsoft.Data.SqlClient;
using System.Data;

namespace Descargar_CFDIS.Repositories
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connecionString;

        //Contructor 
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connecionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connecionString);
        }
    }
}
