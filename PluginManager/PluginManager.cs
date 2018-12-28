using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.CSharp;

using UpsettingBoy.CSharpPlugin;
using UpsettingBoy.CSharpPluginManager.Enums;

namespace UpsettingBoy.CSharpPluginManager
{
    public sealed class PluginManager
    {
        public SortedDictionary<string, PluginState> PluginsState;

        DirectoryInfo _pluginsDirectory;

        public PluginManager(DirectoryInfo pluginsDirectory)
        {
            if(!pluginsDirectory.Exists)
                throw new DirectoryNotFoundException(
                    $"Directory {pluginsDirectory.FullName} does not exists!");
            
            _pluginsDirectory = pluginsDirectory;
            PluginsState = new SortedDictionary<string, PluginState>();
        }

        public PluginManager(string pluginsDirectory) :
                this(new DirectoryInfo(pluginsDirectory))
        { }


        public void LoadPlugins()
        {
            var validPlugins = new List<Type>();
            foreach(var file in _pluginsDirectory.GetFiles())
            {
                if(!file.Extension.Equals(".dll")) /// invalid file extension
                   continue;
                
                Assembly dll = Assembly.LoadFile(file.FullName);
                
                bool hadValid = false;
                foreach(Type type in dll.GetExportedTypes())
                {
                    hadValid = CheckIPluginInterface(type, validPlugins);

                    if(hadValid)
                        break;
                }

                if(!hadValid)
                    PluginsState.Add(dll.GetExportedTypes()[0].Namespace
                        , PluginState.Invalid);
            }

            EnablePlugins(validPlugins);
        }

        private bool CheckIPluginInterface(Type type, List<Type> validPlugins)
        {
            foreach(Type @interface in type.GetInterfaces())
                if(@interface.Name.Equals("IPlugin"))
                {
                    validPlugins.Add(type);
                    return true;
                }
            
            return false;
        }

        private void EnablePlugins(List<Type> types)
        {
            foreach(var type in types)
            {
                dynamic instance = Activator.CreateInstance(type);

                instance.OnEnable();
                PluginsState.Add(instance.Name?? type.Namespace
                    , PluginState.Enabled);
            }
        }
    }
}