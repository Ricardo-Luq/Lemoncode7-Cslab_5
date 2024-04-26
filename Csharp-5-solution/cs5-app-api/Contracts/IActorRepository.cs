using cs5_app_api.Models;
using System.Collections.Generic;

namespace cs5_app_api.Contracts
{
    public interface IActorRepository
    {
     List<Actor> GetActors();
     Actor GetActorById(int id);
     void AddActor(Actor actor);
     void UpdateActor(Actor actor);
     void DeleteActor(int id);
    }
}
