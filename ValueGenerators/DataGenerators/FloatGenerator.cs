namespace Faker.ValueGenerators.DataGenerators;

public class FloatGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
       
        return context.Random.NextSingle()+context.Random.Next(10);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(float);
    }
}