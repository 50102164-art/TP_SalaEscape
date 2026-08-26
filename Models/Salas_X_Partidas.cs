namespace TP_Sala_Escape.Models;

public class Salas_X_Partidas
{
    public int IdSalas_X_Partidas { get; set; }
    public int IdSalas { get; set; }
    public int IdPartidas { get; set; }
    public bool SalaActual { get; set; }

    // Constructor de la clase
    public Salas_X_Partidas(int idSalas, int idPartidas, int idSalas_X_Partidas, bool salaActual)
    {
        IdSalas_X_Partidas = idSalas_X_Partidas;
        IdSalas = idSalas;
        IdPartidas = idPartidas;
        SalaActual = salaActual;
    }
}