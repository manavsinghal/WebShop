global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Data;

global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using MSLOGGING = Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging;
global using Accenture.WebShop.MessageHub;
global using MESSAGEHUBENUMS = Accenture.WebShop.MessageHub.Enumerators;
global using MESSAGEHUBMODELS = Accenture.WebShop.MessageHub.Models;
global using MESSAGEHUBINTERFACES = Accenture.WebShop.MessageHub.Interfaces;

global using Accenture.WebShop.SharedKernal.Libraries;
global using SHAREDKERNALRESX = Accenture.WebShop.SharedKernal.Resources;
global using COREDOMAINDATAMODELSDOMAINENUM = Accenture.WebShop.Core.Domain.DataModels.Domain.Enumerators;
global using COREDOMAINDATAMODELSENUM = Accenture.WebShop.Core.Domain.DataModels.Enumerators;
global using INFRADATAENTITYMSSQL = Accenture.WebShop.Infrastructure.DataEntities.SqlServer;
global using INFRADATAENTITYMSSQLDOMAIN = Accenture.WebShop.Infrastructure.DataEntities.SqlServer.Domain;
global using Obfuscator = Accenture.WebShop.Obfuscator;
global using SHAREDKERNALLIB = Accenture.WebShop.SharedKernal.Libraries;
global using COREDOMAINDATAMODELS = Accenture.WebShop.Core.Domain.DataModels;
global using COREDOMAINDATAMODELSDOMAIN = Accenture.WebShop.Core.Domain.DataModels.Domain;
global using COREAPPDATAREPOMODELS = Accenture.WebShop.Core.Application.DataRepositories.DataModels;
global using COREAPPDATAREPOMODELSDOMAIN = Accenture.WebShop.Core.Application.DataRepositories.DataModels.Domain;
global using COREAPPDENTINTERFACES = Accenture.WebShop.Core.Application.DataEntities.Interfaces;
global using COREAPPDENTINTERFACESDOMAIN = Accenture.WebShop.Core.Application.DataEntities.Interfaces.Domain;
global using MESSAGEHUB = Accenture.WebShop.MessageHub;


