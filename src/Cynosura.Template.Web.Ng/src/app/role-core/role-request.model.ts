import { RoleFilter } from "./role-filter.model";

export class GetRoles {
    pageIndex?: number;
    pageSize?: number;
    filter?: RoleFilter;
}

export class GetRole {
    id: number;
}

export class UpdateRole {
    id: number;
    name: string;
}

export class CreateRole {
    name: string;
}

export class DeleteRole {
    id: number;
}
