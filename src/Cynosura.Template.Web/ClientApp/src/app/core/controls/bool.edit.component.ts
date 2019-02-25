import { Component, Input } from "@angular/core";
import { EditComponent } from "./base.edit.component";

@Component({
    selector: "bool-edit",
    templateUrl: "./bool.edit.component.html"
})
export class BoolEditComponent extends EditComponent<boolean> {
    @Input()
    name: string;

    @Input()
    label: string;
}
