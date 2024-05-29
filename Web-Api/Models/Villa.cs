using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models
{
    public class Villa
    {
        public int Id { get; set; }

       
        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
