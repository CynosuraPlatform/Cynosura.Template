import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";

import { User } from "./user.model";
import { UserService } from "./user.service";

@Component({
    selector: "user-select",
    templateUrl: "./select.component.html"
})

export class UserSelectComponent implements OnInit {
    constructor(private userService: UserService) { }

    users: User[] = [];

    modelValue: number;

    @Input()
    set selectedUserId(value: number | null) {
        this.model = value;
    }

    @Output()
    selectedUserIdChange = new EventEmitter<number>();

    @Input()
    readonly = false;

    get model(): number {
        return this.modelValue;
    }

    set model(value: number) {
        if (this.modelValue !== value) {
            this.modelValue = value;
            this.onValueChange();
            return;
        }
        this.modelValue = value;
    }

    ngOnInit(): void {
        this.userService.getUsers().then(users => this.users = users.pageItems);
    }

    onValueChange() {
        this.selectedUserIdChange.emit(this.modelValue);
    }
}
