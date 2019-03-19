import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "app-date-edit",
    templateUrl: "./date.edit.component.html"
})
export class DateEditComponent {

    get formattedDate(): string {
        return this.value.toISOString();
    }
    set formattedDate(value: string) {
        if (value) {
            this.value = this.removeTimezone(new Date(value));
        } else {
            this.value = null;
        }
    }
    @Input()
    value: Date;

    @Output()
    valueChange = new EventEmitter<Date>();

    @Input()
    name: string;

    @Input()
    label: string;

    removeTimezone(date: Date): Date {
        return new Date(date.getTime() - date.getTimezoneOffset() * 60 * 1000);
    }

    onFormattedDateChange(value: string) {
        this.formattedDate = value;
        this.valueChange.emit(this.value);
    }
}
