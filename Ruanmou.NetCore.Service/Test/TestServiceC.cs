using Ruanmou.NetCore.Interface;
using System;

namespace Ruanmou.NetCore.Servcie
{
    public class TestServiceC : ITestServiceC
    {
        public TestServiceC(ITestServiceB iTestServiceB)
        {
        }
        public void Show()
        {
            Console.WriteLine("C123456");
        }
    }
}
