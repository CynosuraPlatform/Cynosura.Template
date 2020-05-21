import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { File } from '../file-core/file.model';
import { FileService } from '../file-core/file.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-file-edit',
    templateUrl: './file-edit.component.html',
    styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent implements OnInit {
    id: number;
    fileForm = this.fb.group({
        id: [],
        name: [],
        contentType: [],
        content: [],
        url: [],
        groupId: []
    });
    file: File;
    error: Error;

    constructor(public dialogRef: MatDialogRef<FileEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private fileService: FileService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    static show(dialog: MatDialog, id: number): Observable<any> {
        const dialogRef = dialog.open(FileEditComponent, {
            width: '600px',
            data: { id: id }
        });
        return dialogRef.afterClosed()
            .pipe(filter(res => res === true));
    }

    ngOnInit(): void {
        this.getFile();
    }

    private getFile() {
        const getFile$ = !this.id ?
            of(new File()) :
            this.fileService.getFile({ id: this.id });
        getFile$.subscribe(file => {
            this.file = file;
            this.fileForm.patchValue(this.file);
        });
    }

    onSave(): void {
        this.saveFile();
    }

    private saveFile() {
        const saveFile$ = this.id ?
            this.fileService.updateFile(this.fileForm.value) :
            this.fileService.createFile(this.fileForm.value);
        saveFile$.subscribe(() => this.dialogRef.close(true),
            error => this.onError(error));
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.noticeHelper.showError(error);
            Error.setFormErrors(this.fileForm, error);
        }
    }
}
