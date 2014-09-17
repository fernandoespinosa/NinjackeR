using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NinjackeR.Pompidou.Reflection
{
    public struct NamedValue
    {
        private readonly string _name;
        private readonly object _value;
        private readonly Type _type;

        private NamedValue(string name, Type type, object value)
        {
            _name = name;
            _type = type;
            _value = value;
        }

        public static NamedValue FromProperty(PropertyInfo propertyInfo, object target)
        {
            object value;
            try
            {
                value = propertyInfo.GetValue(target);
            }
            catch (Exception ex)
            {
                value = string.Format("Property could not be evaluated: {0}", ex.Message);
            }
            return new NamedValue(propertyInfo.Name, propertyInfo.PropertyType, value);
        }

        public static NamedValue FromParameter(ParameterInfo parameterInfo, object argumentValue)
        {
            return new NamedValue(parameterInfo.Name, parameterInfo.ParameterType, argumentValue);
        }

        public static NamedValue FromField(FieldInfo fieldInfo, object target)
        {
            object value;
            try
            {
                value = fieldInfo.GetValue(target);
            }
            catch (Exception ex)
            {
                value = string.Format("Field could not be evaluated: {0}", ex.Message);
            }
            return new NamedValue(fieldInfo.Name, fieldInfo.FieldType, value);
        }

        public static IEnumerable<NamedValue> FromAnonymousData(object anonymousData)
        {
            return anonymousData
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetIndexParameters().Length == 0)
                .Select(p => FromProperty(p, anonymousData));
        }

        public string Name
        {
            get { return _name; }
        }

        public object Value
        {
            get { return _value; }
        }

        public Type Type
        {
            get { return _type; }
        }
    }
}
