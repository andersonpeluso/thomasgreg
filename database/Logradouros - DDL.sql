

CREATE TABLE Logradouros (
    ID INT IDENTITY(1,1) PRIMARY KEY,  -- Identificador único do logradouro
    ClienteID INT NOT NULL,                      -- Chave estrangeira para o cliente
    Endereco NVARCHAR(100) NOT NULL,           -- Rua/Avenida do cliente
	Bairro NVARCHAR(100) NOT NULL,           -- bairro do cliente
	Cidade NVARCHAR(100) NOT NULL,           -- cidade do cliente
	Estado NVARCHAR(100) NOT NULL,           -- estado do cliente
	Complemento NVARCHAR(255) NOT NULL,           -- estado do cliente
	CEP NVARCHAR(10) NOT NULL,           -- cep do cliente
    CONSTRAINT FK_Cliente_Logradouros FOREIGN KEY (ClienteID) 
    REFERENCES Clientes(Id)               -- Relacionamento com a tabela Clientes
    ON DELETE CASCADE                            -- Se o cliente for excluído, os logradouros também serão
);

/*

DROP TABLE Logradouros;

*/
   