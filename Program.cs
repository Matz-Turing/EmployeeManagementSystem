using System;
using System.Collections.Generic;
using System.Linq;

namespace Empresa
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int Tipo { get; set; }

        public Funcionario(int id, string nome, decimal salario, DateTime dataAdmissao, int tipo)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome), "O nome não pode ser nulo.");
            Salario = salario;
            DataAdmissao = dataAdmissao;
            Tipo = tipo;
        }
    }

    public class Program
    {
        static List<Funcionario> funcionarios = new List<Funcionario>();

        static void Main(string[] args)
        {
            AdicionarFuncionario(1, "João da Silva", 3000.00m, DateTime.Now.AddMonths(-10), 1);
            AdicionarFuncionario(2, "Maria Oliveira", 2500.00m, DateTime.Now.AddMonths(-8), 2);
            AdicionarFuncionario(4, "Ana Costa", 4500.00m, DateTime.Now.AddMonths(-6), 2);
            AdicionarFuncionario(5, "José Santos", 4000.00m, DateTime.Now.AddMonths(-5), 1);

            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("\n==============================");
                Console.WriteLine("        MENU PRINCIPAL        ");
                Console.WriteLine("==============================");
                Console.WriteLine("Por favor, selecione uma opção:");
                Console.WriteLine("1. Buscar Funcionário pelo Nome");
                Console.WriteLine("2. Listar Funcionários Recentes (ID >= 4)");
                Console.WriteLine("3. Exibir Estatísticas (Quantidade e Somatório dos Salários)");
                Console.WriteLine("4. Adicionar Novo Funcionário");
                Console.WriteLine("5. Listar Funcionários por Tipo");
                Console.WriteLine("6. Sair do Programa");
                Console.WriteLine("==============================");
                Console.WriteLine("Créditos: Mateus S.");
                Console.WriteLine("GitHub: Matz-Turing");
                Console.WriteLine("==============================");
                Console.Write("Digite o número da opção desejada: ");

                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Buscar Funcionário pelo Nome.");
                        Console.Write("Por favor, digite o nome completo do funcionário (exemplo: Ana Costa): ");
                        string? nome = Console.ReadLine();
                        if (!string.IsNullOrEmpty(nome))
                        {
                            ObterPorNome(nome);
                        }
                        else
                        {
                            Console.WriteLine("O nome não pode ser nulo ou vazio.");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Listar Funcionários Recentes.");
                        ObterFuncionariosRecentes();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Exibir Estatísticas.");
                        ObterEstatisticas();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Adicionar Novo Funcionário.");
                        AdicionarFuncionarioMenu();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Listar Funcionários por Tipo.");
                        Console.Write("Digite o tipo do funcionário (número, ex: 1 para Gerente, 2 para Assistente): ");
                        string? tipoInput = Console.ReadLine();
                        if (int.TryParse(tipoInput, out int tipo))
                        {
                            ObterPorTipo(tipo);
                        }
                        else
                        {
                            Console.WriteLine("Tipo inválido! Por favor, insira um número válido.");
                        }
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Você escolheu: Sair do Programa.");
                        Console.WriteLine("Encerrando o programa... Obrigado por usar nosso sistema!");
                        sair = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Por favor, selecione um número entre 1 e 6.");
                        break;
                }

                if (!sair)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                    Console.ReadKey();
                }
            }
        }

        static void AdicionarFuncionarioMenu()
        {
            try
            {
                Console.Write("Digite o ID do funcionário (número, ex: 123): ");
                string? idInput = Console.ReadLine();
                if (int.TryParse(idInput, out int id))
                {
                    Console.Write("Digite o nome completo do funcionário (exemplo: João da Silva): ");
                    string? nome = Console.ReadLine();

                    if (string.IsNullOrEmpty(nome))
                    {
                        Console.WriteLine("O nome não pode ser nulo ou vazio.");
                        return;
                    }

                    Console.Write("Digite o salário do funcionário (exemplo: 2500.50): ");
                    string? salarioInput = Console.ReadLine();
                    if (decimal.TryParse(salarioInput, out decimal salario))
                    {
                        Console.Write("Digite a data de admissão (formato: yyyy-MM-dd, ex: 2024-08-17): ");
                        string? dataInput = Console.ReadLine();
                        if (DateTime.TryParse(dataInput, out DateTime dataAdmissao))
                        {
                            Console.Write("Digite o tipo do funcionário (número, ex: 1 para Gerente, 2 para Assistente): ");
                            string? tipoInput = Console.ReadLine();
                            if (int.TryParse(tipoInput, out int tipo))
                            {
                                AdicionarFuncionario(id, nome, salario, dataAdmissao, tipo);
                            }
                            else
                            {
                                Console.WriteLine("Tipo inválido! Por favor, insira um número válido.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Data de admissão inválida! Utilize o formato: yyyy-MM-dd.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Salário inválido! Utilize o formato numérico, exemplo: 2500.50.");
                    }
                }
                else
                {
                    Console.WriteLine("ID inválido! Por favor, insira um número válido.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Erro: Formato de entrada inválido! Por favor, siga as instruções.");
            }
        }

        static void AdicionarFuncionario(int id, string nome, decimal salario, DateTime dataAdmissao, int tipo)
        {
            if (ValidarNome(nome) && ValidarSalarioAdmissao(salario, dataAdmissao))
            {
                funcionarios.Add(new Funcionario(id, nome, salario, dataAdmissao, tipo));
                Console.WriteLine("Funcionário adicionado com sucesso!");
            }
        }

        static void ObterPorNome(string nome)
        {
            var funcionario = funcionarios.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            if (funcionario != null)
            {
                Console.WriteLine($"Funcionário encontrado: ID: {funcionario.Id}, Nome: {funcionario.Nome}, Salário: {funcionario.Salario:C}, Data de Admissão: {funcionario.DataAdmissao:yyyy-MM-dd}, Tipo: {funcionario.Tipo}");
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado.");
            }
        }

        static void ObterFuncionariosRecentes()
        {
            var funcionariosRecentes = funcionarios.Where(f => f.Id >= 4).OrderByDescending(f => f.Salario).ToList();

            Console.WriteLine("\n--- Funcionários Recentes (ID >= 4) ---");
            foreach (var funcionario in funcionariosRecentes)
            {
                Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Salário: {funcionario.Salario:C}, Data de Admissão: {funcionario.DataAdmissao:yyyy-MM-dd}, Tipo: {funcionario.Tipo}");
            }
        }

        static void ObterEstatisticas()
        {
            int quantidade = funcionarios.Count;
            decimal somatorioSalarios = funcionarios.Sum(f => f.Salario);

            Console.WriteLine($"Quantidade de Funcionários: {quantidade}");
            Console.WriteLine($"Somatório dos Salários: {somatorioSalarios:C}");
        }

        static void ObterPorTipo(int tipo)
        {
            var funcionariosPorTipo = funcionarios.Where(f => f.Tipo == tipo).ToList();

            Console.WriteLine($"\n--- Funcionários do Tipo {tipo} ---");
            foreach (var funcionario in funcionariosPorTipo)
            {
                Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Salário: {funcionario.Salario:C}, Data de Admissão: {funcionario.DataAdmissao:yyyy-MM-dd}");
            }
        }

        static bool ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("O nome não pode ser nulo ou vazio.");
                return false;
            }
            return true;
        }

        static bool ValidarSalarioAdmissao(decimal salario, DateTime dataAdmissao)
        {
            if (salario <= 0)
            {
                Console.WriteLine("O salário deve ser maior que zero.");
                return false;
            }
            if (dataAdmissao > DateTime.Now)
            {
                Console.WriteLine("A data de admissão não pode ser no futuro.");
                return false;
            }
            return true;
        }
    }
}