# Lemoncode7-Cslab_5
Laboratory made for learning .net c# api basics

## Tasks:
### 1. Implements the remaining methods from the example:
- __Obtain an actor by Id__

Repository:
```cs
       public Actor GetActorById(int id) {
           try { 
           var actores = GetActors();
           Actor ActorSeleccionado = actores.First(a => a.Id == id);
           return ActorSeleccionado;
           } catch (Exception) {
               throw new Exception("No existe ese actor");
           }
       }
```
Controller:
```cs
        [HttpGet("SelectActor")]
        public ActionResult<Actor> SelectActor(int id) {
            try {
                return _actorRepository.GetActorById(id);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
```
- __Modify an actor__
 
Repository:
```cs
        public void UpdateActor(Actor actor) {
            var actores = GetActors();
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            if (existeActor) {
                Actor selecc = actores.First(a => a.Id == actor.Id);
                selecc.Nombre = actor.Nombre;
                selecc.Apellido = actor.Apellido;
                selecc.Peliculas = actor.Peliculas;
            } else {
                throw new Exception("No existe un autor con ese id");
            }
            UpdateActores(actores);
        }
```
Controller:
```cs
        [HttpPatch("ModifyActor")]
        public ActionResult ModifyActor(Actor actor) {
            try {
                _actorRepository.UpdateActor(actor);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
```
- __Erase an actor__

Repository:
```cs
        public void DeleteActor(int id) {
            try { 
            var actores = GetActors();
            Actor actor = actores.First(a => a.Id == id);
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            actores.Remove(actor);
            UpdateActores(actores);
            } catch (Exception) { 
                throw new Exception("No existe un actor con esa id");
            } 
        }
```
Controller:
```cs
        [HttpDelete("DeleteActor")]
        public ActionResult DeleteActor(int id) {
            try {
                _actorRepository.DeleteActor(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
```
---
### 2. Create a new api web to manage a storage. The controller must allow the following functions:
- __Add a new item:__ it must have the following fields
  - Id
  - Name
  - Description
  - Date of input
  - Amount

Model:
```cs
    public class Articulo {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string FechaEntrada { get; set; }
        public int Cantidad { get; set; }
    }
```
Repository:
```cs
        public void addArticulo(Articulo articulo) {
            var articulos = GetArticulos();
            var existeArticulo = articulos.Exists(a => a.Id == articulo.Id);
            if (existeArticulo) {
                throw new Exception("Ya existe un articulo con esa id");
            }
            articulos.Add(articulo);
            UpdateArticulos(articulos);
        }
```
Controller:
```cs
        [HttpPost("AddArticulo")]
        public ActionResult CreateArticulo(Articulo articulo) {
            try {
                _articuloRepository.addArticulo(articulo);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
```
- __Input item:__ Input the amount of an item you want to increase. If it doesn't exist, output not found.

Repository:
```cs
        public void increaseArticulo(int id, int cantidad) {
            try {
                var articulos = GetArticulos();
                bool exists = articulos.Exists(a => a.Id == id);
                Articulo ArticuloSeleccionado = articulos.First(a => a.Id == id);
                ArticuloSeleccionado.Cantidad += cantidad;
                UpdateArticulos(articulos);
            } catch (InvalidOperationException ex) {
                throw new Exception("Ese articulo no existe");
            } catch (Exception) {
                throw new Exception("No existe ese articulo");
            }
        }
```
Controller:
```cs
        [HttpPatch("SacarArticulos")]
        public ActionResult sacarArticulos(int id, int cantidad) {
            try {
                _articuloRepository.decreaseArticulo(id, cantidad);
                return Ok();
            } catch (Exception ex) {
                if (ex.Message == "Ese articulo no existe") {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
        }
```
- __Output item:__ Input the amount of an item you want to decrease. If it doesn't exist, output not found. If there is not enough amount to output, send that as the exception.

Repository:
```cs
        public void decreaseArticulo(int id, int cantidad) {
            try {
                var articulos = GetArticulos();
                bool exists = articulos.Exists(a => a.Id == id);
                Articulo ArticuloSeleccionado = articulos.First(a => a.Id == id);
                if (ArticuloSeleccionado.Cantidad < cantidad) {
                    throw new Exception($"No hay suficientes articulos (hay {ArticuloSeleccionado.Cantidad}) como para sacar {cantidad}");
                }
                ArticuloSeleccionado.Cantidad -= cantidad;
                UpdateArticulos(articulos);
            } catch (InvalidOperationException) {
                throw new Exception("Ese articulo no existe");
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
```
Controller:
```cs
        [HttpPatch("MeterArticulos")]
        public ActionResult meterArticulos(int id, int cantidad) {
            try {
                _articuloRepository.increaseArticulo(id, cantidad);
                return Ok(); 
            } catch (Exception ex) {
                if (ex.Message == "Ese articulo no existe") {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
        }
```

