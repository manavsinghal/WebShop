global using System.Collections;
global using System.Globalization;
global using System.Collections.Generic;
global using System.Net;
global using System.IO;
global using System.Threading.Tasks;
global using System.Text.Json;
global using Accenture.OAAA.Cryptography;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Azure.Functions.Worker.Middleware;
global using Microsoft.IdentityModel.Protocols.OpenIdConnect;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json.Serialization;
global using Microsoft.Azure.Functions.Worker.Http;
global using System.Collections.Specialized;

global using Microsoft.Extensions.DependencyInjection;
global using MESSAGEHUBINTERFACES = Accenture.WebShop.MessageHub.Interfaces;

global using Accenture.WebShop.DataCache;
global using System.Text.RegularExpressions;

global using Accenture.WebShop.Infrastructure.WebService.SharedLibrary.Extensions;
global using Accenture.WebShop.SharedKernal.Libraries;
global using Accenture.WebShop.Infrastructure.DataRepositories;
global using Accenture.WebShop.Infrastructure.DataEntities.SqlServer;
global using Accenture.WebShop.Core.Application.Models;
global using COREDOMAINDATAMODELSDOMAINENUM = Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;

global using INFRAWEBFUNCTIONSERVICES = Accenture.WebShop.Infrastructure.WebService.Services.FaaS;
global using INFRAWEBFUNCTIONSERVICESDOMAIN = Accenture.WebShop.Infrastructure.WebService.Services.FaaS.Domain;
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
global using INFRAWEBSERVICESHARED = Accenture.WebShop.Infrastructure.WebService.SharedLibrary;
global using COREAPPDATAMODELS = Accenture.WebShop.Core.Application.DataModels;
global using COREAPPDATAMODELSDOMAIN = Accenture.WebShop.Core.Application.DataModels.Domain;
global using INFRADATAENTITYMSSQL = Accenture.WebShop.Infrastructure.DataEntities.SqlServer;
global using INFRADATAENTITYMSSQLDOMAIN = Accenture.WebShop.Infrastructure.DataEntities.SqlServer.Domain;
global using INFRADATAREPO = Accenture.WebShop.Infrastructure.DataRepositories;
global using INFRADATAREPODOMAIN = Accenture.WebShop.Infrastructure.DataRepositories.Domain;
global using COREAPPMODELS = Accenture.WebShop.Core.Application.Models;
global using COREAPPMODELSDOMAIN = Accenture.WebShop.Core.Application.Models.Domain;
global using DataCache = Accenture.WebShop.DataCache;
global using COREAPPINTERFACESDOMAIN = Accenture.WebShop.Core.Application.Interfaces.Domain;
global using COREAPPDREPOINTERFACES = Accenture.WebShop.Core.Application.DataRepositories.Interfaces;
global using COREAPPDREPOINTERFACESDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;



