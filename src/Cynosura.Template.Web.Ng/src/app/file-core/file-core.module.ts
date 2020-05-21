import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { FileService } from './file.service';
import { FileSelectComponent } from './file-select.component';
import { FileShowComponent } from './file-show.component';

@NgModule({
    declarations: [
        FileSelectComponent,
        FileShowComponent
    ],
    imports: [
        CoreModule,
    ],
    providers: [
        FileService
    ],
    exports: [
        FileSelectComponent,
        FileShowComponent
    ]
})
export class FileCoreModule {

}
