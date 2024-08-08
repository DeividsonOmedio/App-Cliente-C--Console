using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.model
{
    public class Produto : Generico
    {
        public List<string> Ingredientes { get; set; }
        public int? Quantidade { get; set; }
        public decimal Preco { get; set; }

    }
}