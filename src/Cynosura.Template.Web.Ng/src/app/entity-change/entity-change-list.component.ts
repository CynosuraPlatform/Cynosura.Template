import { Component, OnInit, Input, ContentChild, TemplateRef } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { EntityChange, EntityChangeListState } from '../entity-change-core/entity-change.model';
import { EntityChangeService } from '../entity-change-core/entity-change.service';
import { EntityChangeValueDirective } from './entity-change-value.directive';

@Component({
    selector: 'app-entity-change-list',
    templateUrl: './entity-change-list.component.html',
    styleUrls: ['./entity-change-list.component.scss']
})
export class EntityChangeListComponent implements OnInit {
    content: Page<EntityChange>;
    pageSizeOptions = [10, 20];
    columns = [
        'action',
        'changes',
        'creationDate',
        'creationUser'
    ];

    @Input()
    state: EntityChangeListState;

    @Input()
    entityName: string;

    @Input()
    entityId: number;

    @Input()
    set refresh(value: any) {
        this.getEntityChanges();
    }

    @ContentChild(EntityChangeValueDirective, { static: true, read: TemplateRef }) entityChangeValueTemplate: TemplateRef<any>;

    constructor(
        private dialog: MatDialog,
        private modalHelper: ModalHelper,
        private entitychangeService: EntityChangeService,
        private noticeHelper: NoticeHelper
        ) {
    }

    entityChangeValueTemplateContext(value: any): object {
        return { $implicit: value };
    }

    ngOnInit() {
        this.getEntityChanges();
    }

    private getEntityChanges() {
        this.entitychangeService.getEntityChanges({
            entityName: this.entityName,
            entityId: this.entityId,
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize
        }).subscribe(content => this.content = content);
    }

    onSearch() {
        this.getEntityChanges();
    }

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getEntityChanges();
    }

    onError(error: Error) {
        if (error) {
            this.noticeHelper.showError(error);
        }
    }
}
