using Microsoft.EntityFrameworkCore;
using testwebAPIapp.Model;

namespace testwebAPIapp.Interfaces
{
    public interface IPersonneRepository
    {
        public bool PersonneExists(int id);
        public ICollection<Personne> GetPersonnes();
        public Personne GetPersonne(int id);
        public bool CreatePersonne(Personne personne);
        public bool UpdatePersonne(Personne personne);

        public bool DeletePersonne(Personne personne);
        public bool Save();
        public int ActualAge(Personne personne);
        
    }
}
