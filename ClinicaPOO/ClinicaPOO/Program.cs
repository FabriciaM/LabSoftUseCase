using ClinicaPOO.Models;
using ClinicaPOO.Services;

var clinica = new ClinicaService();

Console.WriteLine(
    "=== SISTEMA DA CLÍNICA VIDA & SAÚDE ===\n");

// Criando pacientes
var p1 = new Paciente(
    1,
    "Maria Oliveira",
    "111.222.333-44",
    "(11) 98888-7777",
    new DateTime(1990, 5, 15));

var p2 = new Paciente(
    2,
    "João Souza",
    "555.666.777-88",
    "(11) 97777-6666",
    new DateTime(1985, 10, 20));

clinica.CadastrarPaciente(p1);
clinica.CadastrarPaciente(p2);

// Criando médicos
var m1 = new Medico(
    1,
    "Helena Rios",
    "123.456.789-00",
    "(11) 91111-2222",
    "CRM/SP 123456",
    "Cardiologia");

var m2 = new Medico(
    2,
    "Roberto Alves",
    "987.654.321-11",
    "(11) 93333-4444",
    "CRM/SP 654321",
    "Ortopedia");

clinica.CadastrarMedico(m1);
clinica.CadastrarMedico(m2);

// Testando ExibirFicha
Console.WriteLine("--- Cadastros Realizados ---");

p1.ExibirFicha();
m1.ExibirFicha();

Console.WriteLine();

// Agendando consultas
Console.WriteLine("--- Agendamentos ---");

clinica.AgendarConsulta(
    101,
    DateTime.Now.AddDays(2),
    1,
    1);

clinica.AgendarConsulta(
    102,
    DateTime.Now.AddDays(3),
    2,
    2);

// Exibindo relatório
clinica.ExibirRelatorioGeral();

// Cancelando uma consulta
Console.WriteLine(
    "\n--- Cancelamento de Consulta ---");

var consultaParaCancelar =
    clinica.Consultas
        .FirstOrDefault(c => c.Codigo == 101);

consultaParaCancelar?.Cancelar();

// Exibindo relatório atualizado
clinica.ExibirRelatorioGeral();

// Demonstração do Pattern Prototype
Console.WriteLine(
    "\n=== PATTERN PROTOTYPE: " +
    "PACOTES DE EXAMES ===");

var gerenciador =
    new GerenciadorPrototipos();

// Criando o modelo base
var checkupCardio =
    new PacoteExame(
        "Check-up Cardiológico",
        "Jejum de 8h e evitar cafeína");

checkupCardio.AdicionarProcedimento(
    new Procedimento(
        "Eletrocardiograma",
        150.00m));

checkupCardio.AdicionarProcedimento(
    new Procedimento(
        "Ecocardiograma",
        350.00m));

// Cadastrando o modelo no registry
gerenciador.CadastrarPrototipo(
    "CARDIO_PADRAO",
    checkupCardio);

Console.WriteLine(
    "\n--- Modelo Base Cadastrado ---");

checkupCardio.ExibirResumo();

// Testando a cópia profunda
Console.WriteLine(
    "\n--- Atendimento 1: " +
    "Cópia Profunda ---");

var atendimento1 =
    gerenciador.ObterClone(
        "CARDIO_PADRAO");

if (atendimento1 != null)
{
    atendimento1.NomePaciente =
        "Carlos Eduardo";

    atendimento1.AdicionarProcedimento(
        new Procedimento(
            "Holter 24h",
            250.00m));

    atendimento1.ExibirResumo();
}

// Testando a cópia superficial
Console.WriteLine(
    "\n--- Atendimento 2: " +
    "Cópia Superficial ---");

var modeloParaTeste =
    gerenciador.ObterClone(
        "CARDIO_PADRAO");

if (modeloParaTeste != null)
{
    var atendimentoErro =
        modeloParaTeste.ClonarSuperficial();

    atendimentoErro.NomePaciente =
        "Ana Maria";

    atendimentoErro.AdicionarProcedimento(
        new Procedimento(
            "Glicemia em Jejum",
            30.00m));

    Console.WriteLine(
        "\n[Atendimento da Ana Maria]");

    atendimentoErro.ExibirResumo();

    Console.WriteLine(
        "\n[Verificação] O modelo compartilhado " +
        "também foi alterado:");

    modeloParaTeste.ExibirResumo();
}

// Verificando o isolamento da cópia profunda
Console.WriteLine(
    "\n--- Verificação da Cópia Profunda ---");

Console.WriteLine(
    "O Atendimento 1 continua independente " +
    "do modelo base:");

if (atendimento1 != null)
{
    atendimento1.ExibirResumo();
}
