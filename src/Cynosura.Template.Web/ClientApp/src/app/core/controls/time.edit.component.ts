import { Component, Input, Output, EventEmitter } from "@angular/core";
import { EditComponent } from "./base.edit.component";

@Component({
    selector: "time-edit",
    templateUrl: "./time.edit.component.html"
})
export class TimeEditComponent extends EditComponent<Date> {
    @Input()
    name: string;

    @Input()
    label: string;
}
