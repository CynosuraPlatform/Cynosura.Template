import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Menu } from "./menu.model";

@Injectable()
export class MenuService {
    private menu: Menu;
    // private currentUser: User;

    constructor() {
        this.menu = new Menu();
// ADD MENU ITEMS HERE
        this.menu.items.push({ route: "/user", name: "Users", icon: "person", roles: ["Administrator"] });
        this.menu.items.push({ route: "/role", name: "Roles", icon: "lock", roles: ["Administrator"] });
    }

    getMenu(): Promise<Menu> {
        const menu: Menu = {
            items: this.menu.items.filter(item => {
                return true; // item.roles.some(role => this.currentUser ? this.currentUser.roles.includes(role) : false);
            })
        };
        return Promise.resolve(menu);
    }
}
