using System.Globalization;
using ApClientes;
using ApClientes.repositorios;
using AppClientes.interfaces;
using AppClientes.model;
namespace AppClientes;

class Program
{
    static ClienteRepositorio _clienteRepositorio = new ClienteRepositorio();
    static readonly IFuncionario _funcionario = new FuncionarioRepositorio();

    static void Main(string[] args)
    {
        var cultura = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = cultura;
        Thread.CurrentThread.CurrentUICulture = cultura;
        _funcionario.LerDados();

        _clienteRepositorio.LerDados();

        while (true)
        {
            string respostaLogin = Login.Logar();
            if (respostaLogin == "false") break;
            menu.Inicial.MenuInicial();
            Console.ReadKey();
        }
    }

}
