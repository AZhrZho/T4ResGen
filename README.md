# Static String Resource Generator

Generate static nls string resources for c# project using T4 (Text Template Transformation Toolkit).

## Using target file
  To use this targets file: 
  
  1) Put this file and the T4 template file (Strings.props, Strings.tt) in the same directory, anywhere on your file system. Better a relative path of your project. 

     e.g. `../build/Strings.props`, `../build/Strings.tt`

  2) Import this targets file into your project by adding the appropriate `<Import ...>`

     e.g.  `<Import Project="../build/Strings.props" />`
     
     This import statement must be included *after* the standard VB/C# targets import and 
     property difinations, as it depends on them.
     
  3) Set the properties to control how the resource files are generated, or accept the defaults.
  
      `<Nls>zh-CN;en-US</Nls>`
        
        The list of languages to generate resource files for. Semi-colon separated.
      
      `<NlsDirectory>Strings\</NlsDirectory>`
        
        The directory to place the NLS json files in. Relative to the project directory.
        Slash at the end is *required*.
        
      `<NlsNamespace>$(RootNamespace)</NlsNamespace>`
        
        The namespace to place the generated resources in, defaults to the project root namespace.
        
      `<NlsClassName>Strings</NlsClassName>`
        
        The name of the generated class.
        
      `<MSTextTemplateTargetPath>...<MSTextTemplateTargetPath>`
        
        The path to the TextTemplating targets file. It's installed with Visual Studio by default.
          
        If Visual Studio is not installed, you might need to configure the tools manually (not tested).
  
  4) Build your project. for each language, a json file is generated with the same name as the language code in the directory.
        e.g. `Strings\zh-CN.json`, `Strings\en-US.json`
     
     The json file should be a dictionary of key-value pairs, where the key is the resource name
     and the value is the resource string. Key should be a valid C# identifier, since it will be used as a property name. e.g. 

        zh-CN.json:
        ```
        {
            "Hello": "你好",
            "Goodbye": "再见"
        }
        ```

        en-US.json:
        ```
        {
            "Hello": "Hello",
            "Goodbye": "Goodbye"
        }
        ```
            
  5) Rebuid your project. The generated class will be updated with the new resources.

     Now you can access the resources in your code like this:

     ```
     Console.WriteLine(Strings.Hello);
     Console.WriteLine(Strings.Goodbye);
     ```
     
     Also supports getting resources by name:
    
     ```
     Console.WriteLine(Strings.Get("Hello"));
     Console.WriteLine(Strings.Get("Goodbye"));
     ```
     
     In xaml, you can use the resources in XAML like this:

     ```
     <TextBlock Text="{x:Static local:Strings.Hello}" />
     <TextBlock Text="{x:Static local:Strings.Goodbye}" />
     ```
            
  ## API of the generated class
  The generated class has the following API (assuming the class name is 'Strings'):
  
  - `Strings.Get(string key): string`
    
    Get the resource string by key.
    
  - `Strings.SetCulture(string culture): bool`
    
    Set the current culture. The culture should be one of the languages specified in the Nls property.
    
    Returns true if the culture was set successfully, false otherwise.
      
  - `Strings.CultureChanged: event`
  
    Event that is raised when the culture is changed.
    
    Use for dynamicly updating the UI when the culture is changed.