namespace TP_Sala_Escape.Models;

public class RespuestasUsuario
{
    public int IdRespuestas { get; set; }
    public int IdPartidas { get; set; }
    public string Respuesta { get; set;}

public RespuestasUsuario(int idRespuestas, int idPartidas, string respuesta)
{
    IdRespuestas = idRespuestas;
    IdPartidas = idPartidas;
    Respuesta = respuesta;
}
}