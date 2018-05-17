import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { BoolPipe } from "./pipes/bool.pipe";
import { BoolComponent } from "./bool.component";
import { ErrorHandlerComponent } from "./error-handler.component";
import { ModelValidatorComponent } from "./model-validator.component";
import { PagerComponent } from "./pager.component";
import { StoreService } from "./store.service";

@NgModule({
    declarations: [
        BoolPipe,
        BoolComponent,
        ErrorHandlerComponent,
        ModelValidatorComponent,
        PagerComponent
    ],
    imports: [
        CommonModule,
        FormsModule
    ],
    providers: [
        StoreService
    ],
    exports: [
        CommonModule,
        FormsModule,
        BoolPipe,
        BoolComponent,
        ErrorHandlerComponent,
        ModelValidatorComponent,
        PagerComponent
    ]
})
export class CoreModule {

}
