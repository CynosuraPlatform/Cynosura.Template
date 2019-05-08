import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { trigger, state, style, transition, animate } from "@angular/animations";
import { PageEvent } from "@angular/material/paginator";
import { MatDialog, MatSnackBar } from "@angular/material";

import { Role } from "../role-core/role.model";
import { RoleFilter } from "../role-core/role-filter.model";
import { RoleService } from "../role-core/role.service";

import { StoreService } from "../core/store.service";
import { Error } from "../core/error.model";
import { Page } from "../core/page.model";
import { ModalComponent } from "../core/modal.component";

@Component({
    selector: "app-role-list",
    templateUrl: "./role-list.component.html",
    styleUrls: ["./role-list.component.scss"],
    animations: [
    trigger("detailExpand", [
        state("collapsed", style({ height: "0px", minHeight: "0", display: "none" })),
        state("expanded", style({ height: "*" })),
        transition("expanded <=> collapsed", animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")),
    ]),
    trigger("loadingTrigger", [
        state("loading", style({ opacity: 1 })),
        state("loading-done", style({ opacity: 0 })),
        transition("loading <=> loading-done", animate("500ms linear"))
    ]),
    trigger("tableTrigger", [
        state("loading", style({ opacity: .7 })),
        state("loading-done", style({ opacity: 1 })),
        state("error", style({ opacity: 1 })),
        transition("* <=> *", animate("500ms linear"))
    ])
    ]
})
export class RoleListComponent implements OnInit {
    content: Page<Role>;
    pageSize = 10;
    pageSizeOptions = [10, 20];
    filter = new RoleFilter();
    private innerError: Error;
    get error(): Error {
        return this.innerError;
    }
    set error(value: Error) {
        this.innerError = value;
        if (this.innerError) {
            this.snackBar.open(this.innerError.message, "Ok");
        }
    }
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
    columns = [
        "name",
    ];

    constructor(
        private roleService: RoleService,
        private router: Router,
        private route: ActivatedRoute,
        private storeService: StoreService,
        private dialog: MatDialog,
        private snackBar: MatSnackBar
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

    onPage(page: PageEvent) {
        this.pageIndex = page.pageIndex;
        this.pageSize = page.pageSize;
        this.getRoles();
    }
}
