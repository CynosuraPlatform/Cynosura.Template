import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, AbstractControl } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { MatSnackBar } from "@angular/material";

import { ModalHelper } from "../core/modal.helper";
import { Error } from "../core/error.model";
import { AccountService } from "./account.service";

@Component({
    selector: "app-register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    error: any;
    returnUrl: string;

    constructor(
        private formBuilder: FormBuilder,
        private accountService: AccountService,
        private router: Router,
        private route: ActivatedRoute,
        private modalHelper: ModalHelper,
        private snackBar: MatSnackBar
    ) { }

    ngOnInit(): void {
        this.returnUrl = this.route.snapshot.queryParams.returnUrl || "/";
        this.registerForm = this.formBuilder.group({
            email: ["", [Validators.required]],
            password: ["", [Validators.required]],
            confirmPassword: ["", [Validators.required]],
        }, { validator: this.passwordConfirming });
    }

    onSubmit() {
        this.error = null;
        const register = this.registerForm.value;
        this.accountService.register(register)
            .then(() => {
                const message = `You have been successfully signed up, use ${register.email} for log in`;
                this.modalHelper.alert(message, "Sign up successful", "Ok")
                    .subscribe(() => {
                        this.router.navigate(["/login"], {
                            queryParams: {
                                returnUrl: this.returnUrl,
                                email: register.email
                            }
                        });
                    });
            }, error => {
                this.onError(error);
            });
    }

    passwordConfirming(control: AbstractControl): { [key: string]: any } | null {
        if (control.get("password").value !== control.get("confirmPassword").value) {
            return { passwordMismatch: true };
        }
        return null;
    }

    onError(error) {
        this.error = error;
        if (error) {
            this.snackBar.open(error.message, "Ok");
            Error.setFormErrors(this.registerForm, error);
        }
    }
}
