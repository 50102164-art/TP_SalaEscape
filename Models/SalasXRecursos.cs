//Crear un models que contenga la relación entre las salas y los recursos, para poder obtener el recurso asociado a cada sala.
namespace TP_Sala_Escape.Models;
public class SalasXRecursos
{
    public int IdSalasXRecursos { get; set; }
    public int IdSalas { get; set; }
    public int IdRecurso { get; set; }
    
    // Constructor de la clase
    public SalasXRecursos(int idSalas, int idRecurso, int idSalasXRecursos)
    {
        IdSalasXRecursos = idSalasXRecursos;
        IdSalas = idSalas;
        IdRecurso = idRecurso;
    }
}