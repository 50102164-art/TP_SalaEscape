namespace TP_Sala_Escape.Models;

public class Recurso{
    public int IdRecurso{get; set;}
    public string RecursoUrl{get; set;}
    public string TipoRecurso{get; set;}

    public Recurso(int idRecurso, string recursoUrl, string tipoRecurso)
    {
        IdRecurso = idRecurso;
        RecursoUrl = recursoUrl;
        TipoRecurso = tipoRecurso;
    }
}