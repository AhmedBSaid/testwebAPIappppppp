using System.Collections.Immutable;
using testwebAPIapp.Data;
using testwebAPIapp.Interfaces;
using testwebAPIapp.Model;

namespace testwebAPIapp.Repository
{
    public class PersonneRepository  : IPersonneRepository
    {
        private DataContext _context;

        public PersonneRepository(DataContext context)
        {
            _context = context;
        }

        public bool PersonneExists(int id)
        {
            return _context.Personnes.Any(c => c.Id == id);
        }

        public ICollection<Personne> GetPersonnes()
        {
            return _context.Personnes.OrderBy(p => p.FirstName).ToList();
        }

        public Personne GetPersonne(int id)
        {
            return _context.Personnes.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool CreatePersonne(Personne personne)
        {            

            _context.Add(personne);
            return Save();
        }
        public bool UpdatePersonne(Personne personne)
        {
            _context.Update(personne);
            return Save();
        }

        public bool DeletePersonne(Personne personne)
        {
            _context.Remove(personne);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public int ActualAge(Personne personne)
        {
            // Save today's date.
            var today = DateTime.Today;
            // calculate the age from the birthday Date
            return ( today.Year - personne.BirthDay.Year);           
        }
    }
}
