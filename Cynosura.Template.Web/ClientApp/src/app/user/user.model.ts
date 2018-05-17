export class User {
    id: number;
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
    roleIds: number[];
    newPassword: string | null;
    confirmNewPassword: string;
}