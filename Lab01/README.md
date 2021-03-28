# Lab01

Singleton and Factory Method patterns implementation project.

`Juicer` singleton class is used for extracting `Juice` from different fruits, vegetables and berries which are subclasses of `Juicy` abstract class. Before extracting `Juicy`-like `Product`'s must be peeled and sliced by calling `Peel()` and `Slice()` methods, that can be done in a chain: `someProduct.Slice().Peel()`, after that we passing this construction into `Juicer`'s method `makeSomeJuice()`, for example:

```
juicer.makeSomeJuice(someProduct.Slice().Peel())
```

`Juicy` has the base class `Product` which is the *product* of `ProductBag` abstract class. There are several *concrete factories*: `BerryPackage`, `FruitBasket` and `VegetableBag` which are used to create instances of corresponding *concrete product* classes.

Information about possible contents of `ProductBag` is needed to create instance of it's subclasses. Then we can pass some product names to it calling `PutProduct()` method and take resulting product by calling `TakeProduct()` which returns `Product`-like object.

Every `Juice` stores information about ingredients. Inside `Glass` different `Juice`'s can mix to create `Juice` with a new combination of ingredients.
