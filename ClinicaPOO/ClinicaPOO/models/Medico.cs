using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaPOO.Models
{
    public class Medico : Pessoa
    {
        public string Crm { get; set; } = string.Empty;

        public string Especialidade { get; set; } = string.Empty;

        public Medico(
            int codigo,
            string nome,
            string cpf,
            string telefone,
            string crm,
            string especialidade)
            : base(codigo, nome, cpf, telefone)
        {
            Crm = crm;
            Especialidade = especialidade;
        }

        public override void ExibirFicha()
        {
            Console.WriteLine(
                $"[MÉDICO] Cód: {Codigo} | " +
                $"Dr(a). {Nome} | CRM: {Crm} | " +
                $"Esp: {Especialidade}");
        }
    }
}
