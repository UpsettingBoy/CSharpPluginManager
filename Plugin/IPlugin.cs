using System;

namespace UpsettingBoy.CSharpPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        string Author { get; }

        void OnEnable();
    }
}