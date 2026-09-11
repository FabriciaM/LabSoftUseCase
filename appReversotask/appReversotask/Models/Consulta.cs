using System;
using System.Collections.Generic;

namespace appReversotask.Models;

public partial class Consulta
{
    public int Codigo { get; set; }

    public DateTime DataHora { get; set; }

    public string StatusConsulta { get; set; } = null!;

    public int Pacienteid { get; set; }

    public int Medicoid { get; set; }

    public virtual Medico? Medico { get; set; } = null!;

    public virtual Paciente? Paciente { get; set; } = null!;
}
