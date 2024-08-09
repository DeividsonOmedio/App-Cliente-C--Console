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
        public Dictionary<string, int> Produtos { get; set; } //dicionario de produtos e quantidade

        public decimal ValorTotal { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            string produtosFormatados = string.Join(", ", Produtos.Select(p => $"{p.Key} (Qtd: {p.Value})"));
            return $"ID: {Id}, Data: {DataVenda}, Cliente: {Cliente}, Produtos: [{produtosFormatados}], Valor Total: R${ValorTotal:F2}, Status: {Status}";
        }


    }
}