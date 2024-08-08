using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.interfaces;
using AppClientes.repositorios;

namespace AppClientes.menu
{
    public class Produtos
    {
        static readonly IProdutos _produtos = new ProdutoRepositorio();

        public static void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t| Cadastro de Produtos  |");
            Console.WriteLine("\t| --------------------  |");
            Console.WriteLine("\t| 1 - Cadastrar Produtos |");
            Console.WriteLine("\t| 2 - Exibir Produtos   |");
            Console.WriteLine("\t| 3 - Editar Produtos    |");
            Console.WriteLine("\t| 4 - Excluir Produtos   |");
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
                        _produtos.Cadastrar();
                        Menu();
                        break;
                    }
                case 2:
                    {
                        _produtos.Exibir();
                        Menu();
                        break;
                    }
                case 3:
                    {
                        _produtos.Editar();
                        Menu();
                        break;
                    }
                case 4:
                    {
                        _produtos.Excluir();
                        Menu();
                        break;
                    }
                case 5:
                    {
                        _produtos.GravarDados();
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