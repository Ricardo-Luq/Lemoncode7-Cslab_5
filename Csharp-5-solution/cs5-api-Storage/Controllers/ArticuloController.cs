using cs5_api_Storage.Contracts;
using Microsoft.AspNetCore.Http;
using cs5_api_Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace cs5_api_Storage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloRepository _articuloRepository;
        public ArticuloController(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }
        [HttpGet("ListarArticulos")]
        public ActionResult<List<Articulo>> Get() 
        { 
            return _articuloRepository.GetArticulos();
        }
        [HttpPost("AddArticulo")]
        public ActionResult CreateArticulo(Articulo articulo)
        {
            try
            {
                _articuloRepository.addArticulo(articulo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteArticulo")]
        public ActionResult removeArticulo(int id) 
        {
            try
            {
                _articuloRepository.removeArticulo(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("SacarArticulos")]
        public ActionResult sacarArticulos(int id, int cantidad)
        {
            try
            {
                _articuloRepository.decreaseArticulo(id, cantidad);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Ese articulo no existe")
                {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("MeterArticulos")]
        public ActionResult meterArticulos(int id, int cantidad)
        {
            try
            {
                _articuloRepository.increaseArticulo(id, cantidad);
                return Ok(); 
            }
            catch (Exception ex)
            {
                if (ex.Message == "Ese articulo no existe")
                {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
        }

    }
}
