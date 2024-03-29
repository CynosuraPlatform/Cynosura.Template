﻿import { Component, OnInit, Input } from '@angular/core';
import { LegacyPageEvent as PageEvent } from '@angular/material/legacy-paginator';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';
import { TranslocoService } from '@ngneat/transloco';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';
import { ColumnDescription } from '../core/column-settings.component';
import { StoredValueService } from '../core/stored-value.service';

import { FileGroup, FileGroupListState } from '../file-group-core/file-group.model';
import { FileGroupService } from '../file-group-core/file-group.service';
import { FileGroupEditComponent } from './file-group-edit.component';

@Component({
  selector: 'app-file-group-list',
  templateUrl: './file-group-list.component.html',
  styleUrls: ['./file-group-list.component.scss']
})
export class FileGroupListComponent implements OnInit {
  content: Page<FileGroup>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  defaultColumns = [
    'select',
    'name',
    'type',
    'location',
    'accept',
    'action'
  ];
  columnDescriptions: ColumnDescription[] = [
    { name: 'select', isSystem: true },
    { name: 'name', displayName: this.translocoService.translate('Name') },
    { name: 'type', displayName: this.translocoService.translate('Type') },
    { name: 'location', displayName: this.translocoService.translate('Location') },
    { name: 'accept', displayName: this.translocoService.translate('Accept') },
    { name: 'action', isSystem: true },
  ];
  columns = this.storedValueService.getStoredValue('fileGroupColumns', this.defaultColumns,
    ColumnDescription.filter(this.columnDescriptions));
  selectedIds = new Set<number>();

  @Input()
  state: FileGroupListState;

  @Input()
  baseRoute = '/file-group';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private fileGroupService: FileGroupService,
    private noticeHelper: NoticeHelper,
    private translocoService: TranslocoService,
    private storedValueService: StoredValueService
    ) {
  }

  ngOnInit() {
    this.getFileGroups();
  }

  private getFileGroups() {
    this.selectedIds = new Set<number>();
    this.fileGroupService.getFileGroups({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getFileGroups();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getFileGroups();
  }

  onCreate() {
    FileGroupEditComponent.show(this.dialog, null).subscribe(() => {
      this.getFileGroups();
    });
  }

  onExport(): void {
    this.fileGroupService.exportFileGroups({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    FileGroupEditComponent.show(this.dialog, id).subscribe(() => {
      this.getFileGroups();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.fileGroupService.deleteFileGroup({ id }))
      )
      .subscribe(() => this.getFileGroups(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.fileGroupService.deleteFileGroup({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getFileGroups());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getFileGroups();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getFileGroups();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
