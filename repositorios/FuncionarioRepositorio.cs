using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AppClientes.interfaces;
using AppClientes.model;

namespace ApClientes.repositorios
{
    public class FuncionarioRepositorio : IFuncionario
    {
        public List<Funcionario> funcionarios = new List<Funcionario>();

        public FuncionarioRepositorio()
        {
            LerDados();
        }

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
            Console.Clear();

            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine($"Nome: {funcionario.Nome} - Usuário: {funcionario.User} - Função: {funcionario.Funcao}");
            }

            Console.ReadKey();
        }

        public bool Logar(string user, string password)
        {

            var funcionario = funcionarios.FirstOrDefault(x => x.User == user && x.Password == password);

            if (funcionario == null)
            {
                Console.WriteLine("Usuário ou senha inválidos");
                return false;
            }

            Console.WriteLine($"Bem-vindo {funcionario.Nome}");

            return true;
        }

        public void GravarDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "funcionarios.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var json = JsonSerializer.Serialize(funcionarios);
            File.WriteAllText(caminhoArquivo, json);
        }

        public void LerDados()
        {

            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "funcionarios.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            if (File.Exists(caminhoArquivo))
            {
                var dados = File.ReadAllText(caminhoArquivo);
                Console.WriteLine(dados);
                var clientesArquivo = JsonSerializer.Deserialize<List<Funcionario>>(dados);
                if (clientesArquivo != null)
                {
                    funcionarios.AddRange(clientesArquivo);
                    if (funcionarios.Count == 0)
                    {
                        funcionarios.Add(CriarFuncionarioInicial());
                        GravarDados();
                    }
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
                Id = 1,
                Nome = "Admin",
                User = "admin",
                Password = "admin",
                Funcao = "Administrador"
            };
        }
    }
}