using System;
using System.Collections.Generic;

namespace Final_IngenieriaSoftware.Models;

public partial class Votacione
{
    public int IdnewTable { get; set; }

    public int? VotanteIdvotante { get; set; }

    public int? CandidatosIdcandidatos { get; set; }

    public virtual Candidato? CandidatosIdcandidatosNavigation { get; set; }

    public virtual Votante? VotanteIdvotanteNavigation { get; set; }
}
