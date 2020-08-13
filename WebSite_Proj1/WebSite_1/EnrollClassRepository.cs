using Class.Database;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_1
{

    public interface IEnrollClassRepository
    {
        EnrollClassModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        EnrollClassModel[] GetAll(int userId);

    }

    public class EnrollClassModel
    {
        //public int UserId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class EnrollClassRepository : IEnrollClassRepository
    {
        private readonly IClassRepository classRepository;

        public EnrollClassRepository(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public EnrollClassModel Add(int userId, int classId)
        {
            var enrollClass = DatabaseAccessor.Instance.Classes.First(t => t.ClassId == classId);
            User enrollUser = DatabaseAccessor.Instance.Users.Where(t => t.UserId == userId).First();

            enrollUser.Classes.Add(enrollClass);

            DatabaseAccessor.Instance.SaveChanges();
            
            return new EnrollClassModel
            {                
                ClassId = enrollClass.ClassId,
                Name = enrollClass.ClassName,
                Description = enrollClass.ClassDescription,
                Price = enrollClass.ClassPrice
            };
        }

        public EnrollClassModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.RetrieveClassesForStudent(userId)
                .Select(t => 
                {
                    var aClass = classRepository.GetClass(t.ClassId);

                    return new EnrollClassModel
                    {
                        ClassId = t.ClassId,
                        Name = aClass.Name,
                        Description = aClass.Description,
                        Price = aClass.Price
                    };
                }).ToArray();
            return items;
        }

        public bool Remove(int userId, int classId)
        {
            var enrollClass = DatabaseAccessor.Instance.Classes.First(t => t.ClassId == classId);
            User enrollUser = DatabaseAccessor.Instance.Users.Where(t => t.UserId == userId).First();
           
            if (enrollUser.UserId != userId)
            {
                return false;
            }


            enrollUser.Classes.Remove(enrollClass);
            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }





}