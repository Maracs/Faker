using System.Text;

namespace Faker.ValueGenerators.DataGenerators;

public class StringGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {

        var str = new StringBuilder();

        int stringSize = context.Random.Next(12);
        
        for (int i = 0; i < stringSize; i++)
        {
            str.Append((char)context.Random.Next(256));
        }

        return str.ToString();
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(string);
    }
}