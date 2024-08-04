using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClientes.interfaces;
using AppClientes.model;

namespace ApClientes.repositorios
{
    public class FuncionarioRepositorio : IFuncionario
    {
        public List<Funcionario> funcionarios = new List<Funcionario>();


        public void Cadastrar()
        {
            Console.WriteLine("Digite o nome do funcionário: ");
            var nome = Console.ReadLine();
            Console.WriteLine("Digite o usuário do funcionário: ");
            var user = Console.ReadLine();
            Console.WriteLine("Digite a senha do funcionário: ");
            var password = Console.ReadLine();
            Console.WriteLine("Digite a função do funcionário: ");
            var funcao = Console.ReadLine();

            var funcionario = new Funcionario
            {
                Nome = nome,
                User = user,
                Password = password,
                Funcao = funcao
            };

            funcionarios.Add(funcionario);
            GravarDados();
        }

        public void Editar()
        {
            Console.WriteLine("Digite o nome do funcionário que deseja editar: ");
            var nome = Console.ReadLine();
            var funcionario = funcionarios.FirstOrDefault(x => x.Nome == nome);

            if (funcionario == null)
            {
                Console.WriteLine("Funcionário não encontrado");
                return;
            }

            Console.WriteLine("Digite o novo nome do funcionário: ");
            var novoNome = Console.ReadLine();
            Console.WriteLine("Digite o novo usuário do funcionário: ");
            var novoUser = Console.ReadLine();
            Console.WriteLine("Digite a nova senha do funcionário: ");
            var novaSenha = Console.ReadLine();
            Console.WriteLine("Digite a nova função do funcionário: ");
            var novaFuncao = Console.ReadLine();

            funcionario.Nome = novoNome;
            funcionario.User = novoUser;
            funcionario.Password = novaSenha;
            funcionario.Funcao = novaFuncao;

            GravarDados();

        }

        public void Excluir()
        {
            Console.WriteLine("Digite o nome do funcionário que deseja excluir: ");
            var nome = Console.ReadLine();
            var funcionario = funcionarios.FirstOrDefault(x => x.Nome == nome);

            if (funcionario == null)
            {
                Console.WriteLine("Funcionário não encontrado");
                return;
            }

            funcionarios.Remove(funcionario);
            GravarDados();
        }

        public void Exibir()
        {
            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine($"Nome: {funcionario.Nome} - Usuário: {funcionario.User} - Função: {funcionario.Funcao}");
            }
        }

        public void GravarDados()
        {
            var diretorio = "../dados";
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var caminhoArquivo = Path.Combine(diretorio, "funcionarios.txt");
            var json = System.Text.Json.JsonSerializer.Serialize(funcionarios);
            File.WriteAllText(caminhoArquivo, json);
        }

        public void LerDados()
        {
            if (File.Exists("../dados/funcionarios.txt"))
            {
                var dados = File.ReadAllText("../dados/funcionarios.txt");
                var funcionariosArquivo = System.Text.Json.JsonSerializer.Deserialize<List<Funcionario>>(dados);
                funcionarios.AddRange(funcionariosArquivo);

                if (funcionarios.Count == 0)
                {
                    funcionarios.Add(CriarFuncionarioInicial());
                    GravarDados();
                }
            }
            else
            {
                funcionarios.Add(CriarFuncionarioInicial());
                GravarDados();
            }
        }

        public Funcionario CriarFuncionarioInicial()
        {
            return new Funcionario
            {
                Nome = "Admin",
                User = "admin",
                Password = "admin",
                Funcao = "Administrador"
            };
        }
    }
}