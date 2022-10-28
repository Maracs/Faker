using System.Reflection;
using Faker.ValueGenerators;

namespace Faker;

public class Faker:IFaker
{
    private GeneratorContext _context;
    private ValueGenerator _valueGenerator;

    private Dictionary<Type, int> _nesting;
    private int _maxNesting = 2;
    private void AddTypeToNestingDictionary(Type type)
    {
        if (_nesting.ContainsKey(type))
        {
            _nesting[type]++;
        }
        else
        {
            _nesting.Add(type,1);
        }
    }
    
    private void DeleteTypeToNestingDictionary(Type type)
    {
        
        if (_nesting[type] > 1)
        {
            _nesting[type]++;
        }
        else
        {
            _nesting.Remove(type);
        }
        
    }

    private bool IsMaxNesting(Type type)
    {
        if (_nesting.ContainsKey(type))
        {
            if (_nesting[type] >= _maxNesting)
                return true;
        }
        
        return false;
    }
        
        public Faker()
    {
        
        _context = new GeneratorContext(new Random(), this);
        _valueGenerator = new ValueGenerator();
        
        _nesting = new Dictionary<Type, int>();

    }
    
    public T Create<T>()
    {
        return (T)Create(typeof(T)); 
    }

    public object Create(Type t)
    {

        object obj;
        
        if (IsMaxNesting(t))
        {
            obj = null;
        }
        else
        {
            AddTypeToNestingDictionary(t);
            if (_valueGenerator.CanGenerate(t))
            {
                obj = _valueGenerator.Generate(t, _context);
            }
            else
            {

                obj = CreateObject(t);
                if (obj != null)
                {
                    
                    SetFields(obj);
                    
                    SetProperties(obj);
                    
                }

                
            }
            DeleteTypeToNestingDictionary(t);
        }


        return obj;
    }

    private void SetFields(object obj)
    {
        var fields = obj.GetType().GetFields();
        
        foreach (var field in fields)
        {
            if(field.GetValue(obj) != null) continue;
            
            field.SetValue(obj,
                _valueGenerator.CanGenerate(field.FieldType)
                    ? _valueGenerator.Generate(field.FieldType, _context)
                    : Create(field.FieldType));
        }
    }
    
    private void SetProperties(object obj)
    {
        var props = obj.GetType().GetProperties();
        foreach (var prop in props)
        {
            if(prop.GetValue(obj)!=null)continue;
            
            if (prop.CanWrite)
            {
                prop.SetValue(obj,_valueGenerator.CanGenerate(prop.PropertyType)
                    ? _valueGenerator.Generate(prop.PropertyType, _context)
                    : Create(prop.PropertyType) );
            }
        }
    }
    
    
    
    private object CreateObject(Type type)
    {
        var constructors = type.GetConstructors().ToList()
            .OrderByDescending(c => c.GetParameters().Length).ToList();
        foreach (var constructor in constructors)
        {
            try
            {
                return constructor.Invoke(constructor.GetParameters()
                    .Select(info =>
                        {
                            if (_valueGenerator.CanGenerate(info.ParameterType))
                            {
                                 return   _valueGenerator.Generate(info.ParameterType, _context);
                            }
                            else
                            {
                                 return   Create(info.ParameterType);
                            }
                        }
                       ).ToArray());
            }
            catch (Exception) { }
        }
        return CheckOnDefaultValue(type);
    }
    
    
    private  object CheckOnDefaultValue(Type t)
    {
        return t.IsValueType ? Activator.CreateInstance(t) : null;
    }
    
}