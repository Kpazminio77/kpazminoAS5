using kpazminoAS5.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpazminoAS5.Utils
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string Path)
        {

            dbPath = Path; 
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("El nombre es requerido");

                Persona person = new() { Name = nombre };
                result = conn.Insert(person);
                StatusMessage = string.Format("Dato añadido correctamente", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al insertar", ex.Message);
            }
        }
        public List<Persona> GetAllPeople()
        {
            try
            {
                init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al mostrar", ex.Message);
            }
            return new List<Persona>();
        }
    }
}
