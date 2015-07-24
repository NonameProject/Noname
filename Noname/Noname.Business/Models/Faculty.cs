
namespace Abitcareer.Business.Models
{
    public class Faculty
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameEn { get; set; }

        public virtual int UniversityId { get; set; }
    }
}
