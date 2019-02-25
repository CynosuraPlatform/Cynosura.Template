import { Component, Input, Output, EventEmitter } from "@angular/core";
import { EditComponent } from "./base.edit.component";

@Component({
    selector: "text-edit",
    templateUrl: "./text.edit.component.html",
    styleUrls: ["text.edit.component.css"]
})
export class TextEditComponent extends EditComponent<string> {
    @Input()
    name: string;

    @Input()
    label: string;

    @Input()
    multiline: boolean;
}
