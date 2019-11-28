import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material";

import { Error } from "./error.model";

@Injectable()
export class ErrorHelper {

    constructor(private snackBar: MatSnackBar) {}

    showError(error: Error) {
        this.snackBar.open(error.message, "Ok", { duration: 5000 });
    }
}
