using Microsoft.AspNetCore.Mvc;
using testwebAPIapp.DTO;
using testwebAPIapp.Interfaces;
using testwebAPIapp.Model;

namespace testwebAPIapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonneController : Controller
    {
        private readonly IPersonneRepository _personneRepository;

        public PersonneController(IPersonneRepository personneRepository)
        {
            _personneRepository = personneRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonneDTO>))]
        public IActionResult GetPersonnes()
        {

            var personnes = _personneRepository.GetPersonnes();

            IList<PersonneDTO> personneDto = new List<PersonneDTO>();

            // Save today's date.
            var today = DateTime.Today;

            foreach (var p in personnes)
            {
                PersonneDTO personneDTO = new PersonneDTO();

                personneDTO.FirstName = p.FirstName;
                personneDTO.LastName = p.LastName;
                personneDTO.BirthDay = p.BirthDay;

                personneDTO.Age =  _personneRepository.ActualAge(p);         

                personneDto.Add(personneDTO);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(personneDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePersonne([FromBody] Personne personneCreate)
        {
            if (personneCreate == null)
                return BadRequest(ModelState);

            var personne = _personneRepository.GetPersonnes()
                      .Where(p => p.FirstName.Trim().ToUpper() == personneCreate.FirstName.Trim().ToUpper()
                             && p.LastName.Trim().ToUpper() == personneCreate.LastName.Trim().ToUpper())
                      .FirstOrDefault();

            if (personne != null)
            {
                ModelState.AddModelError("", "person already exists");
                return StatusCode(422, ModelState);
            }

            var age = _personneRepository.ActualAge(personneCreate);

            if (age > 150)
            {
                ModelState.AddModelError("", "person age over than 150 years old");
            }

            if (!_personneRepository.CreatePersonne(personneCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully person created");
        }
    }
}
