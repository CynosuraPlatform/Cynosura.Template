import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { User } from '../user-core/user.model';
import { UserService } from '../user-core/user.service';
import { Role } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';
import { UserEditComponent } from './user-edit.component';

@Component({
    selector: 'app-user-view',
    templateUrl: './user-view.component.html',
    styleUrls: ['./user-view.component.scss']
})
export class UserViewComponent implements OnInit {
    id: number;
    user: User;
    roles: Role[] = [];
    userRoles: Role[] = [];

    constructor(private dialog: MatDialog,
                private userService: UserService,
                private roleService: RoleService,
                private route: ActivatedRoute) { }

    async ngOnInit() {
        this.roles = (await this.roleService.getRoles({})).pageItems;
        this.route.params.forEach((params: Params) => {
            this.id = +params.id;
            this.getUser();
        });
    }

    private async getUser() {
        this.user = await this.userService.getUser({ id: this.id });
        this.userRoles = this.roles.filter(r => this.user.roleIds && this.user.roleIds.indexOf(r.id) !== -1);
    }

    onEdit() {
        this.openDialog().then((result) => {
            if (result) {
                this.getUser();
            }
        });
    }

    private openDialog(): Promise<any> {
        const dialogRef = this.dialog.open(UserEditComponent, {
            width: '600px',
            data: { id: this.id }
        });
        return dialogRef.afterClosed().toPromise();
    }

    onBack() {
        window.history.back();
    }

}
