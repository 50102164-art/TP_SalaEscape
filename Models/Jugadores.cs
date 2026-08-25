namespace TP_Sala_Escape.Models;
//Desarrolla los metedos y funcionalidades basicas para esta clase

public class Jugadores
{
    // Atributos de la clase
    public int IdJugadores{get; set;}
    public string Nombre{get; set;}

    // Constructor de la clase
    public Jugadores(int idJugadores, string nombre)
    {
        IdJugadores = idJugadores;
        Nombre = nombre;
    }


}