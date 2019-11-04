import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { MenuService } from "./menu.service";
import { Menu } from "./menu.model";

@Component({
    selector: "app-nav-menu",
    templateUrl: "./nav-menu.component.html",
    styleUrls: ["./nav-menu.component.scss"]
})
export class NavMenuComponent implements OnInit {
    menu: Menu;
    isExpanded = false;

    constructor(private menuService: MenuService,
                private router: Router) {
    }

    ngOnInit(): void {
        // this.authService.state$.subscribe(state => {
            this.menuService.getMenu().then(menu => {
                this.menu = menu;
            });
        // });
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
