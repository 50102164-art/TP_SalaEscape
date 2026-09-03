namespace TP_Sala_Escape.Models;

public class Salas
{
    //IdSalas, Nombre, Nivel, RespuestaCorrecta, Pista1, Pista2, Pista3
    public int IdSalas{get; set;}
    public string Nombre{get; set;}
    public int Nivel{get; set;}
    public string RespuestaCorrecta{get; set;}
    public string Pista1{get; set;}
    public string Pista2{get; set;}
    public string Pista3{get; set;}

    // Constructor de la clase
    public Salas(int idSalas, string nombre, int nivel, string respuestaCorrecta, string pista1, string pista2, string pista3, int idRecurso)
    {
        IdSalas = idSalas;
        Nombre = nombre;
        Nivel = nivel;
        RespuestaCorrecta = respuestaCorrecta;
        Pista1 = pista1;
        Pista2 = pista2;
        Pista3 = pista3;
    }
}
