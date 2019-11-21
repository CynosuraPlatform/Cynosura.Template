import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatSnackBar } from "@angular/material";

import { Error } from "../core/error.model";
import { ProfileService } from "./profile.service";
import { Profile } from "./profile.model";
import { UpdateProfile } from "./profile-request.model";

@Component({
    selector: "app-profile-edit",
    templateUrl: "./edit.component.html",
    styleUrls: ["./edit.component.scss"]
})
export class ProfileEditComponent implements OnInit {
    profile: Profile;
    profileForm = this.fb.group({
        id: [],
        email: [],
        currentPassword: [],
        newPassword: [],
        confirmPassword: []
    });
    error: Error;

    constructor(
        private profileService: ProfileService,
        private fb: FormBuilder,
        private snackBar: MatSnackBar) {
    }

    ngOnInit(): void {
        this.getProfile();
    }

    private getProfile(): void {
        this.profileService.getProfile({})
            .then((profile) => {
                this.profile = profile;
                this.profileForm.reset();
                this.profileForm.patchValue(this.profile);
            });
    }

    onSubmit(): void {
        this.error = null;
        this.saveProfile();
    }

    private saveProfile(): void {
        const profile: UpdateProfile = this.profileForm.value;
        this.profileService.updateProfile(profile)
            .then(
                () => {
                   // this.authService.refreshTokens()
                        // .subscribe((token) => {
                            this.snackBar.open("Profile saved!", "Ok");
                            this.getProfile();
                        // });
                },
                (error) => {
                    this.onError(error);
                }
            );
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.snackBar.open(error.message, "Ok");
            Error.setFormErrors(this.profileForm, error);
        }
    }

}
