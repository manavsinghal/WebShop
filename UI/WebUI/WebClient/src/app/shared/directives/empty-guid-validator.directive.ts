/* Copyright (c) 2023 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2023 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/

import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';

@Directive({
    selector: '[appValidateEmptyGuid]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: EmptyGuidValidatorDirective, multi: true }
    ]
})
export class EmptyGuidValidatorDirective implements Validator {
    validate ( control : AbstractControl ) : ValidationErrors | null {
        const guidPattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
        const isValid = guidPattern.test( control.value ) && control.value !== '00000000-0000-0000-0000-000000000000';
        return isValid ? null : { 'emptyGuid': true };
    }
}

