import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "enumkeys" })
export class EnumKeysPipe implements PipeTransform {
    transform(value, args: string[]): any {
        let keys = [];
        for (var enumMember in value) {
            if (!isNaN(parseInt(enumMember, 10))) {
                keys.push({ key: parseInt(enumMember, 10), value: value[enumMember] });
            }
        }
        return keys;
    }
}
