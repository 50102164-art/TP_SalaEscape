namespace TP_Sala_Escape.Models;

//Desarrolla los metedos y funcionalidades basicas para esta clase
public class Partidas
{
    // Atributos de la clase
    public int IdPartidas{get; set;}
    public int IdJugadores{get; set;}
    public TimeSpan Tiempo{get; set;}
    public int Errores{get; set;}
    public int PistasSolicitadas{get; set;}

    // Constructor de la clase
    public Partidas(int idPartidas, int idJugadores, int idSalas, TimeSpan tiempo, int errores, int pistasSolicitadas)
    {
        IdPartidas = idPartidas;
        IdJugadores = idJugadores;
        Tiempo = tiempo;
        Errores = errores;
        PistasSolicitadas = pistasSolicitadas;
    }
}