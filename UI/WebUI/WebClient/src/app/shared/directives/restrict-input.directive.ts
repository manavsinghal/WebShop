/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({ selector: '[appInputRestriction]' })
export class InputRestrictionDirective {
    @Input('appInputRestriction') appInputRestriction: string = '';
    inputElement: ElementRef;
    arabicRegex = '[\u0600-\u06FF]';

    constructor(el: ElementRef) {
        this.inputElement = el;
    }

    noSpecialChars(event: KeyboardEvent) {
        const e = <KeyboardEvent>event;
        if (e.key === 'Tab' || e.key === 'TAB') {
            return;
        }
        let k;
        k = event.keyCode;  // k = event.charCode;  (Both can be used)
        if ((k > 64 && k < 91) || (k > 96 && k < 123) || k === 8 || k === 95 || k === 45 || k === 46 || (k >= 48 && k <= 57)) {
            return;
        }
        const ch = String.fromCharCode(e.keyCode);
        const regEx = new RegExp(this.arabicRegex);
        if (regEx.test(ch)) {
            return;
        }
        e.preventDefault();
    }

    onlyAlphabets(event: KeyboardEvent) {
        const e = <KeyboardEvent>event;
        if (e.key === 'Tab' || e.key === 'TAB') {
            return;
        }
        let k;
        k = event.keyCode;  // k = event.charCode;  (Both can be used)
        if ((k > 64 && k < 91) || (k > 96 && k < 123)) {
            return;
        }
        const ch = String.fromCharCode(e.keyCode);
        const regEx = new RegExp(this.arabicRegex);
        if (regEx.test(ch)) {
            return;
        }
        e.preventDefault();
    }

    @HostListener('paste', ['$event']) onPaste(event: ClipboardEvent) {
        let regex = /[0-9]/g;
        if (this.appInputRestriction === 'noSpecialChars') {
            regex = /[a-zA-Z0-9\-_.]/g;
        }
        if (this.appInputRestriction === 'onlyAlphabets') {
            regex = /[a-zA-Z]/g;
        }
        const e = <ClipboardEvent>event;
        const pasteData = e.clipboardData?.getData('text/plain');
        let m;
        let matches = 0;
        while ((m = regex.exec(pasteData ?? '')) !== null) {
            // This is necessary to avoid infinite loops with zero-width matches
            if (m.index === regex.lastIndex) {
                regex.lastIndex++;
            }
            // The result can be accessed through the `m`-variable.
            m.forEach((match, groupIndex) => {
                if (match || groupIndex) {

                }
                matches++;
            });
        }
        if (matches === pasteData?.length) {
            return;
        } else {
            e.preventDefault();
        }
    }

    @HostListener('keypress', ['$event']) onKeyPress(event: KeyboardEvent) {
        if (this.appInputRestriction === 'noSpecialChars') {
            this.noSpecialChars(event);
        } else if (this.appInputRestriction === 'onlyAlphabets') {
            this.onlyAlphabets(event);
        } else {
            this.integerOnly(event);
        }

    }

    integerOnly(event: KeyboardEvent) {
        const e = <KeyboardEvent>event;
        if (e.key === 'Tab' || e.key === 'TAB') {
            return;
        }
        if ([46, 8, 9, 27, 13, 110].indexOf(e.keyCode) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode === 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
            (e.keyCode === 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+V
            (e.keyCode === 86 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
            (e.keyCode === 88 && e.ctrlKey === true)) {
            // let it happen, don't do anything
            return;
        }
        if (['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'].indexOf(e.key) === -1) {
            e.preventDefault();
        }
    }


}

