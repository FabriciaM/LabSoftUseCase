
USE dbTasks;
GO

-- Tabela Funcionario
CREATE TABLE Funcionario (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50) NOT NULL
);
GO

-- Tabela Tarefa
CREATE TABLE Tarefa (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Descricao VARCHAR(200) NOT NULL,
    DataPlanejada DATETIME NOT NULL,
    DataIniciada DATETIME NULL,
    DataFinalizada DATETIME NULL,
    DataCancelada DATETIME NULL,
    StatusTarefa VARCHAR(30) NOT NULL,
    Prazo VARCHAR(20) NOT NULL,
    FuncionarioId INT NOT NULL,
    CONSTRAINT FK_Tarefa_Funcionario FOREIGN KEY (FuncionarioId) 
        REFERENCES Funcionario(Codigo)
);
GO

CREATE TABLE Incidente (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    DescricaoProblema VARCHAR(250) NOT NULL,
    DataIncidente DATETIME NOT NULL,
    Solucao VARCHAR(250) NULL,
    Resolvido VARCHAR(3) NOT NULL -- 'sim' ou 'nao'
);
GO

CREATE TABLE Departamento (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    DescricaoDepartamento VARCHAR(250) NOT NULL,
    Ativo VARCHAR(250) NOT NULL
   
);
GO

CREATE TABLE CentralCusto (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
	NomeCusto VARCHAR(250) NOT NULL,
    ValorAnualMeta DECIMAL NOT NULL
 
);
GO