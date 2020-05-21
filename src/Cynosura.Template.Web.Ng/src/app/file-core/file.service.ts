import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { File } from './file.model';
import { GetFiles, GetFile, UpdateFile, CreateFile, DeleteFile, ExportFiles } from './file-request.model';

@Injectable()
export class FileService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getFiles(getFiles: GetFiles): Observable<Page<File>> {
        const url = `${this.apiUrl}/GetFiles`;
        return this.httpClient.post<Page<File>>(url, getFiles);
    }

    getFile(getFile: GetFile): Observable<File> {
        const url = `${this.apiUrl}/GetFile`;
        return this.httpClient.post<File>(url, getFile);
    }

    exportFiles(exportFiles: ExportFiles): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportFiles`;
        return this.httpClient.post(url, exportFiles, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateFile(updateFile: UpdateFile): Observable<{}> {
        const url = `${this.apiUrl}/UpdateFile`;
        return this.httpClient.post(url, updateFile);
    }

    createFile(createFile: CreateFile): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateFile`;
        return this.httpClient.post<CreatedEntity<number>>(url, createFile);
    }

    deleteFile(deleteFile: DeleteFile): Observable<{}> {
        const url = `${this.apiUrl}/DeleteFile`;
        return this.httpClient.post(url, deleteFile);
    }
}
