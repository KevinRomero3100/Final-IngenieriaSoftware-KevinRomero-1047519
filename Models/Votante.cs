using System;
using System.Collections.Generic;

namespace Final_IngenieriaSoftware.Models;

public partial class Votante
{
    public int Idvotante { get; set; }

    public long? Dpi { get; set; }

    public string? Nombre { get; set; }

    public int RolIdRol { get; set; }

    public virtual Rol RolIdRolNavigation { get; set; } = null!;

    public virtual ICollection<Votacione> Votaciones { get; set; } = new List<Votacione>();
}
