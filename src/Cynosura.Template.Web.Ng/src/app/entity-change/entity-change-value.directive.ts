import { Directive, TemplateRef, Input } from '@angular/core';

@Directive({
  selector: '[appEntityChangeValue]'
})
export class EntityChangeValueDirective {

  constructor(public templateRef: TemplateRef<any>) { }

  @Input()
  propertyName: string;
}
