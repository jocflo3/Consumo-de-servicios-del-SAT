using Dapper;
using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.Models;
using Descargar_CFDIS.Excepciones;

namespace Descargar_CFDIS.Repositories
{
    public class UsuarioRepository:IUsuarioRepositoy
    {
        public readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task RegistrarUsuario(Usuario user)
        {
            var sqlaux = @"SELECT COUNT(*) FROM USUARIO WHERE RFC = @RFC";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteScalarAsync<int>(sqlaux, new { RFC = user.RFC });
            if (rows > 0) 
            { 
                throw new DuplicateException("El usuario con RFC", user.RFC); 
            };
            var sql = @"INSERT INTO  USUARIO
                        (
                            RFC,
                            Nombre,
                            PassUser
                        ) 
                        VALUES
                        (
                            @RFC,
                            @Nombre,
                            @PassUser
                        )";

            await connection.ExecuteAsync(sql,user);
        }
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            var sql = @"SELECT 
                            Id,
                            RFC,
                            Nombre,
                            FechaRegistro,
                            Activo 
                        FROM USUARIO";
            using var connection = _context.CreateConnection();

            var usuarios = await connection.QueryAsync<Usuario>(sql);

            return usuarios.ToList();
        }
        public async Task<Usuario?> ObtenerUsuario(int user)
        {
            var sql = @"SELECT 
                            Id,
                            RFC,
                            Nombre,
                            FechaRegistro,
                            Activo 
                        FROM USUARIO 
                        WHERE Id = @Id";
            using var connection = _context.CreateConnection();

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new {Id = user});

            return usuario;
        }
        public async Task<int> ActualizarUsuario(Usuario user)
        {
            var sql = @"UPDATE USUARIO SET 
                            NOMBRE =@Nombre, 
                            Activo = @Activo
                        WHERE Id = @Id";
            using var connection = _context.CreateConnection();

            var rows = await connection.ExecuteAsync(sql, user);
            return rows;
        }
        public async Task<int> EliminarUsuario(int user)
        {
            var sql = @"DELETE FROM USUARIO WHERE Id = @Id";
            using var connection = _context.CreateConnection();

            var rows = await connection.ExecuteAsync(sql, new { Id = user });
            return rows;
        }
    }
}
