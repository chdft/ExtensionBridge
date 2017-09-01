# ExtensionBridge

ExtensionBridge is a simple framework for extensions/plugins/addons (3rd party code running in your application, which is not known at compile time).

## FAQ
Note: these questions aren't really asked, they are just questions I came across while developing this library.

### Why don't you use MEF?
MEF (Managed Extendibility Framework) is a very nice framework for extendibility included in the .Net Framework 4.0. However (in my opinion) it is quite overkill for *simple* extensions. MEF is more geared towards satisfying dependencies than providing optional extension points.
So yes, I know I reinvented the wheel and yes, it isn't as round - but thats exactly what I wanted.

### Why are the Attributes needed?
I decided to go the route of explicitly declaring a class an extension for two reasons:
1) a class may implement the Contract, but not be *intended* as an extension
2) it adds clarity to the code (you don't have an unused class floating around)

## Usage
There is an example application consisting of one [host](/ExtensionBridge.Sample.Host) and one [extension-library](/ExtensionBridge.Sample.Extension1). There are a lot of inline comments explaining how stuff works.
