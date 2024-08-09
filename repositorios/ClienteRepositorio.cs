using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AppClientes.interfaces;
using AppClientes.model;

namespace AppClientes
{
    public class ClienteRepositorio : ICliente
    {
        public List<Cliente> Clientes { get; private set; } = new List<Cliente>();

        public void GravarDados()
        {
            try
            {
                string diretorio = "dados";
                string caminhoArquivo = Path.Combine(diretorio, "clientes.txt");

                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                var json = JsonSerializer.Serialize(Clientes);
                File.WriteAllText(caminhoArquivo, json);
                Console.WriteLine("Dados gravados com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gravar os dados: " + ex.Message);
            }
        }

        public void LerDados()
        {
            try
            {
                string diretorio = "dados";
                string caminhoArquivo = Path.Combine(diretorio, "clientes.txt");

                if (File.Exists(caminhoArquivo))
                {
                    var dados = File.ReadAllText(caminhoArquivo);

                    var clientesArquivo = JsonSerializer.Deserialize<List<Cliente>>(dados);
                    if (clientesArquivo != null)
                    {
                        Clientes = clientesArquivo;
                    }
                    else
                    {
                        AdicionarClientesIniciais();
                        GravarDados();
                    }
                }
                else
                {
                    AdicionarClientesIniciais();
                    GravarDados();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler os dados: " + ex.Message);
            }
        }

        public void Excluir()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            if (int.TryParse(codigo, out int id))
            {
                var cliente = Clientes.FirstOrDefault(p => p.Id == id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente não encontrado! [Enter]");
                    Console.ReadKey();
                    return;
                }

                ImprimirCliente(cliente);
                Console.Write("Deseja realmente excluir o cliente? (S/N): ");
                var resposta = Console.ReadLine();
                if (resposta?.ToUpper() != "S")
                {
                    Console.WriteLine("Operação cancelada! [Enter]");
                    Console.ReadKey();
                    return;
                }

                Clientes.Remove(cliente);
                GravarDados();

                Console.WriteLine("Cliente removido com sucesso! [Enter]");
            }
            else
            {
                Console.WriteLine("Código inválido! [Enter]");
            }

            Console.ReadKey();
        }

        public void Editar()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            if (int.TryParse(codigo, out int id))
            {
                var cliente = Clientes.FirstOrDefault(p => p.Id == id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente não encontrado! [Enter]");
                    Console.ReadKey();
                    return;
                }

                ImprimirCliente(cliente);
                try
                {
                    Console.Write("Nome do cliente: ");
                    var nome = Console.ReadLine();

                    Console.Write("Data de nascimento: ");
                    var dataNascimento = DateOnly.Parse(Console.ReadLine());

                    Console.Write("Desconto: ");
                    var desconto = decimal.Parse(Console.ReadLine());

                    cliente.Nome = nome;
                    cliente.DataNascimento = dataNascimento;
                    cliente.Desconto = desconto;
                    cliente.CadastradoEm = DateTime.Now;

                    GravarDados();

                    Console.WriteLine("Cliente alterado com sucesso! [Enter]");
                    ImprimirCliente(cliente);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao editar cliente, verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Código inválido! [Enter]");
            }

            Console.ReadKey();
        }

        public void Cadastrar()
        {
            Console.Clear();

            try
            {
                Console.Write("Nome do cliente: ");
                var nome = Console.ReadLine();

                Console.Write("Data de nascimento: ");
                var dataNascimento = DateOnly.Parse(Console.ReadLine());

                Console.Write("Email: ");
                var email = Console.ReadLine();

                Console.Write("Telefone: ");
                var telefone = Console.ReadLine();

                Console.Write("Desconto: ");
                var desconto = decimal.Parse(Console.ReadLine());

                var cliente = new Cliente
                {
                    Id = Clientes.Count + 1,
                    Nome = nome,
                    DataNascimento = dataNascimento,
                    Email = email,
                    Telefone = telefone,
                    Desconto = desconto,
                    CadastradoEm = DateTime.Now
                };

                Clientes.Add(cliente);
                GravarDados();

                Console.WriteLine("Cliente cadastrado com sucesso! [Enter]");
                ImprimirCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao cadastrar cliente, verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
            }

            Console.ReadKey();
        }

        public void ImprimirCliente(Cliente cliente)
        {
            Console.WriteLine("ID.............: " + cliente.Id);
            Console.WriteLine("Nome...........: " + cliente.Nome);
            Console.WriteLine("Email..........: " + cliente.Email);
            Console.WriteLine("Telefone.......: " + cliente.Telefone);
            Console.WriteLine("Desconto.......: " + cliente.Desconto.ToString("0.00"));
            Console.WriteLine("Data Nascimento: " + cliente.DataNascimento);
            Console.WriteLine("Data Cadastro..: " + cliente.CadastradoEm);
            Console.WriteLine("------------------------------------");
        }

        public void Exibir()
        {
            LerDados();
            Console.Clear();
            Console.WriteLine("Lista de Clientes");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Total de clientes: " + Clientes.Count + "\n");
            foreach (var cliente in Clientes)
            {
                ImprimirCliente(cliente);
            }

            Console.ReadKey();
        }

        public Cliente BuscarPorId(int id)
        {
            LerDados();
            return Clientes.FirstOrDefault(p => p.Id == id);
        }

        public void AdicionarClientesIniciais()
        {
            Clientes.Add(new Cliente
            {
                Id = 1,
                Nome = "Gaby",
                DataNascimento = new DateOnly(2013, 9, 4),
                Email = "gaby@gmail.com",
                Telefone = "11999999999",
                Desconto = 0.1m,
                CadastradoEm = DateTime.Now
            });

            Clientes.Add(new Cliente
            {
                Id = 2,
                Nome = "Sofia",
                DataNascimento = new DateOnly(2015, 6, 9),
                Email = "sofia@hotmail.com",
                Telefone = "11999999999",
                Desconto = 0.1m,
                CadastradoEm = DateTime.Now
            });
        }
    }
}
