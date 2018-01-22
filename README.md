# Dexiom.Ef.Translation
[![Build status](https://ci.appveyor.com/api/projects/status/85tnfk8noibp2t86/branch/master?svg=true)](https://ci.appveyor.com/project/jpare/dexiom-ef-translation/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Dexiom.Ef.Translation.svg)](https://www.nuget.org/packages/Dexiom.Ef.Translation/)

### Original object
```cs
public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

### Translated version of the same object
```cs
public class Product
{
    public string Code { get; set; }

    [NotMapped]
    public string Name
    {
        get => Translations[CultureInfo.CurrentCulture].Name ?? Translations.FirstOrDefault()?.Name;
        set => Translations[CultureInfo.CurrentCulture].Name = value;
    }

    [NotMapped]
    public string Description
    {
        get => Translations[CultureInfo.CurrentCulture].Description ?? Translations.FirstOrDefault()?.Description;
        set => Translations[CultureInfo.CurrentCulture].Description = value;
    }

    public virtual TranslationCollection<ProductTranslation> Translations { get; set; } = new TranslationCollection<ProductTranslation>();
}

/// <summary>
/// Translations for Product
/// </summary>
public class ProductTranslation : Translation<ProductTranslation>
{
    public int ProductId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
}
```

### Adjustments required in the Context
```cs
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
  modelBuilder.Entity<Product>()
      .HasMany(n => n.Translations)
      .WithRequired()
      .HasForeignKey(n => n.ProductId)
      .WillCascadeOnDelete();

  base.OnModelCreating(modelBuilder);
}
```
