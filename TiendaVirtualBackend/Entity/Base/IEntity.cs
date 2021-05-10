using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}