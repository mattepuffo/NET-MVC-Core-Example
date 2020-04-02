using CSharpNetCoreWeb.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CSharpNetCoreWeb
{
    public class PersonaDb
    {
        private string connectionString;

        public PersonaDb(string connectionString)
        {

            this.connectionString = connectionString;
        }

        public List<Persona> GetAll()
        {
            var list = new List<Persona>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Persone";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Persona persona = new Persona();
                            persona.ID = Convert.ToInt32(dataReader["PersonaId"]);
                            persona.Email = dataReader["PersonaEmail"].ToString();
                            persona.Active = dataReader.GetBoolean(dataReader.GetOrdinal("PersonaAttivo"));
                            list.Add(persona);
                        }
                    }
                    conn.Close();
                }
            }

            return list;
        }

        public bool AddPersona(Persona p)
        {
            bool result = false;
            string commandText = "INSERT INTO Persone (PersonaEmail, PersonaAttivo) VALUES(@email, @attivo)";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(commandText, conn))
                {
                    cmd.Parameters.AddWithValue("@email", p.Email.Trim());
                    cmd.Parameters.AddWithValue("@attivo", p.Active);
                    conn.Open();

                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public bool ModificaPersona(int id, Persona p)
        {
            bool result = false;
            string commandText = "UPDATE Persone SET PersonaEmail = @email, PersonaAttivo = @active WHERE PersonaId = @id";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(commandText, conn))
                {
                    System.Diagnostics.Debug.WriteLine(id);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", p.Email.Trim());
                    cmd.Parameters.AddWithValue("@active", p.Active);
                    conn.Open();

                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public Persona GetDetails(int id)
        {
            var persona = new Persona();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Persone WHERE PersonaId = @id";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            persona.ID = Convert.ToInt32(dataReader["PersonaId"]);
                            persona.Email = dataReader["PersonaEmail"].ToString();
                            persona.Active = dataReader.GetBoolean(dataReader.GetOrdinal("PersonaAttivo"));
                        }

                    }
                    conn.Close();
                }
            }
            return persona;
        }

        public bool DeletePersona(int id)
        {
            bool result = false;
            string commandText = "DELETE FROM Persone WHERE PersonaId = @id";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(commandText, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();

                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
