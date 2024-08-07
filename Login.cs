using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApClientes.repositorios;

namespace AppClientes
{
    public class Login
    {
        public static string Logar()
        {
        Logar:
            Console.Clear();
            Console.WriteLine("Para Sair Aperte Enter com o Campo Vazio");

            Console.WriteLine("Digite seu usuário:");
            string user = Console.ReadLine();

            if (string.IsNullOrEmpty(user)) return "false";

            Console.WriteLine("Digite sua senha:");
            string password = Console.ReadLine();

            FuncionarioRepositorio funcionarioRepositorio = new FuncionarioRepositorio();
            var result = funcionarioRepositorio.Logar(user, password);
            if (result)
            {
                Console.WriteLine($"Bem vindo {user}");
                return "admin";
            }
            else
            {
                Console.WriteLine("Usuário ou senha inválidos");
                Thread.Sleep(1000);
                goto Logar;
            }

        }
    }
}