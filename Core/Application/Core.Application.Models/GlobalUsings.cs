global using Microsoft.EntityFrameworkCore;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using Accenture.WebShop.MessageHub;
global using MESSAGEHUBENUMS =Accenture.WebShop.MessageHub.Enumerators;
global using MESSAGEHUBMODELS = Accenture.WebShop.MessageHub.Models;
global using MESSAGEHUBINTERFACES =Accenture.WebShop.MessageHub.Interfaces;
global using Accenture.WebShop.SharedKernal.Libraries;
global using SHAREDKERNALLIB = Accenture.WebShop.SharedKernal.Libraries;
global using SHAREDKERNALRESX = Accenture.WebShop.SharedKernal.Resources;
global using System.Reflection;
global using COREAPPDATAMODELSDBAPPSETTINGS = Accenture.WebShop.Core.Application.Models.DbAppSettings;

global using COREDOMAINDATAMODELSDOMAINENUM = Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;
global using COREDOMAINDATAMODELSENUM = Accenture.WebShop.Core.Domain.DataModels.Enumerators;
global using COREAPPINTERFACESDBAPPSETTINGS = Accenture.WebShop.Core.Application.Interfaces.DbAppSettings;
global using Accenture.WebShop.Obfuscator;
global using COREAPPMODELS = Accenture.WebShop.Core.Application.Models;
global using COREAPPMODELSDOMAIN = Accenture.WebShop.Core.Application.Models.Domain;
global using COREDOMAINDATAMODELS = Accenture.WebShop.Core.Domain.DataModels;
global using COREDOMAINDATAMODELSDOMAIN = Accenture.WebShop.Core.Domain.DataModels.Domain;
global using COREAPPINTERFACESDOMAIN = Accenture.WebShop.Core.Application.Interfaces.Domain;
global using COREAPPDREPOINTERFACES = Accenture.WebShop.Core.Application.DataRepositories.Interfaces;
global using COREAPPDREPOINTERFACESDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.Interfaces.Domain;
global using COREAPPDATAMODELS = Accenture.WebShop.Core.Application.DataModels;
global using COREAPPDATAMODELSDOMAIN = Accenture.WebShop.Core.Application.DataModels.Domain;
global using COREAPPDATAREPOMODELS = Accenture.WebShop.Core.Application.DataRepositories.DataModels;
global using COREAPPDATAREPOMODELSDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.DataModels.Domain;


