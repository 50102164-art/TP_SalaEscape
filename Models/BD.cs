namespace TP_Sala_Escape.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class BD{
    private static string _connectionString = "Server=localhost;Database=SalaEscape;Integrated Security=True;TrustServerCertificate=True;";
    
    public static Sala GetSalaActual(int idPartida)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Salas s INNER JOIN Salas_X_Partidas sxp ON s.idSalas = sxp.idSalas WHERE sxp.idPartidas = @IdPartida AND sxp.SalaActual = 1";
            return connection.QueryFirstOrDefault<Sala>(query, new { IdPartida = idPartida });
        }
    }

    public static Recurso GetRecurso(int idRecurso)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Recursos WHERE idRecurso = @IdRecurso";
            return connection.QueryFirstOrDefault<Recurso>(query, new { IdRecurso = idRecurso });
        }
    }
}