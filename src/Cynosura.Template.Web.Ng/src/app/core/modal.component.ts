import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

export interface DialogData {
    id: number;
}

@Component({
    templateUrl: "modal.component.html"
})
export class ModalComponent {
    constructor(
        public dialogRef: MatDialogRef<ModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData) { }
    onNoClick(): void {
        this.dialogRef.close();
    }
}
