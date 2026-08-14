using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppTask.Models.Interfaces
{
    public interface IRegraTarefa
    {
        bool validarDataFinal(DateTime? dataInicial, DateTime? dataFinal);
    }
}
