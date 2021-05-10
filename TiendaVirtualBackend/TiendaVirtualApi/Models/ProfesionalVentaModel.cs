using System.Collections.Generic;
using Entity;

namespace Models
{
  public class ProfesionalVentaModel
  {
    public class ProfesionalVentaInputModel
    {
      public Usuario Usuario { get; set; }
    }

    public class ProfesionalVentaViewModel : ProfesionalVentaInputModel
    {
      public ProfesionalVentaViewModel(ProfesionalVenta profesionalVenta)
      {
        Usuario = profesionalVenta.Usuario;
        IdProfesionalVenta = profesionalVenta.Id;
      }
      public int IdProfesionalVenta { get; set; }
    }
  }
}