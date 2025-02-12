namespace Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Products Controller.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/Domain/Products")] 
public partial class ProductsController : INFRAWEBSERVICESERVICES.ControllerBase<COREDOMAINDATAMODELSDOMAIN.Product>
{
	#region Fields

	private readonly COREAPPINTERFACESDOMAIN.IProduct _product;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the Product class
	/// </summary>
	/// <param name="product">product</param>
	/// <param name="logger">logger</param>
	/// <param name="messageHub"></param>
	public ProductsController(COREAPPINTERFACESDOMAIN.IProduct product
							, ILogger<COREDOMAINDATAMODELSDOMAIN.Product> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							) : base(logger, messageHub)
	{
		_product = product;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Products
	/// </summary>
	/// <returns></returns>
	[Route("Products")]
	[HttpGet]
	public virtual async Task<IActionResult> GetProductsAsync([FromQuery] Guid productUId, [FromQuery] Guid languageUId)
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductRequest
			{
				ProductUId = productUId,
				LanguageUId = languageUId,
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductsAsync(request).ConfigureAwait(false);
			
			actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Products);
							
		}
		catch (Exception ex)
		{
			actionResult = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductsAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	/// <summary>
	/// Merge Products
	/// </summary>
	/// <returns></returns>
	[Route("Products")]
	[HttpPost]
	public virtual async Task<IActionResult> MergeProductsAsync()
	{
		IActionResult? actionResult = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await Request.GetRawBodyStringAsync().ConfigureAwait(false);

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsController));

				var appName = this.GetHeaderValue("AppName");
				
				if (!String.IsNullOrEmpty(appName) && appName != SHAREDKERNALLIB.AppSettings.AppName)
				{
				content = this.GetReplacedContent(content, correlationUId);
				}

				var products = content.ToObject<ICollection<COREDOMAINDATAMODELSDOMAIN.Product>>();

				if (products != null && products.Any())
				{
					var request = new COREAPPDATAMODELSDOMAIN.MergeProductRequest
					{
						Products = products,
						EmailId = this.GetUserContext(),
						CorrelationUId = correlationUId 
					};

					var response = await this._product!.MergeProductsAsync(request).ConfigureAwait(false);

					actionResult = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductsAsync)} - {nameof(products)} is null", nameof(ProductsController), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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
			
			await HandleError($"Error occurred in {nameof(MergeProductsAsync)}", nameof(ProductsController), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsController), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return actionResult;
	}

	#endregion

	#region Private Methods

	#endregion
}
