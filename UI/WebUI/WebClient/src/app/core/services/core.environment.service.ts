/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

// Import necessary modules and services
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { CoreNotificationService } from './core.notification.service';
import { ApplicationEnvironment } from '../../shared/models/application-environment.model';

// Define the CoreEnvironmentService
@Injectable({
    providedIn: 'root'
})
export class CoreEnvironmentService {

    // Static environment variable
    public static Environment: ApplicationEnvironment = new ApplicationEnvironment();
    // Environment variable
    public environment: ApplicationEnvironment = new ApplicationEnvironment();
    // Flag to indicate sandbox mode
    public isSandbox: boolean = true;
    // Service URLs model
    public serviceUrls!: ServiceUrlModel;

    // URLs for environment and service configuration files
    getServicesUrl: string = 'assets/ServiceUrls.json';
    getLocalEnvironmentUrl: string = 'assets/Environment.json';
    getAPIEnvironmentUrl: string = 'api/Environment/GetConfiguration';

    // Constructor to inject HttpClient
    constructor(
        private readonly http: HttpClient) {
        // Log message to indicate service creation
        CoreNotificationService.logMessage('CoreEnvironmentService created');
    }

    // Initialize environment settings
    public async initializeEnvironment(): Promise<void> {
        try {
            // Get environment settings from local file or API based on sandbox mode
            const result = await lastValueFrom(this.http.get<ApplicationEnvironment>(
                this.isSandbox ? this.getLocalEnvironmentUrl : this.getAPIEnvironmentUrl, { withCredentials: true }));
            // Get API URL
            this.getAPIUrl();
            // Set environment settings
            this.environment = result;
            CoreEnvironmentService.Environment = result;
        } catch (error) {
            // Log any errors
            console.error(error);
        }
    }

    // Get API URL from service configuration file
    async getAPIUrl(): Promise<void> {
        try {
            // Get service URLs from file
            const result = await lastValueFrom(this.http.get<ServiceUrlModel>(this.getServicesUrl));
            // Set service URLs
            this.serviceUrls = result;
        } catch (error) {
            // Log any errors
            console.error(error);
        }
    }
}

// Interface for service URL model
interface ServiceUrlModel {
    [key: string]: string;
}

