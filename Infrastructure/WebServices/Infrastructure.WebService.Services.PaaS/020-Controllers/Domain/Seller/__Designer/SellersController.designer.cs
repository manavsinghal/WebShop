namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Sellers Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Sellers")] 
public partial class SellersController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Seller>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.ISeller _seller;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Seller class
	/// </summary>
	/// <param name="seller">seller</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public SellersController(COREAPPINTERFACESDOMAIN.ISeller seller
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Seller> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_seller = seller;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Sellers
	/// </summary>
	/// <returns></returns>
	[Route("Sellers")]
	[HttpGet]
	public virtual async Task<IActionResult> GetSellersAsync([FromQuery] Guid sellerUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetSellerRequest
			{
				SellerUId = sellerUId,
				CorrelationUId = correlationUId	
			};

			var response = await _seller!.GetSellersAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Sellers);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetSellersAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Sellers
	/// </summary>
	/// <returns></returns>
	[Route("Sellers")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeSellersAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(SellersController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var sellers = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Seller>>();

				if (sellers != null && sellers.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeSellerRequest
					{
						Sellers = sellers,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._seller!.MergeSellersAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeSellersAsync)} - {nameof(sellers)} is null", nameof(SellersController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeSellersAsync)}", nameof(SellersController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeSellersAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(SellersController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
