namespace Faker.ValueGenerators.DataGenerators;

public class BoolGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return 1 == context.Random.Next(2);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(bool);
    }
    
}