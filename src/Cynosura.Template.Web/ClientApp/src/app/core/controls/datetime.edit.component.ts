import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseEditComponent } from "./base.edit.component";

@Component({
    selector: "datetime-edit",
    templateUrl: "./datetime.edit.component.html"
})
export class DateTimeEditComponent  implements BaseEditComponent<Date> {
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
        this.modelValue = value ? new Date(value) : null;
    }

    onValueChange(): void {
        this.valueChange.emit(this.modelValue);
    }
}
