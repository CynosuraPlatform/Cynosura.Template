import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { MatSnackBar } from "@angular/material";

import { User } from "../user-core/user.model";
import { CreateUser, UpdateUser } from "../user-core/user-request.model";
import { UserService } from "../user-core/user.service";

import { Role } from "../role-core/role.model";
import { RoleService } from "../role-core/role.service";

import { Error } from "../core/error.model";


@Component({
    selector: "app-user-edit",
    templateUrl: "./user-edit.component.html",
    styleUrls: ["./user-edit.component.scss"]
})
export class UserEditComponent implements OnInit {
    user: User;
    password: string;
    confirmPassword: string;
    roles: Role[] = [];

    constructor(private userService: UserService,
                private roleService: RoleService,
                private route: ActivatedRoute,
                private router: Router,
                private snackBar: MatSnackBar) { }

    ngOnInit(): void {
        this.roleService.getRoles({}).then(roles => this.roles = roles.pageItems).then(() =>
            this.route.params.forEach((params: Params) => {
                const id = +params.id;
                this.getUser(id);
            }));
    }

    private getUser(id: number): void {
        if (id === 0) {
            this.user = new User();
        } else {
            this.userService.getUser({ id }).then(user => {
                this.user = user;
                for (const role of this.roles) {
                    if (this.user.roleIds.indexOf(role.id) !== -1) {
                        role.isSelected = true;
                    }
                }
            });
        }
    }

    cancel(): void {
        window.history.back();
    }

    onSubmit(): void {
        this.saveUser();
    }

    private saveUser(): void {
        this.user.roleIds = this.roles
            .filter(role => role.isSelected)
            .map(role => role.id);
        if (this.user.id) {
            const updateUser: UpdateUser = this.user;
            updateUser.password = this.password;
            updateUser.confirmPassword = this.confirmPassword;
            this.userService.updateUser(updateUser)
                .then(
                    () => window.history.back(),
                    error => this.onError(error)
                );
        } else {
            const createUser: CreateUser = this.user;
            createUser.password = this.password;
            createUser.confirmPassword = this.confirmPassword;
            this.userService.createUser(createUser)
                .then(
                    () => window.history.back(),
                    error => this.onError(error)
                );
        }
    }

    onError(error: Error) {
        if (error) {
            this.snackBar.open(error.message, "Ok");
        }
    }
}
