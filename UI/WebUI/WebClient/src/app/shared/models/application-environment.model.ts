/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Injectable } from '@angular/core';

@Injectable()
export class ApplicationEnvironment {
    public BaseUrl: string = '';
    public ClientUrl: string = '';
    public AuthProvider: string = '';
    public TenantId: string = '';
    public ClientId: string = '';
    public RedirectUrl: string = '';
    public ApiScope: string = '';
    public ApiKey: string = '';
    public Scope: string = ''; 
    public LoggedInUserEmail: string = '';    
    public IdealTime: number = 10;
    public FunctionKey: string = '';
}

