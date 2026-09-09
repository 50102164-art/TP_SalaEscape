using Microsoft.AspNetCore.Mvc;
using TP_Sala_Escape.Models;

namespace TP_Sala_Escape.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RespuestaController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromForm] string respuesta)
    {
        int? partidaId = HttpContext.Session.GetInt32("PartidaId");
        if (!partidaId.HasValue)
            return BadRequest(new { error = "Partida no iniciada" });

        BD miBd = new BD();
        miBd.GuardarRespuesta(partidaId.Value, respuesta ?? string.Empty);

        Salas salaActual = miBd.GetSalaActual(partidaId.Value);
        if (salaActual == null)
            return BadRequest(new { error = "Sala actual no encontrada" });

        Salas siguiente = miBd.GetSalaByNivel(salaActual.Nivel + 1);
        if (siguiente == null)
        {
            // No hay siguiente sala -> juego finalizado
            return Ok(new { finished = true });
        }

        // Marcar la sala anterior como no actual e insertar la siguiente como actual
        miBd.MarcarSalaActualFalse(partidaId.Value);
        miBd.CrearSxP(partidaId.Value, siguiente.IdSalas, true);

        return Ok(new { finished = false });
    }
}
