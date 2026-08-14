USE dbTasks;
GO

CREATE TABLE Departamento (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Descricao VARCHAR(250) NOT NULL,
    Ativo VARCHAR(3) NOT NULL -- 'sim' ou 'nao'
);
GO