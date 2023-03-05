using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Ukraine.Services.Identity.Token.Application;

public class GenericControllerLocalizer<TResourceSource> : IGenericControllerLocalizer<TResourceSource>
{
	private readonly IStringLocalizer _localizer;

	public GenericControllerLocalizer(IStringLocalizerFactory factory)
	{
		if (factory == null) throw new ArgumentNullException(nameof(factory));

		var type = typeof(TResourceSource);
		var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
		var typeName = type.Name.Remove(type.Name.IndexOf('`'));
		var baseName = (type.Namespace + "." + typeName)[assemblyName!.Length..].Trim('.');

		_localizer = factory.Create(baseName, assemblyName);
	}

	public virtual LocalizedString this[string name]
	{
		get
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			return _localizer[name];
		}
	}

	public virtual LocalizedString this[string name, params object[] arguments]
	{
		get
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			return _localizer[name, arguments];
		}
	}
}