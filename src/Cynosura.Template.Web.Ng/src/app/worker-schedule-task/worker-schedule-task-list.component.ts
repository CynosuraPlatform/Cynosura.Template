﻿import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { WorkerScheduleTask, WorkerScheduleTaskListState } from '../worker-schedule-task-core/worker-schedule-task.model';
import { WorkerScheduleTaskService } from '../worker-schedule-task-core/worker-schedule-task.service';
import { WorkerScheduleTaskEditComponent } from './worker-schedule-task-edit.component';

@Component({
  selector: 'app-worker-schedule-task-list',
  templateUrl: './worker-schedule-task-list.component.html',
  styleUrls: ['./worker-schedule-task-list.component.scss']
})
export class WorkerScheduleTaskListComponent implements OnInit {
  content: Page<WorkerScheduleTask>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'seconds',
    'minutes',
    'hours',
    'dayOfMonth',
    'month',
    'dayOfWeek',
    'year',
    'action'
  ];

  @Input()
  state: WorkerScheduleTaskListState;

  @Input()
  baseRoute = '/worker-schedule-task';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private workerScheduleTaskService: WorkerScheduleTaskService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getWorkerScheduleTasks();
  }

  private getWorkerScheduleTasks() {
    this.workerScheduleTaskService.getWorkerScheduleTasks({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getWorkerScheduleTasks();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getWorkerScheduleTasks();
  }

  onCreate() {
    WorkerScheduleTaskEditComponent.show(this.dialog, null, this.state.filter.workerInfoId).subscribe(() => {
      this.getWorkerScheduleTasks();
    });
  }

  onExport(): void {
    this.workerScheduleTaskService.exportWorkerScheduleTasks({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    WorkerScheduleTaskEditComponent.show(this.dialog, id).subscribe(() => {
      this.getWorkerScheduleTasks();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.workerScheduleTaskService.deleteWorkerScheduleTask({ id }))
      )
      .subscribe(() => this.getWorkerScheduleTasks(),
        error => this.onError(error));
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getWorkerScheduleTasks();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
