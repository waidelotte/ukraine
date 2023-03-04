using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Ukraine.Services.Identity.Token.Conventions;

public class GenericControllerRouteConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		if (!controller.ControllerType.IsGenericType) return;

		var name = controller.ControllerType.Name;
		var nameWithoutArity = name[..name.IndexOf('`')];
		controller.ControllerName = nameWithoutArity[..nameWithoutArity.LastIndexOf("Controller", StringComparison.Ordinal)];
	}
}