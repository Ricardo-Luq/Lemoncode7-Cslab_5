using cs5_app_api.Contracts;
using cs5_app_api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace cs5_app_api.Repositories
{
    public class ActorRepository : IActorRepository
    {
        const string JSON_PATH = @"C:\Users\I44105\Downloads\LEMONCODE\03 C#\testing\Csharp-5-solution\cs5-app-api\Resources\Actores.json";
        private string GetActorsFromFile()
        {
            var json = File.ReadAllText(JSON_PATH);
            return json;
        }
        private void UpdateActores(List<Actor> actores)
        {
            var actoresJson = JsonConvert.SerializeObject(actores,Formatting.Indented);
            File.WriteAllText(JSON_PATH, actoresJson);
        }
        public void AddActor(Actor actor)
        {
            var actores = GetActors();
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            if (existeActor)
            {
               throw new Exception("Ya existe un autor con ese id");
            }
            actores.Add(actor);
            UpdateActores(actores);
        }

        public void DeleteActor(int id)
        {
            try { 
            var actores = GetActors();
            Actor actor = actores.First(a => a.Id == id);
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            actores.Remove(actor);
            UpdateActores(actores);
            }
            catch (Exception) { 
            
                throw new Exception("No existe un actor con esa id");
            }
           
        }

        public Actor GetActorById(int id)
        {
            try { 
            var actores = GetActors();
            Actor ActorSeleccionado = actores.First(a => a.Id == id);
            return ActorSeleccionado;
            } catch (Exception)
            {
                throw new Exception("No existe ese actor");
            }

        }

        public List<Actor> GetActors()
        {
            try
            {
                var actoresFromFile = GetActorsFromFile();
                List<Actor> actores = JsonConvert.DeserializeObject<List<Actor>>(actoresFromFile);
                return actores;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateActor(Actor actor)
        {
            var actores = GetActors();
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            if (existeActor)
            {
                Actor selecc = actores.First(a => a.Id == actor.Id);
                selecc.Nombre = actor.Nombre;
                selecc.Apellido = actor.Apellido;
                selecc.Peliculas = actor.Peliculas;
            } else
            {
                throw new Exception("No existe un autor con ese id");
            }
            //actores.Add(actor);
            UpdateActores(actores);
        }
    }
}
