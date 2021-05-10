using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
  public abstract class BaseEntity { }

  public abstract class Entity<T> : BaseEntity, IEntity<T>
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; }
  }
}