using CSharpMVC.Database;
using CSharpMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharpMVC.Controllers;

public class AccesoController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string correo, string clave)
    {
        var usuario = new DbUsuario().Find(correo, clave);

        Console.WriteLine(usuario.Nombre);
        Console.WriteLine(usuario.Correo);

        if (usuario.Nombre != null) return RedirectToAction("Index", "Agenda");

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string nombre, string correo, string clave)
    {
        if (CorreoExists(correo))
        {
            ModelState.AddModelError(string.Empty, "El correo electrónico ya está registrado.");
            return View();
        }

        var registroExitoso = new DbUsuario().Register(nombre, correo, clave);

        if (registroExitoso) return RedirectToAction("Login");

        ModelState.AddModelError(string.Empty, "Error en el registro.");
        return View();
    }

    private bool CorreoExists(string correo)
    {
        return new DbUsuario().CorreoExists(correo);
    }
}