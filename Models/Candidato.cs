using System;
using System.Collections.Generic;

namespace Final_IngenieriaSoftware.Models;

public partial class Candidato
{
    public int Idcandidatos { get; set; }

    public string? Nombre { get; set; }

    public string? Partido { get; set; }

    public int? Afiliados { get; set; }

    public virtual ICollection<Votacione> Votaciones { get; set; } = new List<Votacione>();
}
