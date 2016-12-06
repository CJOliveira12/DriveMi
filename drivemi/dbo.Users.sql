CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NCHAR(50) NOT NULL, 
    [Password] NCHAR(50) NOT NULL, 
    [Name] NCHAR(100) NOT NULL, 
    [Address] NCHAR(100) NULL, 
    [Phone] INT NOT NULL, 
    [Id_type] INT NOT NULL DEFAULT 1
)
