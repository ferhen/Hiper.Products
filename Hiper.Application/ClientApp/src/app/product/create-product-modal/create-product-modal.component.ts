import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './create-product-modal.component.html'
})
export class CreateProductModalComponent {
    public productName = '';

    constructor(public readonly activeModal: NgbActiveModal) { }
}
