using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_Sala_Escape.Models;

namespace TP_Sala_Escape.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

public IActionResult Index()
{
    BD miBd = new BD();
    miBd.CrearPartida();
    int partidaId = miBd.GetUltimaPartidaId();
    HttpContext.Session.SetInt32("PartidaId", partidaId);
    
    Salas? salaActual = miBd.GetSalaActual(partidaId);
    if(salaActual == null)
    {
        miBd.CrearSxP(0, partidaId, true);
        salaActual = miBd.GetSalaActual(partidaId);
    }
    
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
    
    ViewBag.RecursoUrl1 = recurso1.RecursoUrl;
    ViewBag.TipoRecurso1 = recurso1.TipoRecurso;
    
    if(recursosIds.Count > 1)
    {
        Recurso? recurso2 = miBd.GetRecurso(recursosIds[1]);
        if(recurso2 != null)
        {
            ViewBag.RecursoUrl2 = recurso2.RecursoUrl;
            ViewBag.TipoRecurso2 = recurso2.TipoRecurso;
        }
    }
    
    ViewBag.Id = salaActual.IdSalas;
    ViewBag.Nombre = salaActual.Nombre;
    ViewBag.Nivel = salaActual.Nivel;
    ViewBag.RespuestaCorrecta = salaActual.RespuestaCorrecta;
    ViewBag.Pista1 = salaActual.Pista1;
    ViewBag.Pista2 = salaActual.Pista2;
    ViewBag.Pista3 = salaActual.Pista3;
    
    return View();
}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
