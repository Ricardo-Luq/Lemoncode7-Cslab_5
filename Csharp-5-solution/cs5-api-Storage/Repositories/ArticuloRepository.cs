using cs5_api_Storage.Contracts;
using cs5_api_Storage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cs5_api_Storage.Repositories

{
    public class ArticuloRepository : IArticuloRepository
    {
        const string JSON_PATH = @"C:\Users\I44105\Downloads\LEMONCODE\03 C#\testing\Csharp-5-solution\cs5-api-Storage\Resources\Articulos.json";
        private string GetArticulosFromFile()
        {
            var json = File.ReadAllText(JSON_PATH);
            return json;
        }
        private void UpdateArticulos(List<Articulo> articulos)
        {
            var articulosJson = JsonConvert.SerializeObject(articulos, Formatting.Indented);
            File.WriteAllText(JSON_PATH, articulosJson);
        }
        public void addArticulo(Articulo articulo)
        {
            var articulos = GetArticulos();
            var existeArticulo = articulos.Exists(a => a.Id == articulo.Id);
            if (existeArticulo)
            {
                throw new Exception("Ya existe un articulo con esa id");
            }
            articulos.Add(articulo);
            UpdateArticulos(articulos);
        }

        public void decreaseArticulo(int id, int cantidad)
        {
            try
            {
                var articulos = GetArticulos();
                bool exists = articulos.Exists(a => a.Id == id);
                Articulo ArticuloSeleccionado = articulos.First(a => a.Id == id);
                if (ArticuloSeleccionado.Cantidad < cantidad)
                {
                    throw new Exception($"No hay suficientes articulos (hay {ArticuloSeleccionado.Cantidad}) como para sacar {cantidad}");
                }
                ArticuloSeleccionado.Cantidad -= cantidad;
                UpdateArticulos(articulos);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Ese articulo no existe");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Articulo> GetArticulos()
        {
            try
            {
                var articulosFromFile = GetArticulosFromFile();
                List<Articulo> articulos = JsonConvert.DeserializeObject<List<Articulo>>(articulosFromFile);
                return articulos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void increaseArticulo(int id, int cantidad)
        {
            try
            {
                var articulos = GetArticulos();
                bool exists = articulos.Exists(a => a.Id == id);
                Articulo ArticuloSeleccionado = articulos.First(a => a.Id == id);
                ArticuloSeleccionado.Cantidad += cantidad;
                UpdateArticulos(articulos);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Ese articulo no existe");
            }
            catch (Exception)
            {
                throw new Exception("No existe ese articulo");
            }
        }

        public void removeArticulo(int id)
        {
            try
            {
                var articulos = GetArticulos();
                Articulo articulo = articulos.First(a => a.Id == id);
                var existeActor = articulos.Exists(a => a.Id == articulo.Id);
                articulos.Remove(articulo);
                UpdateArticulos(articulos);
            }
            catch (Exception)
            {
                throw new Exception("No existe un articulo con esa id");
            }
        }
    }
}
