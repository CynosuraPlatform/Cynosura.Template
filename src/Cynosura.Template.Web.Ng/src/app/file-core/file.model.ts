import { FileFilter } from './file-filter.model';

export class File {
    id: number;
    creationDate: Date;
    modificationDate: Date;
    creationUserId: number;
    modificationUserId: number;
    name: string;
    contentType: string;
    content: Uint8Array;
    url: string;
    groupId: number;
}

export class FileListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new FileFilter();
}
