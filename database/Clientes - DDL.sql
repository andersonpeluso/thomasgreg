
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,  -- Identificador único do cliente
    Nome NVARCHAR(100) NOT NULL,              -- Nome do cliente
    Email NVARCHAR(255) NOT NULL UNIQUE,      -- Email único do cliente
    Logotipo NVARCHAR(MAX) NULL              -- Logotipo opcional (armazenado como binário)
);


