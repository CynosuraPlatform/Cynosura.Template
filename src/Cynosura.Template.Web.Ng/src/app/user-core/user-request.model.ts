import { UserFilter } from "./user-filter.model";
export class GetUsers {
    pageIndex?: number;
    pageSize?: number;
    filter?: UserFilter;
}

export class GetUser {
    id: number;
}

export class UpdateUser {
    id: number;
    password?: string;
    confirmPassword?: string;
    roleIds: number[];
}

export class CreateUser {
    email: string;
    password?: string;
    confirmPassword?: string;
    roleIds: number[];
}

export class DeleteUser {
    id: number;
}
