using AppTask.Models.Interfaces;

namespace AppTask.Models.Services
{
    public class RegraTarefa : IRegraTarefa
    {
        public bool validarDataFinal(DateTime? dataInicial, DateTime? dataFinal)
        {
            return dataFinal>=dataInicial;
        }
    }
}
