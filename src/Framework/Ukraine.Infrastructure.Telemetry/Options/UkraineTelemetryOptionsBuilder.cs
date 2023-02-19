using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ukraine.Infrastructure.Telemetry.Options;

public class UkraineTelemetryOptionsBuilder
{
	private Action<TracerProviderBuilder> _action;

	public UkraineTelemetryOptionsBuilder(string serviceName)
	{
		_action = option => option
			.SetResourceBuilder(ResourceBuilder
				.CreateDefault()
				.AddService(serviceName))
			.SetSampler(new AlwaysOnSampler())
			.AddAspNetCoreInstrumentation()
			.AddHttpClientInstrumentation();
	}

	public void AddZipkinExporter(string serverUrl)
	{
		_action += options => options.AddZipkinExporter(o =>
			o.Endpoint = new Uri(serverUrl));
	}

	public void AddHotChocolate()
	{
		_action += options => options.AddHotChocolateInstrumentation();
	}

	public void AddSql(bool recordException)
	{
		_action += options => options.AddSqlClientInstrumentation(o =>
		{
			o.RecordException = recordException;
		});
	}

	internal Action<TracerProviderBuilder> Build()
	{
		return _action;
	}
}