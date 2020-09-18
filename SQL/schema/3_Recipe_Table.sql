CREATE TABLE Recipe
(
 Id INT CONSTRAINT PK_Recipe_Id PRIMARY KEY IDENTITY,
 Name NVARCHAR(50) CONSTRAINT UQ_Recipe_Name UNIQUE NOT NULL,
 CategoryId INT CONSTRAINT FK_Recipe_To_Category REFERENCES Category (Id) NOT NULL
);