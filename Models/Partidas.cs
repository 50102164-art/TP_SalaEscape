namespace TP_Sala_Escape.Models;

//Desarrolla los metedos y funcionalidades basicas para esta clase
public class Partidas
{
    // Atributos de la clase
    public int IdPartidas{get; set;}
    public TimeSpan Tiempo{get; set;}
    public int Errores{get; set;}
    public int PistasSolicitadas{get; set;}

    // Constructor de la clase
    public Partidas(int idPartidas, int idSalas, TimeSpan tiempo, int errores, int pistasSolicitadas)
    {
        IdPartidas = idPartidas;
        Tiempo = tiempo;
        Errores = errores;
        PistasSolicitadas = pistasSolicitadas;
    }

    public int GetIdPartidas()
    {
        return IdPartidas;
    }
}