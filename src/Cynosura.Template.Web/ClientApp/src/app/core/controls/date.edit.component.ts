import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseEditComponent } from "./base.edit.component";

@Component({
    selector: "date-edit",
    templateUrl: "./date.edit.component.html"
})
export class DateEditComponent implements BaseEditComponent<Date> {
    @Output()
    valueChange = new EventEmitter<Date>();

    @Input()
    value: Date;

    modelValue: Date;

    get model(): Date {
        return this.modelValue;
    }
    set model(setValue: Date) {
        const old = this.modelValue;
        if (typeof (setValue) === "string") {
            this.formattedDate = setValue;
        } else {
            this.modelValue = setValue;
        }
        if (old !== this.modelValue) {
            this.onValueChange();
        }
    }

    @Input()
    name: string;

    @Input()
    label: string;

    get formattedDate(): string {
        return this.modelValue.toISOString();
    }
    set formattedDate(value: string) {
        const test = Date.parse(value);
        if (isNaN(test)) {
            return;
        }
        this.modelValue = value ? new Date(value) : null;
    }

    onValueChange(): void {
        this.valueChange.emit(this.modelValue);
    }
}
