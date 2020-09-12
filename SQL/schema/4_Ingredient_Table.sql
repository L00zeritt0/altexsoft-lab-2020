USE RecipeBook;

CREATE TABLE Ingredient
(
 Id INT CONSTRAINT PK_Ingredient_Id PRIMARY KEY IDENTITY,
 RecipeId INT CONSTRAINT FK_Ingredient_To_Recipe REFERENCES Recipe (Id) NOT NULL,
 FoodProductId INT CONSTRAINT FK_Ingredient_To_FoodProduct REFERENCES FoodProduct (Id) NOT NULL,
 Quantity NVARCHAR(50) NOT NULL
);