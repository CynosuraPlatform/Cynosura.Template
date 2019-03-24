import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "app-text-edit",
    templateUrl: "./text.edit.component.html",
    styleUrls: ["text.edit.component.css"]
})
export class TextEditComponent {
    @Input()
    value: string;

    @Output()
    valueChange = new EventEmitter<string>();

    @Input()
    name: string;

    @Input()
    label: string;

    @Input()
    type = "text";

    @Input()
    multiline = false;

    @Input()
    readonly = false;

    onValueChange(value: string) {
        this.value = value;
        this.valueChange.emit(value);
    }
}
