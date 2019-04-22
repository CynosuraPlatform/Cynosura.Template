import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";

import { Role } from "../role-core/role.model";
import { RoleFilter } from "../role-core/role-filter.model";
import { RoleService } from "../role-core/role.service";

import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";
import { MatDialog } from "@angular/material";
import { ModalComponent } from "../core/modal.component";

@Component({
    selector: "app-role-list",
    templateUrl: "./role-list.component.html",
    styleUrls: ["./role-list.component.scss"]
})
export class RoleListComponent implements OnInit {
    content: Page<Role>;
    error: Error;
    pageSize = 10;
    filter = new RoleFilter();
    private innerPageIndex: number;
    get pageIndex(): number {
        if (!this.innerPageIndex) {
            this.innerPageIndex = this.storeService.get("rolesPageIndex", 0);
        }
        return this.innerPageIndex;
    }
    set pageIndex(value: number) {
        this.innerPageIndex = value;
        this.storeService.set("rolesPageIndex", value);
    }

    constructor(
        private roleService: RoleService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService,
        private dialog: MatDialog
        ) {}

    ngOnInit(): void {
        this.getRoles();
    }

    getRoles(): void {
        this.roleService.getRoles({ pageIndex: this.pageIndex, pageSize: this.pageSize, filter: this.filter })
            .then(content => {
                this.content = content;
            })
            .catch(error => this.error = error);
    }

    reset(): void {
        this.filter.text = null;
        this.getRoles();
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
                this.roleService.deleteRole({ id })
                    .then(() => {
                        this.getRoles();
                    })
                    .catch(error => this.error = error);
            }
        },
        error => this.error = error);
    }

    onPageSelected(pageIndex: number) {
        this.pageIndex = pageIndex;
        this.getRoles();
    }
}
