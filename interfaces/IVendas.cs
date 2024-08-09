using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.model;

namespace AppClientes.interfaces
{
    public interface IVendas : IGenerica
    {
        Venda? VisualizarProximo();
        void Vender(decimal? valorTotal);
    }
}