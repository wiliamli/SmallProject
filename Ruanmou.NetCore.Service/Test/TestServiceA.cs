using Ruanmou.NetCore.Interface;
using System;

namespace Ruanmou.NetCore.Servcie
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine("A123456");
        }
    }
}
