using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.interfaces
{
    public interface IFuncionario
    {
        void Cadastrar();
        void Exibir();
        void Editar();
        void Excluir();
        void GravarDados();
        void LerDados();
    }
}