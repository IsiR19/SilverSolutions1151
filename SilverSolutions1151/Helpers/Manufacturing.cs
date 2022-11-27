//if (ModelState.IsValid)
//{
//    decimal producedQuantity = manufacturingStage.Quantity;
//    manufacturingStage.Id = Guid.NewGuid();
//    if (manufacturingStage.ProductionStage == ProductionStage.Mixing)
//    {
//        var ingredients = _context.Ingredients.Where(x => x.ProductTypeId == manufacturingStage.ProductTypes);
//        {
//            foreach (var ingredient in ingredients)
//            {

//                var calculated = (ingredient.Ratio / 100) * manufacturingStage.Quantity;
//                if (ingredient.Description == "Tobacco")
//                {
//                    manufacturingStage.Ingredients = ingredient.Id;
//                }
//                else
//                {
//                    var addStageIngredient = manufacturingStage;
//                    addStageIngredient.Id = Guid.NewGuid();
//                    addStageIngredient.Quantity = (decimal)calculated;
//                    addStageIngredient.Ingredients = ingredient.Id;
//                    _context.Add(addStageIngredient);
//                    await _context.SaveChangesAsync();
//                    producedQuantity = (decimal)+calculated;
//                }

//            }

//        };
//    }


//    manufacturingStage.Quantity = producedQuantity;
//    _context.Add(manufacturingStage);
//    await _context.SaveChangesAsync();

//    switch (manufacturingStage.ProductionStage)
//    {
//        case ProductionStage.Mixing:
//            var updatedManufacture = new ManufacturingStage
//            {
//                Id = Guid.NewGuid(),
//                ProductionStage = ProductionStage.RawTobacco,
//                Quantity = -manufacturingStage.Quantity,
//                CreatedDate = manufacturingStage.CreatedDate,
//            };

//            _context.Add(updatedManufacture);
//            await _context.SaveChangesAsync();
//            break;

//        case ProductionStage.Packing:
//            var updateMixing = new ManufacturingStage
//            {
//                Id = Guid.NewGuid(),
//                ProductionStage = ProductionStage.Mixing,
//                Quantity = -manufacturingStage.Quantity,
//                CreatedDate = manufacturingStage.CreatedDate,
//            };

//            _context.Add(updateMixing);
//            await _context.SaveChangesAsync();
//            break;

//        case ProductionStage.Complete:
//            var updatePacking = new ManufacturingStage
//            {
//                Id = Guid.NewGuid(),
//                ProductionStage = ProductionStage.Packing,
//                Quantity = -manufacturingStage.Quantity,
//                CreatedDate = manufacturingStage.CreatedDate,
//            };

//            _context.Add(updatePacking);
//            await _context.SaveChangesAsync();
//            break;
//    }

//    return RedirectToAction(nameof(Index));
////}