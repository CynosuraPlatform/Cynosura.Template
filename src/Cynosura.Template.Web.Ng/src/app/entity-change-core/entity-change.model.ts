import { ChangeAction } from '../change-action/change-action.model';
import { User } from '../user-core/user.model';

export class EntityChange {
    id: number;
    creationDate: Date;
    creationUserId: number;
    creationUser: User;
    action: ChangeAction;
    from: string;
    to: string;
}

export class EntityChangeListState {
    pageSize = 10;
    pageIndex = 0;
}
