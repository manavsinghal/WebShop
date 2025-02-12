#region Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

#endregion

namespace Accenture.WebShop.Core.Domain.DataModels.Domain;

#region Namespace References

//// DO NOT PUT ANY REFERENCES HERE USE project level "GlobalUsings.cs" instead 

#endregion

/// <summary>
/// Provision Service App Settings from DB
/// </summary>
public class ProvisionServiceSettings
{
	#region Field

	#endregion

	#region Properties

	/// <summary>
	/// Template
	/// </summary>
	public String ProvisionBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String ProvisionSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String ProvisionSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String ProvisionSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String ProvisionAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// CC eMmail Address
	/// </summary>
	public String ProvisionAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String DecommissionBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String DecommissionSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String DecommissionSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String DecommissionSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String DecommissionAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// CC eMmail Address
	/// </summary>
	public String DecommissionAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String ProvisionReminderBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String ProvisionReminderSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String ProvisionReminderSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String ProvisionReminderSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String ProvisionReminderAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// Cc eMail Address
	/// </summary>
	public String ProvisionReminderAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String DecommissionReminderBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String DecommissionReminderSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String DecommissionReminderSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String DecommissionReminderSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String DecommissionReminderAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// Cc eMail Address
	/// </summary>
	public String DecommissionReminderAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String SelfServiceProvisionBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String SelfServiceProvisionSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String SelfServiceProvisionSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String SelfServiceProvisionSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String SelfServiceProvisionAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// CC eMmail Address
	/// </summary>
	public String SelfServiceProvisionAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String SelfServiceDecommissionBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String SelfServiceDecommissionSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String SelfServiceDecommissionSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String SelfServiceDecommissionSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String SelfServiceDecommissionAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// CC eMmail Address
	/// </summary>
	public String SelfServiceDecommissionAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String SelfServiceProvisionReminderBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String SelfServiceProvisionReminderSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String SelfServiceProvisionReminderSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String SelfServiceProvisionReminderSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String SelfServiceProvisionReminderAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// Cc eMail Address
	/// </summary>
	public String SelfServiceProvisionReminderAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// Template
	/// </summary>
	public String SelfServiceDecommissionReminderBody { get; set; } = default!;

