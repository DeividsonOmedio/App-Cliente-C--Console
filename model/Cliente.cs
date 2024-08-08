using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.model;

namespace AppClientes
{
    public class Cliente : Generico
    {
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime CadastradoEm { get; set; }
        public decimal Desconto { get; set; }

    }
}