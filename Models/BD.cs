namespace TP_Sala_Escape.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class BD{
    private static string _connectionString = "Server=localhost;Database=SalaEscape;Integrated Security=True;TrustServerCertificate=True;";
    
    public Salas GetSalaActual(int idPartida)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM Salas s INNER JOIN Salas_X_Partidas sxp ON s.idSalas = sxp.idSalas WHERE sxp.idPartidas = @IdPartida AND sxp.SalaActual = 1";
            return connection.QueryFirstOrDefault<Salas>(query, new { IdPartida = idPartida });
        }
    }

    public Recurso GetRecurso(int idRecurso)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM Recursos WHERE idRecurso = @IdRecurso";
            return connection.QueryFirstOrDefault<Recurso>(query, new { IdRecurso = idRecurso });
        }
    }

    public int CrearPartida()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "INSERT INTO Partidas (Tiempo, Errores, PistasSolicitadas) VALUES (0, 0, 0)";
            connection.Execute(query);
        }
        return GetUltimaPartidaId();
    }

    public int GetUltimaPartidaId()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT MAX(idPartidas) FROM Partidas";
            return connection.QueryFirstOrDefault<int>(query);
        }
    }

    public void GuardarRespuesta(int idPartida, string respuesta)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "INSERT INTO RespuestasUsuario (IdPartidas, Respuesta) VALUES (@idPartida, @respuesta)";
            connection.Execute(query, new { idPartida = idPartida, respuesta = respuesta });
        }
    }

    public void RegistrarJugador(int idJugador, int idPartida, string nombreJugador)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "INSERT INTO Jugadores (IdJugadores, IdPartidas, Nombre) VALUES (@idJugador, @idPartida, @nombreJugador)";
            connection.Execute(query, new { idJugador = idJugador, idPartida = idPartida, nombreJugador = nombreJugador });
        }
    }
}