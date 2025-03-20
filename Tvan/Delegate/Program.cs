using System;

public delegate int MathOperation(int a, int b);

public delegate void Notify(string message);



public class Notifier
{
    // 定义事件
    public event Notify OnNotify;

    public void DoWork()
    {
        // 当工作完成后，触发事件（如果有订阅者）
        //OnNotify?.Invoke("Work is done!");
        Console.WriteLine("DoWork");
        OnNotify?.Invoke(123.ToString());
    }
}

public class DelegateExample
{
    // 定义两个方法，分别用于加法和乘法
    public static int Add(int a, int b) => a + b;
    public static int Multiply(int a, int b) => a * b;

    public static void Main()
    {
        // 用 Add 方法初始化委托
        MathOperation op = Add;
        Console.WriteLine("Add: " + op(3, 4));  // 输出 7

        // 改变委托指向为 Multiply 方法
        op = Multiply;
        Console.WriteLine("Multiply: " + op(3, 4));  // 输出 12


        Notifier notifier = new Notifier();
        // 使用 Lambda 表达式订阅事件

        void Handler(string message)
        {

            // 使用 Thread.Sleep 方法进行延迟
            Console.WriteLine(message);
            Thread.Sleep(3000);
            Console.WriteLine("Handler: {0}",message);
        }
        notifier.OnNotify += Handler;
        //notifier.OnNotify += message => Console.WriteLine("Received: " + message);
        notifier.DoWork();
    }
}


