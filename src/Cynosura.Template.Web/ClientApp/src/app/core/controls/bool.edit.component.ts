import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "app-bool-edit",
    templateUrl: "./bool.edit.component.html"
})
export class BoolEditComponent {
    @Input()
    value: boolean;

    @Output()
    valueChange = new EventEmitter<boolean>();

    @Input()
    name: string;

    @Input()
    label: string;

    @Input()
    readonly = false;

    onValueChange(value: boolean) {
        this.value = value;
        this.valueChange.emit(value);
    }
}
