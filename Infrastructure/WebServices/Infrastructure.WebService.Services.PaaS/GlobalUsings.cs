global using System.Globalization;
global using System.Collections;
global using System.Collections.Generic;
global using System.Security.Claims;
global using System.Text.Json;
global using System.Net;
global using System.Text.Json.Serialization;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Identity.Web;
global using Microsoft.AspNetCore.OData;
global using Microsoft.AspNetCore.OData.Query;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Rewrite;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Primitives;
global using Accenture.OAAA.Cryptography;
global using Scalar.AspNetCore;
global using RateLimiting.RateLimiter;
global using Accenture.WebShop.MessageHub;
global using MESSAGEHUBENUMS = Accenture.WebShop.MessageHub.Enumerators;
global using MESSAGEHUBMODELS = Accenture.WebShop.MessageHub.Models;
global using MESSAGEHUBINTERFACES = Accenture.WebShop.MessageHub.Interfaces;

global using Accenture.WebShop.DataCache;
global using System.Text.RegularExpressions;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;

global using Accenture.WebShop.Infrastructure.WebService.SharedLibrary.Extensions;
global using INFRAWEBSERVICESHAREDLIB = Accenture.WebShop.Infrastructure.WebService.SharedLibrary.Libraries;
global using Accenture.WebShop.SharedKernal.Libraries;
global using Accenture.WebShop.Infrastructure.DataRepositories;
global using Accenture.WebShop.Infrastructure.DataEntities.SqlServer;
global using Accenture.WebShop.Core.Application.Models;
global using COREDOMAINDATAMODELSDOMAINENUM = Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;

global using INFRAWEBSERVICESERVICES = Accenture.WebShop.Infrastructure.WebService.Services.PaaS;
global using INFRAWEBSERVICESERVICESDOMAIN = Accenture.WebShop.Infrastructure.WebService.Services.PaaS.Domain;
global using Obfuscator = Accenture.WebShop.Obfuscator;
global using SHAREDKERNALRESX = Accenture.WebShop.SharedKernal.Resources;
global using SHAREDKERNALLIB = Accenture.WebShop.SharedKernal.Libraries;
global using COREDOMAINDATAMODELS = Accenture.WebShop.Core.Domain.DataModels;
global using COREDOMAINDATAMODELSDOMAIN = Accenture.WebShop.Core.Domain.DataModels.Domain;
global using COREAPPDATAREPOMODELS = Accenture.WebShop.Core.Application.DataRepositories.DataModels;
global using COREAPPDATAREPOMODELSDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.DataModels.Domain;
global using COREAPPDENTINTERFACES = Accenture.WebShop.Core.Application.DataEntities.Interfaces;
global using COREAPPDENTINTERFACESDOMAIN = Accenture.WebShop.Core.Application.DataEntities.Interfaces.Domain;
global using MESSAGEHUB = Accenture.WebShop.MessageHub;
global using COREAPPINTERFACESDOMAIN = Accenture.WebShop.Core.Application.Interfaces.Domain;
global using COREAPPDREPOINTERFACES = Accenture.WebShop.Core.Application.DataRepositories.Interfaces;
global using COREAPPDREPOINTERFACESDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;
global using COREAPPDATAMODELS = Accenture.WebShop.Core.Application.DataModels;
global using COREAPPDATAMODELSDOMAIN = Accenture.WebShop.Core.Application.DataModels.Domain;
global using INFRAWEBSERVICESHARED = Accenture.WebShop.Infrastructure.WebService.SharedLibrary;
global using INFRADATAENTITYMSSQL = Accenture.WebShop.Infrastructure.DataEntities.SqlServer;
global using INFRADATAENTITYMSSQLDOMAIN = Accenture.WebShop.Infrastructure.DataEntities.SqlServer.Domain;
global using INFRADATAREPO = Accenture.WebShop.Infrastructure.DataRepositories;
global using INFRADATAREPODOMAIN = Accenture.WebShop.Infrastructure.DataRepositories.Domain;
global using COREAPPMODELS = Accenture.WebShop.Core.Application.Models;
global using COREAPPMODELSDOMAIN = Accenture.WebShop.Core.Application.Models.Domain;
global using DataCache = Accenture.WebShop.DataCache;



