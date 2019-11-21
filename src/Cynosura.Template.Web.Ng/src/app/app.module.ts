import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule, APP_INITIALIZER } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, Route } from "@angular/router";

import { ModalModule } from "ngx-modialog";
import { BootstrapModalModule } from "ngx-modialog/plugins/bootstrap";

import { MaterialModule } from "./material.module";
import { ApiAuthorizationModule } from "../api-authorization/api-authorization.module";
import { CoreModule } from "./core/core.module";
import { AccountModule } from "./account/account.module";

import { AuthorizeInterceptor } from "../api-authorization/authorize.interceptor";
import { ConfigService } from "./config/config.service";
import { MenuService } from "./nav-menu/menu.service";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { AuthorizeGuard } from "../api-authorization/authorize.guard";

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
            { path: "", component: HomeComponent, pathMatch: "full", canActivate: [AuthorizeGuard] },
// ADD ROUTES HERE
            { path: "profile", canActivate: [AuthorizeGuard], loadChildren: "./profile/profile.module#ProfileModule" },
            { path: "role", canActivate: [AuthorizeGuard], loadChildren: "./role/role.module#RoleModule" },
            { path: "user", canActivate: [AuthorizeGuard], loadChildren: "./user/user.module#UserModule" },
        ]),
        ModalModule.forRoot(),
        BootstrapModalModule,
        MaterialModule,
        CoreModule,
        ApiAuthorizationModule,
        AccountModule,
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
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
