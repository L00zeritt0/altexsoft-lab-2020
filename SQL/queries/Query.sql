SELECT xxx.Name AS RecipeName, FoodProduct.Name AS Ingredient, Ingredient.Quantity FROM 
(
	SELECT TOP 3 Recipe.* FROM Recipe
	JOIN Category ON Recipe.CategoryId = Category.Id
	WHERE Category.Id = 3 OR Category.ParentId = 3
	ORDER BY Category.Id
) AS xxx
JOIN Ingredient ON xxx.Id = Ingredient.RecipeId
JOIN FoodProduct ON Ingredient.FoodProductId = FoodProduct.Id