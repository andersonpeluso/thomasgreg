

CREATE TABLE LOGGER (
    ID INT IDENTITY(1,1) PRIMARY KEY,  -- Chave prim�ria auto-incremental
    CLASSE NVARCHAR(100) NOT NULL,     -- Nome da classe onde o log foi gerado
    METODO NVARCHAR(100) NOT NULL,     -- Nome do m�todo onde o log foi gerado
    ROTINA NVARCHAR(100) NOT NULL,     -- Tipo de rotina (ex: ERRO, INFO)
    MENSAGEM NVARCHAR(MAX) NOT NULL,   -- Mensagem de log
    CRIADO DATETIME NOT NULL DEFAULT GETDATE()  -- Data e hora da cria��o do log
);