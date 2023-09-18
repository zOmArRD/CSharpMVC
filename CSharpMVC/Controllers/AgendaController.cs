using CSharpMVC.Database;
using CSharpMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers;

public class AgendaController : Controller
{
    private readonly DbUsuario _dbUsuario;

    public AgendaController(DbUsuario dbUsuario)
    {
        _dbUsuario = dbUsuario;
    }

    public IActionResult Index()
    {
        var idUsuario = 1; // Reemplaza esto con la lógica para obtener el ID del usuario autenticado.
        List<Agenda> agendas = _dbUsuario.GetAgendas(idUsuario);

        return View(agendas);
    }

    public IActionResult Create()
    {
        return View("CreateEdit", new Agenda());
    }

    [HttpPost]
    public IActionResult Save(Agenda agenda)
    {
        if (ModelState.IsValid)
        {
            var idUsuario = 1; // Reemplaza esto con la lógica para obtener el ID del usuario autenticado.
            agenda.IdUsuario = idUsuario;

            if (agenda.IdAgenda == 0)
            {
                var resultado = _dbUsuario.CreateAgenda(agenda);

                if (resultado) return RedirectToAction("Index");
            }
            else
            {
                var resultado = _dbUsuario.EditAgenda(agenda);

                if (resultado) return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al guardar la agenda.");
        }

        return View("CreateEdit", agenda);
    }

    public IActionResult Edit(int id)
    {
        var agenda = _dbUsuario.GetAgendaById(id);

        if (agenda == null) return NotFound();

        return View("CreateEdit", agenda);
    }

    public IActionResult Delete(int id)
    {
        var agenda = _dbUsuario.GetAgendaById(id);

        if (agenda == null) return NotFound();

        return View(agenda);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var resultado = _dbUsuario.DeleteAgenda(id);

        if (resultado) return RedirectToAction("Index");

        ModelState.AddModelError(string.Empty, "Error al eliminar la agenda.");
        return View("Delete", _dbUsuario.GetAgendaById(id));
    }
}