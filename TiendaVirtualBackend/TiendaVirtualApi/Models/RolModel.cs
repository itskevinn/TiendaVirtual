using Entity;

namespace Models
{
  public class RolModel
  {
    public class RolInputModel
    {
      public string Nombre { get; set; }
    }
    public class RolViewModel : RolInputModel
    {
      public RolViewModel(Rol rol)
      {
        Nombre = rol.Nombre;
        IdRol = rol.Id;
      }
      public int IdRol { get; set; }
    }

  }
}