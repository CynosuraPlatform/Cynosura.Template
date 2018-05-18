import { Component, OnInit } from "@angular/core";

import { MenuService } from "../core/services/menu.service";
import { Menu } from "../core/models/menu.model";

@Component({
    selector: "app-nav-menu",
    templateUrl: "./nav-menu.component.html",
    styleUrls: ["./nav-menu.component.css"]
})
export class NavMenuComponent implements OnInit {
    menu: Menu;
    isExpanded = false;

    constructor(private menuService: MenuService) { }

    ngOnInit(): void {
        this.menuService.getMenu().then(menu => {
            this.menu = menu;
        });
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
