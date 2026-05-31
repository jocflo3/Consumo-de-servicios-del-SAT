using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.Models;
using Dapper;

namespace Descargar_CFDIS.Repositories
{
    public class SatActionsRepository: ISatActionsRepository
    {
        private readonly DapperContext _dapperContext;

        public SatActionsRepository(DapperContext dapperContext)
        {
            _dapperContext =dapperContext;
        }
        public async Task<Usuario> ObtenerPfxPass(int id)
        {
            var sql = @"SELECT 
                            PasswordKeyEncrypted,
                            PFXFile
                        FROM USUARIO 
                        WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });

            return usuario;
        }
        public async Task<int> GeneraYRegistrarPfx(byte[] pfx, string pass, int id)
        {
            var sql = @"UPDATE USUARIO SET 
                            PFXFile =@PFXFile, 
                            PasswordKeyEncrypted = @Pass
                        WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            var rows = await connection.ExecuteAsync(sql, new
            {
                PFXFile = pfx,
                Pass = pass,
                Id = id
            });
            return rows;
        }
    }
}