	/// <summary>
	/// Subject
	/// </summary>
	public String SelfServiceDecommissionReminderSubject { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String SelfServiceDecommissionReminderSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String SelfServiceDecommissionReminderSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// To eMail Address
	/// </summary>
	public String SelfServiceDecommissionReminderAdditionalToAddress { get; set; } = default!;

	/// <summary>
	/// Cc eMail Address
	/// </summary>
	public String SelfServiceDecommissionReminderAdditionalCcAddress { get; set; } = default!;

	/// <summary>
	/// ProvisioningHelpPageContent
	/// </summary>
	public String ProvisioningHelpPageContent { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String ProvisionReminderServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Container
	/// </summary>
	public String ProvisionServiceMutexContainerName { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String ProvisionRetryServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String QueueMessageRetryServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String ProvisionStatusUpdateServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// CSS 
	/// </summary>
	public String ProvisionServiceEmailTemplateCss { get; set; } = default!;

	/// <summary>
	/// Exclude Provision Service Parameters
	/// </summary>
	public String ExcludeProvisionServiceParameterCsvFromEmail { get; set; } = default!;

	/// <summary>
	/// Worker Loop Interval
	/// </summary>
	public Int32 WorkerIntervalInSecs { get; set; }

	/// <summary>
	/// Retry Loop Interval
	/// </summary>
	public Int32 RetryIntervalInMins { get; set; }

	/// <summary>
	/// Queue Message Retry Loop Interval
	/// </summary>
	public Int32 QueueMessageRetryIntervalInMins { get; set; }

	/// <summary>
	/// Reminder Loop Interval
	/// </summary>
	public Int32 ReminderIntervalInHrs { get; set; }

	/// <summary>
	/// Status Update Loop Interval
	/// </summary>
	public Int32 StatusUpdateIntervalInMins { get; set; }

	/// <summary>
	/// IsEnabled
	/// </summary>
	public Boolean? IsEnabled { get; set; }

	/// <summary>
	/// IsRetryEnabled
	/// </summary>
	public Boolean? IsRetryEnabled { get; set; }

	/// <summary>
	/// IsQueueMessageRetryEnabled
	/// </summary>
	public Boolean? IsQueueMessageRetryEnabled { get; set; }

	/// <summary>
	/// IsReminderEnabled
	/// </summary>
	public Boolean? IsReminderEnabled { get; set; }

	/// <summary>
	/// IsStatusUpdateEnabled
	/// </summary>
	public Boolean? IsStatusUpdateEnabled { get; set; }

	/// <summary>
	/// Number of times to retry email
	/// </summary>
	public Int32 EmailRetryCount { get; set; }

	/// <summary>
	/// Disable Self Service emails
	/// </summary>
	public Boolean? IsProvisionSelfServiceEmailEnabled { get; set; }

	/// <summary>
	/// Template
	/// </summary>
	public String AssetValidationServiceMailTemplateTemplate { get; set; } = default!;

	/// <summary>
	/// Subject Prefix
	/// </summary>
	public String AssetValidationServiceMailTemplateSubjectPrefix { get; set; } = default!;

	/// <summary>
	/// Subject Suffix
	/// </summary>
	public String AssetValidationServiceMailTemplateSubjectSuffix { get; set; } = default!;

	/// <summary>
	/// Should the service send emails 
	/// </summary>
	public Boolean? IsAssetValidationEmailEnabled { get; set; }

	/// <summary>
	/// IsAssetConfigurationValidationEnabled
	/// </summary>
	public Boolean? IsAssetValidationEnabled { get; set; }

	/// <summary>
	/// IsAssetVersionEmailValidationEnabled
	/// </summary>
	public Boolean? IsAssetVersionEmailValidationEnabled { get; set; }

	/// <summary>
	/// Asset Configuration Validation Loop Interval
	/// </summary>
	public Int32 AssetValidationIntervalInMins { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String AssetValidationServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// AssetValidationServiceEmailColumns
	/// </summary>
	public String AssetValidationServiceEmailColumns { get; set; } = default!;

	/// <summary>
	/// Email Service Is Enabled
	/// </summary>
	public Boolean? IsEmailServiceEnabled { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String EmailServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// EmailServiceIntervalInMins
	/// </summary>
	public Int32 EmailServiceIntervalInMins { get; set; }

	/// <summary>
	/// EmailServiceRetryIntervalInMins
	/// </summary>
	public Int32 EmailServiceRetryIntervalInMins { get; set; }

	/// <summary>
	/// Account Service Is Enabled
	/// </summary>
	public Boolean? IsAccountServiceEnabled { get; set; }

	/// <summary>
	/// AccountServiceIntervalInMins
	/// </summary>
	public Int32 AccountServiceIntervalInMins { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String AccountServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// DbEventNotificationService Is Enabled
	/// </summary>
	public Boolean? IsDbEventNotificationServiceEnabled { get; set; }

	/// <summary>
	/// DbEventNotificationService IntervalInMins
	/// </summary>
	public Int32 DbEventNotificationServiceIntervalInMins { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String DbEventNotificationServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// UserLastLoggedInDays
	/// </summary>
	public Int32 UserLastLoggedInDays { get; set; }

	/// <summary>
	/// PurgeEmailQueueAfterDays
	/// </summary>
	public Int32 PurgeEmailQueueAfterDays { get; set; }
	
	/// <summary>
	/// IsAssessmentEmailEnabledToUser
	/// </summary>
	public Boolean? IsAssessmentEmailEnabledToUser { get; set; }

	/// <summary>
	/// StorageProvider
	/// </summary>
	public String StorageProvider { get; set; } = default!;

	/// <summary>
	/// StorageProvider
	/// </summary>
	public String ContainerName { get; set; } = default!;

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String AssessmentStatusUpdateServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// AssessmentStatusUpdateService Is Enabled
	/// </summary>
	public Boolean? IsAssessmentStatusUpdateServiceEnabled { get; set; }

	/// <summary>
	/// AssessmentStatusUpdateService IntervalInMins
	/// </summary>
	public Int32 AssessmentStatusUpdateServiceIntervalInMins { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String AssessmentRequestServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// AssessmentRequestService IntervalInMins
	/// </summary>
	public Int32 AssessmentRequestServiceIntervalInMins { get; set; }

	/// <summary>
	/// AssessmentRequestService Is Enabled
	/// </summary>
	public Boolean? IsAssessmentRequestServiceEnabled { get; set; }

	/// <summary>
	/// Obfuscator Service Is Enabled
	/// </summary>
	public Boolean? IsObfuscatorServiceEnabled { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String ObfuscatorServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// ObfuscatorServiceIntervalInMins
	/// </summary>
	public Int32 ObfuscatorServiceIntervalInMins { get; set; }

	/// <summary>
	/// ObfuscatorServiceRetryIntervalInMins
	/// </summary>
	public Int32 ObfuscatorServiceRetryIntervalInMins { get; set; }

	/// <summary>
	/// DeObfuscator Service Is Enabled
	/// </summary>
	public Boolean? IsDeObfuscatorServiceEnabled { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String DeObfuscatorServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// DeObfuscatorServiceIntervalInMins
	/// </summary>
	public Int32 DeObfuscatorServiceIntervalInMins { get; set; }

	/// <summary>
	/// DeObfuscatorServiceRetryIntervalInMins
	/// </summary>
	public Int32 DeObfuscatorServiceRetryIntervalInMins { get; set; }

	/// <summary>
	/// BlobDistributedMutex - Blob
	/// </summary>
	public String ECOnboardingServiceMutexBlobName { get; set; } = default!;

	/// <summary>
	/// ECOnboardingServiceEnabled Is Enabled
	/// </summary>
	public Boolean? IsECOnboardingServiceEnabled { get; set; }

	/// <summary>
	/// ECOnboardingService IntervalInMins
	/// </summary>
	public Int32 ECOnboardingServiceIntervalInMins { get; set; }

	/// <summary>
	/// ECOnboardingService ToAddress
	/// </summary>
	public String? ECOnboardingServiceToAddress { get; set; }

	/// <summary>
	/// ECOnboardingService CcAddress
	/// </summary>
	public String? ECOnboardingServiceCcAddress { get; set; }

	/// <summary>
	/// ECOnboardingService Subject
	/// </summary>
	public String? ECOnboardingServiceSubject { get; set; }

	/// <summary>
	/// Template
	/// </summary>
	public String ECOnboardingServiceBody { get; set; } = default!;

	/// <summary>
	/// QbPreApprovedUseCase Approval Status
	/// </summary>
	public String? QbPreApprovedUseCaseStatus { get; set; }

	/// <summary>
	/// QbNotDeterminedUseCase Approval Status 
	/// </summary>
	public String? QbNotDeterminedUseCaseStatus { get; set; }

	#endregion

	#region Constructors

	#endregion

	#region Methods

	#endregion
}

