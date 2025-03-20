using System;
using System.Reflection;

public class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    public void SayHello()
    {
        Console.WriteLine("Hello, " + Name);
    }
}

public class ReflectionExample
{
    public static void Main()
    {
        // 获取 Person 类型信息
        Type personType = typeof(Person);
        // 使用构造函数创建 Person 对象实例（传入 "Alice" 作为参数）
        object personInstance = Activator.CreateInstance(personType, "Alice");
        // 获取 SayHello 方法信息
        MethodInfo method = personType.GetMethod("SayHello");
        // 通过反射调用 SayHello 方法
        method.Invoke(personInstance, null);






       Reflection1.ReflectionMembersExample.ReflectionMembersExampleTest();
    }
}