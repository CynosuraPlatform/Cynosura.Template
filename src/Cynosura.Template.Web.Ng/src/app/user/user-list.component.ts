import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { User } from '../user-core/user.model';
import { UserFilter } from '../user-core/user-filter.model';
import { UserService } from '../user-core/user.service';
import { UserEditComponent } from './user-edit.component';

class UserListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new UserFilter();
}

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
    content: Page<User>;
    state: UserListState;
    pageSizeOptions = [10, 20];
    columns = [
        'userName',
        'email',
        'action'
    ];

    constructor(
        private dialog: MatDialog,
        private modalHelper: ModalHelper,
        private userService: UserService,
        private storeService: StoreService,
        private noticeHelper: NoticeHelper
        ) {
        this.state = this.storeService.get('userListState', new UserListState());
    }

    ngOnInit() {
        this.getUsers();
    }

    private async getUsers() {
        this.content = await this.userService.getUsers({
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize,
            filter: this.state.filter
        });
    }

    onSearch() {
        this.getUsers();
    }

    onReset() {
        this.state.filter.text = null;
        this.getUsers();
    }

    onCreate(): void {
        this.openDialog(0).then((result) => {
            if (result) {
                this.getUsers();
            }
        });
    }

    onEdit(id: number) {
        this.openDialog(id).then((result) => {
            if (result) {
                this.getUsers();
            }
        });
    }

    onDelete(id: number) {
        this.modalHelper.confirmDelete()
            .subscribe(async () => {
                try {
                    await this.userService.deleteUser({ id });
                    this.getUsers();
                } catch (error) {
                    this.onError(error);
                }
            });
    }

    private openDialog(id?: number): Promise<any> {
        const dialogRef = this.dialog.open(UserEditComponent, {
            width: '600px',
            data: { id }
        });
        return dialogRef.afterClosed().toPromise();
    }

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getUsers();
    }

    onError(error: Error) {
        if (error) {
            this.noticeHelper.showError(error);
        }
    }
}
