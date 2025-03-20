using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection1
{
    using System;
    using System.Reflection;

    public class Sample
    {
        public int Value { get; set; }
        public void PrintValue()
        {
            Console.WriteLine("Value: " + Value);
        }
    }

    public class ReflectionMembersExample
    {
        public static void ReflectionMembersExampleTest()
        {
            Type sampleType = typeof(Sample);

            Console.WriteLine("Public Methods:");
            // 获取 Sample 类型中声明的所有公共实例方法（不包括继承的方法）
            foreach (MethodInfo method in sampleType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                Console.WriteLine(method.Name);
            }

            Console.WriteLine("Public Properties:");
            // 获取所有公共属性
            foreach (PropertyInfo property in sampleType.GetProperties())
            {
                Console.WriteLine(property.Name);
            }
        }
    }

}
