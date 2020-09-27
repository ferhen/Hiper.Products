import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './delete-product-modal.component.html'
})
export class DeleteProductModalComponent {
    constructor(public readonly activeModal: NgbActiveModal) { }
}
