namespace Faker.ValueGenerators.DataGenerators;

public class DoubleGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return context.Random.NextDouble()+context.Random.Next(10);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(double);
    }
}