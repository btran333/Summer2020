using System.Linq;

namespace WebSite_1
{

    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel GetClass(int class_Id);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                                               .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                               .ToArray();
            }
        }

        public ClassModel GetClass(int class_Id)
        {
            var aClass = DatabaseAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == class_Id)
                                                   .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                                   .First();
            return aClass;
        }


    }

}