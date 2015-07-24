
namespace Abitcareer.Business.Models
{
    public class Region : BaseModel
    {
        public virtual City City { get; set; }
    }
}
