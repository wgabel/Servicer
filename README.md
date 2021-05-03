# Servicer
Servicer is an Invertion of Control system, that handles sertvices, with Constructor injection.
This is a package to be used with unity project.

### How to install
In the unity package manager choose **"Add package from Git URL"**
Paste this github generated https link.
Unity will import this package into packages.

This project has asmdef files, so after importing this package, it should be visible in the Visual studio as a separate project.

### How to edit
To be able to edit this package, you need to create a unity project and import it as mentioned above.

To push any changes to the repo, you need to clone this git repository inside the packages folder. This way it will be added as a local package, and you will be able to make changes and push them to git.

Also, as this will be added tothe packages folder, you can test in in the normal unity project as any other package.

### How to use

All services are handled by the `Services` class, located in `Fertilesky.Services` namespace.

All services need to be registered in the Servicer class before referencing them, using hte Register method.

`Servicer.Register<ServiceInterface, ServiceType>();`

Example:

```csharp

public interface IServiceOne
{
    void Foo();
}

public class ServiceOne : IServiceOne
{
    public void Foo()
    {
        Debug.Log("Foo here");
    }
}

```

To register this service call:

```csharp

void Test()
{
	Servicer.Register<IServiceOne, ServiceOne>();
}

```

to use this registered serice somehere use:

```csharp

void CallSerice()
{
	var x = Servicer.Resolve<IServiceOne>();
	x.Foo();
}

```

### Constructor Injection

Servicer can automatically inject services while creating a service.
It  needs to have it registered, before it can be injected, so order of registering services needs to be maintained.

```csharp

public interface IServiceTwo
{
    void Bar();
}

public class ServiceTwo : IServiceTwo
{
    public ServiceTwo(IServiceOne serviceOne) // serviceOne needs to be registered before this constructor is called.
    {
        serviceOne.Foo();
    }

    public void Bar()
    {
        Debug.Log("Bar here");
    }
}

public void Test()
{
	Servicer.Register<IServiceOne, ServiceOne>();
	Servicer.Register<IServiceTwo, ServiceTwo>();
	
	var x = Servicer.Resolve<IServiceTwo>();
	x.Bar();
}

```
