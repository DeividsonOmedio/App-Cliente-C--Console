using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.interfaces;
using AppClientes.repositorios;

namespace AppClientes.menu
{
    public class Vendas
    {
        static readonly IVendas _pedidos = new VendasRepositorio();

        public static void Menu()
        {
            // Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t| Vendas              |");
            Console.WriteLine("\t| --------------------  |");
            Console.WriteLine("\t| 1 - Novo Pedido       |");
            Console.WriteLine("\t| 2 - Exibir Pedidos    |");
            Console.WriteLine("\t| 3 - Editar Pedidos    |");
            Console.WriteLine("\t| 4 - Excluir Pedidos   |");
            Console.WriteLine("\t| 5 - Sair              |");
            Console.WriteLine("\t| --------------------  |\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            EscolherOpcao();
        }


        static void EscolherOpcao()
        {
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();

            switch (int.Parse(opcao))
            {
                case 1:
                    {
                        _pedidos.Cadastrar();
                        Menu();
                        break;
                    }
                case 2:
                    {
                        _pedidos.Exibir();
                        Menu();
                        break;
                    }
                case 3:
                    {
                        _pedidos.Editar();
                        Menu();
                        break;
                    }
                case 4:
                    {
                        _pedidos.Excluir();
                        Menu();
                        break;
                    }
                case 5:
                    {
                        _pedidos.GravarDados();
                        Console.WriteLine("Saindo...");
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Opção Inválida!");
                        break;
                    }
            }
        }
    }
}