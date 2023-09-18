namespace CSharpMVC.Models;

public class Usuarios
{
    public string? Nombre { get; set; }
    public string? Correo { get; set; }
    public string? Clave { get; set; }

    public Rol? IdRol { get; set; }
}