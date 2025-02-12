namespace Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead

#endregion  

/// <summary>
/// Represents Products Controller.
/// </summary>
public partial class ProductsFunction : FunctionBase<ProductsFunction>
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
	public ProductsFunction(COREAPPINTERFACESDOMAIN.IProduct product
							, ILogger<ProductsFunction> logger
							, MESSAGEHUBINTERFACES.IMessageHub messageHub
							, IFunctionContextAccessor functionContextAccessor
							) : base(logger, messageHub, functionContextAccessor)
	{
		_product = product;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Get Products
	/// </summary>
	/// <returns></returns>
	[Function(nameof(GetProductsAsync))]
	public virtual async Task<HttpResponseData> GetProductsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/Products")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();
		var productUId = GetQueryStringValue("productUId");
		var languageUId = GetQueryStringValue("languageUId");

		Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));

		try
		{
			var request = new COREAPPDATAMODELSDOMAIN.GetProductRequest
			{
				ProductUId = String.IsNullOrEmpty(productUId) ? Guid.Empty : Guid.Parse(productUId),
				LanguageUId = String.IsNullOrEmpty(languageUId) ? Guid.Empty : Guid.Parse(languageUId),
				CorrelationUId = correlationUId	
			};

			var response = await _product!.GetProductsAsync(request).ConfigureAwait(false);
			
			httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response.Products);
							
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);		

			await HandleError($"Error occurred in {nameof(GetProductsAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(GetProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}

	/// <summary>
	/// Merge Products
	/// </summary>
	/// <returns></returns>
	[Function(nameof(MergeProductsAsync))]
	public virtual async Task<HttpResponseData> MergeProductsAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products/Products")] HttpRequestData req)
	{
		HttpResponseData? httpResponseData = null;

		var correlationUId = this.GetCorrelationUId();

		try
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.Begin}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
			var content = await GetRequestBody();

			if (!String.IsNullOrEmpty(content))
			{
			    Logger.LogDebug($"Request Body : {content}", nameof(ProductsFunction));

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

					httpResponseData = response.Faults != null && response.Faults.Any() 
							? GetErrorResponse(response.Faults) 
							: GetSuccessResponse(response);
							
				}
				else
				{
					Logger.LogWarning($"{nameof(MergeProductsAsync)} - {nameof(products)} is null", nameof(ProductsFunction), this.GetType(), SHAREDKERNALLIB.ApplicationTier.Service);
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

				httpResponseData = GetErrorResponse(fault);
			}
		}
		catch (Exception ex)
		{
			httpResponseData = GetErrorResponse(ex);
			
			await HandleError($"Error occurred in {nameof(MergeProductsAsync)}", nameof(ProductsFunction), ex).ConfigureAwait(false);
		}
		finally
		{
			Logger.LogInfo(LogLevel.Information, $"{nameof(MergeProductsAsync)} - {SHAREDKERNALRESX.WebShop.End}", nameof(ProductsFunction), this.GetType(), correlationUId, Convert.ToString(SHAREDKERNALLIB.ApplicationTier.Service));
		}

		return httpResponseData;
	}


	#endregion

	#region Private Methods

	#endregion
}
