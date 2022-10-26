using System.Collections;

namespace Faker.ValueGenerators.DataGenerators;

public class ListGenerator:IValueGenerator
{
    
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        
        
        var genericTypeArgument = typeToGenerate.GenericTypeArguments[0];
        
        var listObject = (IList)Activator.CreateInstance(typeToGenerate);

        var length = context.Random.Next(10);
            
      
            

        for (int i = 0; i < length; i++)
        {
           //TODO add element to list list 
        }

        return listObject;
    }

    public bool CanGenerate(Type type)
    {
        return type.IsGenericType && type.GetInterfaces().Contains(typeof(IList));
    }  
}