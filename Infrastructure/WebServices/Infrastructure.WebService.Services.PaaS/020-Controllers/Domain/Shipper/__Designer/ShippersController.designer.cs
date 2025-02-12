namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Shippers Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Shippers")] 
public partial class ShippersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Shipper>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IShipper _shipper;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Shipper class
	/// </summary>
	/// <param name="shipper">shipper</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public ShippersController(COREAPPINTERFACESDOMAIN.IShipper shipper
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Shipper> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_shipper = shipper;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Shippers
	/// </summary>
	/// <returns></returns>
	[Route("Shippers")]
	[HttpGet]
	public virtual async Task<IActionResult> GetShippersAsync([FromQuery] Guid shipperUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetShipperRequest
			{
				ShipperUId = shipperUId,
				CorrelationUId = correlationUId	
			};

			var response = await _shipper!.GetShippersAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Shippers);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetShippersAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Shippers
	/// </summary>
	/// <returns></returns>
	[Route("Shippers")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeShippersAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ShippersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var shippers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Shipper>>();

				if (shippers != null && shippers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeShipperRequest
					{
						Shippers = shippers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._shipper!.MergeShippersAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeShippersAsync)} - {nameof(shippers)} is null", nameof(ShippersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
				}				
			}	
			else
			{
				var fault = new Fault
				{
					Title = "Exception has occurred",
					Message = "Empty content",
					FaultType = FaultType.Exception,
					Severity = Severity.Critical,
					ApplicationTier = ApplicationTier.Service,
				};

				actionResult = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeShippersAsync)}", nameof(ShippersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeShippersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ShippersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
