import { Component, Input, Output, EventEmitter } from "@angular/core";
import { EditComponent } from "./base.edit.component";

@Component({
    selector: "number-edit",
    templateUrl: "./number.edit.component.html"
})
export class NumberEditComponent extends EditComponent<number> {
    @Input()
    name: string;

    @Input()
    label: string;
}
