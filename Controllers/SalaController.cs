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

    public IActionResult Index()
    {
        BD miBd = new BD();
        int partidaId;
        partidaId = HttpContext.Session.GetInt32("PartidaId").Value;
        Salas salaActual = miBd.GetSalaActual(partidaId);   // método Dapper
        Recurso recurso = miBd.GetRecurso(salaActual.IdRecurso); // método Dapper para obtener el recurso asociado a la sala actual
        ViewBag.Id = salaActual.IdSalas;
        ViewBag.Nombre = salaActual.Nombre;
        ViewBag.Nivel = salaActual.Nivel;
        ViewBag.RespuestaCorrecta = salaActual.RespuestaCorrecta;
        ViewBag.Pista1 = salaActual.Pista1;
        ViewBag.Pista2 = salaActual.Pista2;
        ViewBag.Pista3 = salaActual.Pista3;
        ViewBag.RecursoUrl = recurso.RecursoUrl;
        ViewBag.TipoRecurso = recurso.TipoRecurso;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
