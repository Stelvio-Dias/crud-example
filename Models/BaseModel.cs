using System.ComponentModel.DataAnnotations;

namespace Models;

public abstract class BaseModel
{
    [Key]
    public int Id { get; set; }
}