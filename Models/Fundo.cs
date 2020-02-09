using System;

namespace AtivaAPI.Models
{
    public class Fundo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public decimal InvestimentoMinimo { get; set; }

    }
}
