using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.model
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        //fazer um dicionario de produtos e quantidade
        public Dictionary<Produto, int> Produtos { get; set; }
        public decimal ValorTotal { get; set; }

    }
}