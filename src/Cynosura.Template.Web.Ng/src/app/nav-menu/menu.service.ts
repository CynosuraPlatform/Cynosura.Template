import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { User } from "../auth/user.model";
import { AuthService } from "../auth/auth.service";
import { Menu } from "./menu.model";

@Injectable()
export class MenuService {
    private menu: Menu;
    private currentUser: User;

    constructor(private authService: AuthService) {
        this.menu = new Menu();
// ADD MENU ITEMS HERE
        this.menu.items.push({ route: "./user", name: "Users", icon: "person", roles: ["Administrator"] });
        this.menu.items.push({ route: "./role", name: "Roles", icon: "lock", roles: ["Administrator"] });

        this.authService.currentUser$.subscribe(currentUser => {
            this.currentUser = currentUser;
        });
    }

    getMenu(): Promise<Menu> {
        const menu: Menu = {
            items: this.menu.items.filter(item => {
                return item.roles.some(role => this.currentUser ? this.currentUser.roles.includes(role) : false);
            })
        };
        return Promise.resolve(menu);
    }
}
