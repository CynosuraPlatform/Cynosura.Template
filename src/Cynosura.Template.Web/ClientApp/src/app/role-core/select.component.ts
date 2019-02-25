import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";

import { Role } from "./role.model";
import { RoleService } from "./role.service";

@Component({
    selector: "role-select",
    templateUrl: "./select.component.html"
})

export class RoleSelectComponent implements OnInit {
    constructor(private roleService: RoleService) { }

    roles: Role[] = [];

    modelValue: number;

    @Input()
    set selectedRoleId(value: number | null) {
        this.model = value;
    }

    @Output()
    selectedRoleIdChange = new EventEmitter<number>();

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
        this.roleService.getRoles().then(roles => this.roles = roles.pageItems);
    }

    onValueChange() {
        this.selectedRoleIdChange.emit(this.modelValue);
    }
}
