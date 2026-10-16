using System.Collections.Generic;
using ClinicaPOO.Models;

namespace ClinicaPOO.Services
{
    public class GerenciadorPrototipos
    {
        // Dicionário para guardar os protótipos modelo (Chave = Nome do Modelo, Valor = PacoteExame)
        private Dictionary<string, PacoteExame> _prototipos = new Dictionary<string, PacoteExame>();

        public void CadastrarPrototipo(string chave, PacoteExame prototipo)
        {
            _prototipos[chave] = prototipo;
        }

        public PacoteExame ObterClone(string chave)
        {
            if (_prototipos.ContainsKey(chave))
            {
                // Busca o modelo original e chama o nosso método Clonar() manual
                PacoteExame modeloOriginal = _prototipos[chave];
                return modeloOriginal.Clonar();
            }

            return null;
        }
    }
}
