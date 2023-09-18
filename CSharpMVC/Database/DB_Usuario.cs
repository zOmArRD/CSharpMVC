using CSharpMVC.Models;
using MySqlConnector;

namespace CSharpMVC.Database;

public class DbUsuario
{
    public Usuarios Find(string correo, string clave)
    {
        try
        {
            Console.WriteLine("Starting");

            var objeto = new Usuarios();

            using var connection =
                new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "SELECT * FROM Usuarios WHERE Correo = @Correo AND Clave = @Clave";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Correo", correo);
            command.Parameters.AddWithValue("@Clave", clave);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine("Sucess");
                objeto.Nombre = reader["Nombre"].ToString()!;
                objeto.Correo = reader["Correo"].ToString()!;
                objeto.Clave = reader["Clave"].ToString()!;
            }

            return objeto;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            throw;
        }
    }

    public bool Register(string nombre, string correo, string clave)
    {
        try
        {
            using var connection =
                new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "INSERT INTO Usuarios (Nombre, Correo, Clave) VALUES (@Nombre, @Correo, @Clave)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Correo", correo);
            command.Parameters.AddWithValue("@Clave", clave);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            return false;
        }
    }

    public bool CorreoExists(string correo)
    {
        try
        {
            using var connection =
                new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Correo", correo);

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            throw;
        }
    }

    public List<Agenda> GetAgendas(int idUsuario)
    {
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "SELECT * FROM Agendas WHERE IdUsuario = @IdUsuario";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

            List<Agenda> agendas = new();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var agenda = new Agenda
                    {
                        IdAgenda = Convert.ToInt32(reader["IdAgenda"]),
                        Nombre = reader["Nombre"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        IdUsuario = idUsuario
                    };

                    agendas.Add(agenda);
                }
            }

            return agendas;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            throw;
        }
    }

    public bool CreateAgenda(Agenda agenda)
    {
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "INSERT INTO Agendas (Nombre, Telefono, Direccion, IdUsuario) " +
                        "VALUES (@Nombre, @Telefono, @Direccion, @IdUsuario)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", agenda.Nombre);
            command.Parameters.AddWithValue("@Telefono", agenda.Telefono);
            command.Parameters.AddWithValue("@Direccion", agenda.Direccion);
            command.Parameters.AddWithValue("@IdUsuario", agenda.IdUsuario);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            return false;
        }
    }

    public bool EditAgenda(Agenda agenda)
    {
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "UPDATE Agendas SET Nombre = @Nombre, Telefono = @Telefono, " +
                        "Direccion = @Direccion WHERE IdAgenda = @IdAgenda";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nombre", agenda.Nombre);
            command.Parameters.AddWithValue("@Telefono", agenda.Telefono);
            command.Parameters.AddWithValue("@Direccion", agenda.Direccion);
            command.Parameters.AddWithValue("@IdAgenda", agenda.IdAgenda);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            return false;
        }
    }

    public bool DeleteAgenda(int idAgenda)
    {
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "DELETE FROM Agendas WHERE IdAgenda = @IdAgenda";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdAgenda", idAgenda);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            return false;
        }
    }

    public Agenda? GetAgendaById(int idAgenda)
    {
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=appmvc;User=root;Password=omar;");
            connection.Open();

            var query = "SELECT * FROM Agendas WHERE IdAgenda = @IdAgenda";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdAgenda", idAgenda);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var agenda = new Agenda
                {
                    IdAgenda = Convert.ToInt32(reader["IdAgenda"]),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Direccion = reader["Direccion"].ToString(),
                    IdUsuario = Convert.ToInt32(reader["IdUsuario"])
                };

                return agenda;
            }

            return null; // Si no se encuentra la agenda con el ID especificado.
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL Exception: " + ex.Message);
            throw;
        }
    }
}