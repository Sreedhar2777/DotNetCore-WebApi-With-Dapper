using DapperDemoData.Models;
using DapperDemoData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await personRepository.GetPeople();
            return Ok(people);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await personRepository.GetPersonById(id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("invalid Data");
            }
            var result = await personRepository.AddPerson(person);
            if (!result)
            {
                return BadRequest("could not save to data");
            }
            return Ok();
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Person newperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid Data");
            }
            var person = await  personRepository.GetPersonById(id);
            if(person == null)
            {
                return NotFound();
            }
            newperson.Id = id;
            var result = await personRepository.UpdatePerson(newperson);
            if (!result)
            {
                return BadRequest("could not save to data");
            }
            return Ok();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await personRepository.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            var result = await personRepository.DeletePerson(id);
            if (!result)
            {
                return BadRequest("could not save to data");
            }
            return Ok();
        }
    }
}
