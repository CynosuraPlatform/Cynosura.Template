import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { UserCoreModule } from '../user-core/user-core.module';
import { RoleCoreModule } from '../role-core/role-core.module';

import { UserListComponent } from './user-list.component';
import { UserEditComponent } from './user-edit.component';
import { UserViewComponent } from './user-view.component';

@NgModule({
    declarations: [
        UserListComponent,
        UserEditComponent,
        UserViewComponent
    ],
    imports: [
        RouterModule.forChild([
            { path: '', component: UserListComponent },
            { path: ':id', component: UserViewComponent }
        ]),
        CoreModule,
        TranslocoRootModule,
        UserCoreModule,
        RoleCoreModule,
    ],
    exports: [
    ],
    providers: [
    ],
    entryComponents: [
        UserEditComponent
    ]
})
export class UserModule {

}
