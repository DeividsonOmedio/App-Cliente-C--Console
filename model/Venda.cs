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
        public string Cliente { get; set; }
        //fazer um dicionario de produtos e quantidade
        public Dictionary<string, int> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }

    }
}