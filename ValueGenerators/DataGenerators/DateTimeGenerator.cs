namespace Faker.ValueGenerators.DataGenerators;

public class DateTimeGenerator:IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {

        int year = context.Random.Next();
        
        int month = context.Random.Next(1,13);

        int day = context.Random.Next(1, DateTime.DaysInMonth(year, month)+1);
        
        return   new DateTime(year,month,day);
        
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(DateTime);
    }
}