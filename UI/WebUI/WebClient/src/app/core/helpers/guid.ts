/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

export class Guid {
	static newGuid(): string {
		return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c: string): string => {
			const r: number = Math.random() * 16 | 0;
			const v: number = c === 'x' ? r : (r & 0x3 | 0x8);

			return v.toString(16);
		});
	}

	static empty = '00000000-0000-0000-0000-000000000000';
}

