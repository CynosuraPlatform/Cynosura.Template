import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";

import { User } from "../user-core/user.model";
import { UserFilter } from "../user-core/user-filter.model";
import { UserService } from "../user-core/user.service";

import { ModalHelper } from "../core/modal.helper";
import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";

class UserListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new UserFilter();
}

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html"
})
export class UserListComponent implements OnInit {
    content: Page<User>;
    error: Error;
    state: UserListState;

    constructor(
        private modalHelper: ModalHelper,
        private userService: UserService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService
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
            .catch(error => this.error = error);
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
        this.modalHelper.confirmDelete()
            .then(() => {
                this.userService.deleteUser({ id })
                    .then(() => {
                        this.getUsers();
                    })
                    .catch(error => this.error = error);
            })
            .catch(() => { });
    }

    onPageSelected(pageIndex: number) {
        this.state.pageIndex = pageIndex;
        this.getUsers();
    }
}
