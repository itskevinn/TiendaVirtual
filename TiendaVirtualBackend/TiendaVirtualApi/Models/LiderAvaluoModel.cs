using System.Collections.Generic;
using Entity;

namespace Models
{
  public class LiderAvaluoModel
  {
    public class LiderAvaluoInputModel
    {
      public Usuario Usuario { get; set; }
    }

    public class LiderAvaluoViewModel : LiderAvaluoInputModel
    {
      public LiderAvaluoViewModel(LiderAvaluo liderAvaluo)
      {
        IdLiderAvaluo = liderAvaluo.IdLiderAvaluo;
        Usuario = liderAvaluo.Usuario;
      }
      public int IdLiderAvaluo { get; set; }
    }
  }
}