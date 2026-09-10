using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_Sala_Escape.Models;

namespace TP_Sala_Escape.Controllers;

public class SalaController : Controller
{
    private readonly ILogger<SalaController> _logger;

    public SalaController(ILogger<SalaController> logger)
    {
        _logger = logger;
    }

    private string ObtenerUrlRecurso(string nombreRecurso)
    {
        if (string.IsNullOrWhiteSpace(nombreRecurso))
        {
            return "/Images/ForrestConCorona.jfif";
        }

        string nombre = nombreRecurso.ToLower();

        if (nombre.Contains("ping"))
        {
            return "/Images/PingPong.jfif";
        }

        if (nombre.Contains("jenny") || nombre.Contains("fotojenny"))
        {
            return "/Images/FotoJenny.jfif";
        }

        if (nombre.Contains("shit") || nombre.Contains("happens"))
        {
            return "/Images/ForrestShitHappens.jfif";
        }

        if (nombre.Contains("pluma"))
        {
            return "/Images/PlumaForrestGump.jpg";
        }

        if (nombre.Contains("Escapando") || nombre.Contains("forrestescapando"))
        {
            return "/Images/ForrestEscapando.jpg";
        }

        if (nombre.Contains("abc") || nombre.Contains("numero"))
        {
            return "/Images/AbcNumero.jpg";
        }

        if (nombre.Contains("corona") || nombre.Contains("forrestcon") || nombre.Contains("forrest"))
        {
            return "/Images/ForrestConCorona.jfif";
        }

        if (nombre.Contains("cerebro") || nombre.Contains("cerebero"))
        {
            return "/Images/Cerebero.jfif";
        }

        return "/Images/" + nombreRecurso;
    }

    private bool EsSalaEspecial(string nombreRecurso)
    {
        if (string.IsNullOrWhiteSpace(nombreRecurso))
            return false;

        string nombre = nombreRecurso.ToLower();
        return nombre.Contains("corona") || nombre.Contains("pluma");
    }

    public IActionResult Index()
    {
        BD miBd = new BD();
        int partidaId = HttpContext.Session.GetInt32("PartidaId") ?? 0;
        if (partidaId == 0)
        {
            int nueva = miBd.CrearPartida();
            HttpContext.Session.SetInt32("PartidaId", nueva);
            partidaId = nueva;
        }
    //Método para establecer salaActual = 0 si no hay salaActual y establecer salaActual = 1 en la siguiente sala de la partida. O sea que si no hay sala actual se establece la primera sala de la partida como sala actual y si ya hay una sala actual se establece la siguiente sala de la partida como sala actual.
 
    Salas salaActual = miBd.GetSalaActual(partidaId);
    if (salaActual == null)
    {
        miBd.CrearSxP(partidaId);
        salaActual = miBd.GetSalaActual(partidaId);
    }
    
    ViewBag.Id = salaActual.IdSalas;
    ViewBag.Nombre = salaActual.Nombre;
    ViewBag.Nivel = salaActual.Nivel;
    ViewBag.RespuestaCorrecta = salaActual.RespuestaCorrecta;
    ViewBag.Pista1 = salaActual.Pista1;
    ViewBag.Pista2 = salaActual.Pista2;
    ViewBag.Pista3 = salaActual.Pista3;

    // Nivel 0 no tiene recursos en la BD, pero mostrar imagen de la pluma
    if (salaActual.Nivel == 0)
    {
        ViewBag.RecursoUrl = "/Images/PlumaForrestGump.jpg"; // Imagen de pluma
        return View("~/Views/Home/Index.cshtml");
    }

    // Nivel 6 es el nivel final (sin recursos)
    if (salaActual.Nivel == 6)
    {
        ViewBag.RecursoUrl = "/Images/ForrestConCorona.jfif";
        return View("~/Views/Home/Index.cshtml");
    }

    // Para niveles 1-5, cargar recursos
    List<int>? recursosIds = miBd.GetIdRecursoByIdSala(salaActual.IdSalas);
    if(recursosIds == null || recursosIds.Count == 0)
    {
        return BadRequest("No hay recursos asociados a esta sala");
    }
    
    Recurso? recurso1 = miBd.GetRecurso(recursosIds[0]);
    if(recurso1 == null)
    {
        return BadRequest("Recurso 1 no encontrado");
    }

    ViewBag.RecursoUrl = ObtenerUrlRecurso(recurso1.RecursoUrl);
    ViewBag.TipoRecurso1 = recurso1.TipoRecurso;

    // Solo mostrar imagen chica si no es sala especial (corona or pluma)
    if(recursosIds.Count > 1 && !EsSalaEspecial(recurso1.RecursoUrl))
    {
        Recurso? recurso2 = miBd.GetRecurso(recursosIds[1]);
        if(recurso2 != null)
        {
            ViewBag.RecursoSubUrl = ObtenerUrlRecurso(recurso2.RecursoUrl);
            ViewBag.TipoRecurso2 = recurso2.TipoRecurso;
        }
    }
    
    // Usar la vista ubicada en Views/Home/Index.cshtml
    return View("~/Views/Home/Index.cshtml");
}

    [HttpPost]
    public IActionResult Respuesta([FromForm] string respuesta)
    {
        BD miBd = new BD();
        int partidaId = HttpContext.Session.GetInt32("PartidaId") ?? 0;
        if (partidaId == 0)
        {
            // Si no hay partida, crear una y volver a index
            int nueva = miBd.CrearPartida();
            HttpContext.Session.SetInt32("PartidaId", nueva);
            return RedirectToAction("Index");
        }

        miBd.GuardarRespuesta(partidaId, respuesta ?? string.Empty);

        Salas salaActual = miBd.GetSalaActual(partidaId);
        if (salaActual == null)
            return RedirectToAction("Index");

        // Si estamos en nivel 6 (final), no procesamos respuesta
        if (salaActual.Nivel == 6)
            return RedirectToAction("Index");

        // Comparar ignorando mayúsculas
        // Si estamos en la sala 0, aceptamos cualquier respuesta no vacía como nombre y avanzamos
        if (salaActual.Nivel == 0)
        {
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                Salas siguiente = miBd.GetSalaByNivel(salaActual.Nivel + 1);
                if (siguiente != null)
                {
                    miBd.MarcarSalaActualFalse(partidaId);
                    miBd.CrearSxP(partidaId, siguiente.IdSalas, true);
                }
                return RedirectToAction("Index");
            }
            // Si nombre vacío, volver a la sala 0
            return RedirectToAction("Index");
        }

        // Para salas > 0, comparar respuesta correcta
        if (!string.IsNullOrEmpty(salaActual.RespuestaCorrecta) &&
            string.Equals(salaActual.RespuestaCorrecta.Trim(), (respuesta ?? string.Empty).Trim(), StringComparison.OrdinalIgnoreCase))
        {
            Salas siguiente = miBd.GetSalaByNivel(salaActual.Nivel + 1);
            if (siguiente != null)
            {
                miBd.MarcarSalaActualFalse(partidaId);
                miBd.CrearSxP(partidaId, siguiente.IdSalas, true);
            }
            // Avanzamos (o terminamos si no hay siguiente)
            return RedirectToAction("Index");
        }

        // Respuesta incorrecta -> volver a la misma sala
        return RedirectToAction("Index");
    }

    public IActionResult ReinciarJuego()
    {
        HttpContext.Session.Remove("PartidaId");
        return RedirectToAction("Home", "Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
