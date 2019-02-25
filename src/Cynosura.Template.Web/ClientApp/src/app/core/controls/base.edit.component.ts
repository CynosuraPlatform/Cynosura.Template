import { Input, EventEmitter, Output } from "@angular/core";

export abstract class EditComponent<T> implements BaseEditComponent<T> {

    @Output()
    valueChange = new EventEmitter<T>();

    @Input()
    value: T;

    modelValue: T;

    get model(): T {
        return this.modelValue;
    }

    set model(value: T) {
        if (this.modelValue !== value) {
            this.modelValue = value;
            this.onValueChange();
            return;
        }
        this.modelValue = value;
    }

    onValueChange() {
        this.valueChange.emit(this.modelValue);
    }
}

export interface BaseEditComponent<T> {
    valueChange: EventEmitter<T>;
    value: T;

    modelValue: T;
    model: T;
    onValueChange(): void;
}
