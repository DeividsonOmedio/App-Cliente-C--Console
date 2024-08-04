using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if (user == "admin" && password == "admin")
            {
                Console.WriteLine("Bem vindo Admin");
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