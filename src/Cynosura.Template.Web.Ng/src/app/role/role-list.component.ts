import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";

import { Role } from "../role-core/role.model";
import { RoleFilter } from "../role-core/role-filter.model";
import { RoleService } from "../role-core/role.service";

import { ModalHelper } from "../core/modal.helper";
import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";

class RoleListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new RoleFilter();
}

@Component({
    selector: "app-role-list",
    templateUrl: "./role-list.component.html"
})
export class RoleListComponent implements OnInit {
    content: Page<Role>;
    error: Error;
    state: RoleListState;

    constructor(
        private modalHelper: ModalHelper,
        private roleService: RoleService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService
        ) {
        this.state = this.storeService.get("roleListState", new RoleListState());
    }

    ngOnInit(): void {
        this.getRoles();
    }

    getRoles(): void {
        this.roleService.getRoles({ pageIndex: this.state.pageIndex, pageSize: this.state.pageSize, filter: this.state.filter })
            .then(content => {
                this.content = content;
            })
            .catch(error => this.error = error);
    }

    reset(): void {
        this.state.filter.text = null;
        this.getRoles();
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
                this.roleService.deleteRole({ id })
                    .then(() => {
                        this.getRoles();
                    })
                    .catch(error => this.error = error);
            })
            .catch(() => { });
    }

    onPageSelected(pageIndex: number) {
        this.state.pageIndex = pageIndex;
        this.getRoles();
    }
}
