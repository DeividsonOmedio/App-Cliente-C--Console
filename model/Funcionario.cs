using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.model
{
    public class Funcionario : Generico
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Funcao { get; set; }


    }

}