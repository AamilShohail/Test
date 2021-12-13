using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Context;

namespace Test.Models
{
    public class PersonRepository
    {
        private readonly ProjectDBContext projectDBContext = new ProjectDBContext();

        public List<Person> GetPeople()
        {
            return projectDBContext.People.ToList();
        }
        public Person GetPersonById(int? personId)
        {
            return projectDBContext.People.Find(personId);
        }
        public void InsertPerson(Person person)
        {
            projectDBContext.People.Add(person);
            projectDBContext.SaveChanges();
        }
        public void UpdatePerson(Person person)
        {
            Person personToUpdate = projectDBContext
                .People.SingleOrDefault(x => x.PersonId == person.PersonId);
            personToUpdate.AadharCard = person.AadharCard;
            personToUpdate.AddressInMumbai = person.AddressInMumbai;
            personToUpdate.BankAccountNumber = person.BankAccountNumber;
            personToUpdate.BankName = person.BankName;
            personToUpdate.DirectDependents = person.DirectDependents;
            personToUpdate.DoB = person.DoB;
            personToUpdate.Email = person.Email;
            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.MobileNumber = person.MobileNumber;
            personToUpdate.PANCardNo = person.PANCardNo;
            personToUpdate.PINCode = person.PINCode;
            personToUpdate.UnionMembershipNo = person.UnionMembershipNo;
            personToUpdate.Gender = person.Gender;
            personToUpdate.IFSCode = person.IFSCode;

            projectDBContext.SaveChanges();
        }
        public void DeletePerson(Person person)
        {
            Person personToDelete = projectDBContext
                .People.SingleOrDefault(x => x.PersonId == person.PersonId);
            projectDBContext.People.Remove(personToDelete);
            projectDBContext.SaveChanges();
        }
    }
}