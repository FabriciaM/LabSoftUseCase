using System.Collections.Generic;
using ClinicaPOO.Models;

namespace ClinicaPOO.Services
{
    public class GerenciadorPrototipos
    {
        private readonly Dictionary<string, PacoteExame>
            _prototipos =
                new Dictionary<string, PacoteExame>();

        public void CadastrarPrototipo(
            string chave,
            PacoteExame prototipo)
        {
            _prototipos[chave] = prototipo;
        }

        public object? ObterClone(string chave)
        {
            if (_prototipos.ContainsKey(chave))
            {
                PacoteExame modeloOriginal =
                    _prototipos[chave];

                return modeloOriginal.Clone();
            }

            return null;
        }
    }
}
