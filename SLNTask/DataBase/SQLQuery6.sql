USE dbTasks;
GO

CREATE TABLE CentralCusto (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    NomeCusto VARCHAR(250) NOT NULL,
    ValorAnualMeta DECIMAL NOT NULL
);
GO