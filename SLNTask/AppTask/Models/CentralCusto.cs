using System;
using System.Collections.Generic;

namespace AppTask.Models;

public partial class CentralCusto
{
    public int Codigo { get; set; }

    public string NomeCusto { get; set; } = null!;

    public decimal ValorAnualMeta { get; set; }
}
