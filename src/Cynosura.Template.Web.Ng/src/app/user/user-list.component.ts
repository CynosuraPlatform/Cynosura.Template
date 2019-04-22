import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";

import { User } from "../user-core/user.model";
import { UserFilter } from "../user-core/user-filter.model";
import { UserService } from "../user-core/user.service";

import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";
import { MatDialog } from "@angular/material";
import { ModalComponent } from "../core/modal.component";

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.scss"]
})
export class UserListComponent implements OnInit {
    content: Page<User>;
    error: Error;
    pageSize = 10;
    filter = new UserFilter();
    private innerPageIndex: number;
    get pageIndex(): number {
        if (!this.innerPageIndex) {
            this.innerPageIndex = this.storeService.get("usersPageIndex", 0);
        }
        return this.innerPageIndex;
    }
    set pageIndex(value: number) {
        this.innerPageIndex = value;
        this.storeService.set("usersPageIndex", value);
    }

    constructor(
        private userService: UserService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService,
        private dialog: MatDialog
        ) {}

    ngOnInit(): void {
        this.getUsers();
    }

    getUsers(): void {
        this.userService.getUsers({ pageIndex: this.pageIndex, pageSize: this.pageSize, filter: this.filter })
            .then(content => {
                this.content = content;
            })
            .catch(error => this.error = error);
    }

    reset(): void {
        this.filter.text = null;
        this.getUsers();
    }

    edit(id: number): void {
        this.router.navigate([id], { relativeTo: this.route });
    }

    add(): void {
        this.router.navigate([0], { relativeTo: this.route });
    }

    delete(id: number): void {
        const dialogRef = this.dialog.open(ModalComponent, {
            width: "250px",
            data: { id: id }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result === true) {
                this.userService.deleteUser({ id })
                    .then(() => {
                        this.getUsers();
                    })
                    .catch(error => this.error = error);
            }
        },
        error => this.error = error);
    }

    onPageSelected(pageIndex: number) {
        this.pageIndex = pageIndex;
        this.getUsers();
    }
}
