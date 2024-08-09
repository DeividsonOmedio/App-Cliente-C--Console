using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApClientes.repositorios;
using AppClientes.interfaces;

namespace AppClientes.menu
{
    public class Funcionarios
    {
        static readonly IFuncionario _funcionario = new FuncionarioRepositorio();

        public static void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine("\n\t| Cadastro de Funcionarios     |");
            Console.WriteLine("\t| -------------------------   |");
            Console.WriteLine("\t| 1 - Cadastrar Funcionarios  |");
            Console.WriteLine("\t| 2 - Exibir Funcionarios     |");
            Console.WriteLine("\t| 3 - Editar Funcionarios     |");
            Console.WriteLine("\t| 4 - Excluir Funcionarios    |");
            Console.WriteLine("\t| 5 - Sair                    |");
            Console.WriteLine("\t| --------------------------- |\n");

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
                        _funcionario.Cadastrar();
                        Menu();
                        break;
                    }
                case 2:
                    {
                        _funcionario.Exibir();
                        Menu();
                        break;
                    }
                case 3:
                    {
                        _funcionario.Editar();
                        Menu();
                        break;
                    }
                case 4:
                    {
                        _funcionario.Excluir();
                        Menu();
                        break;
                    }
                case 5:
                    {
                        _funcionario.GravarDados();
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