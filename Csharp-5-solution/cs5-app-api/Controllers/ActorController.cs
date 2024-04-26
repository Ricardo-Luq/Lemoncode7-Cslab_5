using cs5_app_api.Contracts;
using Microsoft.AspNetCore.Http;
using cs5_app_api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace cs5_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        [HttpPost("CreateActor")]
         public ActionResult CreateActor(Actor actor)
         {
             try
             {
                _actorRepository.AddActor(actor);
                return Ok();
             }
                catch (Exception ex)
             {
                return BadRequest(ex.Message);
             }
        }
        [HttpDelete("DeleteActor")]
        public ActionResult DeleteActor(int id)
        {
            try
            {
                _actorRepository.DeleteActor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("ModifyActor")]
        public ActionResult ModifyActor(Actor actor)
        {
            try
            {
                _actorRepository.UpdateActor(actor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SelectActor")]
        public ActionResult<Actor> SelectActor(int id)
        {
            try
            {
                return _actorRepository.GetActorById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private readonly IActorRepository _actorRepository;
        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        [HttpGet("ListActors")]
        public ActionResult<List<Actor>> Get()
        {
            return _actorRepository.GetActors();
        }
    }
}
