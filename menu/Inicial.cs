using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes.menu
{
    public static class Inicial
    {
        // criar um menu inicial que ira chamar os menus de cliente, funcionario, produto e venda
        public static void MenuInicial()
        {
            Console.WriteLine("Bem vindo ao sistema de cadastro de clientes");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Cliente");
            Console.WriteLine("2 - Funcionário");
            Console.WriteLine("3 - Produto");
            Console.WriteLine("4 - Venda");
            Console.WriteLine("5 - Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    Clientes.Menu();
                    MenuInicial();
                    break;
                case "2":
                    Funcionarios.Menu();
                    MenuInicial();
                    break;
                case "3":
                    Produtos.Menu();
                    MenuInicial();
                    break;
                case "4":
                    Vendas.Menu();
                    MenuInicial();
                    break;
                case "5":
                    Console.Write("Saindo...");
                    Thread.Sleep(400);
                    Console.Write(".");
                    Thread.Sleep(400);
                    Console.Write(".");
                    Thread.Sleep(400);
                    Console.WriteLine("Programa Encerrado!");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }
}