using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.interfaces
{
    public interface IVendas : IGenerica
    {
        void VisualizarProximo();
        void Vender();
    }
}