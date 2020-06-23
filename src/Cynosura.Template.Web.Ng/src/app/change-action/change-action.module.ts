import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { ChangeActionViewComponent } from './change-action-view.component';

@NgModule({
    declarations: [
        ChangeActionViewComponent
    ],
    imports: [
        CoreModule
    ],
    providers: [
    ],
    exports: [
        ChangeActionViewComponent
    ]
})
export class ChangeActionModule {

}
