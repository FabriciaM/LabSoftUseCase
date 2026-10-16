using System;

namespace ClinicaPOO.Models
{
    public class Procedimento : ICloneable
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Procedimento(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }

        // MemberwiseClone cria uma nova instância
        // copiando os valores do objeto atual.
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
