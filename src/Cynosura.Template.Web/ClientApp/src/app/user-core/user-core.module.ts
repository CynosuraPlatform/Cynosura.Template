import { NgModule } from "@angular/core";

import { CoreModule } from "../core/core.module";

import { UserService } from "./user.service";
import { UserSelectComponent } from "./user-select.component";

@NgModule({
    declarations: [
        UserSelectComponent
    ],
    imports: [
        CoreModule
    ],
    providers: [
        UserService
    ],
    exports: [
        UserSelectComponent
    ]
})
export class UserCoreModule {

}
