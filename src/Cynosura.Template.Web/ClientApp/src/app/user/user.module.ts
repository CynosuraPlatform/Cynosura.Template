import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { CoreModule } from "../core/core.module";
import { UserCoreModule } from "../user-core/user-core.module";

import { UserListComponent } from "./list.component";
import { UserEditComponent } from "./edit.component";

@NgModule({
    declarations: [
        UserListComponent,
        UserEditComponent
    ],
    imports: [
        RouterModule.forChild([
            { path: "user", component: UserListComponent },
            { path: "user/:id", component: UserEditComponent }
        ]),
        CoreModule,
        UserCoreModule
    ],
    providers: [
    ]
})
export class UserModule {

}
