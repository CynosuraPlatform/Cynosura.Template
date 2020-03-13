import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';

import { Role } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-role-edit',
    templateUrl: './role-edit.component.html',
    styleUrls: ['./role-edit.component.scss']
})
export class RoleEditComponent implements OnInit {
    id: number;
    roleForm = this.fb.group({
        id: [],
        name: []
    });
    role: Role;
    error: Error;

    constructor(public dialogRef: MatDialogRef<RoleEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private roleService: RoleService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    ngOnInit(): void {
        this.getRole();
    }

    private async getRole() {
        if (this.id === 0) {
            this.role = new Role();
        } else {
            this.role = await this.roleService.getRole({ id: this.id });
        }
        this.roleForm.patchValue(this.role);
    }

    onSave(): void {
        this.saveRole();
    }

    private async saveRole() {
        try {
            if (this.id) {
                await this.roleService.updateRole(this.roleForm.value);
            } else {
                await this.roleService.createRole(this.roleForm.value);
            }
            this.dialogRef.close(true);
        } catch (error) {
            this.onError(error);
        }
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.noticeHelper.showError(error);
            Error.setFormErrors(this.roleForm, error);
        }
    }
}
