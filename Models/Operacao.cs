using System;

namespace AtivaAPI.Models
{

    public enum Tipo { Aplicacao, Resgate }

    public class Operacao
    {
        public Guid Id { get; set; }
        public Tipo TipoOperacao { get; set; }
        public Guid IdFundo { get; set; }
        public string CPFCliente { get; set; }
        public decimal ValorMovimentacao { get; set; }
        public DateTime DataMovimentacao { get; set; }


    }
}

