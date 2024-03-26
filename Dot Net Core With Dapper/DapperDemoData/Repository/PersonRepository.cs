using DapperDemoData.Data;
using DapperDemoData.Models;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemoData.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess dataAccess;

        public PersonRepository(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                if (person == null || string.IsNullOrWhiteSpace(person.Name) || string.IsNullOrWhiteSpace(person.Email))
                {
                    throw new ArgumentException("Person data is invalid.");
                }

                string query = "insert into dbo.person(name,email) values(@name,@email)";

                await dataAccess.SaveData(query, new { Name = person.Name, Email = person.Email });
                return true;
            }
            catch (Exception ex)
            {
               // logger.LogError(ex, "Error occurred while adding person: {Person}", person);

                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string query = "delete from person where id=@Id";
                await dataAccess.SaveData(query, new { Id = id });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            string query = "select * from dbo.person";
            var people = await dataAccess.GetData<Person, dynamic>(query, new { });
            return people;
        }

        public async Task<Person> GetPersonById(int id)
        {
            string query = "select * from dbo.person where id=@Id";
            IEnumerable<Person> people = await dataAccess.GetData<Person, dynamic>(query, new { Id = id });
            return people.FirstOrDefault();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string query = "Update dbo.person set name=@Name,email=@Email where id=@Id";
                await dataAccess.SaveData(query, person);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
