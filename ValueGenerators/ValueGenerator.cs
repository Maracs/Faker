using System.Reflection;

namespace Faker.ValueGenerators;

public class ValueGenerator:IValueGenerator
{
    private IList<IValueGenerator> _generators;

   public ValueGenerator()
    {
        _generators = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(t => t.GetInterface(nameof(IValueGenerator)) != null && t.IsClass && t != typeof(ValueGenerator))
            .Select(t => (IValueGenerator)Activator.CreateInstance(t)).ToList(); 
    }
   
   public object Generate(Type typeToGenerate, GeneratorContext context)
   {


       foreach (var generator in _generators)
       {
           if (generator.CanGenerate(typeToGenerate))
           {
               return generator.Generate(typeToGenerate, context);
           }
       }

       
       
       return null;
   }

   public bool CanGenerate(Type type)
   {
       foreach (var generator in _generators)
       {
           if (generator.CanGenerate(type))
           {
               return true;
           }
       }
       
       return false;
   }

}