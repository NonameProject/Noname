
namespace Abitcareer.Business.Models
{
    public class University : BaseModel
    {
        public virtual int Rating { get; set; }

        public virtual string Link { get; set; }

        public virtual City City { get; set; }
    }
}
