import { Component,  OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Role } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';
import { RoleEditComponent } from './role-edit.component';

@Component({
    selector: 'app-role-view',
    templateUrl: './role-view.component.html',
    styleUrls: ['./role-view.component.scss']
})
export class RoleViewComponent implements OnInit {
    id: number;
    role: Role;

    constructor(private dialog: MatDialog,
                private roleService: RoleService,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.id = +params.id;
            this.getRole();
        });
    }

    private async getRole() {
        this.role = await this.roleService.getRole({ id: this.id });
    }

    onEdit() {
        this.openDialog().then((result) => {
            if (result) {
                this.getRole();
            }
        });
    }

    private openDialog(): Promise<any> {
        const dialogRef = this.dialog.open(RoleEditComponent, {
            width: '600px',
            data: { id: this.id }
        });
        return dialogRef.afterClosed().toPromise();
    }

    onBack(): void {
        window.history.back();
    }

}
