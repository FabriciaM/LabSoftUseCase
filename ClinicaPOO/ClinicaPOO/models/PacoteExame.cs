using System;
using System.Collections.Generic;

namespace ClinicaPOO.Models
{
    public class PacoteExame : ICloneable
    {
        public string NomePacote { get; set; }
        public string OrientacoesJejum { get; set; }
        public string NomePaciente { get; set; }
        public List<Procedimento> Procedimentos { get; set; }

        public PacoteExame(
            string nomePacote,
            string orientacoesJejum)
        {
            NomePacote = nomePacote;
            OrientacoesJejum = orientacoesJejum;
            NomePaciente = "NÃO ATRIBUÍDO";
            Procedimentos = new List<Procedimento>();
        }

        public void AdicionarProcedimento(
            Procedimento procedimento)
        {
            Procedimentos.Add(procedimento);
        }

        public object Clone()
        {
            // MemberwiseClone copia as propriedades básicas
            // e inicialmente copia a referência da lista.
            PacoteExame pacoteClonado =
                (PacoteExame)this.MemberwiseClone();

            // Criamos uma lista nova para não compartilhar
            // a lista do pacote original.
            pacoteClonado.Procedimentos =
                new List<Procedimento>();

            // Clonamos cada procedimento individualmente.
            foreach (Procedimento item in this.Procedimentos)
            {
                Procedimento itemCopia =
                    (Procedimento)item.Clone();

                pacoteClonado.Procedimentos.Add(
                    itemCopia);
            }

            return pacoteClonado;
        }


        public PacoteExame ClonarSuperficial()
        {
            PacoteExame pacoteRaso =
                new PacoteExame(
                    this.NomePacote,
                    this.OrientacoesJejum);

            pacoteRaso.NomePaciente =
                this.NomePaciente;

            pacoteRaso.Procedimentos =
                this.Procedimentos;

            return pacoteRaso;
        }

        public void ExibirResumo()
        {
            Console.WriteLine(
                $"\n[PACOTE]: {NomePacote} | " +
                $"Paciente: {NomePaciente}");

            Console.WriteLine(
                $"  Orientação: {OrientacoesJejum}");

            Console.WriteLine(
                "  Procedimentos inclusos:");

            decimal total = 0;

            foreach (var p in Procedimentos)
            {
                Console.WriteLine(
                    $"   - {p.Nome}: " +
                    $"R$ {p.Valor:F2}");

                total += p.Valor;
            }

            Console.WriteLine(
                $"  Total: R$ {total:F2}");
        }
    }
}
