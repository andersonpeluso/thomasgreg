CREATE PROCEDURE SP_LOGGER_I
    @classe NVARCHAR(100),
    @metodo NVARCHAR(100),
    @rotina NVARCHAR(100),
    @mensagem NVARCHAR(MAX),
	@criado DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LOGGER (
        CLASSE,
        METODO,
        ROTINA,
        MENSAGEM,
        CRIADO
    ) 
    VALUES (
        @classe,
        @metodo,
        @rotina,
        @mensagem,
        @criado
    );
END
GO