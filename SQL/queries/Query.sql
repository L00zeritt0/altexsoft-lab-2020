USE RecipeBook;

WITH TopThreeRecipesTable AS
(
 SELECT TOP 3 * FROM Recipe 
 WHERE Recipe.CategoryId = 3
 ORDER BY SubcategoryId
)

SELECT TopThreeRecipesTable.Name, FoodProduct.Name, Quantity FROM TopThreeRecipesTable
JOIN Ingredient ON TopThreeRecipesTable.Id = Ingredient.RecipeId
JOIN FoodProduct ON FoodProductId = FoodProduct.Id