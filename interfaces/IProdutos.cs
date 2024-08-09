using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.model;

namespace AppClientes.interfaces
{
    public interface IProdutos : IGenerica
    {
        Produto BuscarPorNome(string nome);
    }
}