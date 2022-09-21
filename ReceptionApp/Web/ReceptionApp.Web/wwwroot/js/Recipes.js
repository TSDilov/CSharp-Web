var ingredientIndex = 0;
function AddMoreIngredients() {
    $("#IngredientsConteiner").
        append("<div class='form-control'>Name: <input type='text' name='Ingredients[" + ingredientIndex + "].Name'/> Quantity: <input type='text' name='Ingredients[" + ingredientIndex + "].Quantity' /></div > ");
    ingredientIndex++;
}