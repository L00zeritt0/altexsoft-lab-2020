WITH xxx AS
(
	SELECT * FROM Category
	WHERE Category.Id = 3
	UNION ALL
	SELECT Category.Id, Category.Name, Category.ParentId FROM Category
	JOIN xxx ON Category.ParentId = xxx.Id
)

SELECT yyy.Name AS RecipeName, FoodProduct.Name AS FoodProduct, Ingredient.Quantity FROM 
(
	SELECT TOP 3 Recipe.* FROM Recipe
	JOIN xxx ON Recipe.CategoryId = xxx.Id
	ORDER BY Recipe.CategoryId
) AS yyy

JOIN Ingredient ON yyy.Id = Ingredient.RecipeId
JOIN FoodProduct ON Ingredient.FoodProductId = FoodProduct.Id;