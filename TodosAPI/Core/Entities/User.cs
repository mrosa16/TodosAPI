using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodosAPI.Core.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public ICollection<Tarefa> Tasks { get; set; } = [];

    }
}
