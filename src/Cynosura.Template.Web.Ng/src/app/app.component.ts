import { Component, OnInit, ChangeDetectorRef } from "@angular/core";

import { debounceTime } from "rxjs/operators";

import { AuthService } from "./auth/auth.service";
import { LoadingService } from "./core/loading.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
    title = "app";
    isLoading = false;

    constructor(private authService: AuthService,
                private loadingService: LoadingService,
                private cdRef: ChangeDetectorRef) {
        loadingService
            .onLoadingChanged
            .pipe(debounceTime(500))
            .subscribe((isLoading: boolean) => {
                this.isLoading = isLoading;
                this.cdRef.detectChanges();
            });
    }

    ngOnInit(): void {
        this.authService.init()
            .subscribe(
                () => { console.info("Auth init success"); },
                error => console.warn(error)
            );
    }
}
