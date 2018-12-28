# CSharp Plugin Manager
This library reduces the amount of work needed for the implementation of a plugin manager into an application.

## Usage (host application)
Firstly, add **_PluginManager_** project into yours by cloning and referencing or by **Nuget** (added later). Then, follow the example below:

```csharp
using UpsettingBoy.CSharpPluginManager;

public class DesiredClass
{
    public DesiredClass()
    {
        PluginManager manager = new PluginManager("<plugins_location>");
        manager.EnablePlugins();

        //For viewing plugins state use...
        //manager.PluginsState; (is a SortedMap)
    }
}
```

You also need to provide plugin developers an API for access your application features. 

If you are concerned about the lack of UI support for plugin implementation, I am working on it (adding a new method on **_IPlugin_** interface).

## Usage (plugin developer)
In order to your plugin to be loaded by _**PluginManager**_ it has to implement
**_IPlugin_** interface. You can add this interface by cloning and referencing 
**_Plugin_** project into yours or by the **_Nuget_** package (added later on), then, simply implements the interface in **ONLY ONE CLASS** of your plugin (_OnEnable_ method will become the entry point of your plugin). An example class:

```csharp
using UpsettingBoy.CSharpPlugin;

public class MainClass : IPlugin
{
    public string Name => "YourPluginName";
    public string Author => "YourName";

    public void OnEnable()
    {
        //your plugin stuff.
    }
}
```

In the future, it might be added new methods/properties into **_IPlugin_** so backwards compatibility migth be a problem. Watch out **_PluginManager_**
version given by the host application of your plugin.

## License
This project is under a [MIT](https://www.github.com/UpsettingBoy/CSharpPluginManager/blob/master/LICENSE) license. 

**_CSharpPluginManager_** also uses:
- **Microsoft.CSharp** -> [MIT](https://github.com/dotnet/corefx/blob/master/LICENSE.TXT) license.

Additional information of licenses on *Licenses* folder.