
using domain.Entities;

namespace domain.Interface
{
    public interface IAnabolizanteRepositorio
    {
        public void Insert(Anabolizante al);
        public void Update(Anabolizante al);
        public void Delete(Anabolizante al);
        public Anabolizante GetById(int id);
        public List<Anabolizante> GetAll();

    }
}
