using cs5_api_Storage.Models;
using System.Collections.Generic;

namespace cs5_api_Storage.Contracts
{
    public interface IArticuloRepository
    {
        List<Articulo> GetArticulos();
        void addArticulo(Articulo articulo);
        void removeArticulo(int id);
        void increaseArticulo(int id, int cantidad);
        void decreaseArticulo(int id, int cantidad);
    }
}
