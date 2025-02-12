/* Copyright (c) 2023 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2023 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/

import { Directive } from "@angular/core";
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from "@angular/forms"; 

@Directive({
    selector: '[whiteSpaceValidator]',
    providers: [{ provide: NG_VALIDATORS, useExisting: WhiteSpaceValidatorDirective, multi: true }]
})
export class WhiteSpaceValidatorDirective implements Validator {   

    validate(control: AbstractControl): ValidationErrors | null {
        if (control && control.value) {
                      
            if (!control.value.replace(/\s/g, '')) {                

                return { 'whiteSpace': true };
            }
        }

        return null;
    }
}

