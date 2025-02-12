/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Guid } from '../../core/helpers/guid';

export class Theme {
    public static Themes: Array<Theme> = [
        new Theme('00000200-0010-0000-0000-000000000000', '', 'ServiceComposer', 'c-theme-ServiceComposer'),
        new Theme('00000500-0010-0000-0000-000000000000', 'WebShop', 'WebShop', 'c-theme-WebShop')
    ];

    public ThemeUId: Guid;
    public Title: string;
    public Value: string;
    public ThemeCssClassName: string;

    constructor(themeUId: Guid, value: string, title: string, themeCssClassName: string) {
        this.ThemeUId = themeUId;
        this.Title = title;
        this.Value = value;
        this.ThemeCssClassName = themeCssClassName;
    }


    public static getTheme(themeUId: Guid): Theme {

        return Theme.Themes.find((x: Theme): boolean => x.ThemeUId === themeUId) || {} as Theme;
    }
}

