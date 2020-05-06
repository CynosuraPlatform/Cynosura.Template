import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Route } from '@angular/router';
import { MatPaginatorIntl } from '@angular/material/paginator';

import { MaterialModule } from './material.module';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { CoreModule } from './core/core.module';

import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ConfigService } from './config/config.service';
import { MenuService } from './nav-menu/menu.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { MatPaginatorIntlCustom } from './mat-paginator-intl';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent
    ],
    imports: [
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
// ADD ROUTES HERE
            {
                path: 'profile',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
            },
            {
                path: 'role',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./role/role.module').then(m => m.RoleModule)
            },
            {
                path: 'user',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./user/user.module').then(m => m.UserModule)
            },
        ]),
        MaterialModule,
        CoreModule,
        ApiAuthorizationModule,
    ],
    exports: [
        MaterialModule
    ],
    providers: [
        ConfigService,
        MenuService,
        {
            provide: APP_INITIALIZER,
            useFactory: (configService: ConfigService) => {
                return () => configService.load();
            },
            multi: true,
            deps: [ConfigService]
        },
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
        { provide: MatPaginatorIntl, useClass: MatPaginatorIntlCustom}
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
