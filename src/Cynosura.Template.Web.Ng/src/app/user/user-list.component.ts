import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { trigger, state, style, transition, animate } from "@angular/animations";
import { PageEvent } from "@angular/material/paginator";
import { MatDialog, MatSnackBar } from "@angular/material";

import { User } from "../user-core/user.model";
import { UserFilter } from "../user-core/user-filter.model";
import { UserService } from "../user-core/user.service";

import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";
import { ModalComponent } from "../core/modal.component";

class UserListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new UserFilter();
}

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.scss"],
    animations: [
    trigger("detailExpand", [
        state("collapsed", style({ height: "0px", minHeight: "0", display: "none" })),
        state("expanded", style({ height: "*" })),
        transition("expanded <=> collapsed", animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")),
    ])
    ]
})
export class UserListComponent implements OnInit {
    content: Page<User>;
    state: UserListState;
    pageSizeOptions = [10, 20];
    columns = [
        "userName",
        "email",
    ];

    constructor(
        private userService: UserService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService,
        private dialog: MatDialog,
        private snackBar: MatSnackBar
        ) {
        this.state = this.storeService.get("userListState", new UserListState());
    }

    ngOnInit(): void {
        this.getUsers();
    }

    getUsers(): void {
        this.userService.getUsers({ pageIndex: this.state.pageIndex, pageSize: this.state.pageSize, filter: this.state.filter })
            .then(content => {
                this.content = content;
            })
            .catch(error => this.onError(error));
    }

    reset(): void {
        this.state.filter.text = null;
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
                    .catch(error => this.onError(error));
            }
        },
        error => this.onError(error));
    }

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getUsers();
    }

    onError(error: Error) {
        if (error) {
            this.snackBar.open(error.message, "Ok");
        }
    }
}
