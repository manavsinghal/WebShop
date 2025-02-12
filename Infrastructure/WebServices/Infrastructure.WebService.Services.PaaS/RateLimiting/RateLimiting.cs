using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//using RateLimiting.Models;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace RateLimiting.RateLimiter;

public class AEHRateLimiter : IRateLimiterPolicy<String>
{
	private Func<OnRejectedContext, CancellationToken, ValueTask>? _onRejected;
	private readonly MyRateLimitOptions _options;

	public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => _onRejected;

	public AEHRateLimiter(ILogger<AEHRateLimiter> logger,
						  IOptions<MyRateLimitOptions> options)
	{
		_onRejected = (ctx, token) =>
		{
			ctx.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
			logger.LogWarning($"Request rejected by {nameof(AEHRateLimiter)}", nameof(AEHRateLimiter), this.GetType());
			return ValueTask.CompletedTask;
		};
		_options = options.Value;
	}

	public RateLimitPartition<String> GetPartition(HttpContext httpContext)
	{
		if (httpContext.User.Identity?.IsAuthenticated == true)
		{
			return RateLimitPartition.GetFixedWindowLimiter(httpContext.User.Identity.Name!,
				partition => new FixedWindowRateLimiterOptions
				{
					AutoReplenishment = true,
					PermitLimit = 100,
					Window = TimeSpan.FromMinutes(1),
				});
		}

		return RateLimitPartition.GetFixedWindowLimiter(httpContext.Request.Headers.Host.ToString(),
			partition => new FixedWindowRateLimiterOptions
			{
				AutoReplenishment = true,
				PermitLimit = 100,
				Window = TimeSpan.FromMinutes(1),
			});


		//return PartitionedRateLimiter.CreateChained(
		//        PartitionedRateLimiter.Create<HttpContext, String>(httpContext =>

		//            RateLimitPartition.GetFixedWindowLimiter(
		//                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Host.ToString(),
		//                factory: partition => new FixedWindowRateLimiterOptions
		//                {
		//                    AutoReplenishment = _options.AutoReplenishment,
		//                    PermitLimit = _options.PermitLimit,
		//                    //QueueLimit = 10,
		//                    Window = TimeSpan.FromSeconds(_options.Window)
		//                })),
		//        PartitionedRateLimiter.Create<HttpContext, String>(httpContext =>
		//            RateLimitPartition.GetConcurrencyLimiter(
		//                 partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Host.ToString(),
		//                 factory: partition => new ConcurrencyLimiterOptions
		//                 {
		//                     PermitLimit = _options.PermitLimit,
		//                     QueueLimit = _options.QueueLimit

		//                 }))
		//    );
	}
}
