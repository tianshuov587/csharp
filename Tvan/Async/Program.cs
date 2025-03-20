using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main call first");
            Async_Func async_Func = new Async_Func();

            // 等待其执行完成
            //async_Func.CallAsyncMethod().Wait();
            //async_Func.CallAsyncMethod();
            //async_Func.CallCalculateAsync();
            //async_Func.HandleExceptionAsync();
            //async_Func.RunMultipleTasksAsync();
            //async_Func.CancelOperationAsync().Wait();
            //Thread.Sleep(3000);

            //observe thread
            Console.WriteLine("主线程ID: " + Thread.CurrentThread.ManagedThreadId);
            await Async_Func.CallAsyncMethod_Thread();
            Console.WriteLine("主线程继续执行，线程ID: " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("开始时间: " + DateTime.Now);
            Task task1 = Async_Func.MyAsyncMethod("任务1");
            Task task2 = Async_Func.MyAsyncMethod("任务2");
            await Task.WhenAll(task1, task2);
            Console.WriteLine("结束时间: " + DateTime.Now);
            Console.WriteLine("Main call end");
        }
    }

    public class Async_Func
    {
        public static async Task MyAsyncMethod()
        {
            // 异步操作
            await Task.Delay(1000); // 模拟耗时操作
            Console.WriteLine("异步操作完成");
        }

        public async Task<int> CalculateAsync()
        {
            await Task.Delay(1000); // 模拟耗时操作
            return 42;
        }

        public async Task CallCalculateAsync()
        {
            int result = await CalculateAsync();
            Console.WriteLine($"计算结果: {result}");
        }

        public async Task CallAsyncMethod()
        {
            Console.WriteLine("调用异步方法前");
            await MyAsyncMethod();
            await CalculateAsync();
            Console.WriteLine("调用异步方法后");
        }


        public async Task ThrowExceptionAsync()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("发生了一个错误");
        }

        public async Task HandleExceptionAsync()
        {
            try
            {
                await ThrowExceptionAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"捕获异常: {ex.Message}");
            }
        }

        public async Task RunMultipleTasksAsync()
        {
            Task<int> task1 = CalculateAsync();
            Task<int> task2 = CalculateAsync();
            int[] res = await Task.WhenAll(task1, task2);
            Console.WriteLine($"计算结果: {res[0]}, {res[1]}");
        }

        //cancel the async task
        public async Task LongRunningOperationAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                Console.WriteLine($"进度: {i + 1}/10");
            }
        }

        public async Task CancelOperationAsync()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000); // 3秒后取消操作

            try
            {
                await LongRunningOperationAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("操作被取消");
            }
        }

        public static async Task CallAsyncMethod_Thread()
        {
            Console.WriteLine("调用异步方法前，线程ID: " + Thread.CurrentThread.ManagedThreadId);
            await MyAsyncMethod_Thread();
            Console.WriteLine("调用异步方法后，线程ID: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task MyAsyncMethod_Thread()
        {
            Console.WriteLine("异步方法开始，线程ID: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            Console.WriteLine("异步方法完成，线程ID: " + Thread.CurrentThread.ManagedThreadId);
        }

        internal static async Task MyAsyncMethod(string v)
        {
            Console.WriteLine($" {v}开始，线程ID: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            Console.WriteLine($" {v}完成，线程ID: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
