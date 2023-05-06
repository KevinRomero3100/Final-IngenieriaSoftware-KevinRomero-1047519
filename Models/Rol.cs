using System;
using System.Collections.Generic;

namespace Final_IngenieriaSoftware.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Votante> Votantes { get; set; } = new List<Votante>();
}
