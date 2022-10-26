namespace Faker.ValueGenerators.DataGenerators;

public class ByteGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        return (byte)context.Random.Next(256);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(byte);
    }
}