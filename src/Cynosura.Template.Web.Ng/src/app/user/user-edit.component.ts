import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';

import { User } from '../user-core/user.model';
import { UserService } from '../user-core/user.service';
import { Role } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-user-edit',
    templateUrl: './user-edit.component.html',
    styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
    id: number;
    userForm = this.fb.group({
        id: [],
        userName: [],
        email: [],
        password: [],
        confirmPassword: []
    });
    user: User;
    roles: Role[] = [];
    error: Error;

    constructor(public dialogRef: MatDialogRef<UserEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private userService: UserService,
                private roleService: RoleService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    async ngOnInit() {
        this.roles = (await this.roleService.getRoles({})).pageItems;
        this.getUser();
    }

    private async getUser() {
        if (this.id === 0) {
            this.user = new User();
        } else {
            this.user = await this.userService.getUser({ id: this.id });
        }
        this.userForm.patchValue(this.user);
        for (const role of this.roles) {
            if (this.user.roleIds && this.user.roleIds.indexOf(role.id) !== -1) {
                role.isSelected = true;
            }
        }
    }

    onSave() {
        this.saveUser();
    }

    private async saveUser() {
        try {
            const user = this.userForm.value;
            user.roleIds = this.roles
                .filter(role => role.isSelected)
                .map(role => role.id);
            if (this.id) {
                await this.userService.updateUser(user);
            } else {
                await this.userService.createUser(user);
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
            Error.setFormErrors(this.userForm, error);
        }
    }
}
