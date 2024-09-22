

CREATE TABLE LOGGER (
    ID INT IDENTITY(1,1) PRIMARY KEY,  -- Chave primária auto-incremental
    CLASSE NVARCHAR(100) NOT NULL,     -- Nome da classe onde o log foi gerado
    METODO NVARCHAR(100) NOT NULL,     -- Nome do método onde o log foi gerado
    ROTINA NVARCHAR(100) NOT NULL,     -- Tipo de rotina (ex: ERRO, INFO)
    MENSAGEM NVARCHAR(MAX) NOT NULL,   -- Mensagem de log
    CRIADO DATETIME NOT NULL DEFAULT GETDATE()  -- Data e hora da criação do log
);